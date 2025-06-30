using Microsoft.EntityFrameworkCore;

namespace CrudWeb.Infraestructure
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        // Exemplo de DbSet
        // public DbSet<Courier> Couriers { get; set; }
    }
}
