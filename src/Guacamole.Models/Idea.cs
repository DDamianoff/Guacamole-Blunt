using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Guacamole.Models;

[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public class Idea
{
    public int Id 
    { get; set; }

    public string Content
    {
        get;
        set;
    } = null!;

    public DateTime Created
    {
        get;
        set;
    }

    public DateTime Modified
    {
        get;
        set;
    }

    public bool Archived
    {
        get;
        set;
    } = false;
    
    [NotMapped]
    public bool RecentlyViewed 
    { 
        get; 
        set; 
    } = false;
    public int CategoryId { get; set; }
    [JsonIgnore]
    public virtual Category Category
    {
        get;
        set;
    }
}