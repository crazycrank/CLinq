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
    public class MultipleParameterExpressionTests
    {
        private readonly Expression<Func<Employee, Employee, bool>> _sameYearOfBirth = (e1, e2) => e1.Birthdate.Year == e2.Birthdate.Year;

        [TestMethod]
        public void TestMultipleParameterExpression()
        {
            Employee e1;
            using (var dataContext = new DataContext())
            {
                e1 = dataContext.Employees.First(e => e.Birthdate.Year == 1990);
            }

            using (var dataContext = new DataContext())
            {
                var q = dataContext.Employees.AsComposable().Where(e => _sameYearOfBirth.Pass(e1, e));
                var result = q.ToList();

                Assert.AreEqual(10, result.Count);
            }
        }

        [TestMethod]
        public void Test()
        {
            List<Employee> expected;
            using (var dataContext = new DataContext())
            {
                expected = dataContext.Employees.Where(e => e.Superior != null && e.Superior.Superior != null).ToList();
            }

            var hasSuperior = (Expression<Func<Employee, bool>>)(e => e.Superior != null);
            var getSuperior = (Expression<Func<Employee, Employee>>) (e => e.Superior);
            using (var dataContext = new DataContext())
            {
                var q = dataContext.Employees
                                   .AsComposable()
                                   .Where(e => hasSuperior.Pass(getSuperior.Pass(e)));

                var result = q.ToList();

                Assert.AreEqual(result.Count, expected.Count());
            }
        }
    }
}