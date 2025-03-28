# NerdyDuck.CodedExceptions.Configuration.Json

This is an add-on package for the [NerdyDuck.CodedExceptions](https://www.nuget.org/packages/NerdyDuck.CodedExceptions) package.
It allows the configuration of some aspects of the library using JSON files.

## Getting started

The package is only useful if you use the *NerdyDuck.CodedExceptions* libraries, which provide a framework for creating and handling exceptions with a facility and error code.
It can be installed via [NuGet](https://www.nuget.org/packages/NerdyDuck.CodedExceptions).

## Usage

After installation, add the following sections to your debugMode.json (or other JSON) file:
```json
{
  "Contoso.SomeLibrary" : true,
  "Contoso.AnotherLibrary" : false
}
```

For assembly identifier overrides, add the following sections to your facilityIdentifierOverrides.json (or other JSON) file:
```json
{
  "Contoso.SomeLibrary" : 42
}
```

At the start of your application, call the `LoadConfigurationSection` extension methods for `AssemblyDebugModeCache` and/or `AssemblyFacilityOverrideCache` to apply the configuration:
```csharp
_ = NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.LoadJson("debugMode.json");
_ = NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideCache.Global.LoadJson("facilityIdentifierOverrides.json");
```

## Additional documentation

Find the full documentation, including class reference, at [GitHub.io](https://nerdyduck.github.io/CodedExceptions).

## Feedback

You can leave feedback, or open issues at the project's [GitHub repository](https://github.com/NerdyDuck/NerdyDuck.CodedExceptions).