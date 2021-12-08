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
    }


}

