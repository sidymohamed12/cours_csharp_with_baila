using System.Reflection;
using Dapper;
using gesdette.core.database;
using Npgsql;

namespace gesdette.core.database.implement
{
    public abstract class RepositoryBDImplDapper<T> : DataSourceImpl<T>, IRepositoryBD<T>
    {
        protected string? tableName;
        protected string? colomnSelectBy;
        protected Type? clazz;

        public int count()
        {
            throw new NotImplementedException();
        }

        public void insert(T value)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var columnNames = getColumnNames(value);
            var paramNames = getParamNames(value);

            string insertQuery = $"INSERT INTO \"{tableName}\" ({columnNames}) VALUES ({paramNames})";

            var parameters = new DynamicParameters();
            foreach (var prop in GetPropertyInfos(value))
            {
                parameters.Add("@" + prop.Name, prop.GetValue(value));
            }

            connection?.Execute(insertQuery, parameters);

        }

        public List<T> selectAll()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string sql = $"SELECT * FROM \"{tableName}\"";
            return connection.Query<T>(sql).ToList();
        }



        public T selectBy(string name)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string sql = $"SELECT * FROM \"{tableName}\" WHERE {colomnSelectBy} = '{name}'";
            return connection!.Query<T>(sql).First();
        }

        public T selectById(int id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string sql = $"SELECT * FROM \"{tableName}\" WHERE id = {id}";
            return connection!.Query<T>(sql).First();
        }

        public void update(T value)
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var setClause = GetSetClause(value);
            string updateQuery = $"UPDATE \"{tableName}\" SET {setClause} WHERE \"Id\" = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", typeof(T).GetProperty("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(value));

            foreach (var prop in GetPropertyInfos(value))
            {
                parameters.Add("@" + prop.Name, prop.GetValue(value));
            }

            connection?.Execute(updateQuery, parameters);
        }
    }
}