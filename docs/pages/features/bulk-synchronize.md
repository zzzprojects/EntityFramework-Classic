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

[Try it](https://dotnetfiddle.net/4KVPJn)

## Learn more

More documentation can be found here: [EF Extensions - Bulk Synchronize](https://entityframework-extensions.net/bulk-synchronize)
