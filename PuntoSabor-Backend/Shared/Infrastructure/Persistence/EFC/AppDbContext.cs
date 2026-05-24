using Microsoft.EntityFrameworkCore;
using PuntoSabor_Backend.Auth.Domain.Model;
using PuntoSabor_Backend.Discovery.Domain.Model;
using PuntoSabor_Backend.Favorites.Domain.Model;
using PuntoSabor_Backend.Memberships.Domain.Model;
using Subscription = PuntoSabor_Backend.Memberships.Domain.Model.Subscription;
using PuntoSabor_Backend.Promotions.Domain.Model;
using PuntoSabor_Backend.Reviews.Domain.Model;

namespace PuntoSabor_Backend.Shared.Infrastructure.Persistence.EFC;

/**
 * <summary>
 *     Contexto principal de EF Core con las tablas del sistema.
 * </summary>
 */

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Huarique> Huariques { get; set; }
    
    public DbSet<Promo> Promos { get; set; }
    
    public DbSet<Plan> Plans { get; set; }
    
    public DbSet<Review> Reviews { get; set; }

    public DbSet<Favorite> Favorites { get; set; }

    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de tablas
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Category>().ToTable("Categories");
        modelBuilder.Entity<Huarique>().ToTable("Huariques");
        modelBuilder.Entity<Promo>().ToTable("Promos");
        modelBuilder.Entity<Plan>().ToTable("Plans");
        modelBuilder.Entity<Review>().ToTable("Reviews");

        modelBuilder.Entity<Plan>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Plan>()
            .Property(p => p.Id)
            .HasMaxLength(50);

        modelBuilder.Entity<Favorite>().ToTable("Favorites");
        modelBuilder.Entity<Favorite>()
            .HasIndex(f => new { f.UserId, f.HuariqueId })
            .IsUnique();

        modelBuilder.Entity<Subscription>().ToTable("Subscriptions");
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Plan)
            .WithMany()
            .HasForeignKey(s => s.PlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}