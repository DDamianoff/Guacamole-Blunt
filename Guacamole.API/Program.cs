using Guacamole.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Configuration.GetValue<string>("DbProvider");

builder.Services.AddDbContext<GuacamoleContext>(
    c =>
    {
        #warning use Strategy pattern instead.
        switch (provider.ToLower())
        {
            case "sqlite":
                c.UseSqlite(builder.Configuration.GetConnectionString("SQLITE"));
                break;
            case "memory":
                c.UseInMemoryDatabase("Guacamole");
                break;
        }
    });




var app = builder.Build();


app.MapGet("/",  ([FromServices] GuacamoleContext dbContext) => 
    dbContext.Database.EnsureCreated() 
        ? Results.Ok($"ta bien {dbContext.Database.ProviderName}") 
        : Results.Ok($"creada: {dbContext.Database.ProviderName}"));

app.MapGet("/memory",  ([FromServices] GuacamoleContext dbContext) => 
    dbContext.Database.IsInMemory());

app.Run();
