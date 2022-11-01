using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guacamole.Models;


[Table("Composer")]
public class Composer
{
    // TODO: AutoIncrement using FluentAPI.
    public string ComposerSigature 
    { get; set; }
    
    [MaxLength(128)]
    public string ComposerName 
    { get; set; }
    
    public DateOnly ComposerDateBorn 
    { get; set; }
    
    public Period ComposerPeriod 
    { get; set; }
    
    public virtual ICollection<MusicPiece> Pieces { get; }
}