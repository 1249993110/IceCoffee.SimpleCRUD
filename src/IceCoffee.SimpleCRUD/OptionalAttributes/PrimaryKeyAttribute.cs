namespace IceCoffee.SimpleCRUD.OptionalAttributes
{
    /// <summary>
    /// Identify primary key.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrimaryKeyAttribute : Attribute
    {
    }
}