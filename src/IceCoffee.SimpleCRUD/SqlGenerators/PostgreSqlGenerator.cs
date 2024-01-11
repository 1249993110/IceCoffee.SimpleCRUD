using System.Linq;

namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public class PostgreSqlGenerator : SqlGeneratorBase
    {
        public PostgreSqlGenerator(Type entityType) : base(entityType)
        {
        }

        public override string GetInsertOrIgnoreStatement(string? tableName = null)
        {
#if NETCOREAPP
            string primaryKeys = string.Join(',', GetPrimaryKeys());
#else
            string primaryKeys = string.Join(",", GetPrimaryKeys());
#endif
            string sql = string.Format("INSERT INTO {0} {1} ON CONFLICT ({2}) DO NOTHING", tableName ?? TableName, InsertIntoClause, primaryKeys);
            return sql;
        }

        public override string GetInsertOrReplaceStatement(string? tableName = null)
        {
#if NETCOREAPP
            string primaryKeys = string.Join(',', GetPrimaryKeys());
#else
            string primaryKeys = string.Join(",", GetPrimaryKeys());
#endif
            string sql = string.Format("INSERT INTO {0} {1} ON CONFLICT ({2}) DO UPDATE SET {3}",
                tableName ?? TableName, InsertIntoClause, primaryKeys, UpdateSetClause);
            return sql;
        }

        public override string GetSelectPagedStatement(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, string? tableName = null)
        {
            string sql = string.Format(
                "SELECT {0} FROM {1} {2} {3} LIMIT {4} OFFSET {5}",
                SelectColumnClause,
                tableName ?? TableName,
                whereClause == null ? string.Empty : "WHERE " + whereClause,
                orderByClause == null ? string.Empty : "ORDER BY " + orderByClause,
                pageSize,
                (pageNumber - 1) * pageSize);
            return sql;
        }

        public override string GetKeywordLikeClause(string keywordParamName = "Keyword")
        {
            return "I" + base.GetKeywordLikeClause(keywordParamName);
        }

        public override string GetInIdsClause(string propName = "Ids")
        {
            return $"=ANY(@{propName})";
        }
    }
}