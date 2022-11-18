using Microsoft.EntityFrameworkCore;
using Betazon.Models;

namespace Betazon.Models
{
    public class DbEngineContext : DbContext
    {
        public DbEngineContext(DbContextOptions<DbEngineContext> options)
        : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Betazon.Models.EncryptionData> EncryptionData { get; set; }

        public DbSet<Betazon.Models.Admin> Admin { get; set; }
    }
}
