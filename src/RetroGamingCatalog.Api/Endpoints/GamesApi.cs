using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Api.DTOs;
using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api.Endpoints;

public static class GamesApi
{
    public static RouteGroupBuilder BuildGamesApi(this WebApplication webapp)
    {
        var rgb = webapp.MapGroup("/games");
        rgb.MapGet("/", async (CatalogDb db) => await 
            db.Games.Include(g => g.Console.Manufacturer).Select(g => GameDto.From(g)).ToListAsync());

        rgb.MapGet("/{name}", async
            (CatalogDb db, string name) =>
            Results.Ok(await db.Games.Include(g => g.Console.Manufacturer).Where(g => g.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).Select(g => GameDto.From(g)).ToListAsync()));

        rgb.MapPost("/", async (GameDto game, CatalogDb db) =>
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
        
        return rgb;
    }
}
