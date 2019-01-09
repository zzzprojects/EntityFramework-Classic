# Query Include Filter

## Description

The **EF Query Include Filter** feature let you to filter related entities that will be included.

For example, you want to load a blog and include related posts, but only related posts that are not soft deleted.

```csharp
var blogs = ctx.Blogs.IncludeFilter(x => x.Posts.Where(y => !y.IsSoftDeleted)).ToList();
```
[Coming soon](#)

This feature allows you to handle various scenario such as:
- [Exclude soft deleted entities](#exclude-soft-deleted-entities)
- [Include with security access](#include-with-security-access)
- [Include paginated entities](#include-paginated-entities)

### Download
To use this feature, you need to download the following [NuGet Package](https://www.nuget.org/packages/Z.EntityFramework.Plus.QueryIncludeFilter.EFClassic/)

It's planned in 2019 to improve the **Query Include Filter** feature, remove some limitations, and integrated the code directly in Entity Framework Classic package.

## Getting Started

### Include one level
To filter with the `IncludeFilter` method, you need to specify the path as you do with the `Include` method and use a LINQ `Where` clause. Some other LINQ methods could also be used such as `Take`, `Skip`, and more.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// LOAD blogs and related active posts.
var blogs = ctx.Blogs.IncludeFilter(x => x.Posts.Where(y => !y.IsSoftDeleted)).ToList();
```
[Coming soon](#)

### Include multiple levels
To filter multiple levels, you need to use the `IncludeFilter` on every level, not only the last one, unlike the `Include` method.

In this example, we performed an `IncludeFilter` on the `Post` level, and one on the `Comments` level.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// LOAD blogs and related active posts and comments.
var blogs = ctx.Blogs.IncludeFilter(x => x.Posts.Where(y => !y.IsSoftDeleted))
                     .IncludeFilter(x => x.Posts.Where(y => !y.IsSoftDeleted)
                                                .Select(y => y.Comments.Where(z => !z.IsSoftDeleted)))
                     .ToList();
```
[Coming soon](#)

> The limitations to include every level will be removed when the feature will be integrated into **Entity Framework Classic**.

### Include chaining
You can chain multiple `IncludeFilter` methods one after another but you cannot mixte it with other include methods such as `Include`, `AlsoInclude`, `ThenInclude`, `IncludeOptimized`.

If you need to include without a filter, you can still use the `IncludeFilter` method.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// LOAD blogs and related active posts and comments.
var blogs = ctx.Blogs.IncludeFilter(x => x.Posts.Where(y => !y.IsSoftDeleted))
                     .IncludeFilter(x => x.Posts.Where(y => !y.IsSoftDeleted)
                                                .Select(y => y.Comments
                                                              .Where(z => !z.IsSoftDeleted)))
                     .ToList();
```
[Coming soon](#)

> The limitation to chain only with `IncludeFilter` method will be removed when the feature will be integrated into **Entity Framework Classic**.

## Real Life Scenarios

### Exclude soft deleted entities
You need to load a blog and include related posts and comments, but only related posts and comments that are not soft deleted.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// LOAD blogs and related active posts and comments.
var blogs = ctx.Blogs.IncludeFilter(x => x.Posts.Where(y => !y.IsSoftDeleted))
                     .IncludeFilter(x => x.Posts.Where(y => !y.IsSoftDeleted)
                                                .Select(y => y.Comments
                                                              .Where(z => !z.IsSoftDeleted)))
                     .ToList();
```
[Coming soon](#)

### Include with security access
You need to load a post and include related comments, but only related comments the current role have access.

```csharp
// myRoleID = 1; // Administrator

// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

// LOAD posts and available comments for the role level.
var posts= ctx.Posts.IncludeFilter(x => x.Comments.Where(y => y.RoleID >= myRoleID))
                    .ToList();
```
[Coming soon](#)

### Include paginated entities
You need to load a post and include related comments, but only the first 10 related comments sorted by views.

```csharp
```
[Coming soon](#)

## Documentation

### Extension Methods

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `IncludeFilter<TEntityType, TRelatedEntity>(this IQueryable<TEntityType> query, Expression<Func<TEntityType, IEnumerable<TRelatedEntity>>> filter)` | An `IQueryable<TEntityType>` extension method that includes and filter a collection of related entities. | [Coming soon](#) |
| `IncludeFilter<TEntityType, TRelatedEntity>(this IQueryable<TEntityType> query, Expression<Func<TEntityType, TRelatedEntity>> filter)` | An `IQueryable<TEntityType>` extension method that includes and filter a single related entities. | [Coming soon](#) |

## Limitations

 - Cannot be mixed with projection
 - Cannot be mixed with Include
 - Cannot be mixed with IncludeOptimized
 - Cannot be used with `AsNoTracking`
 - Cannot be used with `Many to Many` relationships
 - Cannot filter entities already loaded
 
 > Most of those limitations will be removed when the **Query Include Filter** code will be integrated  directly in **Entity Framework Classic**.

### Cannot filter entities already loaded
If an entity is already part of the `ChangeTracker` (the context), it's impossible to exclude it even with the `IncludeFilter`. That's how the `ChangeTracker` work.

```csharp
// using Z.EntityFramework.Plus; // Don't forget to include this.
var ctx = new EntitiesContext();

ctx.Comments.ToList();

// The posts automatically contain all comments even without using the "Include" method.
ctx.Posts.ToList();

// Trying to load only one comment will obviously not work either.
ctx.Posts.IncludeFilter(q => q.Comments.Take(1)).ToList();
```

[Coming soon](#)

In this case, we recommend to create and load entities from a new `DbContext`.
