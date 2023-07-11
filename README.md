# IceCoffee.SimpleCRUD

| Package | NuGet Stable | Downloads |
| ------- | ------------ | --------- |
| [IceCoffee.SimpleCRUD](https://www.nuget.org/packages/IceCoffee.SimpleCRUD/) | [![IceCoffee.SimpleCRUD](https://img.shields.io/nuget/v/IceCoffee.SimpleCRUD.svg)](https://www.nuget.org/packages/IceCoffee.SimpleCRUD/) | [![IceCoffee.SimpleCRUD](https://img.shields.io/nuget/dt/IceCoffee.SimpleCRUD.svg)](https://www.nuget.org/packages/IceCoffee.SimpleCRUD/) |
| [IceCoffee.SimpleCRUD.DependencyInjection](https://www.nuget.org/packages/IceCoffee.SimpleCRUD.DependencyInjection/) | [![IceCoffee.SimpleCRUD.DependencyInjection](https://img.shields.io/nuget/v/IceCoffee.SimpleCRUD.DependencyInjection.svg)](https://www.nuget.org/packages/IceCoffee.SimpleCRUD.DependencyInjection/) | [![IceCoffee.SimpleCRUD.DependencyInjection](https://img.shields.io/nuget/dt/IceCoffee.SimpleCRUD.DependencyInjection.svg)](https://www.nuget.org/packages/IceCoffee.SimpleCRUD.DependencyInjection/) |

## Description

Provides simple CRUD repository that reduces CRUD operations to a single line of code. It also greatly simplifies executing CRUD operations with filters, executing full queries, and executing stored procedures. It supports Async and non-Async in .NET Framework 4.7+ or .NET Standard2.0+ or .NET6+.

> DB Provider Supported: SQL Server, SQLite, PostgreSQL, MySQL

## Installation

```sh
$ dotnet add package IceCoffee.SimpleCRUD
$ dotnet add package IceCoffee.SimpleCRUD.DependencyInjection # (optional) If you want use DI
```

### Installing Database Providers
#### SQL Server
```sh
$ dotnet add package Microsoft.Data.SqlClient
```

#### SQLite
```sh
$ dotnet add package Microsoft.Data.SQLite
```

#### PostgreSQL
```sh
$ dotnet add package Npgsql
```

#### MySQL
```sh
$ dotnet add package MySql.Data
```

## Docs

### Metadata attributes

* **[Table]** By default the database table name will match the model name but it can be overridden with this.
* **[PrimaryKey]** Use for primary key.
* **[Column]** By default the column name will match the property name but it can be overridden with this.
* **[IgnoreInsert]** This column is not ignore insert.
* **[IgnoreSelect]** This column is not ignore select.
* **[IgnoreUpdate]** This column is not ignore updated.
* **[NotMapped]** For "logical" properties that do not have a corresponding column and have to be ignored by the SQL Generator.

### Quick Examples

#### 1. Introducing namespaces
``` csharp
using IceCoffee.SimpleCRUD;
using IceCoffee.SimpleCRUD.OptionalAttributes;
```

#### 2. Configure database connection options
``` csharp
string connectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
DbConnectionFactory.Default.ConfigureOptions(connectionString, DbType.SQLite);
```

#### 4. Define entity
``` csharp
[Table("Foo")]
public class Foo
{
    [PrimaryKey]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Sex { get; set; }
    [Column("[Address]")]
    public string Address { get; set; }
}
```

#### 5. CRUD
``` csharp
var repository = new GenericRepository<Foo>();

// Get by ID
var entity = repository.GetById(1);

// Get list by where clause and orderBy clause
var entities = repository.GetList("Name like @Name", "Name DESC", new { Name = "%xx%" });

// Get paged list by limit and offset
var page1 = repository.GetPagedList(1, 5);
var page2 = repository.GetPagedList(2, 5);

// Delete
int count = repository.Delete(new Foo(){ Id = 1 });
count = repository.DeleteById(2);
count = repository.DeleteByIds(new int[] { 1, 2 });

// Update
count = repository.Update(entity);

// Insert
count = repository.Insert(new Foo() { Id = 3, Name = "Name3", … … });

// Bulk insert with transaction
entities = new Foo[] { new Foo() { Id = 4, Name = "Name4" }, new Foo() { Id = 5, Name = "Name5" } };
count = repository.Insert(entities, true);

// Insert or ignore
count = repository.InsertOrIgnore(entities);

// Insert or replace
count = repository.InsertOrReplace(entities);

// Your code 
```

#### 5.1 Access multiple databases
``` csharp
string dbAliase1 = "dbStoreA";
string dbAliase2 = "dbStoreB";
DbConnectionFactory.Default.ConfigureOptions(dbAliase1, connectionString1, DbType.SQLite);
DbConnectionFactory.Default.ConfigureOptions(dbAliase2, connectionString2, DbType.SQLite);

var repository1 = new GenericRepository<Foo>(dbAliase1); 
var repository2 = new GenericRepository<Foo>(dbAliase2);
// Your code
```
#### 6. Unit of work
``` csharp
using (IUnitOfWork uow = UnitOfWorkFactory.Default.Create())
{
    var repository = uow.GetGenericRepository<Foo>();
    repository.Insert(new Foo() { Id = 1, Name = "Name1" });
    repository.Update(new Foo() { Id = 1, Name = "Name2" });
    uow.Commit();
}
```

#### 6.1 Access multiple databases
``` csharp
string dbAliase = "dbStoreA";
using (IUnitOfWork uow = UnitOfWorkFactory.Default.Create(dbAliase))
{
    // Your code
}
```

### Example with Asp.Net Core and D.I

#### Implements FooRepository

```csharp
public interface IFooRepository : IRepository<Foo>
{
    // Your code
}

public class FooRepository : RepositoryBase<Foo>, IFooRepository
{
    public FooRepository(IDbConnectionFactory dbConnectionFactory, ISqlGeneratorFactory sqlGeneratorFactory)
        : base(dbConnectionFactory, sqlGeneratorFactory)
    {
    }

    // Your code
}
```

#### Configure Services

```csharp
// Register repositories
services.AddRepositories((config) =>
{
    config.ConnectionString = "";
    config.DbType = DbType.SQLite;
}, assembly);
```

#### Use in API Controller
```csharp
[Route("[controller]")]
public class FooController : ControllerBase
{
    private readonly IFooRepository _fooRepository;
    public FooController(IFooRepository fooRepository)
    {
        _fooRepository = fooRepository;
    }

    [HttpGet("{id}")]
    public ActionResult<Foo> Get([FromRoute] int id)
    {
        return _fooRepository.GetById(id);
    }

    [HttpPost]
    public ActionResult<Foo> Get([FromBody] Foo model, [FromServices] IUnitOfWorkFactory unitOfWorkFactory)
    {
        using (IUnitOfWork uow = unitOfWorkFactory.Create(dbAliase))
        {
            var repository = uow.GetRepository<IFooRepository>();
            // Your code
            uow.Commit();
        }
    }
}
```

## To Be Continued
👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍👍

## License

All contents of this package are licensed under the [MIT license](https://github.com/1249993110/IceCoffee.SimpleCRUD/blob/main/LICENSE).
