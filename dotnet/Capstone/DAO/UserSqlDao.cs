using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class UserSqlDao : IUserDao
    {
        private readonly string connectionString;

        public UserSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public User GetUser(string username)
        {
            User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnUser = GetUserFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUser;
        }

        public RegisterUser GetRegisterUser(string username)
        {
            RegisterUser returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnUser = GetRegisterUserFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUser;
        }

        public User AddUser(string username, string password, string role)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO users (username, password_hash, salt, user_role) VALUES (@username, @password_hash, @salt, @user_role)", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", role);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetUser(username);
        }

        public ReturnUser GetReturnUser(int userId)
        {
            ReturnUser returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT users.user_Id, users.username, users.user_role " +
                        "FROM users " +
                        "WHERE users.user_Id = @user_Id ", conn);
                    cmd.Parameters.AddWithValue("@user_Id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnUser = GetReturnUserFromReader(reader);
                    }
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd2 = new SqlCommand("SELECT genre_name " +
                                                     "FROM users " +
                                                     "JOIN genres_users ON users.user_id = genres_users.user_id " +
                                                     "JOIN genres ON genres.genre_id = genres_users.genre_id " +
                                                     "WHERE users.user_Id = @user_Id", conn);
                    cmd2.Parameters.AddWithValue("@user_Id", userId);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        returnUser.FavoriteGenres.Add(Convert.ToString(reader2["genre_name"]));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUser;
        }

<<<<<<< HEAD
        //public void UpdateUser (User user)
        //{
        //    try
        //    {
        //        using(SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand("Update users Set password_hash = @password_hash, username = @username, salt = @salt, user_role = @user_role where user_id = @user_id", conn);
        //            cmd.Parameters.AddWithValue("@password_hash", user.PasswordHash);
        //            cmd.Parameters.AddWithValue("@username", user.Username);
        //            cmd.Parameters.AddWithValue("@salt", user.Salt);
        //            cmd.Parameters.AddWithValue("@user_role", user.Role);

        //            cmd.ExecuteNonQuery();
        //        }

        //    }
        //    catch (SqlException)
        //    {
        //        throw;
        //    }
        //}

=======
        public bool SaveToExcluded(MovieToExclude movie)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO users_excludedMovies (movie_id, user_Id, opinion, removal_tracker) VALUES (@movie_id, @user_id, @opinion, @removal_tracker)", conn);
                    cmd.Parameters.AddWithValue("@movie_id", movie.MovieId);
                    cmd.Parameters.AddWithValue("@user_id", movie.UserId);
                    cmd.Parameters.AddWithValue("@opinion", movie.Opinion);
                    cmd.Parameters.AddWithValue("@removal_tracker", movie.RemovalTracker);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return true;
        }

        public bool UpdateRemovalTrackers(int userid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE users_excludedMovies SET removal_tracker = removal_tracker + 1 WHERE opinion = 'Passed' AND user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userid);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("DELETE FROM users_excludedMovies WHERE opinion = 'Passed' AND removal_tracker > 30", conn);
                    cmd2.Parameters.AddWithValue("@user_id", userid);
                    cmd2.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return true;
        }

        public List<int> GetExcludedMoviesByUser(int userId)
        {
            List<int> excludedMovieIds = new List<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT movie_id FROM dbo.users_excludedMovies WHERE user_id = @user_id", conn);
                cmd.Parameters.AddWithValue("@user_Id", userId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    excludedMovieIds.Add(Convert.ToInt32(reader["movie_id"]));
                }
            }
            return excludedMovieIds;
        }
>>>>>>> ea17b88ef4f948123b53fdfca9b607fd52910a61
        private User GetUserFromReader(SqlDataReader reader)
        {
            User u = new User()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
                PasswordHash = Convert.ToString(reader["password_hash"]),
                Salt = Convert.ToString(reader["salt"]),
                Role = Convert.ToString(reader["user_role"]),
            };

            return u;
        }

<<<<<<< HEAD
        private RegisterUser GetRegisterUserFromReader(SqlDataReader reader)
        {
            RegisterUser u = new RegisterUser()
            { 
                Username = Convert.ToString(reader["username"]),
                Password = Convert.ToString(reader["password"]),
                ConfirmPassword = Convert.ToString(reader["confirmPassword"]),
                Role = Convert.ToString(reader["user_role"]),
            };

            return u;
        }
=======
>>>>>>> ea17b88ef4f948123b53fdfca9b607fd52910a61

        private ReturnUser GetReturnUserFromReader(SqlDataReader reader)
        {
            ReturnUser userFromReader = new ReturnUser()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
                Role = Convert.ToString(reader["user_role"]),
            };

            return userFromReader;
        }

        public User UpdatePassword(RegisterUser user)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(user.Password);


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Update users Set password_hash = @password_hash, username = @username, salt = @salt, role = @user_role where user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", user.Role);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetUser(user.Username);
        }
    }
}
