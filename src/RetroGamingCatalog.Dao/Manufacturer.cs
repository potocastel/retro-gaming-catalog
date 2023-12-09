namespace RetroGamingCatalog.Dao
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public bool StillActive { get; set; }
        public ICollection<Console> Consoles { get; set; }
    }
}
