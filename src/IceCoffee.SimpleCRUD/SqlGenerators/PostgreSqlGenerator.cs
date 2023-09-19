namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public class PostgreSqlGenerator : SqlGeneratorBase
    {
        public PostgreSqlGenerator(Type entityType) : base(entityType)
        {
        }

        public override string GetInsertOrIgnoreStatement(string? tableName = null)
        {
            string sql = string.Format("INSERT INTO {0} {1} ON CONFLICT WHERE {2} DO NOTHING", tableName ?? TableName, InsertIntoClause, GetPrimaryKeyWhereClause());
            return sql;
        }

        public override string GetInsertOrReplaceStatement(string? tableName = null)
        {
            string sql = string.Format("INSERT INTO {0} {1} ON CONFLICT WHERE {2} DO UPDATE {0} SET {3}",
                tableName ?? TableName, InsertIntoClause, GetPrimaryKeyWhereClause(), UpdateSetClause);
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
    }
}