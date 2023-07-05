namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public class SqlLiteSqlGenerator : SqlGeneratorBase
    {
        public SqlLiteSqlGenerator(Type entityType) : base(entityType)
        {
        }

        public override string GetInsertOrIgnoreStatement(string? tableName = null)
        {
            string sql = string.Format("INSERT OR IGNORE INTO {0} {1}", tableName, InsertIntoClause);
            return sql;
        }

        public override string GetInsertOrReplaceStatement(string? tableName = null)
        {
            string sql = string.Format("REPLACE INTO {0} {1}", tableName, InsertIntoClause);
            return sql;
        }

        public override string GetSelectPagedStatement(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, string? tableName = null)
        {
            string sql = string.Format(
                "SELECT {0} FROM {1} {2} {3} LIMIT {4} OFFSET {5}",
                SelectColumns,
                tableName ?? TableName,
                whereClause == null ? string.Empty : "WHERE " + whereClause,
                orderByClause == null ? string.Empty : "ORDER BY " + orderByClause,
                (pageNumber - 1) * pageSize,
                pageSize);
            return sql;
        }

        public override string GetKeywordLikeClause(string keywordParamName = "Keyword")
        {
            return string.Format("LIKE '%'||@{0}||'%'", keywordParamName);
        }
    }
}