using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Api.DTOs;
using RetroGamingCatalog.Api.Queries;
using RetroGamingCatalog.Dao; 

namespace RetroGamingCatalog.Api.Endpoints;

public static class ConsolesApi
{
    public static RouteGroupBuilder BuildConsolesApi(this WebApplication webapp)
    {
        var rgb = webapp.MapGroup("/consoles");
        rgb.MapGet("/", async ([FromServices] GetAllConsolesQuery query) => await query.SelectAsync());
        rgb.MapPost("/", async (ConsoleDto console, [FromServices] CatalogDb db) =>
        {
            var manufacturer = await db.Manufacturers.FirstOrDefaultAsync(m => m.Id == console.ManufacturerId);
            if (manufacturer == null)
                return Results.NotFound("Manufacturer not found");

            var newConsole = new Dao.Console()
            {
                Id = Guid.NewGuid(),
                Name = console.Name,
                Manufacturer = manufacturer,
                ManufacturerId = manufacturer.Id,
            };

            await db.Consoles.AddAsync(newConsole);
            await db.SaveChangesAsync();

            return Results.Ok(newConsole.Id);
        });

        return rgb;
    }
}
