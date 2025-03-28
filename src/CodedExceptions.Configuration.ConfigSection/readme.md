# NerdyDuck.CodedExceptions.Configuration.ConfigSection

This is an add-on package for the [NerdyDuck.CodedExceptions](https://www.nuget.org/packages/NerdyDuck.CodedExceptions) package.
It allows the configuration of some aspects of the library using the [Microsoft.Extensions.Configuration.IConfiguration](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration) interface.

## Getting started

The package is only useful if you use the *NerdyDuck.CodedExceptions* libraries, which provide a framework for creating and handling exceptions with a facility and error code.
It can be installed via [NuGet](https://www.nuget.org/packages/NerdyDuck.CodedExceptions).

## Usage

After installation, add the following sections to your appsettings.json file:
```json
{
  "nerdyDuck": {
	"codedExceptions": {
	  "facilityIdentifierOverrides": [
		{
		  "assemblyName": "Contoso.SomeLibrary",
		  "identifier": 42
		}
	  ],
	  "debugModes": [
		{
		  "assemblyName": "Contoso.AnotherLibrary"
		}
	  ]
	}
  }
}

```
At the start of your application, call the `LoadConfigurationSection` extension methods for `AssemblyDebugModeCache` and/or `AssemblyFacilityOverrideCache` to apply the configuration:
```csharp

// If you use one of the `IHostBuilder` implementations, you will not need to create the configuration object, as it is already available in the `HostBuilderContext`.
IConfiguration config = new ConfigurationBuilder().AddJsonFile(@"appsettings.json").Build();

_ = NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.LoadConfigurationSection(config.GetSection("nerdyDuck/debugModes"));
_ = NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideCache.Global.LoadConfigurationSection(config.GetSection("nerdyDuck/facilityIdentifierOverrides"));
```

## Additional documentation

Find the full documentation, including class reference, at [GitHub.io](https://nerdyduck.github.io/CodedExceptions).

## Feedback

You can leave feedback, or open issues at the project's [GitHub repository](https://github.com/NerdyDuck/NerdyDuck.CodedExceptions).