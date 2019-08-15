# Overview

## Introduction
Entity Framework Classic is the supported EF6 Fork that's compatible with .NET Standard. The mission is to overcome current EF limitations by adding tons of must-haves built-in features.

## Requirements
- .NET Framework 4.0 or higher
- .NET Standard 2.0 or higher

## Limitations
Currently, only the SQL Server, SQL Compact, and Effort are supported. However, we plan to add providers throughout the year. 

## Installing
Download the [NuGet Package](/download)

## Config Change

A new library name, a new assembly name, a new public Token!

If you are using an existing config file, you will need to change some references:

- `EntityFramework` assembly name to `Z.EntityFramework.Classic`
- `EntityFramework.SqlServer` assembly name to `Z.EntityFramework.Classic.SqlServer`
- `Version=6.0.0.0` must be updated to `Version=7.0.0.0`
- `PublicKeyToken=b77a5c561934e089` public token to `PublicKeyToken=afc61983f100d280`

### Before
```csharp
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
```csharp
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, Z.EntityFramework.Classic, Version=7.0.0.0, Culture=neutral, PublicKeyToken=afc61983f100d280" requirePermission="false" />
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

Unfortunately, migrations tool for .NET Core is not yet supported by our library. However, you can create a side project in .NET Framework to generate the migrations.

## Database First & .NET Core

You can use your Database First Model with your .NET Core project. However, since the `EntityDeploy` is not part of .NET Core, you will need to change the [ModelName].edmx `Copy to Output Directory` to `Copy always` or `Copy if newer` and specify your model name:

```csharp
EntityFramework.EntityFrameworkManager.UseDatabaseFirst("ModelName.edmx");
```

You must also ensure that you use the model copied to the directory output.

```csharp
// BAD
// <add name="Entities" connectionString="metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;..." providerName="System.Data.EntityClient" />

// Good
<add name="Entities" connectionString="metadata=.\Model.csdl|.\Model.ssdl|.\Model.msl;..." providerName="System.Data.EntityClient" />
```

## Demo

You can find a very light demo using EF Classic here: https://github.com/zzzprojects/EntityFramework-Classic/tree/master/demo

You can find a lot of online examples using EF Classic here: https://entityframework-classic.net/online-examples
