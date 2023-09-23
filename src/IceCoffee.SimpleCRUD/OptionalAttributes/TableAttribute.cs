namespace IceCoffee.SimpleCRUD.OptionalAttributes
{
    /// <summary>
    /// Identify table or view name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public TableAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Table or view name.
        /// </summary>
        public string Name { get; private set; }
    }
}