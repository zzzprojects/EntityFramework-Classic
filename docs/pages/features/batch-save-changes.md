# Batch SaveChanges

## Description
The **Batch SaveChanges** feature allows you to reduce the number of database roundtrip by internally batching multiple commands in the same commands when saving your entities.

```csharp
// context.SaveChanges();    
context.BatchSaveChanges();    
```
[Try it](https://dotnetfiddle.net/dJK5Vr)

> HINT: We recommand to always use `BatchSaveChanges` over `SaveChanges` or enable the option `UseBatchForSaveChanges`.

## Performance Comparison

| Operations      | 1,000 Entities | 2,000 Entities | 5,000 Entities |
| :-------------- | -------------: | -------------: | -------------: |
| SaveChanges     | 1,200 ms       | 2,400 ms       | 6,000 ms       |
| BatchSaveChanges| 100 ms         | 200 ms         | 500 ms          |

> HINT: Performance may differ from a database to another. A lot of factors might affect the benchmark time such as index, column type, latency, throttling, etc.

### Why BatchSaveChanges is faster then SaveChanges?
For both methods, the same SQL Syntax to perform the save operation is used.

So if you have 10 rows to insert:
- `SaveChanges`: Will execute one database roundtrip for every row. So 10 database round-trip will be performed.
- `BatchSaveChanges`: Will batch all SQL in one command and will perform one database round-trip. So 1 database round-trip will be performed.

## Getting Started

### Use BatchSaveChanges
```csharp
// context.SaveChanges();    
context.BatchSaveChanges();    
```
[Try it](https://dotnetfiddle.net/PQHDLC)

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
[Try it](https://dotnetfiddle.net/SQ58gU)

## Documentation

###### Properties

| Name | Description | Default | Example |
| :--- | :---------- | :-----: | :------ |
| `IsEnabled` | Gets or sets if the `BatchSaveChanges` feature is enabled. When disabled, a `SaveChanges` will be performed instead. | `true` | [Try it](https://dotnetfiddle.net/jo6QN1) |
| `UseBatchForSaveChanges` | Gets or sets if all `SaveChanges` call should be replaced internally by `BatchSaveChanges`. If you own a commercial license, we recommend to always set this value to true. | `false` | [Try it](https://dotnetfiddle.net/ceeM0J) |

## Limitations
- All providers that don't support multi statements such as SQL Compact and Effort will automatically use `SaveChanges` instead.
