using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password, string role);

        void UpdateUser(User user);

        public ReturnUser GetReturnUser(int userId);
    }
}
