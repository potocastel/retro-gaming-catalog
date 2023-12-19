using Console = RetroGamingCatalog.Dao.Console;

namespace RetroGamingCatalog.Api.DTOs
{
    public class ConsoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ManufacturerId { get; set; }
        public string? ManufacturerName { get; set; }

        public static ConsoleDto From(Console c) => new()
        {
            Id=c.Id,
            Name = c.Name,
            ManufacturerId = c.ManufacturerId,
            ManufacturerName = c.Manufacturer.Name
        };
    }
}
