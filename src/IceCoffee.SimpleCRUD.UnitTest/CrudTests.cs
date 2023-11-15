using IceCoffee.SimpleCRUD.UnitTest.Models;
using System.Data.Common;
using System.Reflection;

namespace IceCoffee.SimpleCRUD.UnitTest
{
    [TestFixture]
    public class CrudTests
    {
        private GenericRepository<Foo> _repository;

        [SetUp]
        public void Setup()
        {
            InMemorySample.Init();

            string sql = @"CREATE TABLE IF NOT EXISTS Foo(
                                    Id INTEGER NOT NULL,
	                                Name TEXT NOT NULL,
	                                Age INTEGER,
	                                PRIMARY KEY(Id)
                                );";

            _repository = new GenericRepository<Foo>();
            _repository.GetType().GetMethod("Execute", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(_repository, new object?[] { sql, null, false });
        }

        [Test]
        [Order(0)]
        public void CrudTest()
        {
            _repository.Delete(_repository.GetAll());

            var entity = new Foo() { Id = 1, Name = "Name1" };
            int count = _repository.Insert(entity);
            Assert.That(count, Is.EqualTo(1));

            var entities = new Foo[] { new Foo() { Id = 2, Name = "Name2" }, new Foo() { Id = 3, Name = "Name3" } };
            count = _repository.Insert(entities);
            Assert.That(count, Is.EqualTo(entities.Length));

            Assert.Catch<DbException>(() =>
            {
                _repository.Insert(entity);
            }, "The expected primary key conflict exception is not thrown.");

            Assert.Catch<DbException>(() =>
            {
                var entities = new Foo[] { new Foo() { Id = 4, Name = "Name4" }, entity };
                _repository.Insert(entities);
            }, "The expected primary key conflict exception is not thrown.");

            Assert.Catch<DbException>(() =>
            {
                var entities = new Foo[] { new Foo() { Id = 5, Name = "Name5" }, entity };
                _repository.Insert(entities, true);
            }, "The expected primary key conflict exception is not thrown.");

            count = _repository.GetRecordCount();
            Assert.That(count, Is.EqualTo(4));

            entity = _repository.GetById(4);
            Assert.That(entity, Is.Not.Null);

            entity = _repository.GetById(5);
            Assert.That(entity, Is.Null);

            var pagedResult = _repository.GetPagedList(2, 2);
            Assert.That(pagedResult.Total, Is.EqualTo(4));
            Assert.That(pagedResult.Items.Count(), Is.EqualTo(2));

            count = _repository.Delete(new Foo() { Id = 4 });
            Assert.That(count, Is.EqualTo(1));
            count = _repository.DeleteById(4);
            Assert.That(count, Is.EqualTo(0));

            count = _repository.DeleteByIds(new int[] { 3, 2 });
            Assert.That(count, Is.EqualTo(2));

            _repository.InsertOrIgnore(new Foo() { Id = 2, Name = "Name2" });
            _repository.InsertOrIgnore(new Foo[] { new Foo() { Id = 2, Name = "Name2" }, new Foo() { Id = 3, Name = "Name3" } });

            Assert.That(_repository.GetRecordCount(), Is.EqualTo(3));

            _repository.Update(new Foo() { Id = 1, Name = "Name1", Age = 18 });
            Assert.That(_repository.GetById(1)?.Age, Is.EqualTo(18));

            _repository.InsertOrReplace(new Foo() { Id = 2, Name = "Name2", Age = 20 });
            Assert.That(_repository.GetById(2)?.Age, Is.EqualTo(20));

            _repository.InsertOrReplace(new Foo[] { new Foo() { Id = 3, Name = "Name3", Age = 30 }, new Foo() { Id = 4, Name = "Name4", Age = 40 } });
            Assert.That(_repository.GetById(3)?.Age, Is.EqualTo(30));
            Assert.That(_repository.GetById(4)?.Age, Is.EqualTo(40));
        }

        [Test]
        [Order(1)]
        public void UnitOfWorkTest()
        {
            _repository.Delete(_repository.GetAll());

            using (IUnitOfWork uow = UnitOfWorkFactory.Default.Create(string.Empty))
            {
                var repository = uow.GetGenericRepository<Foo1>();
                int count = repository.Insert(new Foo1() { Id = 1, Name_ = "Name1" });
                Assert.That(count, Is.EqualTo(1));
                uow.Commit();
            }

            try
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Default.Create(string.Empty))
                {
                    var repository = uow.GetGenericRepository<Foo>();
                    var entity = new Foo() { Id = 2, Name = "Name2" };
                    int count = repository.Insert(entity);
                    Assert.That(count, Is.EqualTo(1));
                    repository.Insert(entity);
                    uow.Commit();
                }
            }
            catch (Exception)
            {
                var entity = new GenericRepository<Foo>().GetById(2);
                Assert.That(entity, Is.Null);
                Assert.Pass("The expected primary key conflict exception is not thrown.");
            }
        }
    }
}