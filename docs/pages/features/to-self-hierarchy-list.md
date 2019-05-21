# ToSelfHierarchyList

## Description

The `ToSelfHierarchyList` method extend your Entity Framework `DbContext` to let you easily include a self hierarchy relationship.

```csharp
var employees = context.Employees.Where(x => x.Name.StartsWith("Employee_"))
	.ToSelfHierarchyList(x => x.Boss);
```
[Try it](https://dotnetfiddle.net/RPc9ag)

## Real Life Scenarios

- [Include boss, and add them in the returned list](#include-boss-and-add-them-in-the-returned-list)
- [Include boss, and filter them](#include-boss-and-filter-them)
- [Include boss, but only direct one](#include-boss-but-only-direct-one)
- [Include boss, but with custom mapping](#include-boss-but-with-custom-mapping)
- [Include boss, but with an inverse navigation](#include-boss-but-with-an-inverse-navigation)

### Include boss, and add them in the returned list
Like the `Include` method, by default, the entity from the hierarchy is materialized but not part of the returned list.

The option `FlatListRecursionLevel` let you include boss in returned list. Use `Int.MaxValue` to return all levels.
- `FlatListRecursionLevel = 0`: will return employee only.
- `FlatListRecursionLevel = 1`: will return employee with direct boss.
- `FlatListRecursionLevel = 2`: will return employee with direct boss and their boss.

```csharp
var employees = context.Employees.Where(x => x.Name.StartsWith("Employee_"))
	.ToSelfHierarchyList(x => x.Boss, 
		options => options.FlatListRecursionLevel = 1);
```
[Try it](https://dotnetfiddle.net/IDXEKV)

### Include boss, and filter them
The employee can be filtered as you normally do within Entity Framework. 

The `SelfHierarchyQuery` option let you filter the query that retrieve boss.

```csharp
// The "Boss_2" will not be retrieved
var employees = context.Employees.Where(x => x.Name.StartsWith("Employee_"))
	.ToSelfHierarchyList(x => x.Boss, options => 
		options.SelfHierarchyQuery = q => q.Where(x => !x.Name.Contains("2")));
```
[Try it](https://dotnetfiddle.net/Sl92lm)

### Include boss, but only direct one
By default, the library make 10 recursions when retriving the boss hierarchy.

The `MaxRecursion` option let you limit the number of recursion. By specifying a `MaxRecursion = 1`, you only retrieve direct boss of your employee.

```csharp
// The CEO will not be retrieved
var employees = context.Employees.Where(x => x.Name.StartsWith("Employee_"))
	.ToSelfHierarchyList(x => x.Boss, options => 
		options.MaxRecursion = 1);
```
[Try it](https://dotnetfiddle.net/PwnmRp)

### Include boss, but with custom mapping
If your entity doesn't has navigation property toward boss or employee, it's impossible to use the join expression.

The `ColumnMappings` option let you specify yourself the mapping. Careful, the column name and not the property name must be used.

```csharp
var employees = context.Employees.Where(x => x.Name.StartsWith("Employee_"))
	.ToSelfHierarchyList(null, options => {
		options.ColumnMappings.Add("BossID", "EmployeeID");
		options.FlatListRecursionLevel = int.MaxValue;
	});
```
[Try it](https://dotnetfiddle.net/CMWRpU)

### Include boss, but with an inverse navigation
If your entity has only a reference to a list of employee and no navigation property toward the boss, it's impossible to use the `JoinExpression` to include the boss.

The `InverseMapping` option let you specify a join expression toward employee but to inverse it. So instead to retrieve their employees, you will retrieve the boss.

```csharp
var employees = context.Employees.Where(x => x.Name.StartsWith("Employee_"))
	.ToSelfHierarchyList(x => x.Employees, options => {
		options.InverseMapping = true;
		options.FlatListRecursionLevel = int.MaxValue;
	});
```
[Try it](https://dotnetfiddle.net/HmRBgB)

## Documentation

### ToSelfHierarchyList

###### Methods

| Name | Description | Example |
| :--- | :---------- | :------ |
| `ToSelfHierarchyList(Expression<Func<T, object>> joinExpression)` | Materialize a list of entity and include the self hierarchy. | [Try it](https://dotnetfiddle.net/woE71l) |
| `ToSelfHierarchyList(Expression<Func<T, object>> joinExpression, Action<SelfHierarchyListOptions<T>> options)` | Materialize a list of entity and include the self hierarchy. | [Try it](https://dotnetfiddle.net/sThJ7K) |


##### Options
| Name | Description | Example |
| :--- | :---------- | :------ |
| `ColumnMappings` | Gets or sets the column mappings. | [Try it](https://dotnetfiddle.net/eQCHEe) |
| `FlatListRecursionLevel` | Gets or sets the flat list recursion level. Default = 0 which return only item from the query. | [Try it](https://dotnetfiddle.net/052avY) |
| `InverseMapping` | Gets or sets a value indicating whether the mapping is inversed. | [Try it](https://dotnetfiddle.net/zte9Uw) |
| `JoinExpression` | Gets or sets the join expression. | [Try it](https://dotnetfiddle.net/HE8Nzz) |
| `MaxRecursion` | Gets or sets the maximum recursion to perform. Default = 10. | [Try it](https://dotnetfiddle.net/YA2C3g) |
| `SelfHierarchyQuery` | Gets or sets the filtered self hierarchy query to use. | [Try it](https://dotnetfiddle.net/ddz55Q) |

## Limitations

- Support SQL Server only
