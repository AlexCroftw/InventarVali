using InventarVali.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventarVali.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }

        public DbSet<Autovehicule> Autovehicule { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<AutovehiculeInvoice> AutovehiculeInvoice { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutovehiculeInvoice>()
                .HasKey(sc => new { sc.AutovehiculeFKID, sc.InvoiceFKID }); 
            modelBuilder.Entity<AutovehiculeInvoice>()
                .HasOne(sc => sc.Autovehicule)
                .WithMany(s => s.AutovehiculeInvoice) 
                .HasForeignKey(sc => sc.AutovehiculeFKID); 
            modelBuilder.Entity<AutovehiculeInvoice>()
                .HasOne(sc => sc.Invoice)
                .WithMany(c => c.AutovehiculeInvoice) 
                .HasForeignKey(sc => sc.InvoiceFKID); 

            modelBuilder.Entity<AutovehiculeInvoice>().ToTable("AutovehiculeInvoice");

            modelBuilder.Entity<Employees>().HasData(
               new Employees { Id = 1, FirstName = "John", LastName = "Doe", Email = "test@email.com", FullName = "John Doe" },
               new Employees { Id = 2, FirstName = "Michael", LastName = "Cox", Email = "test2@email.com", FullName = "Michael Cox" },
               new Employees { Id = 3, FirstName = "Vasile", LastName = "Braconieru", Email = "test4@email.com", FullName = "Vasile Braconieru" }
               );
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    Id = 1,
                    InvoiceNumber = "24/000838503/997",
                    CardNumber = "704310.0109124771",
                    TotalPrice = 30000m,
                    InvoiceUrl = "",
                    Price = 200m,
                    InvoiceDate = DateTime
                .SpecifyKind(DateTime.Parse("02/11/2025"), DateTimeKind.Utc),
                });
            modelBuilder.Entity<Autovehicule>().HasData(
               new Autovehicule
               {
                   Id = 1,
                   HasITP = true,
                   InsurenceDate = DateTime.SpecifyKind(DateTime.Parse("07/11/2024"), DateTimeKind.Utc),
                   LicensePlate = "B 06 CAR",
                   Type = "Duba",
                   VinNumber = "1XPWDBTX48D766660",
                   InsuranceExpirationDate = DateTime.SpecifyKind(DateTime.Parse("07/11/2024"), DateTimeKind.Utc),
                   ITPExpirationDate = DateTime.SpecifyKind(DateTime.Parse("01/09/2024"), DateTimeKind.Utc),
                   HasVinieta = true,
                   VinietaExpirationDate = DateTime.SpecifyKind(DateTime.Parse("04/10/2024"), DateTimeKind.Utc),
                   EmployeeId = 1,

               },
               new Autovehicule
               {
                   Id = 2,
                   HasITP = false,
                   InsurenceDate = DateTime.SpecifyKind(DateTime.Parse("12/11/2024"), DateTimeKind.Utc),
                   LicensePlate = "CL 06 PLM",
                   Type = "Audi R8",
                   VinNumber = "1XPWDBTX48D766660",
                   InsuranceExpirationDate = DateTime.SpecifyKind(DateTime.Parse("02/11/2024"), DateTimeKind.Utc),
                   ITPExpirationDate = DateTime.SpecifyKind(DateTime.Parse("10/09/2024"), DateTimeKind.Utc),
                   HasVinieta = false,
                   VinietaExpirationDate = DateTime.SpecifyKind(DateTime.Parse("07/09/2024"), DateTimeKind.Utc),
                   EmployeeId = 2
               }
               );
            modelBuilder.Entity<Computer>().HasData(
                new Computer { Id = 1, Type = "Laptop", Model = "Asus Rog", Description = "Intel Core I9, RTX 4070, 32 GB RAM", SerialNumber = "12-12AB3", ImageUrl = "", EmployeeId = 2 },
                new Computer { Id = 2, Type = "Desktop", Model = "x570 Aorus Elite", Description = "Intel Core I5, no GPU, 32 GB RAM", ImageUrl = "", EmployeeId = 1 }
                );
        }


    }
}
