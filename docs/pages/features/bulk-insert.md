# Bulk Insert

## Description
`INSERT` all entities in the database.

All entities are considered as new rows and are `INSERTED` in the database.

This feature is provided by [Z.EntityFramework.Extensions](http://entityframework-extensions.net/) that's used by more than 2000 customers all over the world.

```csharp
// Easy to use
context.BulkInsert(list);

// Easy to customize
context.BulkInsert(list, options => options.BatchSize = 100);
```

[Try it](https://dotnetfiddle.net/7PnUvq)

## Purpose
`Inserting` thousand of entities for an initial load or a file importation is a typical scenario.

`SaveChanges` method makes it quite impossible to handle this kind of situation directly from Entity Framework due to the number of database round-trips required.

`SaveChanges` requires one database round-trip for every entity to `insert`. So if you need to `insert` 10000 entities, then 10000 database round-trips will be performed which is **INSANELY** slow.

`BulkInsert` in counterpart requires the minimum database round-trips as possible. By example under the hood for SQL Server, a simple`SqlBulkCopy` could be performed.

## Performance Comparisons

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,000 ms       | 2,000 ms       | 5,000 ms       |
| BulkInsert      | 6 ms           | 10 ms          | 15 ms          |
