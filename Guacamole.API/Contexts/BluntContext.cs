using Guacamole.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Guacamole.API.Contexts;

public sealed class BluntContext : BluntContextBase
{
    public BluntContext(DbContextOptions<BluntContextBase> options)
        : base(options)
    { }
}