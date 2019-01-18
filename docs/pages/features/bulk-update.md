# Bulk Update

## Description
The **EF Bulk Update** feature let you update thousands of entities in your database efficiently.

This feature is provided by the library [EF Extensions](https://entityframework-extensions.net/bulk-update) _(Included with EF Classic)_. EF Extensions it's used by over 2000 customers all over the world and support all Entity Framework version (EF4, EF5, EF6, EF Core, EF Classic).

```csharp
// Easy to use
context.BulkUpdate(customers);

// Easy to customize
context.BulkUpdate(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
[Try it](https://dotnetfiddle.net/WYxuyf)

## Performance Comparison

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,200 ms       | 2,400 ms       | 6,000 ms       |
| BulkUpdate      | 80 ms          | 110 ms         | 170 ms         |

[Try it](https://dotnetfiddle.net/RAuYhO)

> HINT: Performance may differ from a database to another. A lot of factors might affect the benchmark time such as index, column type, latency, throttling, etc.

### Why BulkUpdate is faster then SaveChanges?
Update thousand of entities for a file importation is a typical scenario.

The `SaveChanges` method makes it quite impossible to handle this kind of situation due to the number of database round-trips required. The `SaveChanges` perform one database round-trip for every entity to update. So if you need to update 10,000 entities, 10,000 database round-trips will be performed which is **INSANELY** slow.

The `BulkUpdate` in counterpart requires the minimum database round-trips as possible. By example under the hood for SQL Server, a `SqlBulkCopy` is performed first in a temporary table, then an `UPDATE` from the temporary table to the destionation table is performed which is the most effective tactics available.

## Real Life Scenarios

## Bulk Update with custom key
You need to update a list of `Customer` but you doesn't have the ID, you only have the unique customer code. The [ColumnPrimaryKeyExpression](https://entityframework-extensions.net/column#column-primary-key) let you to choose the key to use.

```csharp
context.BulkUpdate(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
[Try it](https://dotnetfiddle.net/dMGZcV)

## Bulk Update specific columns
You need to update a list of `Customer` but only update some specific column such as FirstName and LastName. The [ColumnInputExpression](https://entityframework-extensions.net/column#column-input) option let you to choose column to update.

```csharp
context.BulkUpdate(customers, options => { 
	options.ColumnInputExpression = customer => new { customer.FirstName, customer.LastName };
	options.ColumnPrimaryKeyExpression = customer => customer.Code;
});
```
[Try it](https://dotnetfiddle.net/WBgGqx)

## Bulk Update invoice and related invoice items (Include Graph)
You need to update a list of `Invoice` and include related `InvoiceItem`. By default, the `BulkUpdate` doesn't include the graph but you can enable it with the [IncludeGraph](https://entityframework-extensions.net/include-graph) option.

```csharp
context.BulkUpdate(invoices, options => options.IncludeGraph = true);
```
[Try it](https://dotnetfiddle.net/ljXay5)

## Learn more

More documentation can be found here: [EF Extensions - Bulk Update](https://entityframework-extensions.net/bulk-update)
