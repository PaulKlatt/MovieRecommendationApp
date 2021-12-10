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
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMovieDao movieDao;

        public MoviesController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IMovieDao _movieDao)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            movieDao = _movieDao;
        }

        [HttpGet("genres/")]
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

        [HttpGet("genres/{genreIds}")]
        public ActionResult<MovieResults> GetMoviesByGenre(string genreIds)
        {
            MovieResults movieList;

            movieList = movieDao.GetMoviesByGenre(genreIds);

            if (movieList == null)
            {
                return NotFound("Movies not found");
            }
            else
            {
                return Ok(movieList);
            }
        }
    }
}
