
using System.Text;
using gesdette.core.database;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection;
using Npgsql;

namespace gesdette.core.database.implement
{
    public class DataSourceImpl<T> : IDataSource<T>
    {
        protected string connectionString = "Host=localhost;Database=gestion_dette_cours_CS;Username=postgres;Password=SMS;Port=5432";
        protected NpgsqlConnection? connection;
        protected string[]? colones;


        public void connexion()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connexion réussie !");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erreur de connexion : {ex.Message}");
            }
        }

        public void closeConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public string generateSql(string action, string tableName, string[] columns, string coloneCondition, object value)
        {
            throw new NotImplementedException();
        }

        public PropertyInfo[] GetPropertyInfos(T value)
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(p => colones!.Contains(p.Name) && p.CanRead && p.GetValue(value) != null)
                    .ToArray();
        }

        public string getColumnNames(T value)
        {
            return string.Join(", ", GetPropertyInfos(value).Select(p => $"\"{p.Name}\""));
        }

        public string getParamNames(T value)
        {
            return string.Join(", ", GetPropertyInfos(value).Select(p => "@" + p.Name));
        }

        public string GetSetClause(T value)
        {
            return string.Join(", ", GetPropertyInfos(value).Select(p =>
                    p.Name == "User" ? $"\"UserId\" = @{p.Name}.Id" : $"\"{p.Name}\" = @{p.Name}"
                ));
        }
    }
}