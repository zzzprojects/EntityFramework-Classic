# PostgreSQL Provider

## Description
You can use PostgreSQL provider by adding a reference to the NuGet package.

NuGet: Z.EntityFramework.Classic.Npgsql

## Configuration

- In the defaultConnectionFactory: `EntityFramework6.Npgsql` must be replace by `Z.EntityFramework.Classic.Npgsql`
- In the provider: `EntityFramework6.Npgsql` must be replaced by `Z.EntityFramework.Classic.Npgsql`

From

```csharp
<entityFramework>
	<defaultConnectionFactory type="Npgsql.NpgsqlConnectionFactory, EntityFramework6.Npgsql" />
	<providers>
		<provider invariantName="Npgsql" type="Npgsql.NpgsqlServices, EntityFramework6.Npgsql" />
	</providers>
</entityFramework>
```

To

```csharp
<entityFramework>
	<defaultConnectionFactory type="Npgsql.NpgsqlConnectionFactory, Z.EntityFramework.Classic.Npgsql" />
	<providers>
		<provider invariantName="Npgsql" type="Npgsql.NpgsqlServices, Z.EntityFramework.Classic.Npgsql" />
	</providers>
</entityFramework>
```

## .NET Core
Because .NET Core doesn't have a config file, you must add the `NpgsqlEFConfiguration` to resolve dependency differently.

### Solution 1
Adding the DbConfigurationTypeAttribute on the context class:

```csharp
[DbConfigurationType(typeof(NpgsqlEFConfiguration))]
public class EntityContext : DbContext
{
	// ...code...
}
```

### Solution 2
Calling `DbConfiguration.SetConfiguration(new NpgsqlEFConfiguration())` at the application start up.

