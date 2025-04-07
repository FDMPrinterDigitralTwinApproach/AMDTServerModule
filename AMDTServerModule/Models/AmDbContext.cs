using Microsoft.EntityFrameworkCore;

namespace AMDTServerModule.Models
{
    public class AmDbContext : DbContext
    {
        public AmDbContext(DbContextOptions<AmDbContext> options) : base(options)
        {

            this.ChangeTracker.LazyLoadingEnabled = false;

        }
       // public DbSet<MobileUsers> MobileUsers { get; set; }
    }
}
