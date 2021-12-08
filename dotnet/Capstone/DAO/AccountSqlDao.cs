using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
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

                    SqlCommand cmd = new SqlCommand("SELECT accounts.account_Id, accounts.user_Id, profile_Name " +
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
                                                     "FROM accounts " +
                                                     "JOIN genres_accounts ON accounts.account_id = genres_accounts.account_id " +
                                                     "JOIN genres ON genres.genre_id = genres_accounts.genre_id " +
                                                     "WHERE accounts.account_Id = @account_Id", conn);
                    cmd2.Parameters.AddWithValue("@account_Id", accountId);
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

        private Account GetAccountFromReader(SqlDataReader reader)
        {
            Account accountFromReader = new Account()
            {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                //FavoriteGenres = Convert.To(reader["favoriteGenres"]),
                ProfileName = Convert.ToString(reader["profile_Name"]),
            };

            return accountFromReader;
        }
    }
}
