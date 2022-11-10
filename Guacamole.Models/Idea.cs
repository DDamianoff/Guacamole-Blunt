using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Guacamole.Models;

public class Idea
{
    public int Id 
    { get; set; }

    public string Content
    {
        get;
        set;
    } = null!;

    public DateOnly DateCreated
    {
        get;
        set;
    }

    public DateOnly DateModified
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
    public virtual Category? Category
    {
        get;
        set;
    }
}