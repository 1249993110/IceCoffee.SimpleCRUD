using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace IceCoffee.SimpleCRUD.UnitTest.Models
{
    [Table("Foo")]
    public class Foo1
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Column("Name")]
        public string Name_ { get; set; }

        [IgnoreInsert, IgnoreUpdate, IgnoreSelect]
        public int? Age { get; set; }

        [NotMapped]
        public string? Sex { get; set; }
    }
}