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

        void DeleteUser(int userId);
        //public RegisterUser GetRegisterUser(string username);


        public bool SaveToExcluded(MovieToExclude movie);

        public bool UpdateRemovalTrackers(int userid);

        public List<int> GetExcludedMoviesByUser(int userId);

    }
}
