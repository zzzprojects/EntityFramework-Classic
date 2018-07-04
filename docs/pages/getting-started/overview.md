# Overview

## Introduction
Entity Framework Classic is the supported EF6 Fork that's compatible with .NET Standard. The mission is to overcome current EF limitations by adding tons of must-haves built-in features.

## Requirements
- .NET Framework 4.0 or higher
- .NET Standard 2.0 or higher

## Limitations
Only the SQL Server provider is supported currently, but we plan to add providers through the year.

## Installing
Download the following NuGet Package:

## Config Change

A new library name, a new assembly name, a new public Token!

If you are using an existing config file, you will need to change some reference:

- `EntityFramework` assembly name to `Z.EntityFramework.Classic`
- `EntityFramework.SqlServer` assembly name to `Z.EntityFramework.Classic.SqlServer`
- `PublicKeyToken=b77a5c561934e089` public token to `PublicKeyToken=afc61983f100d280`

### Before
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
  </configSections>
  <entityFramework>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="mssqllocaldb" />
      </parameters>
    </defaultConnectionFactory>
  </entityFramework>
</configuration>
```

### After
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=afc61983f100d280" requirePermission="false" />
  </configSections>
  <entityFramework>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, Z.EntityFramework.Classic.SqlServer" />
    </providers>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, Z.EntityFramework.Classic">
      <parameters>
        <parameter value="mssqllocaldb" />
      </parameters>
    </defaultConnectionFactory>
  </entityFramework>
</configuration>
```

## Code First Migrations & .NET Core

Unfortunately, migrations tools for .NET Core is not yet supported by our library. However, you can create a side project in .NET Framework to generate the migrations.

## Database First & .NET Core

You can use your Database First Model with your .NET Core project. However, since the `EntityDeploy` is not part of .NET Core, you will need to specify your model name:

```csharp
EntityFramework.EntityFrameworkManager.UseDatabaseFirst("ModelName.edmx");
```
