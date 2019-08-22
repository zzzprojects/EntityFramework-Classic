# Soft Delete

## Description
The **Soft Delete** feature allows you to flag entities as deleted (Soft Delete) instead of deleting them physically (Hard Delete).

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
Try it: [NET Framework](https://dotnetfiddle.net/pkMR5w) | [NET Core](https://dotnetfiddle.net/dnuxaL)

The soft delete feature can be acheived by using the 'IEFSoftDelete' interface. By default, this interface is always added to the manager. Otherwise you can add your own interface and trigger action to perform a soft delete.

The `IEFSoftDelete` interface will handle any entities that has a column named 'IsDeleted' with the boolean type.
Any entity that inherit this intefaced will be soft deleted instead of being completly deleted when saving changes from a context.

### Advantage

- Centralize logic in single interface to handle the soft delete
- Reduce the chance of missing a soft delete when introducing a new entity
- Improve development productivity

## Getting Started

### Default Interface
You can use the default soft delete by inheriting the 'IEFSoftDelete' interface.

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
Try it: [NET Framework](https://dotnetfiddle.net/bRqZHn) | [NET Core](https://dotnetfiddle.net/n85Yj9)

### Custom Interface
You can create an custom soft delete trigger by adding it to the manager

```csharp
public class EntityContext : DbContext
{
	public EntityContext() : base(FiddleHelper.GetConnectionStringSqlServer())
	{
		this.Configuration.SoftDelete.Trigger<ICustomeSoftDelete>((context, customer) =>			
		{
			customer.isActive = false;
			customer.DeletedAt = DateTime.UtcNow;							
		});
	}
		
	public DbSet<Customer> Customers { get; set; }
}
```
Try it: [NET Framework](https://dotnetfiddle.net/8yyF40) | [NET Core](https://dotnetfiddle.net/XEKkm0)

### Enable/Disable Soft Delete Trigger
You can enable/disable all existing soft delete triggers by using `IsEnabled` on the manager or individual trigger and use with the `EnableTrigger(T)` and `DisableTrigger(T)` methods.

```csharp
using (var context = new EntityContext())
{
	context.Configuration.SoftDelete.DisableTrigger<IEFSoftDelete>();  
		
	// Delete A from Customers as A where Name = 'Customer_A'
	var list = context.Customers.Where(x => x.Name == "Customer_A").ToList();
	context.Customers.RemoveRange(list);
	context.SaveChanges();	
			
	FiddleHelper.WriteTable("After Delete With DisableTrigger", context.Customers.ToList());		
			
	context.Configuration.SoftDelete.EnableTrigger<IEFSoftDelete>();  
			
	context.Customers.RemoveRange(context.Customers.ToList());
	context.SaveChanges();	
}
```
Try it: [NET Framework](https://dotnetfiddle.net/7GZbyO) | [NET Core](https://dotnetfiddle.net/ETXTcy)

## Real Life Scenarios

### Soft Delete
Your application uses Soft Delete/Logical Delete to delete entities.

The **Soft Delete** allows you to mark entities as deleted instead of physically deleting them.

```csharp
public class EntityContext : DbContext
{
	public EntityContext() : base(FiddleHelper.GetConnectionStringSqlServer())
	{
		this.Configuration.SoftDelete.Trigger<ICustomSoftDelete>((context, customer) =>								{
			customer.isActive = false;
			customer.DeletedAt = DateTime.UtcNow;
		});
	}
		
	public DbSet<Customer> Customers { get; set; }
		
	public class Customer : ICustomSoftDelete
	{
		public int CustomerID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool isActive { get; set; }
		public DateTime? DeletedAt   { get; set; }
	}
	
	public interface ICustomSoftDelete
	{
		bool isActive  { get; set; }
		DateTime? DeletedAt   { get; set; }
	}
}
```
Try it: [NET Framework](https://dotnetfiddle.net/rpWuks) | [NET Core](https://dotnetfiddle.net/xTzNsW)

## Documentation

### SoftDeleteTrigger

###### Properties

| Name | Description | Example |
| :--- | :---------- | :------ |
| `EntityType` | Gets the `SofDeleteTrigger` entity type. | [NET Framework](https://dotnetfiddle.net/OtNX16) / [NET Core](https://dotnetfiddle.net/dE8ZC5) |
| `IsEnabled` | Gets if the `SofDeleteTrigger` is enabled. Use `Enable()` and `Disable()` method to change the state. Always return false if the `SoftDeleteTrigger` feature is disabled. | [NET Framework](https://dotnetfiddle.net/OtNX16) / [NET Core](https://dotnetfiddle.net/9accmh) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `Enable()` | Enable the `SoftDeleteTrigger`. | [NET Framework](https://dotnetfiddle.net/00reiu) / [NET Core](https://dotnetfiddle.net/k99NFV) |
| `Disable()` | Disable the `SoftDeleteTrigger`. | [NET Framework](https://dotnetfiddle.net/00reiu) / [NET Core](https://dotnetfiddle.net/k99NFV) |

### SoftDeleteManager

###### Properties

| Name | Description | Example |
| :--- | :---------- | :------ |
| `IsEnabled` | Gets or sets if the `Soft Delete` feature is enabled. | [NET Framework](https://dotnetfiddle.net/xchNsI) / [NET Core](https://dotnetfiddle.net/gSaNq4) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `Trigger<TEntityType>(Action<DbContext, TEntityType> action)` | Execute an action when an entity type state is set to Deleted and set the state to modified afterward | [NET Framework](https://dotnetfiddle.net/eAimu3) / [NET Core](https://dotnetfiddle.net/951cjq) |
| `EnableTrigger<TEntityType>()` | Enable the `SoftDeleteTrigger` with the specified id.  | [NET Framework](https://dotnetfiddle.net/7GZbyO) / [NET Core](https://dotnetfiddle.net/lEuuB3)  |
| `DisableTrigger<TEntityType>()` | Disable the `SoftDeleteTrigger` with the specified id. | [NET Framework](https://dotnetfiddle.net/7GZbyO) / [NET Core](https://dotnetfiddle.net/lEuuB3)  |
| `Triggers()` | Get a list of `SoftDeleteTrigger`. | [NET Framework](https://dotnetfiddle.net/OtNX16) / [NET Core](https://dotnetfiddle.net/gXPV1f) |
| `GetTrigger<TEntityType>()` | Get the `SoftDeleteTrigger` with the specified id. | [NET Framework](https://dotnetfiddle.net/OtNX16) / [NET Core](https://dotnetfiddle.net/7I7ZvK) |

## Limitations

There can be only one trigger by entity type. If a second trigger tries to be added an exception will be raised.

The SoftDeleteFeature is not supported by the `BulkSynchronize` operation.
