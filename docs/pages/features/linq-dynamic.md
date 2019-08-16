# LINQ Dynamic

## Description
You can execute query dynamically through free extensions provided by the library [Eval-Expression.NET](http://eval-expression.net/).

This feature is available for free in the EF Classic Community.

## Predicate

All LINQ predicate methods are supported.

```csharp
var list = context.Customers.Where(x => "x.IsActive").ToList();
var list2 = context.Customers.Where(x => "x.IsActive == IsActive", new { IsActive = false }).ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/GTttpq) | [NET Core](https://dotnetfiddle.net/Exa0zS)

###### Deferred Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `Where` | Filters a sequence of values based on a predicate using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/QhVfRW) / [NET Core](https://dotnetfiddle.net/z8t5wV) |

###### Immediate Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `All` | Determines whether all elements of a sequence satisfy a condition using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/YCT73M) / [NET Core](https://dotnetfiddle.net/XrG83V) |
| `Any` | Determines whether any element of a sequence exists or satisfies a condition using a dynamic expression. |[NET Framework](https://dotnetfiddle.net/vEbwLr) / [NET Core](https://dotnetfiddle.net/Gh9OSM) |
| `Count` | Returns the number of elements in a sequence using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/v8rqKV) / [NET Core](https://dotnetfiddle.net/ox7EFW) |
| `First` | Returns the first element of a sequence using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/CfxUKL) / [NET Core](https://dotnetfiddle.net/gW1CqX) |
| `FirstOrDefault` | Returns the first element of a sequence, or a default value if no element is found using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/UX3Ymb) / [NET Core](https://dotnetfiddle.net/3ZlZuq) |
| `LongCount` | Returns an Int64 that represents the number of elements in a sequence using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/4xrM1d) / [NET Core](https://dotnetfiddle.net/fc6TLH) |
| `Single` | Returns a single, specific element of a sequence using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/onW4hW) / [NET Core](https://dotnetfiddle.net/SHPNY8) |
| `SingleOrDefault` | Returns a single, specific element of a sequence, or a default value if that element is not found using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/nU97uw) / [NET Core](https://dotnetfiddle.net/S07cJB) |

## Order & Select

All LINQ selector and order are supported. Most of them require the "Dynamic" suffix to not override default behavior (Ordering or selecting by a string is valid).

```csharp
var list = context.Customers.OrderByDescendingDynamic(x => "x.Name").ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/Fwjgin) | [NET Core](https://dotnetfiddle.net/by9NRe)

###### Deferred  Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `OrderByDescendingDynamic` | Sorts the elements of a sequence in descending order using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/doNrVQ) / [NET Core](https://dotnetfiddle.net/zt3MEa) |
| `OrderByDynamic` | Sorts the elements of a sequence in ascending order using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/rzKycR) / [NET Core](https://dotnetfiddle.net/9GvILu) |
| `SelectMany` | Projects each element of a sequence to an IEnumerable<T> and flattens the resulting sequences into one sequence using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/KLF5e7) / [NET Core](https://dotnetfiddle.net/toMh6j) |
| `SelectDynamic` | Projects each element of a sequence into a new form using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/YE83om) / [NET Core](https://dotnetfiddle.net/X9uPDb) |
| `ThenByDescendingDynamic` | Performs a subsequent ordering of the elements in a sequence in descending order using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/8FxroD) / [NET Core](https://dotnetfiddle.net/Kd2WQY) |
| `ThenByDynamic` | Performs a subsequent ordering of the elements in a sequence in ascending order using a dynamic expression. | [NET Framework](https://dotnetfiddle.net/pVCcRf) / [NET Core](https://dotnetfiddle.net/9pyiEV) |

## Execute

The Execute method is the LINQ Dynamic ultimate methods which let you evaluate and execute a dynamic expression and return the result.

```csharp
var list = context.Customers.Execute<IEnumerable<Customer>>("Where(x => x.IsActive == true)").ToList();
var list2 = context.Customers.Execute<IEnumerable<Customer>>("Where(x => x.IsActive == IsActive)", new { IsActive = false }).ToList();
```
Try it: [NET Framework](https://dotnetfiddle.net/7S3JS0) | [NET Core](https://dotnetfiddle.net/u2HVih)

###### Methods
| Name | Description | Example |
| :--- | :---------- | :------ |
| `Execute` | Execute LINQ dynamic using an expression. | [NET Framework](https://dotnetfiddle.net/z1jIkv) / [NET Core](https://dotnetfiddle.net/FU2FsS) |
| `Execute<TResult>` | Execute LINQ dynamic using an expression. | [NET Framework](https://dotnetfiddle.net/jgOyFi) / [NET Core](https://dotnetfiddle.net/YgaB4Y) |
