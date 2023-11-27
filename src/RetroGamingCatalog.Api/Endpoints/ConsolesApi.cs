using Microsoft.AspNetCore.Mvc;
using RetroGamingCatalog.Api.Queries;

namespace RetroGamingCatalog.Api.Endpoints;

public static class ConsolesApi
{
    public static RouteGroupBuilder BuildConsolesApi(this WebApplication webapp)
    {
        var rgb = webapp.MapGroup("/consoles");
        rgb.MapGet("/", async ([FromServices] GetAllConsolesQuery query) => await query.SelectAsync());

        return rgb;
    }
}
