# AlsoInclude

## Description
You can chain multiple related objects to the query result by using the `AlsoInclude` and `ThenInclude` methods. The `AlsoInclude` method doesn't move the chaining level. It allows to include multiple related objects from the same level.

`AlsoInclude` is a syntactic sugar method to make it easier and clearer to include multiple related objects.

```csharp
ctx.OrderDetails
	.Include(orderDetail => orderDetail.Product)
		.AlsoInclude(product => product.Category)
		.AlsoInclude(product => product.Supplier)
	.ToList();
```

Try it: [NET Framework](https://dotnetfiddle.net/tuONVZ) | [NET Core](https://dotnetfiddle.net/LhSCEh)

### Note
- If you want to reset the level to the root, use [`Include`](include.md)
- If you want to include items from the next level, use [`ThenInclude`](then-include.md)

## Limitations

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

Try it: [NET Framework](https://dotnetfiddle.net/33OIDZ) | [NET Core](https://dotnetfiddle.net/Btyl3G)
