# Query Include Optimized

## Description

The **EF Query Include Optimized** feature let you filter related entities that will be included. However, instead to make one big query like **Query Include Filter**, the queries are split in several queries to optimize the performance like `EF Core` does.

The feature works exactly like [Query Include Filter](/query-include-filter), but you use the `IncludeOptimized` method instead.

The query is split in multiple queries which reduce the amount of data transferred.

```csharp
var customers = context.Customers.IncludeOptimized(x => x.Invoices.Where(y => !y.IsSoftDeleted)).ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/K0gPht) | [NET Core](https://dotnetfiddle.net/SyGPU2)

> The **Query Include Optimized** feature may sometimes reduce the performance. For example, when some database indexes are missing.

### Download
To use this feature, you need to download the following [NuGet Package](https://www.nuget.org/packages/Z.EntityFramework.Plus.QueryIncludeOptimized.EFClassic/)

### Entity Framework Classic - Integration
The **Query Include Optimized** feature will eventually integrate directly in **Entity Framework Classic**.

It's planned that this feature gets a major revamp to work more easily with include methods such as `Include` and `IncludeFilter`.

## Documentation

### QuerryIncludeOptimizedManager

| Name | Description | Default | Example |
| :--- | :---------- | :-----: | :------ |
| `AllowQueryBatch` | Gets or sets if queries should be batched (executed in the same SQL Command) | `true` | [NET Framework](https://dotnetfiddle.net/TREjVl) / [NET Core](https://dotnetfiddle.net/flXpcw) |
| `AllowIncludeSubPath` | Gets or sets if include path should be included automatically. | `false` | [NET Framework](https://dotnetfiddle.net/DUD1Ar) / [NET Core](https://dotnetfiddle.net/KBcTlC) |

### Extension Methods

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `IncludeOptimized<TEntityType, TRelatedEntity>(this IQueryable<TEntityType> query, Expression<Func<TEntityType, IEnumerable<TRelatedEntity>>> filter)` | An `IQueryable<TEntityType>` extension method that includes and filter a collection of related entities. | [NET Framework](https://dotnetfiddle.net/rpw6Ip) / [NET Core](https://dotnetfiddle.net/2yKpjW) |
| `IncludeOptimized<TEntityType, TRelatedEntity>(this IQueryable<TEntityType> query, Expression<Func<TEntityType, TRelatedEntity>> filter)` | An `IQueryable<TEntityType>` extension method that includes and filter a single related entities. | [NET Framework](https://dotnetfiddle.net/jiHkDP) / [NET Core](https://dotnetfiddle.net/NRvEbN) |
