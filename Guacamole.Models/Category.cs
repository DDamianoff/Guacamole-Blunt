using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Guacamole.Models;

public class Category
{
    public int Id 
    { get; set; }
    
    [Required]
    [MaxLength(256)]
    public string Name
    { 
        get; 
        set; 
    } = null!;

    public DateOnly? DateCreated
    {
        get;
        set;
    } = null!;
    
    [JsonIgnore]
    public virtual ICollection<Idea> Ideas
    {
        get;
        set;
    } = null!;
}