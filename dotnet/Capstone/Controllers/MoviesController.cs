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
            GenreList genreList = new GenreList();

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
                    int randomPage;
<<<<<<< HEAD
                    if (firstPageMovieList.TotalPages < 500)
=======
                    if(firstPageMovieList.TotalPages < 500)
>>>>>>> d8e9d885e6a11265317b865b266589f5f3ca3b37
                    {
                        randomPage = rnd.Next(1, firstPageMovieList.TotalPages + 1);
                    }
                    else
                    {
                        randomPage = rnd.Next(1, 501);
                    }
                    
<<<<<<< HEAD
=======
                    
>>>>>>> d8e9d885e6a11265317b865b266589f5f3ca3b37

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

        [HttpGet("users/{userId}")]
        public ActionResult<List<MovieCard>> GetMoviesByUserId(int userId)
        {
            List<MovieCard> movieCardList = userDao.GetSavedMoviesByUserId(userId);           

            if (movieCardList.Count == 0)
            {
                return NotFound("No movies saved.");
            }
            else
            {
                return Ok(movieCardList);
            }
        }

        [HttpPost("users/{userId}")]
        public IActionResult SaveFavoriteMovie(MovieInfo movieInfo, int userId)
        {
            IActionResult result;

            ReturnUser existingUser = userDao.GetReturnUser(movieInfo.MovieToExclude.UserId);
            if (existingUser == null)
            {
                return Conflict(new { message = "User not found.  Please log in and try again." });
            }

            bool isCreated = userDao.SaveMovieCard(movieInfo.MovieCard);

            if (isCreated)
            {
                bool isExcluded = userDao.SaveToExcluded(movieInfo.MovieToExclude);
                result = Created($"/users/{userId}/", null); //values aren't read on client
            }

            else
            {
                result = BadRequest(new { message = $"An error occurred and the movie was not added to the Favorite list." });
            }


            return result;
        }

        [HttpPost("{movieId}/users/{userId}/ban")]
        public IActionResult BanMovie(int movieId, int userId)
        {
            IActionResult result;

            MovieCard movieToBan = movieDao.GetMovieCardByMovieId(movieId);

            if (movieToBan == null)
            {
                return Conflict(new { message = "Movie not found.  Please recheck the movie id and try again." });
            }

            MovieToExclude movieToExclude = new MovieToExclude()
            {
                MovieId = movieToBan.MovieId,

                UserId = userId,

                Opinion = "Banned",

                RemovalTracker = 0
            };
            //probably want all this in a transaction
            bool isDeleted = userDao.DeleteSavedMovie(movieId);
            bool isCreated = userDao.SaveMovieCard(movieToBan);

            if (isCreated)
            {
                bool isExcluded = userDao.SaveToExcluded(movieToExclude);
                result = Created($"movies/{movieId}/users/{userId}/ban", null); //values aren't read on client
            }

            else
            {
                result = BadRequest(new { message = $"An error occurred and the movie was not added to the Banned list." });
            }


            return result;
        }
    }
}
