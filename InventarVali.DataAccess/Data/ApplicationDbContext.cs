using InventarVali.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarVali.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }

        public DbSet<Goods> Goods { get; set; }
        public DbSet<Autovehicule> Autovehicule { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Computer> Computers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goods>().HasData(
                new Goods { Id = 1, Name = "Masina", Type = "Dacia Duster", IsTaken = true, ImageUrl = "" },
                new Goods { Id = 2, Name = "Masina", Type = "Audi A6", IsTaken = false, ImageUrl = "" },
                new Goods { Id = 3, Name = "Laptop", Type = "Asus Rog", IsTaken = true, ImageUrl = "" }
                );
            modelBuilder.Entity<Employees>().HasData(
               new Employees { Id = 1, FirstName = "John", LastName = "Doe", Email = "test@email.com", GoodsId = 1 },
               new Employees { Id = 2, FirstName = "Michael", LastName = "Cox", Email = "test2@email.com", GoodsId = 1 },
               new Employees { Id = 3, FirstName = "Vasile", LastName = "Braconieru", Email = "test4@email.com" , GoodsId = 2 }
               );
            modelBuilder.Entity<Autovehicule>().HasData(
               new Autovehicule { Id = 1, HasITP = true, InsurenceDate = DateTime.SpecifyKind(DateTime.Parse("07/11/2024"), DateTimeKind.Utc), LicensePlate = "B 06 CAR", Type = "Duba", VinNumber = "1XPWDBTX48D766660",
                   InsuranceExpirationDate = DateTime.SpecifyKind(DateTime.Parse("07/11/2024"),DateTimeKind.Utc), ITPExpirationDate = DateTime.SpecifyKind(DateTime.Parse("01/09/2024"), DateTimeKind.Utc),
                   HasVinieta = true,VinietaExpirationDate = DateTime.SpecifyKind(DateTime.Parse("04/10/2024"), DateTimeKind.Utc), GoodsId = 2
               },
               new Autovehicule { Id = 2, HasITP = false, InsurenceDate = DateTime.SpecifyKind(DateTime.Parse("12/11/2024"), DateTimeKind.Utc), LicensePlate = "CL 06 PLM", Type = "Audi R8", VinNumber = "1XPWDBTX48D766660",
                   InsuranceExpirationDate = DateTime.SpecifyKind(DateTime.Parse("02/11/2024"), DateTimeKind.Utc), ITPExpirationDate = DateTime.SpecifyKind(DateTime.Parse("10/09/2024"), DateTimeKind.Utc),
                   HasVinieta = false,VinietaExpirationDate = DateTime.SpecifyKind(DateTime.Parse("07/09/2024"), DateTimeKind.Utc), GoodsId = 1
               }
               );
            modelBuilder.Entity<Computer>().HasData(
                new Computer { Id = 1, Type = "Laptop", Model = "Asus Rog", Description = "Intel Core I9, RTX 4070, 32 GB RAM", SerialNumber = "12-12AB3",GoodsId = 1 },
                new Computer { Id = 2, Type = "Desktop", Model = "x570 Aorus Elite", Description = "Intel Core I5, no GPU, 32 GB RAM", GoodsId = 2}
                );
        }
    

    }
}
