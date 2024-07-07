using BasicDotNetCoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicDotNetCoreAPI.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
