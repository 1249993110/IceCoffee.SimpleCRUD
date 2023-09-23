namespace IceCoffee.SimpleCRUD.OptionalAttributes
{
    /// <summary>
    /// Identify table is a view.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class IsViewAttribute : Attribute
    {
    }
}