# Query Cache

## Description

Caching entities or query results to improve an application's performance is a very frequent scenario.

Caching is very simple to understand, the first time a query is invoked, data are retrieved from the database and stored in the memory before being returned. All future calls will retrieve data from the memory to avoid making additional database round trips which drastically increases an application's performance.

To use caching, simply append to the query "Cache" method before using an immediate resolution method like "ToList()" or "FirstOrDefault()".

```csharp
var ctx = new EntitiesContext();

// The first call perform a database round trip
var countries1 = ctx.Countries.FromCache().ToList();

// Subsequent calls will take the value from the memory instead
var countries2 = ctx.Countries.FromCache().ToList();
```

[Try it](https://dotnetfiddle.net/lXIiex)

## Query Cache

Return the query result from the cache. If the query is not cached yet, it will be materialized and cached before being returned.

{% include template-example.html %} 
```csharp
var ctx = new EntitiesContext();

// The query is cached using default QueryCacheManager options
var countries = ctx.Countries.Where(x => x.IsActive).FromCache();

var states = ctx.States.Where(x => x.IsActive).FromCache(DateTime.Now.AddHours(2));
```

## Query Cache Async

Return the query result from the cache. If the query is not cached yet, the query will be materialized asynchronously and cached before being returned.

**FromCacheAsync** methods are available starting from .NET Framework 4.5 and support all the same options as "FromCache" methods.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// The query is cached using default QueryCacheManager options
var task = ctx.Countries.Where(x => x.IsActive).FromCacheAsync();

var task = ctx.States.Where(x => x.IsActive).FromCacheAsync(DateTime.Now.AddHours(2));
```

## Query Cache Query Deferred

Immediate resolution methods like **Count()** and **FirstOrDefault()** cannot be cached since their expected behavior is to cache the count result or only the first item.

```csharp
// Oops! The query is already executed, we cannot cache it.
var count = ctx.Customers.Count();

// Oops! All customers are cached instead of customer count.
var count = ctx.Customers.FromCache().Count();
```

**Query Deferred** has been created to resolve this issue, the resolution is now deferred instead of being immediate which lets us cache the expected result.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// The count is deferred, it is now cached.
var count = ctx.Customers.DeferredCount().FromCache();
```

Query Deferred supports all Queryable extension methods and overloads.

## Query Cache Tag & ExpireTag

Tagging the cache lets you expire later on all cached entries with a specific tag by calling the **ExpireTag** method.

For example, the daily countries & states importation has been completed and you need to refresh from the database all queries related to the country table. You can now simply expire the tag **countries** to remove all related cached entries.

```csharp
var ctx = new EntitiesContext();

// Cache queries with related tags
var states = ctx.States.FromCache("countries", "states");
var stateCount = ctx.States.DeferredCount().FromCache("countries", "states", "stats");

// Expire all cache entries using the "countries" tag
QueryCacheManager.ExpireTag("countries");
```

## Query Cache Expiration

All common caching features like absolute expiration, sliding expiration, removed callback are supported.

```csharp
var ctx = new EntitiesContext();

// Make the query expire after 2 hours of inactivity

var options = new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromHours(2)};
var states = ctx.States.FromCache(options);
```

## Query Cache Control

Query Cache is very flexible and lets you have full control over the cache.

You can use your own cache:

```csharp
// Cache must inherit from Sytem.Runtime.Caching.ObjectCache
QueryCacheManager.Cache = MemoryCache.Default;
```

You can set default policy

```csharp
// The query is cached for 2 hours of inactivity
QueryCacheManager.CacheItemPolicyFactory = () => new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromHours(2) };
```
