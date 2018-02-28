using System;
using System.Linq;
using System.Linq.Expressions;
using CLinq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.Database;
using Testing.Database.Model;

namespace Testing.Runner
{
    [TestClass]
    public class SimpleExpressionTest
    {
        private int _employeeId;
        private int _superiorId;


        private Expression<Func<Employee, Employee>> _getSuperiorField = e => e.Superior;

        private readonly Expression<Func<Employee, Employee>> _getSuperiorFieldReadonly = e => e.Superior;

        // ReSharper disable once ConvertToAutoProperty
        private Expression<Func<Employee, Employee>> GetSuperiorProperty => _getSuperiorField;

        private Expression<Func<Employee, Employee>> GetSuperiorMethod() => e => e.Superior;

        [TestInitialize]
        public void Init()
        {
            using (var dataContext = new DataContext())
            {
                _employeeId = dataContext.Employees.First(e => e.Name == "Employee 3").Id;
                // ReSharper disable once PossibleInvalidOperationException
                _superiorId = dataContext.Employees.First(e => e.Id == _employeeId).SuperiorId.Value;
            }
            
        }

        [TestMethod]
        public void SimpleStaticFieldTest()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => StaticExpressionHolder.GetSuperiorField.Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

        [TestMethod]
        public void SimpleStaticFieldReadonlyTest()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => StaticExpressionHolder.GetSuperiorFieldReadonly.Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

        [TestMethod]
        public void SimpleStaticPropertyTest()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => StaticExpressionHolder.GetSuperiorProperty.Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

        [TestMethod]
        public void SimpleStaticMethodTest()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => StaticExpressionHolder.GetSuperiorMethod().Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }


        [TestMethod]
        public void SimpleInstanceFieldTest()
        {
            var instance = new InstanceExpressionHolder();
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => instance.GetSuperiorField.Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

        [TestMethod]
        public void SimpleInstanceFieldReadonlyTest()
        {
            var instance = new InstanceExpressionHolder();
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => instance.GetSuperiorFieldReadonly.Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

        [TestMethod]
        public void SimpleInstancePropertyTest()
        {
            var instance = new InstanceExpressionHolder();
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => instance.GetSuperiorProperty.Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

        [TestMethod]
        public void SimpleInstanceMethodTest()
        {
            var instance = new InstanceExpressionHolder();
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => instance.GetSuperiorMethod().Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }



        [TestMethod]
        public void SimpleLocalFieldTest()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => _getSuperiorField.Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

        [TestMethod]
        public void SimpleLocalFieldReadonlyTest()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => _getSuperiorFieldReadonly.Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

        [TestMethod]
        public void SimpleLocalPropertyTest()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => GetSuperiorProperty.Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

        [TestMethod]
        public void SimpleLocalMethodTest()
        {
            using (var dataContext = new DataContext())
            {
                var query = dataContext.Employees
                                       .AsComposable()
                                       .Where(e => e.Id == _employeeId)
                                       .Select(e => GetSuperiorMethod().Pass(e));

                var result = query.ToList();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(_superiorId, result.First().Id);
            }
        }

    }

    public static class StaticExpressionHolder
    {
        public static Expression<Func<Employee, Employee>> GetSuperiorField = e => e.Superior;

        public static readonly Expression<Func<Employee, Employee>> GetSuperiorFieldReadonly = e => e.Superior;

        private static readonly Expression<Func<Employee, Employee>> _GetSuperiorProperty = e => e.Superior;

        // ReSharper disable once ConvertToAutoProperty
        public static Expression<Func<Employee, Employee>> GetSuperiorProperty => _GetSuperiorProperty;

        public static Expression<Func<Employee, Employee>> GetSuperiorMethod() => e => e.Superior;
    }

    public class InstanceExpressionHolder
    {
        public Expression<Func<Employee, Employee>> GetSuperiorField = e => e.Superior;

        public readonly Expression<Func<Employee, Employee>> GetSuperiorFieldReadonly = e => e.Superior;

        private readonly Expression<Func<Employee, Employee>> _getSuperiorProperty = e => e.Superior;

        // ReSharper disable once ConvertToAutoProperty
        public Expression<Func<Employee, Employee>> GetSuperiorProperty => _getSuperiorProperty;

        public Expression<Func<Employee, Employee>> GetSuperiorMethod() => e => e.Superior;
    }
}
