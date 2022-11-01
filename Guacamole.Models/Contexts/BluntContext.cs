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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Primary keys

        modelBuilder.Entity<Composer>()
            .HasKey(c => c.ComposerSigature);

        modelBuilder.Entity<MusicPiece>()
            .HasKey(m => m.PieceCode);
        

        modelBuilder.Entity<Period>()
            .HasKey(p => p.PeriodId);
        modelBuilder.Entity<Period>()
            .Property(p => p.PeriodId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TimePeriod>()
            .HasKey(t => t.TimePeriodId);
        modelBuilder.Entity<TimePeriod>()
            .Property(t => t.TimePeriodId)
            .ValueGeneratedOnAdd();

        #endregion

        #region Foreign Keys

        


        #endregion
    }
}