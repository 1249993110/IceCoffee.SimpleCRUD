namespace IceCoffee.SimpleCRUD.OptionalAttributes
{
    /// <summary>
    /// Identify ignore insertion.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IgnoreInsertAttribute : Attribute
    {
    }
}