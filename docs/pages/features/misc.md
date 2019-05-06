# Misc

## SQL Server && DateTime2
By default, Entity Framework still uses `DateTime` in the manifest.

Nowadays, it is recommended to use the column type `DateTime2` which has better precision.

You can force this behavior with the `UseDateTime2` options.

```csharp
EntityFrameworkManager.SqlServer.BackwardCompatibility.UseDateTime2 = true
```
