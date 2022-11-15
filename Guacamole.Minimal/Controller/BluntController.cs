using Guacamole.Models;
using Guacamole.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Guacamole.Minimal.Controller;

public static class BluntController
{
    private const int DefaultLimit = 10;
    public static async Task<IResult> HelloWorld ([FromServices] BluntContext db)
    {
        var result = await db.Database.EnsureCreatedAsync();

        return Results.Ok("Hello world! " + (result
            ? "Db recently created"
            : "Working with an exiting db"));
    }
    
    
    public static async Task<IResult> GetIdeas (int? limit, [FromServices]  BluntContext db) => 
        Results.Ok(await db.Ideas
                .Take(limit.EnsureValue())
                .ToListAsync());

    public static async Task<IResult> GetIdeaById (int id, [FromServices] BluntContext db)
    {
        var idea = await db.Ideas.FindAsync(id);

        return idea is null 
            ? Results.NotFound() 
            : Results.Ok(idea);
    }
    
    public static async Task<IResult> GetByCategory(int? limit, string category, [FromServices] BluntContext db)
    {
        var result = await db.Categories
            // ReSharper disable once SpecifyStringComparison
            .FirstOrDefaultAsync(c => category.ToLower() == c.Name.ToLower());
        
        if (result is null) 
            return Results.NotFound();

        return Results.Ok(await db.Ideas
                .Where(i => i.CategoryId == result.Id)
                .Take(limit.EnsureValue())
                .ToListAsync());
    }
    
    public static async Task<IResult> AddCategory(Category input, [FromServices] BluntContext db)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (input.Name == null) return Results.UnprocessableEntity("Name must be provided");

        Category category = new() { Name = input.Name, DateCreated = DateOnly.FromDateTime(DateTime.Now) };

        await db.AddAsync(category);
        await db.SaveChangesAsync();

        category = await db.Categories.FirstAsync(c => c.Name == input.Name);

        return Results.Created("/categories/{category}/", category);
    }
    
    public static async Task<IResult> GetCategory(int? limit, [FromServices] BluntContext db)
    {
        return Results.Ok(await db.Categories
            .Take(limit.EnsureValue())
            .ToListAsync());
    }

    public static async Task<IResult> DeleteIdea (int id, [FromServices] BluntContext db)
    {
        var result = await db.Ideas.FindAsync(id);
        
        if (result is null)
            return Results.NotFound();

        db.Ideas.Remove(result);
        await db.SaveChangesAsync();
        
        return Results.Ok(result);
    }
    public static async Task<IResult> UpdateIdea (int id, Idea input, [FromServices] BluntContext db)
    {
        var idea = await db.Ideas.FindAsync(id);

        if (idea is null)
            return Results.NotFound();

        
        if (idea.CategoryId != input.CategoryId)
        {
            var category = await db.Categories.FindAsync(input.CategoryId);

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
        
        await db.SaveChangesAsync();

        return Results.Ok(idea);
    }

    public static async Task<IResult> AddIdea(Idea input, [FromServices] BluntContext db)
    {
        db.ChangeTracker.LazyLoadingEnabled = false;
        
        if (await db.Categories.FindAsync(input.CategoryId) is null) 
            return Results.NotFound("Category not found");
        
        Idea idea = new ()
        {
            Content = input.Content,
            DateCreated = DateOnly.FromDateTime(DateTime.Today),
            DateModified = DateOnly.FromDateTime(DateTime.Today),
            CategoryId = input.CategoryId
        };
        
        await db.Ideas.AddAsync(idea);

        await db.SaveChangesAsync();
        
        return Results.Created("/ideas/{id:int}", idea);
    }
    
    private static int EnsureValue(this int? v) => v ?? DefaultLimit;
}