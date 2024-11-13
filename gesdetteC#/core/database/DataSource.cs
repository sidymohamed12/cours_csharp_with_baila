using System.Reflection;

namespace gesdette.core.database
{
    public interface IDataSource<T>
    {
        void connexion();

        void closeConnection();

        string generateSql(string action, string tableName, string[] columns, string coloneCondition, object value);

        PropertyInfo[] GetPropertyInfos(T value);

        string getColumnNames(T value);

        string getParamNames(T value);

        string GetSetClause(T value);

    }
}