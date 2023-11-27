using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api;

public static class ManufacturersApi
{
   public static RouteGroupBuilder BuildManufacturers(this WebApplication webapp)
    {
        var rgb = webapp.MapGroup("/manufacturers");
        rgb.MapGet("/", async (CatalogDb db) =>await db.Manufacturers.Select(m=>ManufacturerDto.From(m)).ToListAsync());

        return rgb;
    }
}
