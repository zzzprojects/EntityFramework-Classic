# Community

## Description
Like Entity Framework 6, the EF Classic community version is 100% free, can be used in commercial application, and is royalty free. The enterprise version only has additional features.

When using EF Classic as the free version, it's recommended (optional) to enable the community version to throw an error when an enterprise feature is used. 

## Setup IsCommunity from config file
The community version can be forced directly in the app.config or web.config file in the appSettings section.


```csharp
<appSettings>
	<add key="Z_EntityFramework_Classic_IsCommunity" value="true"/>
</appSettings>
```

## Setup IsCommunity from appsettings.json file
The community version can be forced directly in the appsettings.json.

```csharp
{
  "Z.EntityFramework.Classic": {
    "IsCommunity": "true",
  }
}
```

## Setup IsCommunity from code
The community version can be forced directly in the code of your application. Make sure to follow recommendations about where to add this code.

```csharp
Z.EntityFramework.Classic.EntityFrameworkManager.IsCommunity = true;
```
