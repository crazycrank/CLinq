using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.Database;
using Testing.Database.Model;

namespace Testing.Runner
{
    [TestClass]
    public class TestInitialization
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext ctx)
        {
            using (var context = new DataContext())
            {
                System.Data.Entity.Database.Delete(context.Database.Connection);
            }

            using (var context = new DataContext())
            {
                var ceo = new Employee
                          {
                              Name = "CEO",
                              Birthdate = new DateTime(1960, 01, 01),
                              SuperiorId = null
                          };
                context.Employees.Add(ceo);

                var manager1 = new Employee
                               {
                                   Name = "Manager 1",
                                   Birthdate = new DateTime(1980, 02, 24),
                                   Superior = ceo,
                                   EmployeeProjects = new List<EmployeeProject>()
                               };
                context.Employees.Add(manager1);

                var manager2 = new Employee
                               {
                                   Name = "Manager 2",
                                   Birthdate = new DateTime(1970, 11, 01),
                                   Superior = ceo,
                                   EmployeeProjects = new List<EmployeeProject>()
                               };
                context.Employees.Add(manager2);

                var manager3 = new Employee
                               {
                                   Name = "Manager 3",
                                   Birthdate = new DateTime(1975, 05, 09),
                                   Superior = ceo,
                                   EmployeeProjects = new List<EmployeeProject>()
                               };
                context.Employees.Add(manager3);

                foreach (var id in Enumerable.Range(0, 10))
                {
                    var employee = new Employee
                                   {
                                       Name = $"Employee {id}",
                                       Birthdate = new DateTime(1990, 01, id + 1),
                                       Superior = id < 7 ? manager1 : manager2,
                                       EmployeeProjects = new List<EmployeeProject>()
                                   };
                    context.Employees.Add(employee);
                }


                foreach (var id in Enumerable.Range(0, 5))
                {
                    var internalProject = new Project
                                          {
                                              ProjectName = $"Internal Project {id}",
                                              CustomerId = null,
                                              Value = (decimal) (new Random().NextDouble()*100_000),
                                              EmployeeProjects = new List<EmployeeProject>()
                                          };

                    var manager = id%3 != 0 ? manager1 : manager2;
                    manager.EmployeeProjects.Add(new EmployeeProject
                                                 {
                                                     Employee = manager,
                                                     Project = internalProject
                                                 });
                }

                context.Customers.AddRange(Enumerable.Range(0, 20)
                                                     .Select(id => new Customer
                                                                   {
                                                                       Name = $"Customer {id}",
                                                                       Projects = new List<Project>()
                                                                   }));

                context.SaveChanges();
            }

            using (var context = new DataContext())
            {
                var employees = context.Employees.Where(e => e.Name != "CEO").ToArray();

                var random = new Random();
                foreach (var customer in context.Customers)
                {
                    var next = random.Next(10);
                    for (var i = 0; i < next; i++)
                    {
                        var project = new Project
                                      {
                                          ProjectName = $"{customer.Name} Project {i}",
                                          Value = (decimal) (random.NextDouble()*10_000_000),
                                          Customer = customer,
                                          EmployeeProjects = new List<EmployeeProject>()
                                      };


                        var numOfEmployees = random.Next(5);
                        var usedEmployees = new HashSet<int>();

                        for (var j = 0; j <= numOfEmployees; j++)
                        {
                            int nextEmployee;
                            do
                            {
                                nextEmployee = random.Next(employees.Length);
                            } while (!usedEmployees.Add(nextEmployee));

                            var employee = employees[nextEmployee];
                            var employeeProject = new EmployeeProject {Employee = employee, Project = project};
                            employee.EmployeeProjects.Add(employeeProject);
                        }

                        context.Projects.Add(project);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
