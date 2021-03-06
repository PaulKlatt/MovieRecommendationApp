using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;


namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDao userDao;

        public UsersController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IUserDao _userDao)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            userDao = _userDao;
        }

        [HttpGet("{userId}")]
        public ActionResult<ReturnUser> GetAccountById(int userId)
        {
            ReturnUser returnUser = null;

            returnUser = userDao.GetReturnUser(userId);

            if (returnUser == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(returnUser);
            }
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteAccountById(int userId)
        {
            userDao.DeleteUser(userId);

            ReturnUser returnUser = null;
            returnUser = userDao.GetReturnUser(userId);

            if (returnUser == null)
            {
                return NoContent();
            }
            else
            {
                return NotFound("Account does not exist");
            }
        }


        [HttpPost("{userId}/exclude")]
        public IActionResult SaveToExcluded(MovieInfo movieInfo)
        {
            IActionResult result;

            ReturnUser existingUser = userDao.GetReturnUser(movieInfo.MovieToExclude.UserId);
            if (existingUser == null)
            {
                return Conflict(new { message = "User not found.  Please log in and try again." });
            }
            
            bool isCreated = userDao.SaveToExcluded(movieInfo.MovieToExclude);

            if (isCreated)
            {
                if (movieInfo.MovieToExclude.Opinion == "Favorite")
                {
                    bool isFavorited = userDao.SaveMovieCard(movieInfo.MovieCard);
                }
                else
                {
                    bool isUpdated = userDao.UpdateRemovalTrackers(movieInfo.MovieToExclude.UserId);
                }
                // Might need to be in a transaction inside SaveToExcluded, but should work for now
                
                result = Created($"{movieInfo.MovieToExclude.UserId}/exclude", null); //values aren't read on client
            }

            else
            {
                result = BadRequest(new { message = $"An error occurred and the movie was not added to the {movieInfo.MovieToExclude.Opinion} list." });
            }


            return result;
        }
    

        [HttpPost("{userId}")]

        public IActionResult UpdateUserPassword(LoginUser userParam)
        {
            IActionResult result = Unauthorized(new { message = "Password is incorrect" });

            User user = userDao.GetUser(userParam.Username);

            if (user != null && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
            {
                result = Ok();
            }
            return result;
        }

        [HttpPut("update")]

        public ActionResult<RegisterUser> UpdateUserPassword(RegisterUser user)
        {
            User newUser = userDao.GetUser(user.Username);
            // Update password - takes in user object (user.password)
            // Put user into update password

            if (newUser == null)
            {
                return Conflict(new { message = "User not found, please log in again." });
            }

            userDao.UpdatePassword(user);
            return Ok();

        }
    }
}
