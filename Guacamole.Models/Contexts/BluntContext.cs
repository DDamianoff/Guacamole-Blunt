using Microsoft.EntityFrameworkCore;

namespace Guacamole.Models.Contexts;

public class BluntContext : DbContext
{
    public BluntContext (DbContextOptions<BluntContext> options)
        : base(options) 
    { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}