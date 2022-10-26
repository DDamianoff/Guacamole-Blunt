using Guacamole.API;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/artists", () =>
{
    using var db = new GuacamoleContext();
    return db.Artists.ToList();
});

app.MapGet("/albums", () =>
{
    using var db = new GuacamoleContext();
    return (from album in db.Albums
                orderby album.Artist
                select album.Title
            ).ToList().Take(5);
});

app.Run();
