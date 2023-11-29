using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Api.DTOs;
using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api.Endpoints;

public static class ManufacturersApi
{
   public static RouteGroupBuilder BuildManufacturers(this WebApplication webapp)
    {
        var rgb = webapp.MapGroup("/manufacturers");
        rgb.MapGet("/", async (CatalogDb db) =>await db.Manufacturers.Select(m=>ManufacturerDto.From(m)).ToListAsync());
        rgb.MapPost("/", async (ManufacturerDto manufacturer, [FromServices] CatalogDb db) =>
        {
            var newManufacturer = new Manufacturer()
            {
                Id=Guid.NewGuid(),
                Name = manufacturer.Name,
            };

            await db.Manufacturers.AddAsync(newManufacturer);
            await db.SaveChangesAsync();

            return Results.Ok(newManufacturer.Id);
        });

        return rgb;
    }
}
