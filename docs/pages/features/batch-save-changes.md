# Batch SaveChanges

## Description
The **Batch SaveChanges** feature allows you to reduce the number of database round-trips by internally batching multiple commands in the same command when saving your entities.

```csharp
// context.SaveChanges();    
context.BatchSaveChanges();    
```
Try it: [NET Framework](https://dotnetfiddle.net/dJK5Vr) | [NET Core](https://dotnetfiddle.net/nRotN4)

> HINT: We recommend to always use `BatchSaveChanges` over `SaveChanges` or enable the option `UseBatchForSaveChanges`.

## Performance Comparison

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,200 ms       | 2,400 ms       | 6,000 ms       |
| BatchSaveChanges| 100 ms         | 200 ms         | 500 ms          |

Try it: [NET Framework](https://dotnetfiddle.net/2MDZQh) | [NET Core](https://dotnetfiddle.net/ouVK6Z)

> HINT: Performance may differ from a database to another. A lot of factors might affect the benchmark time such as index, column type, latency, throttling, etc.

### Why BatchSaveChanges is faster than SaveChanges?
For both methods, the same SQL Syntax to perform the save operation is used.

So if you have 10 rows to insert:
- `SaveChanges`: Will execute one database round-trip for every row. So 10 database round-trips will be performed.
- `BatchSaveChanges`: Will batch all SQL in one command and will perform one database round-trip. So, one database round-trip will be performed.

## Getting Started

### Use BatchSaveChanges
```csharp
// context.SaveChanges();    
context.BatchSaveChanges();    
```
Try it: [NET Framework](https://dotnetfiddle.net/PQHDLC) | [NET Core](https://dotnetfiddle.net/CFZhU9)

### Internally replace SaveChanges by BatchSaveChanges
```csharp
public class EntityContext : DbContext
{
    public EntityContext() : base(FiddleHelper.GetConnectionStringSqlServer())
    {
        this.Configuration.BatchSaveChanges.UseBatchForSaveChanges = true;
    }
    
    public DbSet<Customer> Customers { get; set; }
}

context.Customers.AddRange(customers);
context.SaveChanges();
```
Try it: [NET Framework](https://dotnetfiddle.net/SQ58gU) | [NET Core](https://dotnetfiddle.net/ciy7du)

## Documentation

###### Properties

| Name | Description | Default | Example |
| :--- | :---------- | :-----: | :------ |
| `IsEnabled` | Gets or sets if the `BatchSaveChanges` feature is enabled. When disabled, a `SaveChanges` will be performed instead. | `true` | [NET Framework](https://dotnetfiddle.net/jo6QN1) / [NET Core](https://dotnetfiddle.net/NqAJ1Q) |
| `UseBatchForSaveChanges` | Gets or sets if all `SaveChanges` call should be replaced internally by `BatchSaveChanges`. If you own a commercial license, we recommend to always set this value to true. | `false` | [NET Framework](https://dotnetfiddle.net/ceeM0J) / [NET Core](https://dotnetfiddle.net/F4NEpM) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `BatchSaveChanges()` | Saves all changes made in this context to the underlying database by combining sql command generated. | [NET Framework](https://dotnetfiddle.net/mtICR7) / [NET Core](https://dotnetfiddle.net/uiFeW9) |
| `BatchSaveChangesAsync()` | Saves all changes asynchronously made in this context to the underlying database by combining sql command generated. | [NET Framework](https://dotnetfiddle.net/E8LJmC) / [NET Core](https://dotnetfiddle.net/wg4syB) |
| `BatchSaveChangesAsync(cancellationToken)` | Saves all changes asynchronously made in this context to the underlying database by combining sql command generated. | [NET Framework](https://dotnetfiddle.net/1PLKzr) / [NET Core](https://dotnetfiddle.net/MFO4J9) |

## Limitations
- All providers that don't support multi statements such as SQL Compact and Effort will automatically use `SaveChanges` instead.
