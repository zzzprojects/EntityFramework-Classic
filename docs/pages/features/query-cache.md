# Query Cache

## Description

Caching entities or query results to improve an application's performance is a very frequent scenario.

Caching is very simple to understand, the first time a query is invoked, data are retrieved from the database and stored in the memory before being returned. All future calls will retrieve data from the memory to avoid making additional database round trips which drastically increases an application's performance.

To use caching, simply append to the query "Cache" method before using an immediate resolution method like "ToList()" or "FirstOrDefault()".

## Query Cache

Return the query result from the cache. If the query is not cached yet, it will be materialized and cached before being returned.

```csharp
// The first call perform a database round trip
var countries1 = context.Countries.Cache().ToList();

// Subsequent calls will take the value from the memory instead
var countries2 = context.Countries.Cache().ToList();
```

Try it: [NET Framework](https://dotnetfiddle.net/lXIiex) | [NET Core](https://dotnetfiddle.net/7BX0pc)

## Query Cache Async

Return the query result from the cache. If the query is not cached yet, the query will be materialized asynchronously and cached before being returned.

**CacheAsync** methods are available starting from .NET Framework 4.5 and support all the same options as "Cache" methods.

```csharp
// The first call perform a database round trip
var countries1 = await context.Countries.CacheAsync().ConfigureAwait(false);

// Subsequent calls will take the value from the memory instead
var countries2 = await context.Countries.CacheAsync().ConfigureAwait(false);
```

Try it: [NET Framework](https://dotnetfiddle.net/r7JqVE) | [NET Core](https://dotnetfiddle.net/ZDHgex)

## Query Cache Query Deferred

Immediate resolution methods like **Count()** and **FirstOrDefault()** cannot be cached since their expected behavior is to cache the count result or only the first item.

```csharp
// Oops! The query is already executed, we cannot cache it.
var count = context.Customers.Count();

// Oops! All customers are cached instead of customer count.
var count = context.Customers.Cache().Count();
```

**Query Deferred** has been created to resolve this issue, the resolution is now deferred instead of being immediate which lets us cache the expected result.

```csharp
// The count is deferred, it is now cached.
var count = context.Countries.DeferredCount().Cache();

Console.WriteLine("Countries Count: " + count);
```
Try it: [NET Framework](https://dotnetfiddle.net/ouZ2wI) | [NET Core](https://dotnetfiddle.net/3n1pRZ)

Query Deferred supports all Queryable extension methods and overloads.

## Query Cache Tag & ExpireTag

Tagging the cache lets you expire later on all cached entries with a specific tag by calling the **ExpireTag** method.

For example, the daily countries & states importation has been completed and you need to refresh from the database all queries related to the country table. You can now simply expire the tag **countries** to remove all related cached entries.

```csharp
// Cache queries with related tags
var countries = context.Countries.Cache(new string[] { "countries", "states" });
var count = context.Countries.DeferredCount().Cache(new string[] { "countries", "states", "stats" });

FiddleHelper.WriteTable(countries);
Console.WriteLine("Countries Count: " + count);

// Expire all cache entries using the "countries" tag
QueryCacheManager.ExpireTag("countries");
```
Try it: [NET Framework](https://dotnetfiddle.net/bCEdyg) | [NET Core](https://dotnetfiddle.net/H44EaR)

## Query Cache Expiration

All common caching features like absolute expiration, sliding expiration, removed callback are supported.

```csharp
// Make the query expire after 2 hours of inactivity
var options = new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromHours(2)};

var countries = context.Countries.Cache(options).ToList();			
FiddleHelper.WriteTable(countries);
```
Try it: [NET Framework](https://dotnetfiddle.net/i2VOeg) | [NET Core](https://dotnetfiddle.net/D4d0pv)

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
Try it: [NET Framework](https://dotnetfiddle.net/rKmdXl) | [NET Core](https://dotnetfiddle.net/x3hVfR)

## Redis Cache (Beta)
This feature is currently in beta, you can try it but be aware that some code such as parameter order might change when it is officially released.

```csharp
var connectionMultiplexer =  ConnectionMultiplexer.Connect("127.0.0.1:6379,allowAdmin=true");
QueryCacheManager.Cache = new RedisObjectCacheBeta(connectionMultiplexer.GetDatabase(), Newtonsoft.Json.JsonConvert.SerializeObject, (type, s) => Newtonsoft.Json.JsonConvert.DeserializeObject(s, type));
```


