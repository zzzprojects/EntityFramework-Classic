# Query Filter

## Description

Filter entities returned by Query.

The QueryFilter features modify the SQL generated to include a filter but also use the [QueryResultFilter](/query-result-filter) to ensure the result is filtered.

It's always recommended to use the QueryFilter over the QueryResultFilter whenever you can to reduce the number of rows returned from the Database. However, the QueryResultFilter might also be useful if your filter cannot be translated into an expression.

### Example

```csharp
var context = new EntitiesContext();
context.Configuration.QueryFilter<Post>(x => !x.IsSoftDeleted);

// SELECT * FROM Post WHERE IsSoftDeleted = false
var list = ctx.Posts.ToList();
```
