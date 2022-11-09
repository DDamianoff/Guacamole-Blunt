using Guacamole.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BluntContext>(options =>
{
    options.UseInMemoryDatabase("BluntDb");
});

var app = builder.Build();

app.MapGet("/", ([FromServices] BluntContext db) =>
{
    var result = db.Database.EnsureCreated();

    return "Hello world!" + "\n\n" + (result
        ? "Db recently created"
        : "Working with an exiting db");
});

app.Run();