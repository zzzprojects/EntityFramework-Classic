# Bulk Merge

## Description
The **EF Bulk Merge** feature let you update thousands of entities in your database efficiently.

This feature is provided by the library [EF Extensions](https://entityframework-extensions.net/bulk-merge) _(Included with EF Classic)_. EF Extensions it's used by over 2000 customers all over the world and support all Entity Framework version (EF4, EF5, EF6, EF Core, EF Classic).

```csharp
// Easy to use
context.BulkMerge(customers);

// Easy to customize
context.BulkMerge(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
[Try it](https://dotnetfiddle.net/HxfhEn)

## Performance Comparison

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 4,000 ms       | To long...     | Way way to long... |
| BulkUpdate      | 80 ms          | 110 ms         | 170 ms         |

[Try it](https://dotnetfiddle.net/L1yqaL)

> HINT: Performance may differ from a database to another. A lot of factors might affect the benchmark time such as index, column type, latency, throttling, etc.

### Why BulkMerge is faster then AddOrUpdate + SaveChanges?
Merging thousand of entities for a file importation is a typical scenario.

The `AddOrUpdate` method perform a database round-trips for every entity to check if it already exists. The `DetectChanges` change method is also called for every entity which makes this method even slower (it's like using the `Add` method instead of `AddRange`).

The `SaveChanges` method perform one database round-trip for every entity to update.

So if you need to merge 10,000 entities, 20,000 database round-trips will be performed + 10,000 `DetectChanges` call which is **INSANELY** slow.

The `BulkMerge` in counterpart requires the minimum database round-trips as possible. By example under the hood for SQL Server, a `SqlBulkCopy` is performed first in a temporary table, then an `MERGE` from the temporary table to the destionation table is performed which is the most effective tactics available.

## Real Life Scenarios

## Bulk Merge with custom key
You need to update a list of `Customer` but you doesn't have the ID, you only have the unique customer code. The [ColumnPrimaryKeyExpression](https://entityframework-extensions.net/column#column-primary-key) let you to choose the key to use.

```csharp
context.BulkMerge(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
[Try it](https://dotnetfiddle.net/xItcSY)

## Bulk Merge specific columns
You need to update a list of `Customer` but only update some specific column such as FirstName and LastName. The [ColumnInputExpression](https://entityframework-extensions.net/column#column-input) option let you to choose column to update.

```csharp
context.BulkMerge(customers, options => { 
	options.ColumnInputExpression = customer => new { customer.FirstName, customer.LastName };
	options.ColumnPrimaryKeyExpression = customer => customer.Code;
});
```
[Try it](https://dotnetfiddle.net/0eArw7)

## Bulk Merge specific columns on Update or Insert
You need to update a list of `Customer` but only save the `CreatedDate` on insert and save the `ModifiedDate` on update.
- The [IgnoreOnMergeInsert](https://entityframework-extensions.net/column#ignore-on-merge-insert) option let you to ignore column when an insert is performed.
- The [IgnoreOnMergeUpdate](https://entityframework-extensions.net/column#ignore-on-merge-insert) option let you to ignore column when an update is performed.

```csharp
context.BulkMerge(customers, options => { 
	options.IgnoreOnMergeInsertExpression = customer => customer.ModifiedDate;
	options.IgnoreOnMergeUpdateExpression = customer => customer.CreatedDate;
});
```
[Try it](https://dotnetfiddle.net/mycIU1)

## Bulk Merge invoice and related invoice items (Include Graph)
You need to update a list of `Invoice` and include related `InvoiceItem`. By default, the `BulkUpdate` doesn't include the graph but you can enable it with the [IncludeGraph](https://entityframework-extensions.net/include-graph) option.

```csharp
context.BulkMerge(invoices, options => options.IncludeGraph = true);
```
[Try it](https://dotnetfiddle.net/owLagp)

## Learn more

More documentation can be found here: [EF Extensions - Bulk Merge](https://entityframework-extensions.net/bulk-merge)
