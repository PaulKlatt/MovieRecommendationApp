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

                    SqlCommand cmd = new SqlCommand("SELECT accounts.accountId, accounts.userId, profileName " +
                        "FROM accounts " +
                        "JOIN genre_accounts ON account.account_id = genre.account_id " +
                        "JOIN genres ON genre.genre_id = genre_accounts.genre_id" +
                        "WHERE accounts.accountId = @accountId", conn);
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnAccount = GetAccountFromReader(reader);
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
                ProfileName = Convert.ToString(reader["profileName"]),
            };

            return accountFromReader;
        }
    }
}
