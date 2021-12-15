using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IMovieDao
    {

        public GenreList GetGenres();

        public MovieResults GetTotalPagesByGenre(string genres);

        public MovieResults GetRandomMoviePage(string genres, int pageNumber);

        public MovieCard GetMovieCardByMovieId(int movieId);
    }
}
