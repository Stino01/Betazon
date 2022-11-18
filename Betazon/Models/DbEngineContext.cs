using Microsoft.EntityFrameworkCore;

namespace Betazon.Models
{
    public class DbEngineContext : DbContext
    {
        public DbEngineContext(DbContextOptions<DbEngineContext> options)
        : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
    }
}
