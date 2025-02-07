namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public class DaMengSqlGenerator : SqlGeneratorBase
    {
        public DaMengSqlGenerator(Type entityType) : base(entityType)
        {
        }

        public override string GetInsertOrIgnoreStatement(string? tableName = null)
        {
            string sql = string.Format("BEGIN IF NOT EXISTS (SELECT 1 FROM {0} WHERE {1}) THEN INSERT INTO {0} {2}; END IF; END",
                tableName ?? TableName, GetPrimaryKeyWhereClause(), InsertIntoClause);
            return sql;
        }

        public override string GetInsertOrReplaceStatement(string? tableName = null)
        {
            string sql = string.Format(@"
BEGIN
    UPDATE {0} SET {3} WHERE {1};
    IF NOT EXISTS (SELECT 1 FROM {0} WHERE {1}) THEN
        INSERT INTO {0} {2};
    END IF;
END", tableName ?? TableName, GetPrimaryKeyWhereClause(), InsertIntoClause, UpdateSetClause);
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

        public override string GetSelectAutoIncrement()
        {
            return ";SELECT LAST_INSERT_ID()";
        }
    }
}