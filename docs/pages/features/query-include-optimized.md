# Query Include Optimized

## Description

The **EF Query Include Optimized** feature let you to filter related entities that will be included. However, instead to make one big query like **Query Include Filter**, the queries is split in several queries to optimize the performance like `EF Core` does.

The feature work exactly like [Query Include Filter](/query-include-filter), but you use the `IncludeOptimized` method instead.

The query is split in multiples queries which reduce the amount of data transferred.

```csharp
var customers = context.Customers.IncludeOptimized(x => x.Invoices.Where(y => !y.IsSoftDeleted)).ToList();
```
[Try it](https://dotnetfiddle.net/K0gPht)

> The **Query Include Optimized** feature may sometimes reduce the performance. For example, when some database index are missing.

### Download
To use this feature, you need to download the following [NuGet Package](https://www.nuget.org/packages/Z.EntityFramework.Plus.QueryIncludeOptimized.EFClassic/)

### Entity Framework Classic - Integration
The **Query Include Optimized** feature will be eventually integrated directly in **Entity Framework Classic**.

It's planned that this feature get a major revamp to work more easily with include methods such as `Include` and `IncludeFilter`.

## Documentation

### QuerryIncludeOptimizedManager

| Name | Description | Default | Example |
| :--- | :---------- | :-----: | :------ |
| `AllowQueryBatch` | Gets or sets if queries should be batched (executed in the same SQL Command) | `true` | [Try it](https://dotnetfiddle.net/TREjVl) |
| `AllowIncludeSubPath` | Gets or sets if include path should be included automatically. | `false` | [Try it](https://dotnetfiddle.net/DUD1Ar) |

### Extension Methods

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `IncludeOptimized<TEntityType, TRelatedEntity>(this IQueryable<TEntityType> query, Expression<Func<TEntityType, IEnumerable<TRelatedEntity>>> filter)` | An `IQueryable<TEntityType>` extension method that includes and filter a collection of related entities. | [Try it](https://dotnetfiddle.net/rpw6Ip) |
| `IncludeOptimized<TEntityType, TRelatedEntity>(this IQueryable<TEntityType> query, Expression<Func<TEntityType, TRelatedEntity>> filter)` | An `IQueryable<TEntityType>` extension method that includes and filter a single related entities. | [Try it](https://dotnetfiddle.net/jiHkDP) |
