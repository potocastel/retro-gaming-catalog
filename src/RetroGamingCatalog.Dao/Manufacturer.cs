namespace RetroGamingCatalog.Dao
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Console> Consoles { get; set; }
    }
}
