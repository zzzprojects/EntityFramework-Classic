# Licensing

## Community Version
The community version doesn't require licensing and can be used forever including commercial application.

## Evaluation Period
- You can evaluate the library for several months before purchasing it.
- The latest version always contains a trial that expires at the **end of the month**. 
- You can extend your trial for several months by downloading the latest version at the begining of every month.

## How can I purchase the library?
- You can purchase the library [here](pricing)
- Upon purchase, you will receive an email with a license name and a license key.
- Make sure to check your **SPAM** folder if you don't receive the license within 24h.

## How can I get a free license for a personal or academic purpose?
We don't offer free licenses.

However, you can benefit from all the prime features for personal or academic projects for free by downloading the trial at the beginning of every month.

## Setup License from config file
The license name and key can be added directly in the app.config or web.config file in the appSettings section.


```csharp
<appSettings>
	<add key="Z_EntityFramework_Classic_LicenseName" value="[licenseName]"/>
	<add key="Z_EntityFramework_Classic_LicenseKey" value="[licenseKey]"/>
</appSettings>
```

## Setup License from appsettings.json file (.NET Core)
The license name and key can be added directly in the appsettings.json.

```csharp
{
  "Z.EntityFramework.Classic": {
    "LicenseName": "[licenseName]",
    "LicenseKey": "[licenseKey]"
  }
}
```

## Setup License from code
The license can be added directly in the code of your application. Make sure to follow recommendations about where to add this code.

```csharp
Z.EntityFramework.Classic.EntityFrameworkManager.AddLicense([licenseName], [licenseKey]);
```

Try it: [NET Framework](https://dotnetfiddle.net/yvFFQU) | [NET Core](https://dotnetfiddle.net/KdhZt3)

### Recommendations
- **Web App:** Use Application_Start in global.asax to activate your license.
- **WinForm App:** Use the main thread method to activate your license.
- **Win Service:** Use the OnStart method to activate your license

> Add the license before the first call to the library. Otherwise, the library will be enabled using the evaluation period.

## How can I check if my license is valid?
You can use the **ValidateLicense** method to check if the current license is valid or not.


```csharp
string licenseErrorMessage;
if (!Z.EntityFramework.Classic.EntityFrameworkManager.ValidateLicense(out licenseErrorMessage))
{
    throw new Exception(licenseErrorMessage);
}
```

Try it: [NET Framework](https://dotnetfiddle.net/yvFFQU) | [NET Core](https://dotnetfiddle.net/KdhZt3)
