namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public interface ISqlGenerator
    {
        string TableName { get; }
        bool IsView { get; }

        // string StatementTerminator { get; }
        string[] PrimaryKeys { get; }
        string PrimaryKeyWhereClause { get; }

        string GetInsertStatement(string? tableName = null);
        string GetDeleteStatement(string whereClause, string? tableName = null);
        string GetSelectStatement(string? whereClause = null, string? orderByClause = null, string? tableName = null);
        string GetUpdateStatement(string setClause, string whereClause, string? tableName = null);
        string GetUpdateStatement(string? tableName = null);

        string GetInsertOrIgnoreStatement(string? tableName = null);
        string GetInsertOrReplaceStatement(string? tableName = null);
        string GetSelectPagedStatement(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, string? tableName = null);
    
        string GetKeywordLikeClause(string keywordParamName = "Keyword");
        string GetRecordCountStatement(string? whereClause, string? tableName = null);
        string GetSingleKey();
    }
}