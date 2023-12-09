using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Api.DTOs;
using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturersController : ControllerBase
{
    private CatalogDb _db;

    public ManufacturersController(CatalogDb db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManufacturerDto>>> GetManufacturers()
    {
        return await _db.Manufacturers.Select(m => ManufacturerDto.From(m)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ManufacturerDto>> GetById(Guid id)
    {
        var manufacturer = await _db.Manufacturers
            .Where(m => m.Id == id)
            .Select(m => ManufacturerDto.From(m))
            .FirstOrDefaultAsync();
        if (manufacturer == null)
            return NotFound();
        return manufacturer;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> NewManufacturer(ManufacturerDto manufacturer)
    {
        if (await _db.Manufacturers.AnyAsync(m => m.Name.Contains(manufacturer.Name, StringComparison.InvariantCultureIgnoreCase)))
            return Conflict();
        var newManufacturer = new Manufacturer
        {
            Id = Guid.NewGuid(),
            Name = manufacturer.Name,
        };
        await _db.Manufacturers.AddAsync(newManufacturer);
        await _db.SaveChangesAsync();

        return newManufacturer.Id;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateManufacturer(ManufacturerDto manufacturer)
    {
        var manufacturerDao = await _db.Manufacturers.FirstOrDefaultAsync(m => m.Id == manufacturer.Id);
        if (manufacturerDao == null)
            return NotFound();
        manufacturerDao.Name = manufacturer.Name;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteManufacturer(ManufacturerDto manufacturer)
    {
        var manufacturerDao = await _db.Manufacturers.FirstOrDefaultAsync(m => m.Id == manufacturer.Id);
        if (manufacturerDao == null)
            return NotFound();
        _db.Manufacturers.Remove(manufacturerDao);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}