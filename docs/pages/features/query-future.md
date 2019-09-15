# QueryFuture

## Description
Every time an immediate method like **ToList** or **FirstOrDefault** is invoked on a query, a database round trip is made to retrieve data. While most applications don't have performance issues with making multiple round trips, batching multiple queries into one can be critical for some heavy traffic applications scalability.

**EF Classic Query Future** opens up all batching future queries features for Entity Framework users.

To batch multiple queries, simply append **Future** or **FutureValue** method to the query. All future queries will be stored in a pending list, and when the first future query requires a database round trip, all queries will be resolved in the same SQL command.

### Provider Supported
- SQL Server

## Future

### Example
```csharp
// CREATE a pending list of future queries
var customers = context.Customers.Future();
var ActiveCustomers = context.Customers.Where(x => x.IsActive).Future();

// TRIGGER all pending queries in one database round trip			
FiddleHelper.WriteTable("Customers", customers.ToList());		
FiddleHelper.WriteTable("Active Customers", ActiveCustomers);			
```
Try it: [NET Framework](https://dotnetfiddle.net/DoWJ3t) | [NET Core](https://dotnetfiddle.net/vMGvuy)

## Documentation

### QueryFutureManager

###### Properties

| Name | Description | Default | Example |
| :--- | :---------- | :-----: | :------ |
| `IsEnabled` | Gets or sets if the `QueryFuture` feature is enabled. | true | [NET Framework](https://dotnetfiddle.net/n6VcfE) / [NET Core](https://dotnetfiddle.net/ezWALV) |

### QueryFutureEnumerable<TEntityType>
  
The `QueryFutureEnumerable<TEntityType>` inherit from the `IEnumerable<T>` interfaces.

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `ToArrayAsync()` | Converts this object to an array asynchronous. | [NET Framework](https://dotnetfiddle.net/pg1rnj) / [NET Core](https://dotnetfiddle.net/MljKoE) |
| `ToArrayAsync(CancellationToken cancellationToken)` | Converts this object to an array asynchronous. | [NET Framework](https://dotnetfiddle.net/RBEL7B) / [NET Core](https://dotnetfiddle.net/EbmY1e) |
| `ToListAsync()` | Converts this object to a list asynchronous. | [NET Framework](https://dotnetfiddle.net/JYXOMi) / [NET Core](https://dotnetfiddle.net/DGqyL1) |
| `ToListAsync(CancellationToken cancellationToken)` | Converts this object to a list asynchronous. | [NET Framework](https://dotnetfiddle.net/RQkqUb) / [NET Core](https://dotnetfiddle.net/enqN1i) |
  
### QueryFutureValue<TResult>
  
###### Properties

| Name | Description | Default | Example |
| :--- | :---------- | :-----: | :------ |
| `Value` | Gets the value of the future query. | null | [NET Framework](https://dotnetfiddle.net/ObIY3v) / [NET Core](https://dotnetfiddle.net/xjEqrX) |

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `ValueAsync()` | Gets the value of the future query. | [NET Framework](https://dotnetfiddle.net/hJ6VrE) / [NET Core](https://dotnetfiddle.net/r920Fj) |
| `ValueAsync(CancellationToken cancellationToken)` | Gets the value of the future query. | [NET Framework](https://dotnetfiddle.net/sgbK5g) / [NET Core](https://dotnetfiddle.net/COg8Wi) |
