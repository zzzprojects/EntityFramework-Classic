# .NET Fiddle Support

## Description
.NET Fiddle is an online coding environment that features an active and rich community. It allows you to write, compile and execute code without having to install anything locally. Unlike snippet, it is not a fragment of code but rather a complete program which facilitates collaboration and sharing without having to worry about environment settings. For more information, you can consult the [Getting Started](https://dotnetfiddle.net/GettingStarted/) section.

[Try it](https://dotnetfiddle.net/)

## SQL Server Example
 .NET Fiddle doesn't support SQL Server yet.

However, this feature is under development by our company. We expect to be able to release a new version of .NET Fiddle that allows creating examples with SQL Server in September.

## SQL Server Compact Example
```csharp
// Custom configuration specific for .NET Fiddle
Z.EntityFramework.Classic.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

GenerateData();

using (var context = new EntityContext())
{
	var list = context.Customers.ToList();
	
	FiddleHelper.WriteTable("Customers", list);			
}
```

[Try it](https://dotnetfiddle.net/aw6MDk)

## Effort Example
 .NET Fiddle doesn't support Effort yet.

However, this feature is under development by our company. We expect to be able to release a new version of .NET Fiddle that allows creating examples with Effort in September.
