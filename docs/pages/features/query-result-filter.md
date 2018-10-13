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
		this.Configuration.QueryResultFilter.Filter<Customer>(customer => customer.IsActive);
	}
	
	public DbSet<Customer> Customers { get; set; }
}
```

[Try it](https://dotnetfiddle.net/39wJxN)
