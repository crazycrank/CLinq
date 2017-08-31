using System;
using System.Collections.Generic;

namespace Testing.Database.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }

        public int? SuperiorId { get; set; }
        public Employee Superior { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
