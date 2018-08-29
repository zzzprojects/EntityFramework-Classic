# Delete from Query

## Description
`DELETE` all rows from the database using a LINQ Query without loading entities in the context.

A `DELETE` statement is built using the LINQ expression and directly executed in the database.

This feature is provided by [Z.EntityFramework.Extensions](http://entityframework-extensions.net/) that's used by more than 2000 customers all over the world.

```csharp
// DELETE all customers that are inactive
context.Customers.Where(x => !x.IsActif).DeleteFromQuery();

// DELETE customers by id
context.Customers.Where(x => x.ID == userId).DeleteFromQuery();
```

[Try it](https://dotnetfiddle.net/msiYwA)

## Purpose
`Deleting` entities using `SaveChanges` normally requires to load them first in the `ChangeTracker`. These additional round-trips are often not necessary.

`DeleteFromQuery` gives you access to directly execute a `DELETE` statement in the database and provide a **HUGE** performance improvement.

## Performance Comparisons

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,000 ms       | 2,000 ms       | 5,000 ms       |
| DeleteFromQuery | 1 ms           | 1 ms           | 1 ms           |
