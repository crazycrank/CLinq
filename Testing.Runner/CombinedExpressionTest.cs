using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LinqOnSteroids;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.Database;
using Testing.Database.Model;

namespace Testing.Runner
{
    [TestClass]
    public class CombinedExpressionTest
    {
        private int _employeeId;

        private static readonly Expression<Func<Employee, IEnumerable<Employee>>> GetSubordinates = e => e.Employees;

        private static readonly Expression<Func<Employee, bool>> YoungerThan1980 = e => e.Birthdate > new DateTime(1980, 1, 1);
        private static readonly Expression<Func<Employee, bool>> MoreThan2Projects = e => e.EmployeeProjects.Count > 2;
        private static readonly Expression<Func<Employee, bool>> YoungerThan1980AndMoreThan2Projects = e => YoungerThan1980.Pass(e) && MoreThan2Projects.Pass(e);

        private static readonly Expression<Func<ICollection<EmployeeProject>, bool>> MoreThan2Projects2 = e => e.Count > 2;
        private static readonly Expression<Func<Employee, bool>> YoungerThan1980AndMoreThan2Projects2 = e => YoungerThan1980.Pass(e) && MoreThan2Projects2.Pass(e.EmployeeProjects);


        private static readonly Expression<Func<IEnumerable<Employee>, IEnumerable<Project>>> GetProjectsForEmployees =
            employees => employees.SelectMany(e => e.EmployeeProjects).Select(ep => ep.Project).Distinct();

        private IEnumerable<Project> _projectsOfManager1Subordinates;
        private IEnumerable<Employee> _employeesYoungerThan1980AndMin2Projects;


        [TestInitialize]
        public void Init()
        {
            using (var dataContext = new DataContext())
            {
                this._employeeId = dataContext.Employees.First(e => e.Name == "Manager 1").Id;
                // ReSharper disable once PossibleInvalidOperationException
                this._projectsOfManager1Subordinates = dataContext.Employees
                                           .Where(e => e.SuperiorId == _employeeId)
                                           .SelectMany(e => e.EmployeeProjects)
                                           .Select(ep => ep.Project)
                                           .Distinct()
                                           .ToList();

                this._employeesYoungerThan1980AndMin2Projects = dataContext.Employees
                                                                           .Where(e => e.Birthdate > new DateTime(1980, 1, 1) && e.EmployeeProjects.Count > 2)
                                                                           .ToList();
            }
        }

        [TestMethod]
        public void TestExpressionAsParameterForExpression()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsExpandable()
                                       .Where(e => e.Id ==_employeeId)
                                       .SelectMany(e => GetProjectsForEmployees.Pass(GetSubordinates.Pass(e)))
                                       .Distinct();

                var projectsOfManager1Subordinates = query.ToList();
                Assert.AreEqual(_projectsOfManager1Subordinates.Count(), projectsOfManager1Subordinates.Count);
            }
        }

        [TestMethod]
        public void TestCombinedConditionalExpressions()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsExpandable()
                                       .Where(e => YoungerThan1980.Pass(e) && MoreThan2Projects.Pass(e));
                var result = query.ToList();
                Assert.AreEqual(_employeesYoungerThan1980AndMin2Projects.Count(), result.Count);
            }
        }

        [TestMethod]
        public void TestCombinedConditionalExpressionsCombinedInExpression()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsExpandable()
                                       .Where(e => YoungerThan1980AndMoreThan2Projects.Pass(e));
                var result = query.ToList();
                Assert.AreEqual(_employeesYoungerThan1980AndMin2Projects.Count(), result.Count);
            }
        }


        [TestMethod]
        public void TestParameterRenaming()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsExpandable()
                                       .Where(e => YoungerThan1980AndMoreThan2Projects2.Pass(e));
                var result = query.ToList();
                Assert.AreEqual(_employeesYoungerThan1980AndMin2Projects.Count(), result.Count);
            }
        }

        [TestMethod]
        public void TestExpressionAsParam1()
        {
            using (var dataContext = new DataContext())
            {
                var query = MethodWithExpressionAsParam(dataContext.Employees.AsExpandable(), YoungerThan1980AndMoreThan2Projects);
                var result = query.ToList();
                Assert.AreEqual(_employeesYoungerThan1980AndMin2Projects.Count(), result.Count);
            }
        }

        [TestMethod]
        public void TestExpressionAsParam2()
        {
            using (var dataContext = new DataContext())
            {
                var query = MethodWithExpressionAsParam(MethodWithExpressionAsParam(dataContext.Employees.AsExpandable(), YoungerThan1980), MoreThan2Projects);
                var result = query.ToList();
                Assert.AreEqual(_employeesYoungerThan1980AndMin2Projects.Count(), result.Count);
            }
        }

        private IQueryable<Employee> MethodWithExpressionAsParam(IQueryable<Employee> query, Expression<Func<Employee, bool>> conditional) 
            => query.Where(conditional);
    }
}
