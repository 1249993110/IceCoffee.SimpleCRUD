using Dapper;
using IceCoffee.SimpleCRUD.OptionalAttributes;
using System.Reflection;
using System.Text;

namespace IceCoffee.SimpleCRUD.SqlGenerators
{
    public abstract class SqlGeneratorBase : ISqlGenerator
    {
        public virtual string TableName { get; private set; }
        public virtual bool IsView { get; private set; }
        public virtual string SelectColumnClause { get; private set; }
        public virtual string InsertIntoClause { get; private set; }
        public virtual string UpdateSetClause { get; private set; }

        private readonly string[]? _primaryKeys;
        private readonly string? _primaryKeyWhereClause;

        public SqlGeneratorBase(Type entityType)
        {
            var tableAttribute = entityType.GetCustomAttribute<TableAttribute>(true);
            TableName = tableAttribute?.Name ?? entityType.Name;
            IsView = entityType.GetCustomAttribute<IsViewAttribute>(true) != null;

            var properties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.CanWrite && p.GetCustomAttribute<NotMappedAttribute>(true) == null);

            var stringBuilder1 = new StringBuilder();
            var stringBuilder2 = new StringBuilder();
            var stringBuilder3 = new StringBuilder();
            var stringBuilder4 = new StringBuilder();
            foreach (var prop in properties)
            {
                string propertyName = prop.Name;
                string columnName = prop.GetCustomAttribute<ColumnAttribute>(true)?.Name ?? propertyName;

                if (prop.GetCustomAttribute<IgnoreInsertAttribute>(true) == null)
                {
                    stringBuilder2.AppendFormat("{0},", columnName);
                    stringBuilder3.AppendFormat("@{0},", propertyName);
                }
                if (prop.GetCustomAttribute<IgnoreSelectAttribute>(true) == null)
                {
                    stringBuilder1.AppendFormat("{0},", columnName);
                }

                if (prop.GetCustomAttribute<IgnoreUpdateAttribute>(true) == null)
                {
                    stringBuilder4.AppendFormat("{0}=@{1},", columnName, propertyName);
                }
            }

            SelectColumnClause = stringBuilder1.Remove(stringBuilder1.Length - 1, 1).ToString();
            if (IsView == false)
            {
                InsertIntoClause = string.Format("({0}) VALUES({1})",
                stringBuilder2.Remove(stringBuilder2.Length - 1, 1).ToString(),
                stringBuilder3.Remove(stringBuilder3.Length - 1, 1).ToString());
                UpdateSetClause = stringBuilder4.Remove(stringBuilder4.Length - 1, 1).ToString();

                var keyPropInfos = properties.Where(p => p.GetCustomAttribute<PrimaryKeyAttribute>(true) != null);
                if (keyPropInfos.Any())
                {
                    var stringBuilder5 = new StringBuilder();
                    var keyNames = new List<string>();
                    foreach (var item in keyPropInfos)
                    {
                        string keyName = item.GetCustomAttribute<ColumnAttribute>(true)?.Name ?? item.Name;

                        keyNames.Add(keyName);
                        stringBuilder5.AppendFormat("{0}=@{1} AND ", keyName, item.Name);
                    }

                    stringBuilder5.Remove(stringBuilder5.Length - 5, 5);

                    _primaryKeys = keyNames.ToArray();
                    _primaryKeyWhereClause = stringBuilder5.ToString();
                }
            }

            var propertyMap = new CustomPropertyTypeMap(entityType,
                (type, columnName) =>
                {
                    var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Where(p => p.CanWrite && p.GetCustomAttribute<NotMappedAttribute>(true) == null);
                    // 过滤定义了Column特性的属性
                    var result = properties.FirstOrDefault(prop => prop
                            .GetCustomAttributes(false)
                            .OfType<ColumnAttribute>()
                            .Any(attr => attr.Name == columnName));
                    if (result != null)
                    {
                        return result;
                    }
                    // Column特性为空则返回默认对应列名的属性
                    return properties.First(prop => prop.Name == columnName);
                }
            );

            SqlMapper.SetTypeMap(entityType, propertyMap);
        }

        public virtual string[] GetPrimaryKeys()
        {
            return _primaryKeys ?? throw new Exception("No primary key is identified.");
        }
        public virtual string GetPrimaryKeyWhereClause()
        {
            return _primaryKeyWhereClause ?? throw new Exception("No primary key is identified.");
        }

        public virtual string GetInsertStatement(string? tableName = null)
        {
            string sql = string.Format("INSERT INTO {0} {1}", tableName ?? TableName, InsertIntoClause);
            return sql;
        }
        public virtual string GetDeleteStatement(string whereClause, string? tableName = null)
        {
            string sql = string.Format("DELETE FROM {0} WHERE {1}", tableName ?? TableName, whereClause);
            return sql;
        }
        public virtual string GetSelectStatement(string? whereClause = null, string? orderByClause = null, string? tableName = null)
        {
            string sql = string.Format("SELECT {0} FROM {1} {2} {3}", 
                SelectColumnClause, 
                tableName ?? TableName,
                whereClause == null ? string.Empty : "WHERE " + whereClause,
                orderByClause == null ? string.Empty : "ORDER BY " + orderByClause);
            return sql;
        }

        public string GetUpdateStatement(string? tableName = null)
        {
            string sql = string.Format("UPDATE {0} SET {1} WHERE {2}", tableName ?? TableName, UpdateSetClause, GetPrimaryKeyWhereClause());
            return sql;
        }

        public abstract string GetInsertOrIgnoreStatement(string? tableName = null);
        public abstract string GetInsertOrReplaceStatement(string? tableName = null);
        public abstract string GetSelectPagedStatement(int pageNumber, int pageSize, string? whereClause = null, string? orderByClause = null, string? tableName = null);

        public virtual string GetKeywordLikeClause(string keywordParamName = "Keyword")
        {
            string sql = string.Format("LIKE CONCAT('%',@{0},'%')", keywordParamName);
            return sql;
        }

        public virtual string GetRecordCountStatement(string? whereClause = null, string? tableName = null)
        {
            string sql = string.Format("SELECT COUNT(*) FROM {0} {1}", tableName ?? TableName, whereClause == null ? string.Empty : "WHERE " + whereClause);
            return sql;
        }
        public virtual string GetSingleKey()
        {
            var primaryKeysp = GetPrimaryKeys();
            if (primaryKeysp.Length > 1)
            {
                throw new InvalidOperationException("Multiple primary keys are defined, only supports an entity with a single [PrimaryKey] attribute.");
            }

            return primaryKeysp[0];
        }

        public virtual string GetInIdsClause(string propName = "Ids")
        {
            return " IN @" + propName;
        }
    }
}