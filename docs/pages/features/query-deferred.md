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
| `DeferredAll` | QueryDeferred extension method. Determines whether all elements of a sequence satisfy a condition. | [Try it](https://dotnetfiddle.net/R4nKJc) |
| `DeferredAny` | QueryDeferred extension method. Determines whether a sequence contains any elements. | [Try it](https://dotnetfiddle.net/Nnv3fB) |
| `DeferredAverage` | QueryDeferred extension method. Computes the average of a sequence of Single values. | [Try it](https://dotnetfiddle.net/KUPPPf) |
| `DeferredCount` | QueryDeferred extension method. Returns the number of elements in a sequence. | [Try it](https://dotnetfiddle.net/GAEt8F) |
| `DeferredFirst` | QueryDeferred extension method. Returns the first element of a sequence. | [Try it](https://dotnetfiddle.net/VNtEF2) |
| `DeferredFirstOrDefault` | QueryDeferred extension method. Returns the first element of a sequence, or a default value if the sequence contains no elements. | [Try it](https://dotnetfiddle.net/MEM6Ub) |
| `DeferredLongCount` | QueryDeferred extension method. Returns an Int64 that represents how many elements in a sequence satisfy a condition. | [Try it](https://dotnetfiddle.net/0wPWSF) |
| `DeferredMax` | QueryDeferred extension method. Returns the maximum value in a sequence | [Try it](https://dotnetfiddle.net/9GljhW) |
| `DeferredMin` | QueryDeferred extension method. Returns the minimum value in a sequence | [Try it](https://dotnetfiddle.net/8h3Fjt) |
| `DeferredSingle` | QueryDeferred extension method. Returns the minimum value in a sequence of Single values. | [Try it](https://dotnetfiddle.net/YmhLeU) |
| `DeferredSingleOrDefault` | QueryDeferred extension method. Returns the minimum value in a sequence of nullable Single values. | [Try it](https://dotnetfiddle.net/8k6V4Q) |
| `DeferredSum` | QueryDeferred extension method. Computes the sum of a sequence | [Try it](https://dotnetfiddle.net/ugoMmG) |

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
// Not do Select
var futurValue = context.Customers.DeferredCount().FutureValue();

context.Customers.Add(new Customer() { Name = "Customer_D", Description = "Description"});
context.SaveChanges();	

// SELECT COUNT(1) FROM Customers
Console.WriteLine("Count Customer is : " +   futurValue.Value);	
```
[Try it](https://dotnetfiddle.net/OshIRK)

## Documnentation

### QueryDeferred<TResult>

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `Execute()` | Execute the deferred expression and return the result. | [Try it](https://dotnetfiddle.net/byuQpD) |
| `ExecuteAsync()` | Execute asynchrounously the deferred expression and return the result. | [Coming soon](#) |
| `ExecuteAsync(CancellationToken cancellationToken)` | Execute asynchrounously the deferred expression and return the result.  | [Coming soon](#)  |


