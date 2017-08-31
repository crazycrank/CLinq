using System.Collections.Generic;

namespace Testing.Database.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }
}