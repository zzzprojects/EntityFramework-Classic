# QueryFuture

## Description
Every time an immediate method like **ToList** or **FirstOrDefault** is invoked on a query, a database round trip is made to retrieve data. While most applications don't have performance issues with making multiple round trips, batching multiple queries into one can be critical for some heavy traffic applications for scalability.

**EF Classic Query Future** opens up all batching future queries features for Entity Framework users.

To batch multiple queries, simply append **Future** or **FutureValue** method to the query. All future queries will be stored in a pending list, and when the first future query requires a database round trip, all queries will be resolved in the same SQL command.

### Provider Supported
- SQL Server

## Future

### Example
```csharp
// CREATE a pending list of future queries
var customers = context.Customers.Future();
var actifCustomers = context.Customers.Where(x => x.IsActif).Future();

// TRIGGER all pending queries in one database round trip			
FiddleHelper.WriteTable("Customers", customers.ToList());		
FiddleHelper.WriteTable("Actif Customers", actifCustomers);		
```
[Try it](https://dotnetfiddle.net/DoWJ3t)
