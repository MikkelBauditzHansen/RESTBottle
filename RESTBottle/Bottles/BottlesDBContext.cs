using Microsoft.EntityFrameworkCore;

namespace RESTBottle.Bottles
{
    public class BottlesDBContext : DbContext
    {
        public BottlesDBContext(DbContextOptions<BottlesDBContext> options) : base(options)
        {
        }
        public DbSet<Bottle> Bottles { get; set; }
    }
}
