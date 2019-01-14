# LINQ Dynamic

## Description
You can execute query dynamically through free extensions provided by the library [Eval-Expression.NET](http://eval-expression.net/).

This feature is available for free in the EF Classic Community.

### Predicate

All LINQ predicate methods are supported.

```csharp
var list = context.Customers.Where(x => "x.IsActive").ToList();
var list2 = context.Customers.Where(x => "x.IsActive == IsActive", new { IsActive = false }).ToList();
```
[Try it](https://dotnetfiddle.net/GTttpq)

###### Deferred Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `Where` | Filters a sequence of values based on a predicate using a dynamic expression. | [Try it](https://dotnetfiddle.net/QhVfRW) |

###### Immediate Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `All` | Determines whether all elements of a sequence satisfy a condition using a dynamic expression. | [Try it](https://dotnetfiddle.net/YCT73M) |
| `Any` | Determines whether any element of a sequence exists or satisfies a condition using a dynamic expression. | [Try it](https://dotnetfiddle.net/vEbwLr) |
| `Count` | Returns the number of elements in a sequence using a dynamic expression. | [Try it](https://dotnetfiddle.net/v8rqKV) |
| `First` | Returns the first element of a sequence using a dynamic expression. | [Try it](https://dotnetfiddle.net/CfxUKL) |
| `FirstOrDefault` | Returns the first element of a sequence, or a default value if no element is found using a dynamic expression. | [Try it](https://dotnetfiddle.net/UX3Ymb) |
| `LongCount` | Returns an Int64 that represents the number of elements in a sequence using a dynamic expression. | [Try it](https://dotnetfiddle.net/4xrM1d) |
| `Single` | Returns a single, specific element of a sequence using a dynamic expression. | [Try it](https://dotnetfiddle.net/onW4hW) |
| `SingleOrDefault` | Returns a single, specific element of a sequence, or a default value if that element is not found using a dynamic expression. | [Try it](https://dotnetfiddle.net/nU97uw) |

## Order & Select

All LINQ selector and order are supported. Most of them require the "Dynamic" suffix to not override default behavior (Ordering or selecting by a string is valid).

```csharp
var list = context.Customers.OrderByDescendingDynamic(x => "x.Name").ToList();
```
[Try it](https://dotnetfiddle.net/Fwjgin)

###### Deferred  Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `OrderByDescendingDynamic` | Sorts the elements of a sequence in descending order using a dynamic expression. | [Try it](https://dotnetfiddle.net/doNrVQ) |
| `OrderByDynamic` | Sorts the elements of a sequence in ascending order using a dynamic expression. | [Try it](https://dotnetfiddle.net/rzKycR) |
| `SelectMany` | Projects each element of a sequence to an IEnumerable<T> and flattens the resulting sequences into one sequence using a dynamic expression. | [Try it](https://dotnetfiddle.net/KLF5e7) |
| `SelectDynamic` | Projects each element of a sequence into a new form using a dynamic expression. | [Try it](https://dotnetfiddle.net/YE83om) |
| `ThenByDescendingDynamic` | Performs a subsequent ordering of the elements in a sequence in descending order using a dynamic expression. | [Try it](https://dotnetfiddle.net/8FxroD) |
| `ThenByDynamic` | Performs a subsequent ordering of the elements in a sequence in ascending order using a dynamic expression. | [Try it](https://dotnetfiddle.net/pVCcRf) |

## Execute

The Execute method is the LINQ Dynamic ultimate methods which let you evaluate and execute a dynamic expression and return the result.

```csharp
var list = context.Customers.Execute<IEnumerable<Customer>>("Where(x => x.IsActive == true)").ToList();
var list2 = context.Customers.Execute<IEnumerable<Customer>>("Where(x => x.IsActive == IsActive)", new { IsActive = false }).ToList();
```
[Try it](https://dotnetfiddle.net/7S3JS0)

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `Execute` | Execute LINQ dynamic using an expression. | [Try it](https://dotnetfiddle.net/z1jIkv) |
| `Execute<TResult>` | Execute LINQ dynamic using an expression. | [Try it](https://dotnetfiddle.net/jgOyFi) |
