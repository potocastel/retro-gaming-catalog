using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api;

public class ManufacturerDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public static ManufacturerDto From(Manufacturer m) => new()
    {
        Id = m.Id,
        Name = m.Name
    };
}
