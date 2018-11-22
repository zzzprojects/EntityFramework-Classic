# Bulk SaveChanges (Enterprise Feature)

## Description
BulkSaveChanges method is the upgraded version of `SaveChanges`.

This feature is provided by <a href="http://entityframework-extensions.net/" target="_blank">Z.EntityFramework.Extensions</a> that's used by more than 2000 customers all over the world.

All changes made in the context are persisted in the database but way faster by reducing the number of database round-trip required!

BulkSaveChanges supports everything:

- Association (One to One, One to Many, Many to Many, etc.)
- Complex Type
- Enum
- Inheritance (TPC, TPH, TPT)
- Navigation Property
- Self-Hierarchy
- Etc.

```csharp
context.Customers.AddRange(listToAdd); // add
context.Customers.RemoveRange(listToRemove); // remove
listToModify.ForEach(x => x.DateModified = DateTime.Now); // modify

// Easy to use
context.BulkSaveChanges();

// Easy to customize
context.BulkSaveChanges(bulk => bulk.BatchSize = 100);
```

[Try it](https://dotnetfiddle.net/1JFvZe)

## Purpose
Using the `ChangeTracker` to detect and persist changes automatically is great! However, it leads very fast to some problems when multiple entities need to be saved.

`SaveChanges` method makes a database round-trip for every change. So if you need to insert 10000 entities, then 10000 database round-trips will be performed which is INSANELY slow.

`BulkSaveChanges` works exactly like `SaveChanges` but reduces the number of database round-trips required to greatly help improve the performance.

## Performance Comparisons

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,000 ms       | 2,000 ms       | 5,000 ms       |
| BulkSaveChanges | 90 ms          | 150 ms         | 350 ms         |
