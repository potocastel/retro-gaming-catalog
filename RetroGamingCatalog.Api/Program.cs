using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Api;
using RetroGamingCatalog.Dao;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddDbContext<CatalogDb>();//opt => opt.UseInMemoryDatabase("catalog", x => { }));
builder.Services.AddTransient<SampleDataInitialization>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
    await scope.ServiceProvider.GetRequiredService<SampleDataInitialization>().Initialize();


var gamesApi = app.MapGroup("/games");
gamesApi.MapGet("/",async (CatalogDb db) => await db.Games.Include(g => g.Console.Manufacturer).Select(g=> GameDto.From(g)).ToListAsync());

gamesApi.MapGet("/{name}", async
    (CatalogDb db, string name) =>
    Results.Ok(await db.Games.Include(g=>g.Console.Manufacturer).Where(g => g.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).Select(g => GameDto.From(g)).ToListAsync()));
         

gamesApi.MapPost("/", async (GameDto game, CatalogDb db) =>
{
    var console = await db.Consoles.FirstOrDefaultAsync(g => g.Name == game.ConsoleName) ?? (await db.Consoles.FirstOrDefaultAsync() ?? throw new NullReferenceException());
    var gameDao = new Game()
    {
        Id = Guid.NewGuid(),
        Console = console,
        Description = game.Description,
        Name = game.Name,
        ConsoleId = console.Id
    };
    await db.AddAsync(gameDao);
    await db.SaveChangesAsync();

    return Results.Ok(gameDao.Id);
});

var manufacturerApi = app.MapGroup("/manufacturers");
manufacturerApi.MapGet("/", (CatalogDb db) => db.Manufacturers.ToList());

var consoleApi = app.MapGroup("/consoles");
consoleApi.MapGet("/", (CatalogDb db) => db.Consoles.ToList());


app.Run();

//public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Game[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
