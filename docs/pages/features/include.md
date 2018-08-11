# Include Feature

## Description
You can add related objects to the query result by using the `Include` method.

In EF Classic, the `Include` method doesn't longer return an `IQueryable` but instead an `IncludeDbQuery` that allows you to chain multiple related objects to the query result by using the `AlsoInclude` and `ThenInclude` methods.

You can convert the `IQueryable` to `DbQuery` by using the `AsDbQuery` method.

We recommend creating and resolving multiple different queries rather than trying to included everything in a single query. 

### Note
- If you want to include items from the same level, use [`AlsoInclude`](also-include.md)
- If you want to include items from the next level, use [`ThenInclude`](then-include.md)

### Examples
```csharp
ctx.Customers
	.Include(customer => customer.Orders)
		.ThenInclude(order => order.OrderDetails)
		.ThenInclude(orderDetail => orderDetail.Product)
			.AlsoInclude(product => product.Category)
			.AlsoInclude(product => product.Supplier)
	.ToList();
```

[Try it](https://dotnetfiddle.net/MkpoSo)

```csharp
ctx.OrderDetails
	.Where(orderDetail => orderDetail.Quantity > 1)
	.AsDbQuery()
	.Include(orderDetail => orderDetail.Product)
		.AlsoInclude(product => product.Category)
		.AlsoInclude(product => product.Supplier)
	.ToList();
```

[Try it](https://dotnetfiddle.net/33OIDZ)
