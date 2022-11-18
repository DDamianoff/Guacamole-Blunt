using Guacamole.Minimal.Contexts;
using Guacamole.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Guacamole.Minimal.Controller;

public static class BluntController
{
    private const int DefaultLimit = 10;

    private const int MaxLimit = 150;
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

        Category category = new() { Name = input.Name, Created = DateTime.Now };

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
        idea.Created = input.Created;
        idea.Modified = DateTime.Now;
        
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
            Created = DateTime.Now,
            Modified = DateTime.Now,
            CategoryId = input.CategoryId
        };
        
        await db.Ideas.AddAsync(idea);

        await db.SaveChangesAsync();
        
        return Results.Created("/ideas/{id:int}", idea);
    }
    
    public static async Task<IResult> DeleteCategoryByName(string category, [FromServices] BluntContext db)
    {
        var result = await db.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == category.ToLower());

        if (result is null) return Results.NotFound();

        db.Categories.Remove(result);

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    public static async Task<IResult> DeleteCategoryById(int id, [FromServices] BluntContext db)
    {
        var result = await db.Categories.FindAsync(id);

        if (result is null) return Results.NotFound();

        db.Categories.Remove(result);

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    public static async Task<IResult> DetailedDeleteCategoryByName(string category, [FromServices] BluntContext db)
    {
        var result = await db.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == category.ToLower());

        if (result is null) return Results.NotFound();

        var outInfo = new
        {
            CategoryName = result.Name, 
            AffectedIdeas = (from ideas in db.Ideas 
                where ideas.CategoryId == result.Id 
                select ideas.Id).ToArray(),
        };

        db.Categories.Remove(result);
        await db.SaveChangesAsync();

        return Results.Ok(outInfo);
    }

    public static async Task<IResult> DetailedDeleteCategoryById(int id, [FromServices] BluntContext db)
    {
        var result = await db.Categories.FindAsync(id);

        if (result is null) return Results.NotFound();

        var outInfo = new
        {
            CategoryName = result.Name, 
            AffectedIdeas = (from ideas in db.Ideas
                where ideas.CategoryId == result.Id 
                select ideas.Id).ToArray(),
        };

        db.Categories.Remove(result);

        await db.SaveChangesAsync();

        return Results.Ok(outInfo);
    }
    
    private static int EnsureValue(this int? v)
    {
        var outValue = v ?? DefaultLimit;

        if (outValue > MaxLimit)
            outValue = MaxLimit;

        return outValue;
    }
}