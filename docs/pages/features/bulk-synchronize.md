# Bulk Synchronize

## Definition
`SYNCHRONIZE` all entities from the database.

A synchronize is a mirror operation from the data source to the database. All rows that match the entity key are `UPDATED`, non-matching rows that exist from the source are `INSERTED`, non-matching rows that exist in the database are `DELETED`.

The database table becomes a mirror of the entity list provided.

This feature is provided by [Z.EntityFramework.Extensions](http://entityframework-extensions.net/) that's used by more than 2000 customers all over the world.

```csharp
// Easy to use
ctx.BulkSynchronize(list);

// Easy to customize
context.BulkSynchronize(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```

## Purpose
`Synchronizing` entities with the database is a very rare scenario, but it may happen when two databases need to be synchronized.

`BulkSynchronize` give you the scalability and flexibility required when if you encounter this situation.

## Performance Comparisons

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,000 ms       | 2,000 ms       | 5,000 ms       |
| BulkSynchronize | 55 ms          | 65 ms          | 85 ms          |
