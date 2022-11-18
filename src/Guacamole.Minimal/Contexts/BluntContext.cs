using Guacamole.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Guacamole.Minimal.Contexts;

public sealed class BluntContext : BluntContextBase
{
    public BluntContext(DbContextOptions<BluntContext> options)
        : base(options)
    {
        // Disable lazy loading:
        // ChangeTracker.LazyLoadingEnabled = false;
    }
}