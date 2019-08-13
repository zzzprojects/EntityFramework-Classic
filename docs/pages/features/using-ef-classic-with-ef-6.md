# Using EF Classic with EF 6

## Description
In some special scenario, you might want to use EF Classic in the same project as EF6.

By default, it doesn't work since both libraries are the same library (Entity Framework) so share similar code which leads to reference conflict.

To make it work, you need to:
- Use an extern alias in one of both libraries
- Use another config section for entity framework classic

## Extern Alias
Using [extern alias](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/extern-alias) allow solving namespace issue when two libraries use the same fully-qualified type names.

Usually, you choose one of them to keep the `global` alias (can be accessed without specifying the extern alias) and you specify an alias for the other. You can also use an alias for both libraries.

We recommend following alias name:
- Entity Framework 6: `EF6`
- Entity Framework Classic: `EFClassic`

Once an alias has been specified, you need to use it in your using directive.

### Example EF6
```csharp
extern alias EF6;
using EF6::System.Data.Entity;
```

### Example EF Classic
```csharp
extern alias EFClassic;
using EFClassic::System.Data.Entity;
```

## Config Section
We recommend to use the `entityFrameworkClassic` section name in your config file.

```csharp
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
    <section name="entityFrameworkClassic" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, Z.EntityFramework.Classic, Version=7.0.0.0, Culture=neutral, PublicKeyToken=afc61983f100d280" requirePermission="false" />
  </configSections>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
  </startup>
  <entityFramework>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
  </entityFramework>
  <entityFrameworkClassic>
    <providers>
       <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, Z.EntityFramework.Classic.SqlServer" />
    </providers>
  </entityFrameworkClassic>
</configuration>
```

and specify the name to the `EntityFrameworkManager` for EF Classic:

```csharp
EntityFrameworkManager.ConfigSectionName = "entityFrameworkClassic";
```

## Demo
You can find a demo here: https://github.com/zzzprojects/EntityFramework-Classic/tree/master/demo
