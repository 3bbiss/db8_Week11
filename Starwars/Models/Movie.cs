namespace Starwars.Models
{
    public class Movie
    {
        public string title { get; set; }
        public int year { get; set; }
        public List<string> names { get; set; }
        public List<string> ships { get; set; }
    }
}
