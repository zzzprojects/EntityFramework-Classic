# MySql Provider

## Description
You can use MySql provider by adding a reference to the NuGet package.

NuGet: https://www.nuget.org/packages/Z.EntityFramework.Classic.MySql/

## Configuration

- In the defaultConnectionFactory: `EntityFramework` must be replace by `Z.EntityFramework.Classic`
- In the provider: `MySql.Data.Entity.EF6, Version=6.8.3.0, Culture=neutral, PublicKeyToken=afc61983f100d280` must be replaced by `Z.EntityFramework.Classic.MySql, Version=7.0.0.0, Culture=neutral, PublicKeyToken=afc61983f100d280`

From

```csharp
<entityFramework>
	<defaultConnectionFactory type="MySql.Data.Entity.MySqlConnectionFactory, EntityFramework"></defaultConnectionFactory>
	<providers>
		<provider invariantName="MySql.Data.MySqlClient" type="MySql.Data.MySqlClient.MySqlProviderServices, MySql.Data.Entity.EF6, Version=6.8.3.0, Culture=neutral, PublicKeyToken=afc61983f100d280"></provider>
	</providers>
</entityFramework>
```

To

```csharp
<entityFramework>
	<defaultConnectionFactory type="MySql.Data.Entity.MySqlConnectionFactory, Z.EntityFramework.Classic"></defaultConnectionFactory>
	<providers>
		<provider invariantName="MySql.Data.MySqlClient" type="MySql.Data.MySqlClient.MySqlProviderServices, Z.EntityFramework.Classic.MySql, Version=7.0.0.0, Culture=neutral, PublicKeyToken=afc61983f100d280"></provider>
	</providers>
</entityFramework>
```
