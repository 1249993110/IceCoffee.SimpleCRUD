namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public class SqliteSqlGenerator : SqlGeneratorBase
    {
        public SqliteSqlGenerator(Type entityType) : base(entityType)
        {
        }

        public override string GetInsertOrIgnoreStatement(string? tableName = null)
        {
            string sql = string.Format("INSERT OR IGNORE INTO {0} {1}", tableName ?? TableName, InsertIntoClause);
            return sql;
        }

        public override string GetInsertOrReplaceStatement(string? tableName = null)
        {
            string sql = string.Format("REPLACE INTO {0} {1}", tableName ?? TableName, InsertIntoClause);
            return sql;
        }

        public override string GetSelectPagedStatement(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, string? tableName = null)
        {
            string sql = string.Format(
                ";SELECT {0} FROM {1} {2} {3} LIMIT {4} OFFSET {5}",
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
            return string.Format("LIKE '%'||@{0}||'%'", keywordParamName);
        }

        public override string GetSelectAutoIncrement()
        {
            return ";SELECT last_insert_rowid()";
        }
    }
}