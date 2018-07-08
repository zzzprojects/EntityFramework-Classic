
# AlsoInclude Feature

## Description
You can chain multiples related objects to the query result by using the `AlsoInclude` and `ThenInclude` methods. The `ThenInclude` method moves the chaining level to the property included. It allows to include related objects from the next level.

`ThenInclude` is a syntactic sugar method to make it easier and clearer to include multiples related objects.

### Note
- If you want to reset the level to the root, use [`Include`](/include.md)
- If you want to include items from the same level, use [`AlsoInclude`](https://github.com/zzzprojects/EntityFramework-Classic/blob/master/docs/pages/features/also-include.md)

## Examples
```csharp
ctx.Customers
	.Include(customer => customer.Orders)
		.ThenInclude(order => order.OrderDetails)
		.ThenInclude(orderDetail => orderDetail.Product)
	.ToList();
	
ctx.Customers
	.Include(customer => customer.Orders)
		.ThenInclude(order => order.OrderDetails.Select(orderDetail => orderDetail.Product);
	.ToList();
	
ctx.OrderDetails
	.Include(orderDetail => orderDetail.Product)
		.ThenInclude(product => product.Category);
	.Include(orderDetail => orderDetail.Product)
		.ThenInclude(product => product.Supplier);
	.ToList();
```

## Options
None

## Limitations
None
