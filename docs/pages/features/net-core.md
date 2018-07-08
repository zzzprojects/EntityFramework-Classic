# .NET Core Support

## Description

Even if Microsoft has created a new library named `EF Core`, we also added the support for the .NET Core to Entity Framework Classic since we believe that a lot of developers still prefer `EF6` over `EF Core`.

Here are some features in EF6 which are not supported in EF Core:

- EDMX/ Graphical Visualization of Model
- Entity Data Model Wizard (for DB-First approach)
- ObjectContext API
- Querying using Entity SQL.
- Automated Migration
- Inheritance: Table per type (TPT)
- Inheritance: Table per concrete class (TPC)
- Many-to-Many without join entity
- Entity Splitting
- Spatial Data
- Lazy loading of related data
- Stored procedure mapping with DbContext for CUD operation
- Seed data
- Automatic migration

_Reference: [Entity Framework Tutorial](http://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx#efcore-vs-ef6)_

## Limitations

### Database First & .NET Core

You can use your Database First Model with your .NET Core project. However, since the `EntityDeploy` is not part of .NET Core, you will need to change the [ModelName].edmx `Copy to Output Directory` to `Copy always` or `Copy if newer` and specify your model name:

```csharp
EntityFramework.EntityFrameworkManager.UseDatabaseFirst("ModelName.edmx");
```

You must also ensure that you use the model copied to the directory output

```csharp
// BAD
// <add name="Entities" connectionString="metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;..." providerName="System.Data.EntityClient" />

// Good
<add name="Entities" connectionString="metadata=.\Model.csdl|.\Model.ssdl|.\Model.msl;..." providerName="System.Data.EntityClient" />
```

### Migration
Migration is not yet available. You will need to create a .NET Framework project to generate migration files and add it to your .NET Core project.
