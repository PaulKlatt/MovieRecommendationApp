using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password, string role);

        public ReturnUser GetReturnUser(int userId);

        public bool SaveToExcluded(MovieToExclude movie);

        public bool UpdateRemovalTrackers(int userid);

        public List<int> GetExcludedMoviesByUser(int userId);
    }
}
