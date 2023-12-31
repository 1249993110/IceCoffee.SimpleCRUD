﻿using IceCoffee.SimpleCRUD.SqlGenerators;
using IceCoffee.SimpleCRUD.UnitTest.Models;

namespace IceCoffee.SimpleCRUD.UnitTest
{
    [TestFixture]
    public class OptionalAttributesTests
    {
        [Test]
        [Order(0)]
        public void InsertTest()
        {
            var sqlGenerator = SqlGeneratorFactory.GetSqlGenerator(DbType.SQLite, typeof(Foo1));

            Assert.That(sqlGenerator.TableName, Is.EqualTo("Foo"));
            Assert.That(sqlGenerator.IsView, Is.EqualTo(false));
            Assert.That(sqlGenerator.GetPrimaryKeys(), Does.Contain("Id"));
            Assert.That(sqlGenerator.GetPrimaryKeys(), Does.Contain("Name"));
            Assert.That(sqlGenerator.GetPrimaryKeyWhereClause(), Is.EqualTo("Id=@Id AND Name=@Name_"));
        }
    }
}