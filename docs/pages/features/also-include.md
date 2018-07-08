# AlsoInclude Feature

## Description
You can chain multiples related objects to the query result by using the `AlsoInclude` and `ThenInclude` methods. The `AlsoInclude` method doesn’t move the chaining level. It allows to include multiple related objects from the same level.

`AlsoInclude` is a syntactic sugar method to make it easier and clearer to include multiples related objects.

### Note
- If you want to reset the level to the root, use [`Include`](https://github.com/zzzprojects/EntityFramework-Classic/blob/master/docs/pages/features/include.md)
- If you want to include items from the next level, use [`ThenInclude`](https://github.com/zzzprojects/EntityFramework-Classic/blob/master/docs/pages/features/then-include.md)

## Examples
```csharp
ctx.OrderDetails
	.Include(orderDetail => orderDetail.Product)
		.AlsoInclude(product => product.Category)
		.AlsoInclude(product => product.Supplier)
	.ToList();
```

## Options
None

## Limitations
None
