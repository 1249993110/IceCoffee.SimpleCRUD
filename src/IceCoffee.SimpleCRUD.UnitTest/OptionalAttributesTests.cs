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
        private GenericRepository<Foo> _repository;
        [SetUp]
        public void Setup()
        {
            InMemorySample.Init();
        }

        [Test]
        [Order(0)]
        public void InsertTest()
        {
            Assert.That(1, Is.EqualTo(1));
        }
    }
}
