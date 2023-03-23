using Microsoft.EntityFrameworkCore;

namespace CRUDTest.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<OrderDB> Order { get; set; } = null!;
        public DbSet<OrderItemDB> OrderItem { get; set; } = null!;
        public DbSet<ProviderDB> Provider { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }    
}
