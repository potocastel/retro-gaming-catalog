namespace RetroGamingCatalog.Dao
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Console Console { get; set; }
        public Guid ConsoleId{get;set;}
    }
}
