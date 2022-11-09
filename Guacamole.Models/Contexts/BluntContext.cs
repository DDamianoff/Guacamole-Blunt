using Microsoft.EntityFrameworkCore;

namespace Guacamole.Models.Contexts;

public class BluntContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Idea> Ideas { get; set; }
    public BluntContext (DbContextOptions<BluntContext> options)
        : base(options) 
    { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Idea>()
            .HasKey(i => i.Id);
        
        modelBuilder.Entity<Category>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Idea>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();
    }
}