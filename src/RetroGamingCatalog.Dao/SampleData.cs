namespace RetroGamingCatalog.Dao
{
    public class SampleDataInitialization
    {
        private readonly CatalogDb _db;

        public SampleDataInitialization(CatalogDb db)
        {
            _db = db;
        }

        public async Task Initialize()
        {

            var nintendo = new Manufacturer
            {
                Id = Guid.NewGuid(),
                Name = "Nintendo",
            };

            var console = new Console
            {
                Id = Guid.NewGuid(),
                Name = "NES",
                Manufacturer = nintendo,
            };

            await _db.Manufacturers.AddAsync(nintendo);
            await _db.Consoles.AddAsync(console);

            await _db.Games.AddRangeAsync(new[]
            {
                new Game()
                {
                    Id = Guid.NewGuid(),
                    Name = "Double dragon 2",
                    Description = "Double dragon 2, the revenge",
                    Console = console
                },
                new Game()
                {
                    Id = Guid.NewGuid(),
                    Name = "Super mario bros",
                    Description = "The legendary NES game",
                    Console = console
                },
                new Game()
                {
                    Id = Guid.NewGuid(),
                    Name = "Teenage turtles ninja mutant",
                    Description = "Le jeu mythique des tortues ninja!",
                    Console = console
                }

            });
            await _db.SaveChangesAsync();
        }
    }
}
