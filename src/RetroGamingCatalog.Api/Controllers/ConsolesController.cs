using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Api.DTOs;
using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsolesController : ControllerBase
{
    private readonly CatalogDb _db;

    public ConsolesController(CatalogDb db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConsoleDto>>> GetConsoles()
    {
        return await _db.Consoles
            .Include(c => c.Manufacturer)
            .Select(m => ConsoleDto.From(m)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConsoleDto>> GetById(Guid id)
    {
        var console = await _db.Consoles
            .Include(c => c.Manufacturer)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (console == null)
            return NotFound();
        return ConsoleDto.From(console);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateConsole(ConsoleDto console)
    {
        if (await _db.Consoles.AnyAsync(m => m.Name.Contains(console.Name, StringComparison.InvariantCultureIgnoreCase)))
            return Conflict();

        var manufacturerDao = await _db.Manufacturers.FindAsync(console.ManufacturerId);
        if (manufacturerDao ==null)
            return BadRequest();

        var newConsole = new Dao.Console()
        {
            Id = Guid.NewGuid(),
            Name = console.Name,
            ManufacturerId = manufacturerDao.Id,
            Manufacturer = manufacturerDao
        };
        await _db.Consoles.AddAsync(newConsole);
        await _db.SaveChangesAsync();

        return newConsole.Id;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateConsole(Guid id, ConsoleDto console)
    {
        var consoleDao = await _db.Consoles.FindAsync(id);
        if (consoleDao == null)
            return NotFound();

        var manufacturerDao = await _db.Manufacturers.FindAsync(console.ManufacturerId);
        if (manufacturerDao is null)
            return BadRequest();
        if (manufacturerDao.Id != console.ManufacturerId)
        {
            consoleDao.ManufacturerId = manufacturerDao.Id;
            consoleDao.Manufacturer = manufacturerDao;
        }

        consoleDao.Name = console.Name;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteConsole(Guid id)
    {
        var consoleDao = await _db.Consoles.FindAsync(id);
        if (consoleDao == null)
            return NotFound();
        _db.Consoles.Remove(consoleDao);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}