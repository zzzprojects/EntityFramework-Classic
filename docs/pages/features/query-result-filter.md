# Query Result Filter

## Description

The `QueryResultFilter` feature allows you to filter the result of a query. The filter is not performed on a database but in the result returned from the query.

It's always recommended to use the QueryFilter over the QueryResultFilter whenever you can to reduce the number of rows returned from the Database. However, the QueryResultFilter might also be useful if your filter cannot be translated into an expression.

### Example

```csharp
public class EntityContext : DbContext
{
	public EntityContext() : base(FiddleHelper.GetConnectionStringSqlServer())
	{
		// Add your QueryResultFilter here
		this.Configuration.QueryResultFilter.Filter<ISoftDelete>(customer => !customer.IsDeleted || DisplaySoftDelete());
	}
	
	public bool DisplaySoftDelete()
	{
		// Check for example if current user has admin right
		return false;
	}
	
	public DbSet<Customer> Customers { get; set; }
}

var list = context.Customers.ToList();
```

Try it: [NET Framework](https://dotnetfiddle.net/39wJxN) | [NET Core](https://dotnetfiddle.net/0h4JL1)

## Documentation

### QueryResultFilter

###### Properties

| Name | Description | Example |
| :--- | :---------- | :------ |
| `ID` | Gets the `QueryFilter` ID. | [NET Framework](https://dotnetfiddle.net/pWIl86) / [NET Core](https://dotnetfiddle.net/JBtES6) |
| `EntityType` | Gets the `QueryFilter` entity type on which the filter is applied. | [NET Framework](https://dotnetfiddle.net/Scty5l) / [NET Core](https://dotnetfiddle.net/H4Exdo) |
| `IsEnabled` | Gets if the `QueryFilter` is enabled. Use `Enable()` and `Disable()` method to change the state. Always return false if the `QueryFilter` feature is disabled. | [NET Framework](https://dotnetfiddle.net/rZzXUv) / [NET Core](https://dotnetfiddle.net/WH66vP) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `Enable()` | Enable the `QueryFilter`. | [NET Framework](https://dotnetfiddle.net/R4nKJc) / [NET Core](https://dotnetfiddle.net/gEVSU7) |
| `Disable()` | Disable the `QueryFilter`. | [NET Framework](https://dotnetfiddle.net/27CbSm) / [NET Core](https://dotnetfiddle.net/vsnVxs) |

### QueryResultFilterManager

###### Properties

| Name | Description | Example |
| :--- | :---------- | :------ |
| `IsEnabled` | Gets or sets if the `QueryFilter` feature is enabled. | [NET Framework](https://dotnetfiddle.net/47jkME) / [NET Core](https://dotnetfiddle.net/Uk0XVb) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `Filter<T>(Expression<Func<T, bool>> filter)` | Filter an entity type using a predicate. | [NET Framework](https://dotnetfiddle.net/zcIngq) / [NET Core](https://dotnetfiddle.net/7VdErj) |
| `Filter<T>(string id, Expression<Func<T, bool>> filter)` | Filter an entity type using a predicate. The `QueryFilter` will be created with the specified ID. | [NET Framework](https://dotnetfiddle.net/g2Hj0r) / [NET Core](https://dotnetfiddle.net/6zeiIN) |
| `EnableFilter(string id)` | Enable the `QueryFilter` with the specified id.  | [NET Framework](https://dotnetfiddle.net/WSTNTV) / [NET Core](https://dotnetfiddle.net/OVQLaN)  |
| `DisableFilter(string id)` | Disable the `QueryFilter` with the specified id. | [NET Framework](https://dotnetfiddle.net/9B3fnF) / [NET Core](https://dotnetfiddle.net/ieKaYT)  |
| `GetFilter(string id)` | Get the `QueryFilter` with the specified id. | [NET Framework](https://dotnetfiddle.net/feGJtz) / [NET Core](https://dotnetfiddle.net/5P0bTw) |
