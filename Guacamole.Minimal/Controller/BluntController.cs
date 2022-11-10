using Guacamole.Models;
using Guacamole.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Guacamole.Minimal.Controller;

public static class BluntController
{
    public static IResult HelloWorld ([FromServices] BluntContext db)
    {
        var result = db.Database.EnsureCreated();

        return Results.Ok("Hello world!" + "\n\n" + (result
            ? "Db recently created"
            : "Working with an exiting db"));
    }
    
    
    public static IResult GetIdeas (int limit, [FromServices]  BluntContext db) => 
        Results.Ok(db.Ideas
            .Take(limit)
            .ToList());

    public static IResult GetByCategory(int limit, string category, [FromServices] BluntContext db)
    {
        var result = db.Categories
            // ReSharper disable once SpecifyStringComparison
            .FirstOrDefault(c => category.ToLower() == c.Name.ToLower());
        
        if (result is null) 
            return Results.NotFound();

        return Results.Ok(db.Ideas
                .Where(i => i.CategoryId == result.Id)
                .Take(limit)
                .ToList());
    }

    public static IResult DeleteIdea (int id, [FromServices] BluntContext db)
    {
        var result = db.Ideas.Find(id);
        
        if (result is null)
            return Results.NotFound();

        db.Ideas.Remove(result);
        db.SaveChanges();
        
        return Results.Ok(result);
    }
    public static IResult UpdateIdea (int id, Idea input, [FromServices] BluntContext db)
    {
        var idea = db.Ideas.Find(id);

        if (idea is null)
            return Results.NotFound();

        
        if (idea.CategoryId != input.CategoryId)
        {
            var category = db.Categories.Find(input.CategoryId);

            if (category is null)
                return Results.BadRequest("Specified category not found");

            idea.CategoryId = input.CategoryId;
            idea.Category = category;
        }
        
        idea.Archived = input.Archived;
        idea.Content = input.Content;
        idea.RecentlyViewed = input.RecentlyViewed;
        idea.DateCreated = input.DateCreated;
        idea.DateModified = DateOnly.FromDateTime(DateTime.Today);
        
        db.SaveChanges();

        return Results.Ok(idea);
    }

    public static IResult AddIdea(Idea input, [FromServices] BluntContext db)
    {
        db.ChangeTracker.LazyLoadingEnabled = false;
        
        if (db.Categories.Find(input.CategoryId) is null) 
            return Results.NotFound("Category not found");
        
        Idea idea = new ()
        {
            Content = input.Content,
            DateCreated = DateOnly.FromDateTime(DateTime.Today),
            DateModified = DateOnly.FromDateTime(DateTime.Today),
            CategoryId = input.CategoryId
        };
        
        db.Ideas.Add(idea);

        db.SaveChanges();
        
        return Results.Created("/ideas/{id:int}", idea);
    }
}