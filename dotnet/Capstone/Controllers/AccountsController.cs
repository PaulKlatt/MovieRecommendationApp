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

    public class AccountsController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IAccountDao accountDao;

        public AccountsController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IAccountDao _accountDao)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            accountDao = _accountDao;
        }

        [HttpGet("{accountId}")]
        public ActionResult<Account> GetAccountById(int accountId)
        {
            Account returnAccount = null;

            returnAccount = accountDao.GetAccount(accountId);

            if (returnAccount == null)
            {
                return NotFound("Account not found");
            }
            else
            {
                return Ok(returnAccount);
            }
        }
    }


}

