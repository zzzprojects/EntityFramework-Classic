# Bulk Delete (Enterprise Feature)

## Definition
`DELETE` all entities from the database.

All rows that match the entity key are `DELETED` from the database.

This feature is provided by [Z.EntityFramework.Extensions](http://entityframework-extensions.net/) that's used by more than 2000 customers all over the world.

```csharp
// Easy to use
context.BulkDelete(list);

// Easy to customize
context.BulkDelete(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Code);
```

[Try it](https://dotnetfiddle.net/vnq5Dw)

## Purpose
`Deleting` entities using a custom key from file importation is a typical scenario.

Despite the `ChangeTracker` being outstanding to track what's modified, it lacks in term of scalability and flexibility.

`SaveChanges` requires one database round-trip for every entity to `delete`. So if you need to `delete` 10000 entities, then 10000 database round-trips will be performed which is **INSANELY** slow.

`BulkDelete` in counterpart offers great customization and requires the minimum database round-trips as possible.

## Performance Comparisons

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,000 ms       | 2,000 ms       | 5,000 ms       |
| BulkDelete      | 45 ms          | 50 ms          | 60 ms          |
