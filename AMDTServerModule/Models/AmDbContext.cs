using AMDTServerModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMDTServerModule.Models
{
    public class AmDbContext : DbContext
    {
        public AmDbContext(DbContextOptions<AmDbContext> options) : base(options)
        {

            this.ChangeTracker.LazyLoadingEnabled = false;

        }
        public DbSet<Users> Users { get; set; }
    }
}
