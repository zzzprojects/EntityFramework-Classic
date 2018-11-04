# Query Filter

## Description
The **Query Filter** feature allows you to filter returned entities from a query using a predicate (**where clause**).

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
[Try it](https://dotnetfiddle.net/aDsTWW)

The filter is applied in the database and application side:
- **Database side**: Whenever possible, the filter is applied in the SQL query.
- **Application side**: The filter is always applied to the query result.

This feature allows to exclude some entities or include only specific entities to handle some scenarios such as:
- [Soft Delete](#soft-delete)
- [Multi-Tenancy](#multi-tenancy)
- [Logical Data Partitioning](#logical-data-partitioning)
- [Security Access](#security-access)

### What is supported?
All cases are supported:
- Include
- Lazy Loading
- Many to Many
- Etc.

### Advantage

- Centralize logic in **Query Filter** instead of adding it on every queries
- Reduce the chance of missing a filter when creating a new query
- Improve code readability
- Improve development productivity


## Getting Started

### Global Query Filter
You can create **Global Query Filter** by creating the filter in your context constructor. All your context instances will use created `QueryFilter`.

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
[Try it](https://dotnetfiddle.net/7cKY2x)

### Instance Query Filter
You can create **Instance Query Filter** by creating the filter after your context is instanced. Only this specific context instance will use created `Query Filter`.

```csharp
using (var context = new EntityContext())
{
	// Add your Instance Query Filter here
	context.Configuration.QueryFilter.Filter<ISoftDelete>(customer => !customer.IsDeleted);
	
	// SELECT * FROM Customers WHERE IsActive = 1
	var list = context.Customers.ToList();
	
	FiddleHelper.WriteTable("Customers", list);			
}
```
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

That is not a bug, that’s how the `ChangeTracker` works.

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
