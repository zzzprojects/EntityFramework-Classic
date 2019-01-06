# Query Deferred

## Description

There are two types of IQueryable extension methods:

- Deferred Methods: The query expression is modified but the query is not resolved (Select, Where, etc.).
- Immediate Methods: The query expression is modified and the query is resolved (Count, First, etc.).

However, some features like **Query Cache** and **Query Future** cannot be used directly with Immediate Method since the query is already resolved.

**Query Deferred** provides more flexibility to other features.

### All LINQ IQueryable extension methods and overloads are supported:

| Name | Description | Example |
| :--- | :---------- | :------ |
| `DeferredAggregate` | QueryDeferred extension method. Applies an accumulator function over a sequence. | [Coming soon](#) |
| `DeferredAll` | | [Coming soon](#) |
| `DeferredAny` | | [Coming soon](#) |
| `DeferredAverage` | | [Coming soon](#) |
| `DeferredContains` | | [Coming soon](#) |
| `DeferredCount` | | [Coming soon](#) |
| `DeferredElementAt` | | [Coming soon](#) |
| `DeferredElementAtOrDefault` | | [Coming soon](#) |
| `DeferredFirst` | | [Coming soon](#) |
| `DeferredFirstOrDefault` | | [Coming soon](#) |
| `DeferredLast` | | [Coming soon](#) |
| `DeferredLastOrDefault` | | [Coming soon](#) |
| `DeferredLongCount` | | [Coming soon](#) |
| `DeferredMax` | | [Coming soon](#) |
| `DeferredMin` | | [Coming soon](#) |
| `DeferredSequenceEqual` | | [Coming soon](#) |
| `DeferredSingle` | | [Coming soon](#) |
| `DeferredSingleOrDefault` | | [Coming soon](#) |
| `DeferredSum` | | [Coming soon](#) |

## Real Life Scenarios
### Query Cache
You want to cache the customer count (immediate method) with the [Query Cache](query-cache) feature. You can defer the customer count with the `DeferredCount` method.

```csharp
// ... Coming soon...
```
[Coming soon](#)

### Query Future
You want to return the customer count (immediate method) with a paged list using the [Query Future](query-future) feature. You can defer the customer count with the `DeferredCount` method.

```csharp
// ... Coming soon...
```
[Coming soon](#)

## Documnentation

### QueryDeferred<TResult>

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `Execute()` | Execute the deferred expression and return the result. | [Coming soon](#) |
| `ExecuteAsync()` | Execute asynchrounously the deferred expression and return the result. | [Coming soon](#) |
| `ExecuteAsync(CancellationToken cancellationToken)` | Execute asynchrounously the deferred expression and return the result.  | [Coming soon](#)  |


