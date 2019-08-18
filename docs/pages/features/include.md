# Include

## Description
The `Include` method let you add related entities to the query result.

In EF Classic, the `Include` method no longer returns an `IQueryable` but instead an `IncludeDbQuery` that allows you to chain multiple related objects to the query result by using the `AlsoInclude` and `ThenInclude` methods.

```csharp
ctx.Customers
	.Include(customer => customer.Orders)
		.ThenInclude(order => order.OrderDetails)
		.ThenInclude(orderDetail => orderDetail.Product)
			.AlsoInclude(product => product.Category)
			.AlsoInclude(product => product.Supplier)
	.ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/MkpoSo) | [NET Core](https://dotnetfiddle.net/dyWV1T)

### Note
- If you want to include items from the same level, use [`AlsoInclude`](also-include.md)
- If you want to include items from the next level, use [`ThenInclude`](then-include.md)

## Limitation

### DbQuery
Chaining includes only work if the first include call is from a `DbQuery`. If you used some LINQ and the query is currently an `IQueryable`, you can use the method `AsDbQuery` to tell the compiler that's a `DbQuery`.
This restriction is currently required to avoid some side impact with queries that are not directly using `DbQuery` class.

```csharp
ctx.OrderDetails
	.Where(orderDetail => orderDetail.Quantity > 1)
	.AsDbQuery()
	.Include(orderDetail => orderDetail.Product)
		.AlsoInclude(product => product.Category)
		.AlsoInclude(product => product.Supplier)
	.ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/2XJrc5) | [NET Core](https://dotnetfiddle.net/5g8jcN)

> It's planned to remove this limitation.

