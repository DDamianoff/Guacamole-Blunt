using Guacamole.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Guacamole.API.Contexts;

public sealed class BluntContext : BluntContextBase
{
    public BluntContext(DbContextOptions<BluntContext> options)
        : base(options)
    { }
}