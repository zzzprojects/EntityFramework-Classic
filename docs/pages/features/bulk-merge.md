# Bulk Merge (Enterprise Feature)

## Description
`MERGE` all entities from the database.

A merge is an `UPSERT` operation. All rows that match the entity key are considered as existing and are `UPDATED`, other rows are considered as new rows and are `INSERTED` in the database. 

This feature is provided by [Z.EntityFramework.Extensions](http://entityframework-extensions.net) that's used by more than 2000 customers all over the world.

```csharp
// Easy to use
ctx.BulkMerge(list);

// Easy to customize
context.BulkMerge(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```
[Try it](https://dotnetfiddle.net/HxfhEn)

## Purpose
`Merging` entities using a custom key from file importation is a typical scenario.

Despite the `ChangeTracker` being outstanding to track what's modified, it lacks in term of scalability and flexibility.

`SaveChanges` requires one database round-trip for every entity to `insert` or `update`. So if you need to `insert` or `update` 10000 entities, then 10000 database round-trips will be performed which is **INSANELY** slow.

`BulkMerge` in counterpart offers great customization and requires the minimum database round-trips as possible.

## Performance Comparisons

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,000 ms       | 2,000 ms       | 5,000 ms       |
| BulkMerge       | 65 ms          | 80 ms          | 110 ms         |
