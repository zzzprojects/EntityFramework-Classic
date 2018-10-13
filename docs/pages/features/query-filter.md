# Query Filter

## Description

Filter entities in database side.

The QueryFilter features modify the SQL generated to include a filter but also use the [QueryResultFilter](/query-result-filter) to ensure the result is filtered for query that cannot be filtered yet in the database side (TPC, TPH, TPT, Include)

It's always recommended to use the QueryFilter over the QueryResultFilter whenever you can to reduce the number of rows returned from the Database. However, the QueryResultFilter might also be useful if your filter cannot be translated into an expression.

```csharp
public class EntityContext : DbContext
{
	public EntityContext() : base(FiddleHelper.GetConnectionStringSqlServer())
	{
		// Add your QueryResultFilter here
		this.Configuration.QueryFilter.Filter<Customer>(customer => customer.IsActive);
	}
	
	public DbSet<Customer> Customers { get; set; }
}
```

[Try it](https://dotnetfiddle.net/aDsTWW)
