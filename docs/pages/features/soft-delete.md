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
[Try it](https://dotnetfiddle.net/m6lnqs#)

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
}
```
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
        }
```
[Try it](https://dotnetfiddle.net/pkMR5w)

### Enable/Disable Soft Delete Trigger
You can enable/disable all existing triggers by using `IsEnabled` on the manager or individual trigger and use with the `EnableTrigger(T)` and `DisableTrigger(T)` methods.

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

## Documentation

### SoftDeleteTrigger

###### Properties

| Name | Description | Example |
| :--- | :---------- | :------ |
| `EntityType` | Gets the `SofDeleteTrigger` entity type. | [Try it](https://dotnetfiddle.net/8z8spq) |
| `IsEnabled` | Gets if the `SofDeleteTrigger` is enabled. Use `Enable()` and `Disable()` method to change the state. Always return false if the `SoftDeleteTrigger` feature is disabled. | [Try it](https://dotnetfiddle.net/28AdvH) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `Enable()` | Enable the `SoftDeleteTrigger`. | [Try it](https://dotnetfiddle.net/H7cqIU) |
| `Disable()` | Disable the `SoftDeleteTrigger`. | [Try it](https://dotnetfiddle.net/ArFGJh) |

### SoftDeleteManager

###### Properties

| Name | Description | Example |
| :--- | :---------- | :------ |
| `IsEnabled` | Gets or sets if the `Soft Delete` feature is enabled. | [Try it](https://dotnetfiddle.net/ykhwxO) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `Trigger<TEntityType>(Action<DbContext, TEntityType> action)`) | Execute an action when an entity type state is set to Deleted and set the state to modified afterward | [Try it](https://dotnetfiddle.net/lqfF8b) |
| `Filter<T>(string id, Expression<Func<T, bool>> filter)` | Filter an entity type using a predicate. The `QueryFilter` will be created with the specified ID. | [Try it](https://dotnetfiddle.net/dBOdw2) |
| `EnableTrigger<TEntityType>` | Enable the `SoftDeleteTrigger` with the specified id.  | [Try it](https://dotnetfiddle.net/q7T7nl)  |
| `DisableTrigger<TEntityType>` | Disable the `SoftDeleteTrigger` with the specified id. | [Try it](https://dotnetfiddle.net/Zoric3)  |
| `GetTrigger<TEntityType>` | Get the `SoftDeleteTrigger` with the specified id. | [Try it](https://dotnetfiddle.net/2IBfGq) |

## Limitations

There can be only one trigger by entity type. If a second trigger try to be added an exception will be raised.

The SoftDeleteFeature is not supported by the `BulkSynchronize` operation.
