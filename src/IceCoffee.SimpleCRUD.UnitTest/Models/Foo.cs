using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace IceCoffee.SimpleCRUD.UnitTest.Models
{
    public class Foo
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }
    }
}