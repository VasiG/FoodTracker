using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Measure> Measures{ get; set; }
        
    }
}
