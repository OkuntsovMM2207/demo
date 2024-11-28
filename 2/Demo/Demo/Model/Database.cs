using System;
using System.Data;
using System.Data.SqlClient;

namespace Demo.Model
{
    public class Database
    {
        private readonly string _connectionString = "Server=(local);Database=demo;Trusted_Connection=True";

        // Проверка логина и пароля через таблицу inputDataUsers
        public bool CheckLoginFromUsers(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM inputDataUsers WHERE login = @Login AND password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Login", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка проверки логина: {ex.Message}");
                return false;
            }
        }

    }
}
