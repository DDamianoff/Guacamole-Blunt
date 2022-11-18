using System.Text.Json.Serialization;
using Guacamole.Minimal.Contexts;
using Microsoft.EntityFrameworkCore;
using Guacamole.Minimal.Controller;

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

app.MapGet("/ideas/", BluntController.GetIdeas);

app.MapGet("/ideas/{id:int}", BluntController.GetIdeaById);

app.MapPut("/ideas/{id:int}", BluntController.UpdateIdea);

app.MapPost("/ideas/", BluntController.AddIdea);

app.MapDelete("/ideas/{id:int}", BluntController.DeleteIdea);

app.MapGet("/categories/", BluntController.GetCategory);

app.MapPost("/categories/", BluntController.AddCategory);

app.MapGet("/categories/{category}/", BluntController.GetByCategory);

app.MapDelete("/categories/{category}/",  BluntController.DeleteCategoryByName);

app.MapDelete("/categories/{id:int}/",  BluntController.DeleteCategoryById);

app.MapDelete("/categories/{category}/detailed/",  BluntController.DetailedDeleteCategoryByName);

app.MapDelete("/categories/{id:int}/detailed/",  BluntController.DetailedDeleteCategoryById);

app.Run();