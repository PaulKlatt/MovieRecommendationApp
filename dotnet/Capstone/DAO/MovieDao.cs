using Capstone.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class MovieDao : IMovieDao
    {
        private readonly RestClient client = new RestClient();

        public MovieDao()
        {
  
        }

        public GenreList GetGenres()
        {
            RestRequest request = new RestRequest("https://api.themoviedb.org/3/genre/movie/list?api_key=bb16218327fa750dbdfc80a7ff286caf&language=en-US");
            IRestResponse <GenreList> response = client.Get<GenreList>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }

        public MovieResults GetMoviesByGenre(string genres)
        {
            RestRequest request = new RestRequest("https://api.themoviedb.org/3/discover/movie?api_key=bb16218327fa750dbdfc80a7ff286caf&language=en-US&include_adult=false&include_video=false&with_genres=" + genres);
            IRestResponse<MovieResults> response = client.Get<MovieResults>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }

        public MovieResults GetRandomMoviePage(string genres, int pageNumber)
        {
            RestRequest request = new RestRequest("https://api.themoviedb.org/3/discover/movie?api_key=bb16218327fa750dbdfc80a7ff286caf&language=en-US&include_adult=false&include_video=false&with_genres=" + genres + "&page=" + pageNumber);
            IRestResponse<MovieResults> response = client.Get<MovieResults>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }
    }
}
