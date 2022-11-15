using System.Text.Json.Serialization;
using Guacamole.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Guacamole.Minimal.Controller;
using Guacamole.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BluntContext>(options =>
{
    var settings = builder.Configuration;
    
    switch (settings.GetValue<string>("DataBaseProvider").ToLower())
    {
        case "sqlite":
            options.UseSqlite(settings.GetConnectionString("SQLite"));
            break;
        case "inmemory":
            options.UseInMemoryDatabase(settings.GetConnectionString("InMemory") ?? "db");
            break;
        default:
            options.UseInMemoryDatabase( "db");
            break;
    }
});

builder.Services.AddMvc().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

var app = builder.Build();

app.MapGet("/", BluntController.HelloWorld);

app.MapGet("/ideas/", async (int? limit, [FromServices] BluntContext db) =>
{
    limit ??= 10;
    return await BluntController.GetIdeas((int)limit, db);
});

app.MapGet("/categories/", BluntController.GetCategory);

app.MapPost("/categories/", BluntController.AddCategory);

app.MapGet("/categories/{category}/", async (string category, int? limit, [FromServices] BluntContext db) =>
{
    limit ??= 10;
    return await BluntController.GetByCategory((int)limit, category, db);
});

app.MapDelete("/ideas/{id:int}", async (int id, [FromServices] BluntContext db) =>
    await BluntController.DeleteIdea(id, db));
        

app.MapPut("/ideas/{id:int}", async (int id, Idea ideaInput, [FromServices] BluntContext db) => 
    await BluntController.UpdateIdea(id, ideaInput, db));

app.MapPost("/ideas/", async (Idea input, [FromServices] BluntContext db) => 
    await BluntController.AddIdea(input,db));

app.Run();