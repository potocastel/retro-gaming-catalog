using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace RetroGamingCatalog.Dao
{
    public class CatalogDb : DbContext
    {
        public CatalogDb() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("catalog", x => { });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Console)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.ConsoleId);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(m => m.Consoles)
                .WithOne(c => c.Manufacturer)
                .HasForeignKey(m => m.ManufacturerId);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Console> Consoles { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

    }
}
