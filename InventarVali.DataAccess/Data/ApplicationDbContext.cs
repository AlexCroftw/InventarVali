using InventarVali.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarVali.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Goods> Goods { get; set; }
       // public DbSet<Autovehicule> Autovehicules { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goods>().HasData(
                new Goods { Id = 1, Name = "Masina", Type = "Dacia Duster", IsTaken = true, ImageUrl = ""},
                new Goods { Id = 2, Name = "Masina", Type = "Audi A6", IsTaken = false, ImageUrl = "" },
                new Goods { Id = 3, Name = "Laptop", Type = "Asus Rog", IsTaken = true, ImageUrl = "" }
                );
            modelBuilder.Entity<Employee>().HasData(
               new Employee { Id = 1, FirstName = "John", LastName = "Doe", Email = "test@email.com" },
               new Employee { Id = 2, FirstName = "Michael", LastName = "Cox", Email = "test2@email.com" },
               new Employee { Id = 3, FirstName = "Vasile", LastName = "Braconieru", Email = "test4@email.com" }
               );
        }

    }
}
