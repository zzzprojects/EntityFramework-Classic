# Include Feature

## Description
You can add related objects to the query result by using the `Include` method.

In EF Classic, the `Include` method doesn’t longer return an `IQueryable` but instead an `IncludeDbQuery` that allows you to chain multiples related objects to the query result by using the `AlsoInclude` and `ThenInclude` methods.

It’s recommended to create and resolve multiple different queries than trying to include everything in the query.

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

## Options
None

## Limitations
None
