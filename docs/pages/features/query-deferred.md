# Query Deferred

## Description

There are two types of IQueryable extension methods:

- Deferred Methods: The query expression is modified but the query is not resolved (Select, Where, etc.).
- Immediate Methods: The query expression is modified and the query is resolved (Count, First, etc.).

However, some features like **Query Cache** and **Query Future** cannot be used directly with Immediate Method since the query is already resolved.

**Query Deferred** provides more flexibility to other features.

### All LINQ IQueryable extension methods and overloads are supported:

 - DeferredAggregate
 - DeferredAll
 - DeferredAny
 - DeferredAverage
 - DeferredContains
 - DeferredCount
 - DeferredElementAt
 - DeferredElementAtOrDefault
 - DeferredFirst
 - DeferredFirstOrDefault
 - DeferredLast
 - DeferredLastOrDefault
 - DeferredLongCount
 - DeferredMax
 - DeferredMin
 - DeferredSequenceEqual
 - DeferredSingle
 - DeferredSingleOrDefault
 - DeferredSum

---

# The following documentation is about EF+ since `FromCache` feature has not beed added yet on EF Classic. A better documentation should be soon available related to EF Classic.

```csharp
// Oops! The query is already executed, we cannot cache it.
var count = ctx.Customers.Count();

// Oops! All customers are cached instead of the customer count.
var count = ctx.Customers.FromCache().Count();
```

Here comes in play the deferred query which acts exactly like deferred methods, by modifying the query expression without resolving it.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// The count is deferred and cached.
var count = ctx.Customers.DeferredCount().FromCache();

```

## EF+ Query Deferred

Defer the execution of a query which is normally executed to allow some features like Query Cache and Query Future.

{% include template-example.html %} 
```csharp

// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// Query Cache
ctx.Customers.DeferredCount().FromCache();

// Query Future
ctx.Customers.DeferredCount().FutureValue();

```

## EF+ Query Deferred Execute

Execute the deferred query and return the result.

```csharp

// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

var countDeferred = ctx.Customers.DeferredCount();
var count = countDeferred.Execute();

```

## EF+ Query Deferred Execute Async

Execute the Deferred query asynchronously and return the result.

**ExecuteAsync** methods are available starting from .NET Framework 4.5 and support all the same options than **Execute** methods.

```csharp

// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

var countDeferred = ctx.Customers.DeferredCount();
var taskCount = countDeferred.ExecuteAsync();

```

## Real Life Scenarios

EF Query Deferred brings advantages to other third party features:

 - Allows to use Immediate Method with EF+ Query Cache.
 - Allows to use Immediate Method with EF+ Query Future.
 - Allows to use Immediate Method with YOUR own features.
