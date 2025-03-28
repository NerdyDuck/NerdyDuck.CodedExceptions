# NerdyDuck.CodedExceptions

This package provides a library of classes derived from [`System.Exception`](https://docs.microsoft.com/en-us/dotnet/api/system.exception) that offer constructors to set the `HResult` property with a custom value.
It also includes helper classes to create standardized HRESULT values compliant to Microsoft's usage of HRESULT. See [here](https://msdn.microsoft.com/en-us/library/cc231198.aspx) for more information.

## Getting started

The library contains exception classes derived from the most commonly used exception types, like `System.Exception`, `System.ArgumentException`, `System.IO.IOException`, and many more.
To use the library, simply install it via [NuGet](https://www.nuget.org/packages/NerdyDuck.CodedExceptions).

## Usage

The exceptions work like their base classes, but offer additional constructors to set the `HResult` property.
```csharp
try
{
	// Do something that throws an exception
}
catch (Exception ex)
{
	throw new CodedException(42,"An error occurred", ex);
	// ex.HResult will be 42
}
```

If you want to use standardized HRESULT values, you can use the `HResult` class, that is added to the default namespace of your project by the assembly.
To set the facility identifier for your assembly, add an `AssemblyFacilityIdentifierAttribute` to your *AssemblyInfo.cs*.
```csharp
using NerdyDuck.CodedExceptions;
[assembly: AssemblyFacilityIdentifier(15)] // Must be between 1 and 2047
```

Then you can use the `HResult` class to create standardized HRESULT values.
```csharp
try
{
	// Do something that throws an exception
}
catch (Exception ex)
{
	throw new CodedException(HResult.Create(42), "An error occurred", ex);
	// ex.HResult will be 0xa0ff002a
}
```

You can also use enumerations to create standardized HRESULT values.
```csharp
public enum MyErrors
{
	InvalidParameter = 42
}

try
{
	// Do something that throws an exception
}
catch (Exception ex)
{
	throw new CodedException(HResult.Create(MyErrors.InvalidParameter), "An error occurred", ex);
	// ex.HResult will be 0xa0ff002a
}
```

The package also includes the possibility to override the facility identifiers for other assemblies, and to set debug mode flags for assemblies.

## Additional documentation

Find the full documentation, including class reference, at [GitHub.io](https://nerdyduck.github.io/CodedExceptions).

## Feedback

You can leave feedback, or open issues at the project's [GitHub repository](https://github.com/NerdyDuck/NerdyDuck.CodedExceptions).