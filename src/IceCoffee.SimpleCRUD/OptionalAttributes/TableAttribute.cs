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
        /// <param name="isView"></param>
        public TableAttribute(string name, bool isView = false)
        {
            Name = name;
            IsView = isView;
        }

        /// <summary>
        /// Table or view name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Is it a view.
        /// </summary>

        public bool IsView { get; private set; }
    }
}