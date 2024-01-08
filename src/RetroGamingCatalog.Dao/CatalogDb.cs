using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RetroGamingCatalog.Dao
{
    public class CatalogDb : DbContext

    {
        public CatalogDb() : base() { }
        public CatalogDb(DbContextOptions<CatalogDb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Console)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.ConsoleId);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(m => m.Consoles)
                .WithOne(c => c.Manufacturer)
                .HasForeignKey(m => m.ManufacturerId);

            modelBuilder.Entity<User>();

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }
        public DbSet<Console> Consoles { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

    }
}
