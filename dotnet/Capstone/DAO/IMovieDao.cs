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

        public MovieResults GetMoviesByGenre(string genres);

        public MovieResults GetRandomMoviePage(string genres, int pageNumber);
    }
}
