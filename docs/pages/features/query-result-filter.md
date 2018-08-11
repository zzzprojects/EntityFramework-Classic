# Query Result Filter

## Description

The `QueryResultFilter` feature allows you to filter the result of a query. The filter is not performed on a database but in the result returned from the query.

### Example

```csharp
public class EntityContext : DbContext
{
	public EntityContext() : base(@"Data Source=ZZZ_Projects.sdf")
	{
		// Add your QueryResultFilter here
		this.Configuration.QueryResultFilter.Filter<Customer>(customer => customer.IsActif);
	}
	
	public DbSet<Customer> Customers { get; set; }
}
```

[Try it](https://dotnetfiddle.net/39wJxN)
