using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public Guid ConsoleId { get; set; }
        public string? ConsoleName { get; set; }
        public Guid? ManufacturerId { get; set; }
        public string? ManufacturerName { get; set; }

        public static GameDto From(Game g) => new()
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            ConsoleId = g.ConsoleId,
            ConsoleName = g.Console.Name,
            ManufacturerId = g.Console.ManufacturerId,
            ManufacturerName = g.Console.Manufacturer.Name,
        };
    }
}
