namespace Starwars.Models
{
    public class FilmResponse
    {
        public string title { get; set; }
        public int release_date { get; set; }
        public List<string> characters { get; set; }
        public List<string> starships { get; set; }
    }

    public class PeopleResponse
    {
        public string name { get; set; }
    }

    public class StarshipResponse
    {
        public string name { get; set; }
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

        public static async Task<Movie> FindMovie(int num)
        {
            HttpClient web = GetHttpClient();
            var connection = await web.GetAsync($"films/{num}/");
            FilmResponse resp = await connection.Content.ReadAsAsync<FilmResponse>();

            Movie movie = new Movie();
            movie.title = resp.title;
            movie.year = resp.release_date;

            // Do i actually need to create a new connection for the Characters and Starships?
            // Wondering because I can extract their characters and starships from the movie api and insert them into lists, right?
            //web = GetHttpClient();
            //connection = await web.GetAsync($"");


            foreach (string person in resp.characters)
            {
                PeopleResponse per = new PeopleResponse();
                per.name = person;
                movie.characters.Add(per.name);
            }

            foreach (string starship in resp.starships)
            {
                StarshipResponse ship = new StarshipResponse();
                ship.name = starship;
                movie.ships.Add(ship.name);
            }

            return movie;
        }

    }
}
