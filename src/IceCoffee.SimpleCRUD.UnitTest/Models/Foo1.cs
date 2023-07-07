using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace IceCoffee.SimpleCRUD.UnitTest.Models
{
    [Table("Foo")]
    public class Foo1
    {
        [PrimaryKey]
        public int Id { get; set; }

        [PrimaryKey, Column("Name")]
        public string Name_ { get; set; }

        public int? Age { get; set; }

        [IgnoreInsert, IgnoreUpdate, IgnoreSelect]
        public string? Address { get; set; }

        [NotMapped]
        public string? Sex { get; set; }
    }
}