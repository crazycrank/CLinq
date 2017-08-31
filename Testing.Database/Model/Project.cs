
using System.Collections.Generic;

namespace Testing.Database.Model
{
    public class Project
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string ProjectName { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
        public decimal Value { get; set; }

    }
}