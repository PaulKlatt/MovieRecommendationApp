using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    /*
    public class AccountSqlDao : IAccountDao
    {
        private readonly string connectionString;
        public AccountSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetAccount(int accountId)
        {
            Account returnAccount = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT users.user_Id, " +
                        "FROM accounts " +
                        "WHERE accounts.account_Id = @account_Id ", conn);
                    cmd.Parameters.AddWithValue("@account_Id", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnAccount = GetAccountFromReader(reader);
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
                        returnAccount.FavoriteGenres.Add(Convert.ToString(reader2["genre_name"]));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnAccount;
        }

        public Account AddAccount(int userId, List<string> favoriteGenres, string profileName)
        {
            int newAccountId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO accounts (accounts.userId, favoriteGenres, profileName) OUTPUT INSERTED.accountId VALUES (@accounts.userId, @favoriteGenres, @profileName)", conn);
                    cmd.Parameters.AddWithValue("@accounts.userId", userId);
                    cmd.Parameters.AddWithValue("@favoriteGenres", favoriteGenres);
                    cmd.Parameters.AddWithValue("@profileName", profileName);
                    newAccountId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetAccount(newAccountId);
        }

        private ReturnUser GetReturnUserFromReader(SqlDataReader reader)
        {
            ReturnUser userFromReader = new ReturnUser()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["user"]),
            };

            return userFromReader;
        }
    }
    */
}
