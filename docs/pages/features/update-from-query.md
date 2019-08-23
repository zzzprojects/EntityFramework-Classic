# Update from Query

## Description
`UPDATE` all rows from the database using a LINQ Query without loading entities in the context.

An `UPDATE` statement is built using the LINQ expression and directly executed in the database.

This feature is provided by [Z.EntityFramework.Extensions](http://entityframework-extensions.net/) that's used by more than 2000 customers all over the world.

```csharp
// UPDATE all customers that are inactive for more than two years
var date = DateTime.Now.AddYears(-2);
context.Customers
    .Where(x => x.IsActive && x.LastLogin < date)
    .UpdateFromQuery(x => new Customer {IsActive = false});
	
// UPDATE customers by id
context.Customers.Where(x => x.ID == userId).UpdateFromQuery(x => new Customer {IsActive = false});
```

Try it: [NET Framework](https://dotnetfiddle.net/gSJJeh) | [NET Core](https://dotnetfiddle.net/8JuppD)

## Purpose
`Updating` entities using `SaveChanges` normally requires to load them first in the `ChangeTracker`. These additional round-trips are often not necessary.

`UpdateFromQuery` gives you access to directly execute an `UPDATE` statement in the database and provide a **HUGE** performance improvement.

## Performance Comparisons

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,000 ms       | 2,000 ms       | 5,000 ms       |
| UpdateFromQuery | 1 ms           | 1 ms           | 1 ms           |
