# Bulk Insert

## Description
The **EF Bulk Insert** feature let you insert thousands of entities in your database efficiently.

This feature is provided by the library [EF Extensions](https://entityframework-extensions.net/bulk-insert) _(Included with EF Classic)_. EF Extensions is used by over 2000 customers all over the world and supports all Entity Framework versions (EF4, EF5, EF6, EF Core, EF Classic).

```csharp
// Easy to use
context.BulkInsert(list);

// Easy to customize
context.BulkInsert(list, options => options.BatchSize = 100);
```
Try it: [NET Framework](https://dotnetfiddle.net/7PnUvq) | [NET Core](https://dotnetfiddle.net/Ws2dgA)

## Performance Comparison

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,200 ms       | 2,400 ms       | 6,000 ms       |
| BulkInsert      | 50 ms          | 55 ms          | 75 ms          |

Try it: [NET Framework](https://dotnetfiddle.net/hfbiys) | [NET Core](https://dotnetfiddle.net/KHmNWf)

> HINT: Performance may differ from a database to another. A lot of factors might affect the benchmark time such as index, column type, latency, throttling, etc.

### Why BulkInsert is faster than SaveChanges?
Inserting thousands of entities for an initial load or a file importation is a typical scenario.

The `SaveChanges` method makes it quite impossible to handle this kind of situation due to the number of database round-trips required. The `SaveChanges` perform one database round-trip for every entity to insert. So, if you need to insert 10,000 entities, 10,000 database round-trips will be performed which is **INSANELY** slow.

The `BulkInsert` in counterpart requires the minimum database round-trips possible. For example, under the hood of SQL Server, a `SqlBulkCopy` is performed to insert 10,000 entities which is the most effective tactic available.

## Real Life Scenarios

### Bulk Insert invoice and related invoice items (Include Graph)
You need to insert a list of `Invoice` and include related `InvoiceItem`. By default, the `BulkInsert` doesn't include the graph but you can enable it with the [IncludeGraph](https://entityframework-extensions.net/include-graph) option.

```csharp
context.BulkInsert(invoices, options => options.IncludeGraph = true);
```
Try it: [NET Framework](https://dotnetfiddle.net/DGkPHC) | [NET Core](https://dotnetfiddle.net/mlFNqB)

### Bulk Insert customers that don't already exist
You need to insert a list of `Customer`, but only the ones that doesn't already exists using the customer codes as the key.

- The `InsertIfNotExists` option let you insert customers that doesn't already exists.
- The [ColumnPrimaryKey](https://entityframework-extensions.net/column#column-primary-key) option let you choose the key.

```csharp
context.BulkInsert(customers, options => {
    options.InsertIfNotExists = true;
    options.ColumnPrimaryKeyExpression = x => new { x.Code };
});
```
Try it: [NET Framework](https://dotnetfiddle.net/CtwBQw) | [NET Core](https://dotnetfiddle.net/THtLSm)

### Bulk Insert specific columns
You need to insert a list of `Customer` but only insert some specific column. The [ColumnInputExpression](https://entityframework-extensions.net/column#column-input) option let you choose a column to insert.

```csharp
context.BulkInsert(customers, options => {
    options.ColumnInputExpression = x => new { x.Code, x.CreatedDate };
});
```
Try it: [NET Framework](https://dotnetfiddle.net/x5qTfp) | [NET Core](https://dotnetfiddle.net/XBpAvg)

## Documentation

### BulkInsert

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `BulkInsert<T>(items)` | Bulk insert entities in your database. | [NET Framework](https://dotnetfiddle.net/hThOZA) / [NET Core](https://dotnetfiddle.net/78icWN)|
| `BulkInsert<T>(items, options)` | Bulk insert entities in your database.  | [NET Framework](https://dotnetfiddle.net/JCoqCP) / [NET Core](https://dotnetfiddle.net/ygSotx)|
| `BulkInsertAsync<T>(items)` | Bulk insert entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/p8c3Z3) / [NET Core](https://dotnetfiddle.net/URNBzO)|
| `BulkInsertAsync<T>(items, options)` | Bulk insert entities asynchronously in your database.  | [NET Framework](https://dotnetfiddle.net/pJuoy0) / [NET Core](https://dotnetfiddle.net/YE7dkP)|
| `BulkInsertAsync<T>(items, cancellationToken)` | Bulk insert entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/Ke5B5e) / [NET Core](https://dotnetfiddle.net/nNLf9I) |
| `BulkInsertAsync<T>(items, options, cancellationToken)` | Bulk insert entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/TKxYDS) / [NET Core](https://dotnetfiddle.net/ZvRUNj)|

## Learn more

More documentation can be found here: [EF Extensions - Bulk Insert](https://entityframework-extensions.net/bulk-insert)
