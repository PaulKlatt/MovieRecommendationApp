using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password, string role);
        User UpdatePassword(RegisterUser user);
        public ReturnUser GetReturnUser(int userId);
<<<<<<< HEAD
        public RegisterUser GetRegisterUser(string username);
=======

        public bool SaveToExcluded(MovieToExclude movie);

        public bool UpdateRemovalTrackers(int userid);

        public List<int> GetExcludedMoviesByUser(int userId);
>>>>>>> ea17b88ef4f948123b53fdfca9b607fd52910a61
    }
}
