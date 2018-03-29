using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CLinq.Core.ComposableQuery;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.Database;
using Testing.Database.Model;

namespace Testing.Runner
{
    [TestClass]
    public class ExtensionExpressionTest
    {
        [TestMethod]
        public void TestExtensionExpression()
        {
            int employeeId;
            int superiorId;
            using (var dataContext = new DataContext())
            {
                employeeId = dataContext.Employees.First(e => e.Name == "Manager 1").Id;
                superiorId = dataContext.Employees.First(e => e.Name == "Manager 1").SuperiorId.Value;
            }

            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == employeeId)
                                       .Select(e => e.GetSuperior().Pass(e));

                var result = query.First();
                Assert.AreEqual(superiorId, result.Id);
            }
        }

        [TestMethod]
        public void TestParemeteredExtensionExpression()
        {

            var minValue = 9_000_000m;

            int numOfProjectsWithMinValue;
            using (var dataContext = new DataContext())
            {
                numOfProjectsWithMinValue = dataContext.Projects
                                                       .Count(p => p.CustomerId != null && p.Value > minValue);
            }
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Customers
                                       .AsComposable()
                                       .SelectMany(c => c.GetCustomerProjectsWithMinValue(minValue).Pass(c))
                                       .Distinct();

                var result = query.ToList();
                Assert.AreEqual(numOfProjectsWithMinValue, result.Count);
            }
        }
    }

    public static class EmployeeExtension
    {
        public static Expression<Func<Employee, Employee>> GetSuperior(this Employee employee)
        {
            return e => e.Superior;
        }

        public static Expression<Func<Customer, IEnumerable<Project>>> GetCustomerProjectsWithMinValue(this Customer customer, decimal minValue)
        {
            return c => c.Projects.Where(p => p.Value >= minValue);
        }
    }
}