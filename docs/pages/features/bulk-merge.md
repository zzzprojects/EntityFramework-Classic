# Bulk Merge

## Description
The **EF Bulk Merge** feature let you update thousands of entities in your database efficiently.

This feature is provided by the library [EF Extensions](https://entityframework-extensions.net/bulk-merge) _(Included with EF Classic)_. EF Extensions is used by over 2000 customers all over the world and supports all Entity Framework versions (EF4, EF5, EF6, EF Core, EF Classic).

```csharp
// Easy to use
context.BulkMerge(customers);

// Easy to customize
context.BulkMerge(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
Try it: [NET Framework](https://dotnetfiddle.net/HxfhEn) | [NET Core](https://dotnetfiddle.net/9Z8Cr9)

## Performance Comparison

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 4,000 ms       | To long...     | Way way to long... |
| BulkMerge       | 80 ms          | 110 ms         | 170 ms         |

Try it: [NET Framework](https://dotnetfiddle.net/L1yqaL) | [NET Core](https://dotnetfiddle.net/3d1KUv)

> HINT: Performance may differ from a database to another. A lot of factors might affect the benchmark time such as index, column type, latency, throttling, etc.

### Why BulkMerge is faster than AddOrUpdate + SaveChanges?
Merging thousands of entities for a file importation is a typical scenario.

The `AddOrUpdate` method performs a database round-trip for every entity to check if it already exists. The `DetectChanges` change method is also called for every entity which makes this method even slower (it's like using the `Add` method instead of `AddRange`).

The `SaveChanges` method performs one database round-trip for every entity to update.

So if you need to merge 10,000 entities, 20,000 database round-trips will be performed + 10,000 `DetectChanges` calls which is **INSANELY** slow.

The `BulkMerge` in counterpart requires the minimum database round-trips possible. For example, under the hood of SQL Server, a `SqlBulkCopy` is performed first in a temporary table, then an `MERGE` from the temporary table to the destination table is performed which is the most effective tactic available.

## Real Life Scenarios

## Bulk Merge with custom key
You need to update a list of `Customer` but you dont have the IDs, you only have the unique customer codes. The [ColumnPrimaryKeyExpression](https://entityframework-extensions.net/column#column-primary-key) let you choose the key to use.

```csharp
context.BulkMerge(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
Try it: [NET Framework](https://dotnetfiddle.net/xItcSY) | [NET Core](https://dotnetfiddle.net/XJLfKe)

## Bulk Merge specific columns
You need to update a list of `Customer` but only update some specific columns such as FirstName and LastName. The [ColumnInputExpression](https://entityframework-extensions.net/column#column-input) option let you to choose columns to update.

```csharp
context.BulkMerge(customers, options => { 
	options.ColumnInputExpression = customer => new { customer.FirstName, customer.LastName };
	options.ColumnPrimaryKeyExpression = customer => customer.Code;
});
```
Try it: [NET Framework](https://dotnetfiddle.net/0eArw7) | [NET Core](https://dotnetfiddle.net/ServiU)

## Bulk Merge specific columns on Update or Insert
You need to update a list of `Customer` but only save the `CreatedDate` on insert and save the `ModifiedDate` on update.
- The [IgnoreOnMergeInsert](https://entityframework-extensions.net/column#ignore-on-merge-insert) option let you ignore column when an insert is performed.
- The [IgnoreOnMergeUpdate](https://entityframework-extensions.net/column#ignore-on-merge-insert) option let you ignore column when an update is performed.

```csharp
context.BulkMerge(customers, options => { 
	options.IgnoreOnMergeInsertExpression = customer => customer.ModifiedDate;
	options.IgnoreOnMergeUpdateExpression = customer => customer.CreatedDate;
});
```
Try it: [NET Framework](https://dotnetfiddle.net/mycIU1) | [NET Core](https://dotnetfiddle.net/dDKgsR)

## Bulk Merge invoice and related invoice items (Include Graph)
You need to update a list of `Invoice` and include related `InvoiceItem`. By default, the `BulkUpdate` doesn't include the graph but you can enable it with the [IncludeGraph](https://entityframework-extensions.net/include-graph) option.

```csharp
context.BulkMerge(invoices, options => options.IncludeGraph = true);
```
Try it: [NET Framework](https://dotnetfiddle.net/owLagp) | [NET Core](https://dotnetfiddle.net/hQfCEO)

## Documentation

### BulkMerge

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `BulkMerge<T>(items)` | Bulk merge entities in your database. | [NET Framework](https://dotnetfiddle.net/mNuYTm) / [NET Core](https://dotnetfiddle.net/C7prtD) |
| `BulkMerge<T>(items, options)` | Bulk merge entities in your database.  | [NET Framework](https://dotnetfiddle.net/FznXCU) / [NET Core](https://dotnetfiddle.net/3B5JqX) |
| `BulkMergeAsync<T>(items)` | Bulk merge entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/T5qnNK) / [NET Core](https://dotnetfiddle.net/W3WRSp) |
| `BulkMergeAsync<T>(items, cancellationToken)` | Bulk merge entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/TYZXgS) / [NET Core](https://dotnetfiddle.net/KMXNx4) |
| `BulkMergeAsync<T>(items, options, cancellationToken)` | Bulk merge entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/joyorH) / [NET Core](https://dotnetfiddle.net/YPDwBl) |

## Learn more

More documentation can be found here: [EF Extensions - Bulk Merge](https://entityframework-extensions.net/bulk-merge)
