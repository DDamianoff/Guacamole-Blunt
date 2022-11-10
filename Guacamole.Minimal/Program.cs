using System.Text.Json.Serialization;
using Guacamole.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Guacamole.Minimal.Controller;
using Guacamole.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BluntContext>(options =>
{
    options.UseSqlite("Data Source=./db/Blunt.db");
});

builder.Services.AddMvc().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

var app = builder.Build();

app.MapGet("/", BluntController.HelloWorld);

app.MapGet("/ideas/", (int? limit, [FromServices] BluntContext db) =>
{
    limit ??= 10;
    return BluntController.GetIdeas((int)limit, db);
});

app.MapGet("/categories/{category}/", (string category, int? limit, [FromServices] BluntContext db) =>
{
    limit ??= 10;
    return BluntController.GetByCategory((int)limit, category, db);
});

app.MapDelete("/ideas/{id:int}", (int id, [FromServices] BluntContext db) => 
    BluntController.DeleteIdea(id, db) 
        ? Results.Ok(new {Id = id})
        : Results.NotFound());

app.MapPut("/ideas/{id:int}", (int id, Idea ideaInput, [FromServices] BluntContext db) => 
    BluntController.UpdateIdea(id, ideaInput, db));

app.MapPost("/ideas/", (Idea input, [FromServices] BluntContext db) => 
    BluntController.AddIdea(input,db));

app.Run();