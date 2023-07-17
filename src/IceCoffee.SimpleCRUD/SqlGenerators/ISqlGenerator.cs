namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public interface ISqlGenerator
    {
        string TableName { get; }
        bool IsView { get; }
        string SelectColumnClause { get; }
        string InsertIntoClause { get; }
        string UpdateSetClause { get; }

        string[] GetPrimaryKeys();
        string GetPrimaryKeyWhereClause();

        string GetInsertStatement(string? tableName = null);
        string GetDeleteStatement(string whereClause, string? tableName = null);
        string GetSelectStatement(string? whereClause = null, string? orderByClause = null, string? tableName = null);
        string GetUpdateStatement(string? tableName = null);

        string GetInsertOrIgnoreStatement(string? tableName = null);
        string GetInsertOrReplaceStatement(string? tableName = null);
        string GetSelectPagedStatement(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, string? tableName = null);

        string GetKeywordLikeClause(string keywordParamName = "Keyword");
        string GetRecordCountStatement(string? whereClause = null, string? tableName = null);
        string GetSingleKey();
    }
}