# Bulk Delete

## Description
The **EF Bulk Delete** feature let you delete thousands of entities in your database efficiently.

This feature is provided by the library [EF Extensions](https://entityframework-extensions.net/bulk-delete) _(Included with EF Classic)_. EF Extensions is used by over 2000 customers all over the world and supports all Entity Framework versions (EF4, EF5, EF6, EF Core, EF Classic).

```csharp
// Easy to use
context.BulkDelete(customers);

// Easy to customize
context.BulkDelete(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
Try it: [NET Framework](https://dotnetfiddle.net/vnq5Dw) | [NET Core](https://dotnetfiddle.net/UP8x9D)

## Performance Comparison

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,200 ms       | 2,400 ms       | 6,000 ms       |
| BulkDelete      | 50 ms          | 55 ms          | 75 ms         |

Try it: [NET Framework](https://dotnetfiddle.net/BnBmqF) | [NET Core](https://dotnetfiddle.net/cKxsEq)

> HINT: Performance may differ from a database to another. A lot of factors might affect the benchmark time such as index, column type, latency, throttling, etc.

### Why BulkDelete is faster than SaveChanges?
Deleting thousands of entities for a file importation is a typical scenario.

The `SaveChanges` method makes it quite impossible to handle this kind of situation due to the number of database round-trips required. The `SaveChanges` performs one database round-trip for every entity to delete. So, if you need to delete 10,000 entities, 10,000 database round-trips will be performed which is **INSANELY** slow.

The `BulkDelete` in counterpart requires the minimum database round-trips possible. For example, under the hood of SQL Server, a `SqlBulkCopy` is performed first in a temporary table, then a `DELETE` from the temporary table to the destination table is performed which is the most effective tactic available.

## Real Life Scenarios

## Bulk Delete with custom key
You need to delete a list of `Customer` but you dont have the IDs, you only have the unique customer codes. The [ColumnPrimaryKeyExpression](https://entityframework-extensions.net/column#column-primary-key) let you to choose the key to use.

```csharp
context.BulkDelete(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
Try it: [NET Framework](https://dotnetfiddle.net/cGvtjF) | [NET Core](https://dotnetfiddle.net/3uvfUv)

## Documentation

### BulkDelete

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `BulkDelete<T>(items)` | Bulk delete entities in your database. | [NET Framework](https://dotnetfiddle.net/3j4XQs) / [NET Core](https://dotnetfiddle.net/lk9xv9)|
| `BulkDelete<T>(items, options)` | Bulk delete entities in your database.  | [NET Framework](https://dotnetfiddle.net/zZH1fj) / [NET Core](https://dotnetfiddle.net/JVOA2l) |
| `BulkDeleteAsync<T>(items)` | Bulk delete entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/ifGB5A) / [NET Core](https://dotnetfiddle.net/CjWATE) |
| `BulkDeleteAsync<T>(items, cancellationToken)` | Bulk delete entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/dvLpqE) / [NET Core](https://dotnetfiddle.net/cJwS2R) |
| `BulkDeleteAsync<T>(items, options, cancellationToken)` | Bulk delete entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/iUQ6Pi) / [NET Core](https://dotnetfiddle.net/33mbtS) |

## Learn more

More documentation can be found here: [EF Extensions - Bulk Delete](https://entityframework-extensions.net/bulk-delete)
