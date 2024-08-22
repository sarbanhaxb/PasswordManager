using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows;

namespace PasswordManager
{
    public static class SQLquery
    {
        public static void CreateDatabase()
        {
            using (var connection = new SQLiteConnection($"Data Source=database.db"))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.Connection = connection;

                command.CommandText = "CREATE TABLE IF NOT EXISTS users " +
                    "(id INTEGER PRIMARY KEY, " +
                    "login VARCHAR(20) NOT NULL UNIQUE," +
                    "password VARCHAR(20) NOT NULL)";

                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE IF NOT EXISTS passwordObjects " +
                    "(id INTEGER PRIMARY KEY, " +
                    "userID INTEGER NOT NULL, " +
                    "title VARCHAR(50), "+
                    "login VARCHAR(20)," +
                    "password VARCHAR(20), "+
                    "FOREIGN KEY (userID) REFERENCES users(id) ON DELETE CASCADE)";

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteObj(string title, int userID)
        {
            using (var connection = new SQLiteConnection($"Data Source=database.db"))
            {
                connection.Open();
                var command = new SQLiteCommand($"DELETE FROM passwordObjects WHERE userID = {userID} AND title = '{title}'", connection);
                command.ExecuteNonQuery();
            }
        }

        public static List<Item> GetItems(int userID)
        {
            List<Item> list = new List<Item>();
            string sqlExpression = $"SELECT * FROM passwordObjects WHERE userID='{userID}'";

            using (var connection = new SQLiteConnection($"Data Source=database.db"))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Item item = new Item();
                        item.Id = Convert.ToInt32(reader["id"]);
                        item.UserID = Convert.ToInt32(reader["userID"]);
                        item.Title = Convert.ToString(reader["title"]);
                        item.Login = Convert.ToString(reader["login"]);
                        item.Password = Convert.ToString(reader["password"]);
                        list.Add(item);
                    }
                }
            }
            return list;

        }

        public static int GetUserID(string username)
        {
            string sqlExpression = $"SELECT id FROM users WHERE login='{username}'";

            using (var connection = new SQLiteConnection($"Data Source=database.db"))
            {
                connection.Open ();
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                int id = Convert.ToInt32(command.ExecuteScalar());
                return id;
            }
        }

        public static void InsertNewToObj(int userID, string title, string login, string password)
        {
            string sqlExpression = $"INSERT INTO passwordObjects (title, userID, login, password) VALUES ('{title}', '{userID}', '{login}', '{password}')";
            using (var connection = new SQLiteConnection($"Data Source=database.db"))
            {
                connection.Open();
                try
                {
                    SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ошибка при выполнении операции.", $"{ex.Message}", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public static bool CheckLogin(string username)
        {
            string sqlExpression = $"SELECT EXISTS(SELECT * FROM users WHERE login ='{username}')";

            using (var connection = new SQLiteConnection("Data Source=database.db"))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                return Convert.ToBoolean(command.ExecuteScalar());
            }
        }

        public static bool CheckPassword(string username, string password)
        {
            string sqlExpression = $"SELECT password FROM users WHERE login ='{username}'";

            using (var conn = new SQLiteConnection("Data Source=database.db"))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(sqlExpression, conn);
                if ((string)command.ExecuteScalar() == password)
                {
                    return true;
                }
                return false;
            }
        }
        public static void RegisterUser(string username, string password)
        {
            string sqlExpression = $"INSERT INTO users (login, password) VALUES ('{username}', '{password}')";

            using (var connection = new SQLiteConnection($"Data Source=database.db"))
            {
                connection.Open();
                try
                {
                    SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ошибка при выполнении операции.", $"{ex.Message}", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }
    }
}
