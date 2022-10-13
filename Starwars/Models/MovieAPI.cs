namespace Starwars.Models
{
    public class FilmResponse
    {
        public string title { get; set; }
        public int release_date { get; set; }
        public List<string> people { get; set; }
        public List<string> starships { get; set; }
    }

    public class PeopleResponse
    {
        public string name { get; set; }
    }

    public class StarshipResponse
    {
        public string name { get; set; }
        public string model { get; set; }
    }


    public class MovieAPI
    {
        // https://swapi.dev/api/

        public static HttpClient _web = null;

        public static HttpClient GetHttpClient()
        {

            if (_web == null)
            {
                _web = new HttpClient();
                _web.BaseAddress = new Uri("https://swapi.dev/api/");
            }

            return _web;
        }
    }
}
