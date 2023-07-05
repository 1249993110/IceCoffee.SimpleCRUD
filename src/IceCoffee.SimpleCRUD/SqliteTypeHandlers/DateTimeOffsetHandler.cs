namespace IceCoffee.SimpleCRUD.SqliteTypeHandlers
{
    public class DateTimeOffsetHandler : SqliteTypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value)
            => DateTimeOffset.Parse((string)value);
    }
}
