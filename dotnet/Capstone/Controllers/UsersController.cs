﻿using System;
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
        public ActionResult<Account> GetAccountById(int userId)
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

        [HttpPost("{userId}/exclude")]
        public IActionResult SaveToExcluded(MovieToExclude movie)
        {
            IActionResult result;

            ReturnUser existingUser = userDao.GetReturnUser(movie.UserId);
            if (existingUser == null)
            {
                return Conflict(new { message = "User not found.  Please log in and try again." });
            }
            if (movie.Opinion == "Passed")
            {
                movie.RemovalTracker = 0;
            }
            bool isCreated = userDao.SaveToExcluded(movie);
            
            if (isCreated)
            {
                // Might need to be in a transaction inside SaveToExcluded, but should work for now
                bool isUpdated = userDao.UpdateRemovalTrackers(movie.UserId);
                result = Created($"{movie.UserId}/exclude", null); //values aren't read on client
            }

            else
            {
                result = BadRequest(new { message = $"An error occurred and the movie was not added to the {movie.Opinion} list." });
            }
           

            return result;
        }
    }


}

