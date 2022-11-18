using Guacamole.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Guacamole.Minimal.Contexts;

public sealed class BluntContext : BluntContextBase
{
    public BluntContext(DbContextOptions<BluntContextBase> options)
        : base(options)
    {
        // Disable lazy loading:
        // ChangeTracker.LazyLoadingEnabled = false;
    }
}