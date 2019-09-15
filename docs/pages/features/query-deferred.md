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
| `DeferredAll` | QueryDeferred extension method. Determines whether all elements of a sequence satisfy a condition. | [NET Framework](https://dotnetfiddle.net/R4nKJc) / [NET Core](https://dotnetfiddle.net/rXloUh) |
| `DeferredAny` | QueryDeferred extension method. Determines whether a sequence contains any elements. | [NET Framework](https://dotnetfiddle.net/Nnv3fB) / [NET Core](https://dotnetfiddle.net/A2Q5Yi) |
| `DeferredAverage` | QueryDeferred extension method. Computes the average of a sequence of Single values. | [NET Framework](https://dotnetfiddle.net/KUPPPf) / [NET Core](https://dotnetfiddle.net/uOU29N) |
| `DeferredCount` | QueryDeferred extension method. Returns the number of elements in a sequence. | [NET Framework](https://dotnetfiddle.net/GAEt8F) / [NET Core](https://dotnetfiddle.net/Vj5Zjo) |
| `DeferredFirst` | QueryDeferred extension method. Returns the first element of a sequence. | [NET Framework](https://dotnetfiddle.net/VNtEF2) / [NET Core](https://dotnetfiddle.net/crZUfE) |
| `DeferredFirstOrDefault` | QueryDeferred extension method. Returns the first element of a sequence, or a default value if the sequence contains no elements. | [NET Framework](https://dotnetfiddle.net/MEM6Ub) / [NET Core](https://dotnetfiddle.net/IAurZU) |
| `DeferredLongCount` | QueryDeferred extension method. Returns an Int64 that represents how many elements in a sequence satisfy a condition. | [NET Framework](https://dotnetfiddle.net/0wPWSF) / [NET Core](https://dotnetfiddle.net/3zjKYj) |
| `DeferredMax` | QueryDeferred extension method. Returns the maximum value in a sequence | [NET Framework](https://dotnetfiddle.net/9GljhW) / [NET Core](https://dotnetfiddle.net/rF8RDF) |
| `DeferredMin` | QueryDeferred extension method. Returns the minimum value in a sequence | [NET Framework](https://dotnetfiddle.net/8h3Fjt) / [NET Core](https://dotnetfiddle.net/IskFgT) |
| `DeferredSingle` | QueryDeferred extension method. Returns the minimum value in a sequence of Single values. | [NET Framework](https://dotnetfiddle.net/YmhLeU) / [NET Core](https://dotnetfiddle.net/sPXcC1) |
| `DeferredSingleOrDefault` | QueryDeferred extension method. Returns the minimum value in a sequence of nullable Single values. | [NET Framework](https://dotnetfiddle.net/8k6V4Q) / [NET Core](https://dotnetfiddle.net/C4KZbM) |
| `DeferredSum` | QueryDeferred extension method. Computes the sum of a sequence | [NET Framework](https://dotnetfiddle.net/ugoMmG) / [NET Core](https://dotnetfiddle.net/U4TEb1) |

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
Try it: [NET Framework](https://dotnetfiddle.net/OshIRK) | [NET Core](https://dotnetfiddle.net/I7kZ13)

## Documnentation

### QueryDeferred<TResult>

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `Execute()` | Execute the deferred expression and return the result. | [NET Framework](https://dotnetfiddle.net/byuQpD) / [NET Core](https://dotnetfiddle.net/dHXqhH) |
| `ExecuteAsync()` | Execute asynchrounously the deferred expression and return the result. | [NET Framework](https://dotnetfiddle.net/eK16Eh) / [NET Core](https://dotnetfiddle.net/Qm0RnT) |
| `ExecuteAsync(CancellationToken cancellationToken)` | Execute asynchrounously the deferred expression and return the result.  | [NET Framework](https://dotnetfiddle.net/80edw0) / [NET Core](https://dotnetfiddle.net/JmBS0R)  |


