# LINQ Dynamic

## Description
You can execute query dynamically through free extensions provided by our library [Eval-Expression.NET](http://eval-expression.net/) Library.

This feature is available for free in the EF Classic Community.

### Predicate

All LINQ predicate methods are supported. A string expression which return a Boolean function can be used as parameter.

 - Deferred
 - SkipWhile
 - TakeWhile
 - Where
 - Immediate
 - All
 - Any
 - Count
 - First
 - FirstOrDefault
 - Last
 - LastOrDefault
 - LongCount
 - Single
 - SingleOrDefault

{% include template-example.html %} 
```csharp
var list = context.Customers.Where(x => "x.IsActif").ToList();
var list2 = context.Customers.Where(x => "x.IsActif == isActif", new { isActif = false }).ToList();
```
[Try it](https://dotnetfiddle.net/GTttpq)

## Order && Select

All LINQ selector and order are supported. Most of them require the "Dynamic" suffix to not override default behavior (Ordering or selecting by a string is valid).

 - OrderByDescendingDynamic
 - OrderByDynamic
 - SelectDynamic
 - SelectMany
 - ThenByDescendingDynamic
 - ThenByDynamic

{% include template-example.html %} 
```csharp
var list = context.Customers.OrderByDescendingDynamic(x => "x.Name").ToList();
```
[Try it](https://dotnetfiddle.net/Fwjgin)

## Execute

The Execute method is the LINQ Dynamic ultimate methods which let you evaluate and execute a dynamic expression and return the result.

 - Execute
 - Execute< TResult >

```csharp
var list = context.Customers.Execute<IEnumerable<Customer>>("Where(x => x.IsActif == true)").ToList();
var list2 = context.Customers.Execute<IEnumerable<Customer>>("Where(x => x.IsActif == isActif)", new { isActif = false }).ToList();
```
[Try it](https://dotnetfiddle.net/7S3JS0)

