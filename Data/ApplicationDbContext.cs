using Microsoft.EntityFrameworkCore;
using MultilingualMenuApp.Models;

namespace MultilingualMenuApp.Data
{
    /// <summary>
    /// Database context for the multilingual menu application
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<MenuTranslation> MenuTranslations { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Menu entity
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DefaultName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DefaultDescription).HasMaxLength(1000);
            });

            // Configure MenuTranslation entity
            modelBuilder.Entity<MenuTranslation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Language).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);

                // Configure relationship with Menu
                entity.HasOne(e => e.Menu)
                    .WithMany(m => m.Translations)
                    .HasForeignKey(e => e.MenuId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Language entity
            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Code).IsUnique();
            });

            // Seed default languages
            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Code = "en", Name = "English", IsActive = true },
                new Language { Id = 2, Code = "tr", Name = "Türkçe", IsActive = true },
                new Language { Id = 3, Code = "es", Name = "Español", IsActive = true }
            );
        }
    }
}
