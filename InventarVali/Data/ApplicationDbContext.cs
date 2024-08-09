using InventarVali.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarVali.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Goods> Goods { get; set; }
    }
}
