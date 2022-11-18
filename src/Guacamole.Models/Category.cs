using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Guacamole.Models;

[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
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
    }

    public DateTime Created
    {
        get;
        set;
    }
    
    [JsonIgnore]
    public virtual ICollection<Idea> Ideas
    {
        get;
        set;
    }
}