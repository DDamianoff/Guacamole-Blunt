using System.ComponentModel.DataAnnotations;

namespace Guacamole.Models;

public class MusicPiece
{
    [Key]
    public string PieceCode { get; set; }
    
    public Composer ComposerName { get; set; }
}
