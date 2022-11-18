using Microsoft.EntityFrameworkCore;

namespace Guacamole.Models.Contexts;

public class BluntContextBase : DbContext
{
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Idea> Ideas { get; set; }

    public BluntContextBase(DbContextOptions<BluntContextBase> options)
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
            .HasMany(c => c.Ideas)
            .WithOne(c => c.Category)
            .OnDelete(DeleteBehavior.Cascade);
        
        model.Entity<Category>()
            .HasData(new Category()
            {
                Id = 1,
                Name = "Default",
                Created = DateTime.Now
            });
        
        model.Entity<Idea>()
            .HasData(new Idea()
            {
                Id = 1,
                Content = "My first idea!",
                Created = DateTime.Now,
                Modified = DateTime.Now,
                CategoryId = 1
            });
    }
}


