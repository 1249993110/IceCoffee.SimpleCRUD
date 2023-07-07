using IceCoffee.SimpleCRUD.SqlGenerators;
using IceCoffee.SimpleCRUD.UnitTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCoffee.SimpleCRUD.UnitTest
{
    [TestFixture]
    public class OptionalAttributesTests
    {
        [Test]
        [Order(0)]
        public void InsertTest()
        {
            var sqlGenerator = SqlGeneratorFactory.Default.GetSqlGenerator(DbType.SQLite, typeof(Foo1));

            Assert.That(sqlGenerator.TableName, Is.EqualTo("Foo"));
            Assert.That(sqlGenerator.IsView, Is.EqualTo(false));
            Assert.That(sqlGenerator.PrimaryKeys, Does.Contain("Id"));
            Assert.That(sqlGenerator.PrimaryKeys, Does.Contain("Name"));
            Assert.That(sqlGenerator.PrimaryKeyWhereClause, Is.EqualTo("Id=@Id AND Name=@Name_"));
        }
    }
}
