# Bulk Synchronize

## Description
The **EF Bulk Synchronize** feature let you synchronize the database with the source.

A synchronize is a mirror operation from the data source to the database. All rows that match the entity key are `UPDATED`, non-matching rows that exist from the source are `INSERTED`, non-matching rows that exist in the database are `DELETED`.

The database table becomes a mirror of the entity list provided.

This feature is provided by the library [EF Extensions](https://entityframework-extensions.net/bulk-synchronize) _(Included with EF Classic)_. EF Extensions is used by over 2000 customers all over the world and supports all Entity Framework versions (EF4, EF5, EF6, EF Core, EF Classic).

```csharp
// Easy to use
ctx.BulkSynchronize(list);

// Easy to customize
context.BulkSynchronize(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```

Try it: [NET Framework](https://dotnetfiddle.net/4KVPJn) | [NET Core](https://dotnetfiddle.net/PMxuNO)

## Documentation

### BulkSynchronize

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `BulkSynchronize<T>(items)` | Bulk synchronize entities in your database. | [NET Framework](https://dotnetfiddle.net/edgXau) | [NET Core](https://dotnetfiddle.net/I1uQOq) |
| `BulkSynchronize<T>(items, options)` | Bulk synchronize entities in your database.  | [NET Framework](https://dotnetfiddle.net/ERqbU6) | [NET Core](https://dotnetfiddle.net/NMwuRW)|
| `BulkSynchronizeAsync<T>(items)` | Bulk synchronize entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/NgkYF0) | [NET Core](https://dotnetfiddle.net/upgLxQ) |
| `BulkSynchronizeAsync<T>(items, cancellationToken)` | Bulk synchronize entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/tTyCiK) | [NET Core](https://dotnetfiddle.net/MnaGIn) |
| `BulkSynchronizeAsync<T>(items, options, cancellationToken)` | Bulk synchronize entities asynchronously in your database. | [NET Framework](https://dotnetfiddle.net/nDbfGU) | [NET Core](https://dotnetfiddle.net/Vg468C) |

## Learn more

More documentation can be found here: [EF Extensions - Bulk Synchronize](https://entityframework-extensions.net/bulk-synchronize)
