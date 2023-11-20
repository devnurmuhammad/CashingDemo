using CashingDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashingDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cashe> Cashes { get; set; }
    }
}
