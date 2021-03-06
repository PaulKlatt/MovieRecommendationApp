using System.Collections.Generic;

namespace Capstone.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
    }

    /// <summary>
    /// Model of user data to return upon successful login
    /// </summary>
    public class ReturnUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<string> FavoriteGenres { get; set; } = new List<string>();

        // list of favorite movies
        // list of movies I don't want to see again
    }

    /// <summary>
    /// Model to return upon successful login (user data + token)
    /// </summary>
    public class LoginResponse
    {
        public ReturnUser User { get; set; }
        public string Token { get; set; }
    }

    /// <summary>
    /// Model to accept login parameters
    /// </summary>
    public class LoginUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Model to accept registration parameters
    /// </summary>
    public class RegisterUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }

    //public class ChangePasswordUser
    //{
    //    public string CurrentPassword { get; set; }
    //    public string NewPassword { get; set; }
    //    public string ConfirmNewPassword { get; set; }
    //    public string PasswordHash { get; set; }
    //    public string Salt { get; set; }
    //}
}
