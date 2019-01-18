# Bulk Delete

## Description
The **EF Bulk Delete** feature let you delete thousands of entities in your database efficiently.

This feature is provided by the library [EF Extensions](https://entityframework-extensions.net/bulk-delete) _(Included with EF Classic)_. EF Extensions it's used by over 2000 customers all over the world and support all Entity Framework version (EF4, EF5, EF6, EF Core, EF Classic).

```csharp
// Easy to use
context.BulkDelete(customers);

// Easy to customize
context.BulkDelete(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
[Try it](https://dotnetfiddle.net/vnq5Dw)

## Performance Comparison

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,200 ms       | 2,400 ms       | 6,000 ms       |
| BulkDelete      | 50 ms          | 55 ms          | 75 ms         |

[Try it](https://dotnetfiddle.net/BnBmqF)

> HINT: Performance may differ from a database to another. A lot of factors might affect the benchmark time such as index, column type, latency, throttling, etc.

### Why BulkDelete is faster then SaveChanges?
Deleting thousand of entities for a file importation is a typical scenario.

The `SaveChanges` method makes it quite impossible to handle this kind of situation due to the number of database round-trips required. The `SaveChanges` perform one database round-trip for every entity to delete. So if you need to delete 10,000 entities, 10,000 database round-trips will be performed which is **INSANELY** slow.

The `BulkDelete` in counterpart requires the minimum database round-trips as possible. By example under the hood for SQL Server, a `SqlBulkCopy` is performed first in a temporary table, then an `DELETE` from the temporary table to the destionation table is performed which is the most effective tactics available.

## Real Life Scenarios

## Bulk Delete with custom key
You need to delete a list of `Customer` but you doesn't have the ID, you only have the unique customer code. The [ColumnPrimaryKeyExpression](https://entityframework-extensions.net/column#column-primary-key) let you to choose the key to use.

```csharp
context.BulkDelete(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
[Try it](https://dotnetfiddle.net/cGvtjF)

## Learn more

More documentation can be found here: [EF Extensions - Bulk Delete](https://entityframework-extensions.net/bulk-delete)
