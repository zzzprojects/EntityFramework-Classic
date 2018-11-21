# Soft Delete

## Description
The **Soft Delete** feature allows you to flag record as deleted instead of deleting them (Hard Delete).

```csharp
public class SoftDeleteEntitiesContext : DbContext
{
    public SoftDeleteEntitiesContext() : base("EntitiesContext")
    {
          this.Configuration.SoftDelete.Trigger<SoftDeleteEntity>((context, delete) =>{delete.IsDeleted = false;});
     }
     public DbSet<SoftDeleteEntity> SoftDeleteEntities { get; set; }
}

```
[Try it](https://dotnetfiddle.net/aDsTWW)

The soft delete feature can be acheived by using the IEFSoftDelete interface. This interface is always added to the manager by defaut. Otherwise you can add your own trigger by specifing the action and the type on which to execute a soft delete.

The IEFSoftDelete interface will handle any entities that has a column named IsDeleted with the boolean type.
Any entity that inherit this intefaced will be soft delete instead of completly delete when saving changes from a context.

### Advantage

- Centralize logic in single inteface to handle the soft delete
- Reduce the chance of missing a soft delete when introducing a new entity
- Improve development productivity

## Getting Started

### Default Interface
You can use the default soft delete by inheriting the IEFSoftDelete interface.

```csharp
        public class SoftDeleteEntity : IEFSoftDelete
        {
            public int SoftDeleteEntityID { get; set; }
            public int ColumnInt { get; set; }
            public string ColumnString { get; set; }
            public bool IsDeleted { get; set; }
            public bool AutoDelete { get; set; }
        }```
[Try it](https://dotnetfiddle.net/7cKY2x)

### Custom Action
You can create an custom soft delete trigger by adding it to the manager

```csharp
        public class SoftDeleteEntitiesContext : DbContext
        {
            public SoftDeleteEntitiesContext() : base("EntitiesContext")
            {
               this.Configuration.SoftDelete.Trigger<SoftDeleteEntity>((context, delete) =>{delete.IsDeleted = false;});
            }

            public DbSet<SoftDeleteEntity> SoftDeleteEntities { get; set; }
        }```
[Try it](https://dotnetfiddle.net/qjRFbZ)

### Enable/Disable Query Filter
You can enable/disable your **Query Filter** with the `Enable()`, `Disable()`, `EnableFilter(id)`, and `DisableFilder(id)` methods.

```csharp
using (var context = new EntityContext())
{
	var filter = context.Configuration.QueryFilter.Filter<ISoftDelete>(QueryFilterType.SoftDelete.ToString(), customer => !customer.IsDeleted);
	
	// QueryFilter.Disable()
	{
		filter.Disable();

		// SELECT * FROM Customers
		var list = context.Customers.ToList();

		FiddleHelper.WriteTable("Customers", list);
	}

	// QueryFilter.Enable()
	{
		filter.Enable();

		// SELECT * FROM Customers WHERE IsActive = 1
		var list = context.Customers.ToList();

		FiddleHelper.WriteTable("Customers", list);
	}
	
	// QueryFilterManager.DisableFilter(string id)
	{
		// DOC: You can enable/disable your `QueryFilter` with the `Enable()`, `Disable()`, `EnableFilter(id)`, and `DisableFilder(id)` methods.
		context.Configuration.QueryFilter.DisableFilter(QueryFilterType.SoftDelete.ToString());

		// SELECT * FROM Customers
		var list = context.Customers.ToList();

		FiddleHelper.WriteTable("Customers", list);
	}

	// QueryFilterManager.EnableFilter(string id)
	{
		// DOC: You can enable/disable your `QueryFilter` with the `Enable()`, `Disable()`, `EnableFilter(id)`, and `DisableFilder(id)` methods.
		context.Configuration.QueryFilter.EnableFilter(QueryFilterType.SoftDelete.ToString());

		// SELECT * FROM Customers WHERE IsActive = 1
		var list = context.Customers.ToList();

		FiddleHelper.WriteTable("Customers", list);
	}
}
```
[Try it](https://dotnetfiddle.net/tctGi0)

> DANGER: DO NOT disable `Global Query Filter` unless you want to disable the filter for all your context instances.

> HINT: Use an `enum` when using a specified ID `QueryFilterType.FilterName.ToString()` to advoid hardcoding a `string`.

## Real Life Scenarios

### Soft Delete
Your application uses Soft Delete/Logical Delete to delete entities.

The **Query Filter** allows you to exclude all entities that are soft deleted from all your queries.

```csharp
public class EntityContext : DbContext
{
	public EntityContext() : base(FiddleHelper.GetConnectionStringSqlServer())
	{
		// Add your Global Query Filter here
		this.Configuration.QueryFilter.Filter<ISoftDelete>(customer => !customer.IsDeleted);
	}
	
	public DbSet<Customer> Customers { get; set; }
}

// SELECT * FROM Customers WHERE IsDeleted = 0
var list = context.Customers.ToList();
```
[Try it](https://dotnetfiddle.net/b1kwHs)

> HINT: The filter is usually applied to an interface named `ISoftDelete` inherited by all entity type that uses Soft Delete. 

### Multi-Tenancy
Your application uses a single instance to serve multiple tenants.

The **Query Filter** allows you to include only data that's related to the specified `TenantID` to all your queries.

```csharp
public static int ApplicationTenantID = 1;

public class EntityContext : DbContext
{
	public EntityContext() : base(FiddleHelper.GetConnectionStringSqlServer())
	{
		// Add your Global Query Filter here
		this.Configuration.QueryFilter.Filter<ITenant>(x => x.TenantID == ApplicationTenantID);
	}
	
	public DbSet<Customer> Customers { get; set; }
}

// SELECT * FROM Customers WHERE TenantID = 1
var list = context.Customers.ToList();
```
[Try it](https://dotnetfiddle.net/WuWGCy)

> HINT: The filter is usually applied to an interface named `ITenant` inherited by all entity types that uses multi-tenancy.

### Logical Data Partitioning
Your application store data in the same table but only a specific range should be available by example for a country.

The **Query Filter** allows you to include only data available for the specified country to all your queries.

```csharp
// TODO
// [Try it](8c3zgI)
```

### Security Access
Your application has security access. The logged user can only view some data depending on his role.

The `Query Filter` allows you to include only data the user has access to all your queries.

```csharp
public static int CurrentRoleId = 1; // admin

public class EntityContext : DbContext
{
	public EntityContext() : base(FiddleHelper.GetConnectionStringSqlServer())
	{
		this.Configuration.QueryFilter.Filter<ISoftDelete>(customer => !customer.IsDeleted || CurrentRoleId == 1);
		this.Configuration.QueryFilter.Filter<Customer>(customer => customer.Type == CustomerType.Web || CurrentRoleId == 1);
	}
	
	public DbSet<Customer> Customers { get; set; }
}
```
[Try it](https://dotnetfiddle.net/HP9Fbe)

## Documentation

### QueryFilter

###### Properties

| Name | Description | Example |
| :--- | :---------- | :------ |
| `ID` | Gets the `QueryFilter` ID. | [Try it](https://dotnetfiddle.net/8z8spq) |
| `EntityType` | Gets the `QueryFilter` entity type on which the filter is applied. | [Try it](https://dotnetfiddle.net/DP6Del) |
| `IsEnabled` | Gets if the `QueryFilter` is enabled. Use `Enable()` and `Disable()` method to change the state. Always return false if the `QueryFilter` feature is disabled. | [Try it](https://dotnetfiddle.net/28AdvH) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `Enable()` | Enable the `QueryFilter`. | [Try it](https://dotnetfiddle.net/H7cqIU) |
| `Disable()` | Disable the `QueryFilter`. | [Try it](https://dotnetfiddle.net/ArFGJh) |

### QueryFilterManager

###### Properties

| Name | Description | Example |
| :--- | :---------- | :------ |
| `IsEnabled` | Gets or sets if the `QueryFilter` feature is enabled. | [Try it](https://dotnetfiddle.net/ykhwxO) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `Filter<T>(Expression<Func<T, bool>> filter)`) | Filter an entity type using a predicate. | [Try it](https://dotnetfiddle.net/lqfF8b) |
| `Filter<T>(string id, Expression<Func<T, bool>> filter)` | Filter an entity type using a predicate. The `QueryFilter` will be created with the specified ID. | [Try it](https://dotnetfiddle.net/dBOdw2) |
| `EnableFilter(string id)` | Enable the `QueryFilter` with the specified id.  | [Try it](https://dotnetfiddle.net/q7T7nl)  |
| `DisableFilter(string id)` | Disable the `QueryFilter` with the specified id. | [Try it](https://dotnetfiddle.net/Zoric3)  |
| `GetFilter(string id)` | Get the `QueryFilter` with the specified id. | [Try it](https://dotnetfiddle.net/2IBfGq) |

## Limitations

### Change Tracker
If an entity is already loaded in the `ChangeTracker`, the filter may not apply due to how the `ChangeTracker` works.

For example: 
1. You load a customer with all his invoices
2. You add a filter to the invoice entity type
3. You load the same customer with all his invoices
4. The customer invoices have not been filtered

That is because both loaded customers are the same object instance. You can use `AsNoTracking` or use a new context instance if you need the customer with his invoice filtered.

That is not a bug, that's how the `ChangeTracker` works.

```csharp
using (var context = new EntityContext())
{
	// 1. You load a customer with all his invoices
	var customerA = context.Customers.Include(x => x.Invoices).FirstOrDefault();
	FiddleHelper.WriteTable(customerA.Invoices);
	
	// 2. You add a filter to the invoice entity type
	var filter = context.Configuration.QueryFilter.Filter<ISoftDelete>(customer => !customer.IsDeleted);
	
	// 3. You load the same customer with all his invoices
	var customerB = context.Customers.Include(x => x.Invoices).FirstOrDefault();
	
	// 4. The customer invoices have not been filtered
	FiddleHelper.WriteTable(customerB.Invoices);
	
	// Cause: That is because both loaded customers are the same object instance
	Console.WriteLine("Object reference equals: " + object.ReferenceEquals(customerA, customerB));				
}
```
[Try it](https://dotnetfiddle.net/oPE2ve)

## FAQ

<details>
<summary>Why should I use `Query Filter` over `Query ResultFilter`?</summary>

The **Query Filter** in most cases filters on the database side, so less rows are returned which leads to better performance.

The **Query ResultFilter** should only be used when the predicate cannot be interpreted as a query expression.
</details>
