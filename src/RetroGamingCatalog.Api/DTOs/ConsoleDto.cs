using Console = RetroGamingCatalog.Dao.Console;

namespace RetroGamingCatalog.Api.DTOs
{
    public class ConsoleDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public static ConsoleDto From(Console c) => new()
        {
            Id=c.Id,
            Name = c.Name
        };
    }
}
