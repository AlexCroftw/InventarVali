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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goods>().HasData(
                new Goods { Id = 1, Name = "Masina", Type = "Dacia Duster", IsTaken = true, ImageUrl = ""},
                new Goods { Id = 2, Name = "Masina", Type = "Audi A6", IsTaken = false, ImageUrl = "" },
                new Goods { Id = 3, Name = "Laptop", Type = "Asus Rog", IsTaken = true, ImageUrl = "" }
                );
        }
    }
}
