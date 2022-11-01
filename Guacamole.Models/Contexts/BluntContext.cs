using Microsoft.EntityFrameworkCore;

namespace Guacamole.Models.Contexts;

public class BluntContext : DbContext
{
    public System.Data.Entity.DbSet<Composer> Composers;

    public System.Data.Entity.DbSet<MusicPiece> Pieces;
    public System.Data.Entity.DbSet<Period> Periods;
    public System.Data.Entity.DbSet<TimePeriod> TimePeriods;

    public BluntContext (DbContextOptions<BluntContext> options)
        : base(options) 
    { }
}