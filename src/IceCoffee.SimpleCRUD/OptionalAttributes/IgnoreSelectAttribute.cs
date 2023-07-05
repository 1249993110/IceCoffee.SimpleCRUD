namespace IceCoffee.SimpleCRUD.OptionalAttributes
{
    /// <summary>
    /// Identify ignore query field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IgnoreSelectAttribute : Attribute
    {
    }
}