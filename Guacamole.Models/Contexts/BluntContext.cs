using Microsoft.EntityFrameworkCore;

namespace Guacamole.Models.Contexts;

public sealed class BluntContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Idea> Ideas { get; set; }

    public BluntContext(DbContextOptions<BluntContext> options)
        : base(options)
    {
        // Disable lazy loading:
        // ChangeTracker.LazyLoadingEnabled = false;
    }
    
    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Category>()
            .HasKey(c => c.Id);
        model.Entity<Idea>()
            .HasKey(i => i.Id);
        
        model.Entity<Category>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        model.Entity<Idea>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();

        model.Entity<Category>()
            .HasData(new Category()
                {
                    Id = 1,
                    Name = "Default",
                    DateCreated = DateOnly.FromDateTime(DateTime.Today)
                });
        
        model.Entity<Idea>()
            .HasData(new Idea()
                {
                    Id = 1,
                    Content = "My first idea!",
                    DateCreated = DateOnly.FromDateTime(DateTime.Today),
                    DateModified = DateOnly.FromDateTime(DateTime.Today),
                    CategoryId = 1
                });
    }
}