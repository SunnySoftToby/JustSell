using JustSell.Models;
using Microsoft.EntityFrameworkCore;

namespace JustSell.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
