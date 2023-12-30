using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Api.DTOs;
using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly CatalogDb _db;
    public GamesController(CatalogDb db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
    {
        return await GamesQuery().Select(g => GameDto.From(g)).ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetGameById(Guid id)
    {
        return await GamesQuery().Where(g => g.Id == id).Select(g => GameDto.From(g)).FirstOrDefaultAsync();
    }

    [HttpGet("byname/{name}")]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGameByName(string name)
    {
        return await GamesQuery().Where(g => g.Name.ToLower().Contains(name.ToLower())).Select(g => GameDto.From(g)).ToListAsync();
    }

    private IQueryable<Game> GamesQuery() =>
    _db.Games.Include(g => g.Console.Manufacturer)
    .OrderBy(g => g.Console.Manufacturer.Name)
    .ThenBy(g => g.Console.Name)
    .ThenBy(g => g.Name);

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateGame(GameDto game)
    {

        var console = await _db.Consoles.FindAsync(game.ConsoleId);
        if (console == null)
            return BadRequest();

        if (await _db.Games.AnyAsync(g => g.Name.ToLower() == game.Name.ToLower()))
            return Conflict("Game already exist");

        var gameDao = new Game()
        {
            Id = Guid.NewGuid(),
            Console = console,
            Description = game.Description,
            Name = game.Name,
            ConsoleId = console.Id
        };
        await _db.AddAsync(gameDao);
        await _db.SaveChangesAsync();

        return gameDao.Id;
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateGame(Guid id, GameDto game)
    {
        var gameDao = await _db.Games.FindAsync(id);
        if (gameDao == null)
            return NotFound();

        if (gameDao.ConsoleId != game.ConsoleId)
        {
            var console = await _db.Consoles.FindAsync(game.ConsoleId);
            gameDao.Console = console;
            gameDao.ConsoleId = console.Id;
        }

        gameDao.Description = game.Description;
        gameDao.Name = game.Name;

        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGame(Guid id)
    {
        var game = await _db.Games.FindAsync(id);
        if (game == null)
            return NotFound();
        _db.Games.Remove(game);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}