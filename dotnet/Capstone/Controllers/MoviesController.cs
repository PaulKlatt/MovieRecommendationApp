using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMovieDao movieDao;
        private readonly IUserDao userDao;

        public MoviesController(IUserDao _userDao, IMovieDao _movieDao)
        {
            movieDao = _movieDao;
            userDao = _userDao;
        }

        [HttpGet("genrelist/")]
        public ActionResult<GenreList> GetGenres()
        {
            GenreList genreList;

            genreList = movieDao.GetGenres();

            if (genreList == null)
            {
                return NotFound("Genres not found");
            }
            else
            {
                return Ok(genreList);
            }
        }

        [HttpGet("genres/{genreIds}/users/{userId}")]
        public ActionResult<int> GetTotalPagesByGenre(string genreIds, int userId)
        {
            Movie returnMovie;
            //call the excluded list
            List<int> excludedMovieIds = userDao.GetExcludedMoviesByUser(userId);
            do
            {
                MovieResults firstPageMovieList;

                firstPageMovieList = movieDao.GetTotalPagesByGenre(genreIds);

                if (firstPageMovieList == null)
                {
                    return NotFound("Movies not found");
                }
                else
                {
                    Random rnd = new Random();
                    int randomPage = rnd.Next(1, firstPageMovieList.TotalPages + 1);

                    MovieResults randomPageMovieList = movieDao.GetRandomMoviePage(genreIds, randomPage);

                    if (randomPageMovieList == null)
                    {
                        return NotFound("Movies not found");
                    }
                    else
                    {
                        returnMovie = randomPageMovieList.Results[rnd.Next(0, randomPageMovieList.Results.Count - 1)];
                        
                    }

                }
            }
            while (excludedMovieIds.Contains(returnMovie.Id));

            return Ok(returnMovie);

        }
        [HttpGet("genres/")]
        public ActionResult<int> GetPageNumberOfAllMovies()
        {
            MovieResults movieList;

            movieList = movieDao.GetTotalPagesByGenre("");

            if (movieList == null)
            {
                return NotFound("Movies not found");
            }
            else
            {
                return Ok(movieList);
            }
        }

        [HttpGet("genres/{genreIds}/page/{pageNumber}")]
        public ActionResult<MovieResults> GetRandomMovie(string genreIds, int pageNumber)
        {
            MovieResults movieList;

            movieList = movieDao.GetRandomMoviePage(genreIds, pageNumber);

            if (movieList == null)
            {
                return NotFound("Movies not found");
            }
            else
            {

                Random rnd = new Random();
                Movie returnMovie = movieList.Results[rnd.Next(0, 20)];
                return Ok(returnMovie);
            }
        }
    }
}
