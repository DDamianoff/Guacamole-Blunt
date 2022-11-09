using Microsoft.EntityFrameworkCore;

namespace Guacamole.Models.Contexts;

public class BluntContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Idea> Ideas { get; set; }
    public BluntContext (DbContextOptions<BluntContext> options)
        : base(options) 
    { }
    
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
    }
}