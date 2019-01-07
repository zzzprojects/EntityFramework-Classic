# LINQ Dynamic

## Description
You can execute query dynamically through free extensions provided by the library [Eval-Expression.NET](http://eval-expression.net/).

This feature is available for free in the EF Classic Community.

### Predicate

All LINQ predicate methods are supported. A string expression which return a Boolean function can be used as parameter.

###### Deferred Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `SkipWhile` | | [Coming soon](#) |
| `TakeWhile` | | [Coming soon](#) |
| `Where` | | [Coming soon](#) |

###### Immediate Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `All` | | [Coming soon](#) |
| `Any` | | [Coming soon](#) |
| `Count` | | [Coming soon](#) |
| `First` | | [Coming soon](#) |
| `FirstOrDefault` | | [Coming soon](#) |
| `Last` | | [Coming soon](#) |
| `LastOrDefault` | | [Coming soon](#) |
| `LongCount` | | [Coming soon](#) |
| `Single` | | [Coming soon](#) |
| `SingleOrDefault` | | [Coming soon](#) |

```csharp
var list = context.Customers.Where(x => "x.IsActive").ToList();
var list2 = context.Customers.Where(x => "x.IsActive == IsActive", new { IsActive = false }).ToList();
```
[Try it](https://dotnetfiddle.net/GTttpq)

## Order & Select

All LINQ selector and order are supported. Most of them require the "Dynamic" suffix to not override default behavior (Ordering or selecting by a string is valid).

###### Deferred  Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `OrderByDescendingDynamic` | | [Coming soon](#) |
| `OrderByDynamic` | | [Coming soon](#) |
| `SelectDynamic` | | [Coming soon](#) |
| `SelectManyDynamic` | | [Coming soon](#) |
| `ThenByDescendingDynamic` | | [Coming soon](#) |
| `ThenByDynamic` | | [Coming soon](#) |

```csharp
var list = context.Customers.OrderByDescendingDynamic(x => "x.Name").ToList();
```
[Try it](https://dotnetfiddle.net/Fwjgin)

## Execute

The Execute method is the LINQ Dynamic ultimate methods which let you evaluate and execute a dynamic expression and return the result.

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `Execute` | | [Coming soon](#) |
| `Execute<TResult>` | | [Coming soon](#) |

```csharp
var list = context.Customers.Execute<IEnumerable<Customer>>("Where(x => x.IsActive == true)").ToList();
var list2 = context.Customers.Execute<IEnumerable<Customer>>("Where(x => x.IsActive == IsActive)", new { IsActive = false }).ToList();
```
[Try it](https://dotnetfiddle.net/7S3JS0)

