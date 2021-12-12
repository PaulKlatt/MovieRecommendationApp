using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password, string role);
        User UpdatePassword(RegisterUser user);
        public ReturnUser GetReturnUser(int userId);
        public RegisterUser GetRegisterUser(string username);
    }
}
