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

        //public RegisterUser GetRegisterUser(string username)
        //{
        //    RegisterUser returnUser = null;

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role FROM users WHERE username = @username", conn);
        //            cmd.Parameters.AddWithValue("@username", username);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            if (reader.Read())
        //            {
        //                returnUser = GetRegisterUserFromReader(reader);
        //            }
        //        }
        //    }
        //    catch (SqlException)
        //    {
        //        throw;
        //    }

        //    return returnUser;
        //}

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

        public void DeleteUser(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Delete From users_excludedMovies where dbo.users_excludedMovies.user_id = @userId " +
                                                    "Delete From dbo.users where user_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

        }


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

        public bool SaveMovieCard(MovieCard movie)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (@movie_id, @title, @poster_path, @genre_ids)", conn);
                    cmd.Parameters.AddWithValue("@movie_id", movie.MovieId);
                    cmd.Parameters.AddWithValue("@title", movie.Title);
                    cmd.Parameters.AddWithValue("@poster_path", movie.PosterPath);
                    cmd.Parameters.AddWithValue("@genre_ids", movie.GenreIds);
                    cmd.ExecuteNonQuery();
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
            try
            {
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
            catch (SqlException)
            {
                throw;
            }
            
        }

        public List<MovieCard> GetSavedMoviesByUserId(int userId)
        {
            List<MovieCard> savedMovieCards = new List<MovieCard>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT movieCards.movie_id, title, poster_path, genre_ids FROM movieCards " +
                                                    "JOIN users_excludedMovies ON movieCards.movie_id = users_excludedMovies.movie_id " +
                                                    "WHERE user_id = @user_id AND (opinion = 'Favorite' OR opinion = 'Banned')", conn);
                    cmd.Parameters.AddWithValue("@user_Id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        savedMovieCards.Add(GetMovieCardFromReader(reader));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return savedMovieCards;
        }

        public bool DeleteSavedMovie(int movieId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM users_excludedMovies " +
                                                     "WHERE movie_id = @movie_id; " +
                                                     "DELETE FROM movieCards " +
                                                     "WHERE movie_id = @movie_id", conn);
                    cmd.Parameters.AddWithValue("@movie_id", movieId);
                    int rowsAffected= cmd.ExecuteNonQuery();                                      
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return true;
        }

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

        private MovieCard GetMovieCardFromReader(SqlDataReader reader)
        {
            MovieCard movieCardFromReader = new MovieCard()
            {
                MovieId = Convert.ToInt32(reader["movie_id"]),
                Title = Convert.ToString(reader["title"]),
                PosterPath = Convert.ToString(reader["poster_path"]),
                GenreIds = Convert.ToString(reader["genre_ids"])
            };

            return movieCardFromReader;
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

                    SqlCommand cmd = new SqlCommand("Update users Set password_hash = @password_hash, username = @username, salt = @salt, user_role = @user_role WHERE username = @username", conn);
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
