using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Api.DTOs;
using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api.Queries;

public class GetAllConsolesQuery : IQuery
{
    private readonly CatalogDb _db;
    public GetAllConsolesQuery(CatalogDb db)
    {
        _db = db;
    }
    public async Task<List<ConsoleDto>> SelectAsync() =>
        await _db.Consoles.Select(c => ConsoleDto.From(c)).ToListAsync();
}
