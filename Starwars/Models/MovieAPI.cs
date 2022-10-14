namespace Starwars.Models
{

    public class FilmResponse
    {
        public string title { get; set; }
        public string release_date { get; set; }
        public List<string> characters { get; set; }
        public List<string> starships { get; set; }
    }

    public class CharacterResponse
    {
        public string name { get; set; }
    }

    public class StarshipResponse
    {
        public string name { get; set; }
    }

    public class MovieAPI
    {
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


        public async static Task<Movie> FindMovie(int filmnum)
        {
            Movie mymovie = new Movie();

            HttpClient web = GetHttpClient();
            var connection = await web.GetAsync($"films/{filmnum}");
            FilmResponse fresp = await connection.Content.ReadAsAsync<FilmResponse>();

            mymovie.title = fresp.title;
            mymovie.year = int.Parse(fresp.release_date.Substring(0, 4));
            mymovie.characters = new List<string>();
            mymovie.starships = new List<string>();

            int count = 0;

            foreach (string url in fresp.characters)
            {
                connection = await web.GetAsync(url);
                CharacterResponse ch = await connection.Content.ReadAsAsync<CharacterResponse>();
                mymovie.characters.Add(ch.name);
                count++;

                if (count == 3) { break; }
            }

            count = 0;

            foreach (string url in fresp.starships)
            {
                connection = await web.GetAsync(url);
                StarshipResponse sh = await connection.Content.ReadAsAsync<StarshipResponse>();
                mymovie.starships.Add(sh.name);
                count++;

                if (count == 3) { break; }
            }

            return mymovie;
        }


    }



}
