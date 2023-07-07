namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public class SqlServerSqlGenerator : SqlGeneratorBase
    {
        public SqlServerSqlGenerator(Type entityType) : base(entityType)
        {
        }

        public override string GetInsertOrIgnoreStatement(string? tableName = null)
        {
            string sql = string.Format("IF NOT EXISTS(SELECT 1 FROM {0} WHERE {1}) BEGIN INSERT INTO {0} {2} END",
                tableName ?? TableName, PrimaryKeyWhereClause, InsertIntoClause);
            return sql;
        }

        public override string GetInsertOrReplaceStatement(string? tableName = null)
        {
            string sql = string.Format("UPDATE {0} SET {1} WHERE {2} IF @@ROWCOUNT=0 BEGIN INSERT INTO {0} {3} END",
                tableName ?? TableName, UpdateSetClause, PrimaryKeyWhereClause, InsertIntoClause);
            return sql;
        }

        public override string GetSelectPagedStatement(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, string? tableName = null)
        {
            string sql = string.Format(
                "SELECT {0} FROM {1} {2} ORDER BY {3} OFFSET {4} ROWS FETCH NEXT {5} ROWS ONLY",
                SelectColumns,
                tableName ?? TableName,
                whereClause == null ? string.Empty : "WHERE " + whereClause,
                orderByClause ?? (IsView ? "1" : string.Join(",", PrimaryKeys)),
                (pageNumber - 1) * pageSize,
                pageSize);
            return sql;
        }
    }
}