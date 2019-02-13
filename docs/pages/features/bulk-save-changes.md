# Bulk SaveChanges

## Description
The **EF Bulk SaveChanges** feature let you save thousands of entities in your database efficiently.

This feature is provided by the library [EF Extensions](https://entityframework-extensions.net/bulk-savechanges) _(Included with EF Classic)_. EF Extensions it's used by over 2000 customers all over the world and support all Entity Framework version (EF4, EF5, EF6, EF Core, EF Classic).

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

## Performance Comparison

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,200 ms       | 2,400 ms       | 6,000 ms       |
| BulkSaveChanges | 175 ms         | 325 ms         | 750 ms         |
| BulkSaveChanges(false) | 125 ms  | 200 ms         | 450 ms         |

[Try it](https://dotnetfiddle.net/Ad1bmZ)

> HINT: Performance may differ from a database to another. A lot of factors might affect the benchmark time such as index, column type, latency, throttling, etc.

### Why BulkSaveChanges is faster than SaveChanges?

Using the `ChangeTracker` to detect and persist changes automatically is great! However, it leads very fast to some problems when multiple entities need to be saved.

`SaveChanges` method makes a database round-trip for every change. So if you need to insert 10000 entities, then 10000 database round-trips will be performed which is INSANELY slow.

`BulkSaveChanges` works exactly like `SaveChanges` but reduces the number of database round-trips required to help significantly improve the performance.

### Why BulkSaveChanges(false) is faster than BulkSaveChanges?
The `BulkSaveChanges` methods use a lot of method coming from `Entity Framework`. When passing `false` in parameter, some logic such as identity propagation use custom logic that has been optimized. Learn more about [EF Extensions - Improve BulkSaveChanges](https://entityframework-extensions.net/improve-bulk-savechanges)

## Learn more

More documentation can be found here: [EF Extensions - Bulk SaveChanges](https://entityframework-extensions.net/bulk-savechanges)
