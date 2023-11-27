namespace RetroGamingCatalog.Dao
{
    public class Console
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Guid ManufacturerId {get;set;}

        public ICollection<Game> Games { get; set; }
    }
}
