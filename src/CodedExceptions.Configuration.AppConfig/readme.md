# NerdyDuck.CodedExceptions.Configuration.AppConfig

This is an add-on package for the [NerdyDuck.CodedExceptions](https://www.nuget.org/packages/NerdyDuck.CodedExceptions) package.
It allows the configuration of some aspects of the library using sections in app.config/web.config files.

## Getting started

The package is only useful if you use the *NerdyDuck.CodedExceptions* libraries, which provide a framework for creating and handling exceptions with a facility and error code.
It can be installed via [NuGet](https://www.nuget.org/packages/NerdyDuck.CodedExceptions).

## Usage

After installation, add the following sections to your app.config or web.config file:
```xml
<configuration>
  <configSections>
    <!-- Add this sectionGroup to the configSections element -->
    <sectionGroup name="nerdyDuck">
      <section name="codedExceptions" type="NerdyDuck.CodedExceptions.Configuration.CodedExceptionsSection, NerdyDuck.CodedExceptions.Configuration.AppConfig" allowDefinition="Everywhere" />
    </sectionGroup>
  </configSections>

  <nerdyDuck>
    <codedExceptions>
      <facilityIdentifierOverrides>
        <!-- Add assemblyName and identifier attributes to override the default facility identifier for the assembly -->
        <add assemblyName="Contoso.SomeLibrary" identifier="42" />
      </facilityIdentifierOverrides>
      <debugModes>
        <!-- Add assemblyName attribute to enable debug mode for a specific assembly -->
        <add assemblyName="Contoso.AnotherLibrary" />
      </debugModes>
    </codedExceptions>
  </nerdyDuck>
</configuration>
```

At the start of your application, call the `LoadApplicationConfiguration` extension methods for `AssemblyDebugModeCache` and/or `AssemblyFacilityOverrideCache` to apply the configuration:
```csharp
NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.LoadApplicationConfiguration();
NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideCache.Global.LoadApplicationConfiguration();
```

## Additional documentation

Find the full documentation, including class reference, at [GitHub.io](https://nerdyduck.github.io/CodedExceptions).

## Feedback

You can leave feedback, or open issues at the project's [GitHub repository](https://github.com/NerdyDuck/NerdyDuck.CodedExceptions).