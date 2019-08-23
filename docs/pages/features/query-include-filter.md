# Query Include Filter

## Description

The **EF Query Include Filter** feature let you filter related entities that will be included.

For example, you want to load your customers and their invoices, but only related invoices that are not soft deleted.

```csharp
var customers = context.Customers.IncludeFilter(x => x.Invoices.Where(y => !y.IsSoftDeleted)).ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/pesV1x) | [NET Core](https://dotnetfiddle.net/WcfASx)

This feature allows you to handle various scenarios such as:
- [Exclude soft deleted entities](#exclude-soft-deleted-entities)
- [Include with security access](#include-with-security-access)
- [Include paginated entities](#include-paginated-entities)

### Download
To use this feature, you need to download the following [NuGet Package](https://www.nuget.org/packages/Z.EntityFramework.Plus.QueryIncludeFilter.EFClassic/)

It's planned in 2019 to improve the **Query Include Filter** feature, remove some limitations, and integrate the code directly in Entity Framework Classic package.

## Getting Started

### Include one level
To filter with the `IncludeFilter` method, you need to specify the path as you do with the `Include` method and use a LINQ `Where` clause. Some other LINQ methods could also be used such as `Take`, `Skip`, and more.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var context = new EntityContext()

// LOAD customers and related active invoices.
var customers = context.Customers.IncludeFilter(x => x.Invoices.Where(y => !y.IsSoftDeleted)).ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/H85eO9) | [NET Core](https://dotnetfiddle.net/RAjpIj)

### Include multiple levels
To filter multiple levels, you need to use the `IncludeFilter` on every level, not only the last one, unlike the `Include` method.

In this example, we performed an `IncludeFilter` on the `Invoices` level, and one on the `InvoiceItems` level.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var context = new EntityContext()

// LOAD customers and related active invoices and InvoiceItems[NEEDGOODWORD!!!].
var customers = context.Customers.IncludeFilter(x => x.Invoices.Where(y => !y.IsSoftDeleted))
				.IncludeFilter(x => x.Invoices.Where(y => !y.IsSoftDeleted)
							   .Select(y => y.InvoiceItems
							   		.Where(z => !z.IsSoftDeleted)))
                     .ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/v6AgLP) | [NET Core](https://dotnetfiddle.net/Ltp75I)

> The limitations to include every level will be removed when the feature will be integrated into **Entity Framework Classic**.

### Include chaining
You can chain multiple `IncludeFilter` methods one after another but you cannot mix it with other include methods such as `Include`, `AlsoInclude`, `ThenInclude`, `IncludeOptimized`.

If you need to include without a filter, you can still use the `IncludeFilter` method.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var context = new EntityContext()

// LOAD customers and related active invoices and InvoiceItems.
var customers = context.Customers.IncludeFilter(x => x.Invoices.Where(y => !y.IsSoftDeleted))
				.IncludeFilter(x => x.Invoices.Where(y => !y.IsSoftDeleted)
							   .Select(y => y.InvoiceItems
							   		.Where(z => !z.IsSoftDeleted)))
                     .ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/C4qVc1) | [NET Core](https://dotnetfiddle.net/4lf3Mi)

> The limitation to chain only with `IncludeFilter` method will be removed when the feature will be integrated into **Entity Framework Classic**.

## Real Life Scenarios

### Exclude soft deleted entities
You need to load `Customer` and include related `Invoice` and `InvoiceItem`, but only related `Invoice` and `InvoiceItem` that are not soft deleted.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var context = new EntityContext()

// LOAD customers and related active invoices, and InvoiceItems.
var customers = context.Customers.IncludeFilter(x => x.Invoices.Where(y => !y.IsSoftDeleted))
				.IncludeFilter(x => x.Invoices.Where(y => !y.IsSoftDeleted)
							   .Select(y => y.InvoiceItems
							   		.Where(z => !z.IsSoftDeleted)))
                     .ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/AmqKb0) | [NET Core](https://dotnetfiddle.net/a5b9FM)

### Include with security access
You need to load a post and include related comments, but only related comments the current role have access.

```csharp
// myRoleID = 1; // Administrator

// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// LOAD posts and available comments for the role level.
var posts= ctx.Posts.IncludeFilter(x => x.Comments.Where(y => y.RoleID >= myRoleID))
                    .ToList();
```

### Include paginated entities
You need to load a post and include related comments, but only the first 10 related comments sorted by views.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var context = new EntityContext()

context.Invoices.IncludeFilter(x => x.InvoiceItems.Take(10));
```
Try it: [NET Framework](https://dotnetfiddle.net/wFBdRt) | [NET Core](https://dotnetfiddle.net/iYKloB)

## Documentation

### Extension Methods

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `IncludeFilter<TEntityType, TRelatedEntity>(this IQueryable<TEntityType> query, Expression<Func<TEntityType, IEnumerable<TRelatedEntity>>> filter)` | An `IQueryable<TEntityType>` extension method that includes and filter a collection of related entities. | [NET Framework](https://dotnetfiddle.net/72nPzP) / [NET Core](https://dotnetfiddle.net/deYDiM) |
| `IncludeFilter<TEntityType, TRelatedEntity>(this IQueryable<TEntityType> query, Expression<Func<TEntityType, TRelatedEntity>> filter)` | An `IQueryable<TEntityType>` extension method that includes and filter a single related entities. | [NET Framework](https://dotnetfiddle.net/BpUD4q) / [NET Core](https://dotnetfiddle.net/rv3yeQ) |

## Limitations

 - Cannot be mixed with projection
 - Cannot be mixed with Include
 - Cannot be mixed with IncludeOptimized
 - Cannot be used with `AsNoTracking`
 - Cannot be used with `Many to Many` relationships
 - Cannot filter entities already loaded
 
 > Most of those limitations will be removed when the **Query Include Filter** code will be integrated  directly in **Entity Framework Classic**.

### Cannot filter entities already loaded
If an entity is already part of the `ChangeTracker` (the context), it's impossible to exclude it even with the `IncludeFilter`. That's how the `ChangeTracker` works.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var context = new EntityContext()

context.Invoices.ToList();

// The Invoices automatically contain all InvoiceItems [NEEDGOODWORD!!!] even without using the "Include" method.
context.InvoiceItems.ToList();

// Trying to load only one InvoiceItems [NEEDGOODWORD!!!] will obviously not work either.
context.Invoices.IncludeFilter(x => x.InvoiceItems.Take(1)).ToList();
```

Try it: [NET Framework](https://dotnetfiddle.net/t2FLxe) | [NET Core](https://dotnetfiddle.net/LwC9GH)

In this case, we recommend to create and load entities from a new `DbContext`.
