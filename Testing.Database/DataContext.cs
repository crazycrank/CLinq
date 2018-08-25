using System.Data.Entity;
using Testing.Database.Model;

namespace Testing.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
