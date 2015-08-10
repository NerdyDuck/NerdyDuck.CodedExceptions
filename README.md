# NerdyDuck.CodedExceptions
## Exceptions with custom HRESULTs for .NET 4.5+

### Motivation
In any larger software project, clearly identifying errors is very important.
It helps customer service representatives to communicate with customers seeking for help, or forwarding bugs to the developers.
Using a knowledgebase to find help for a problem is much easier if you can use a unique identifier as a search pattern instead of a (possibly even localized) error message.

Since .NET 1.0, the [`System.Exception`](https://msdn.microsoft.com/en-us/library/System.Exception.aspx) class and its derived classes contained the `HResult` property to provide a unique error identifier.
Unfortunately, the framework uses it mainly for operating system related error codes only, so it is usually set to its default, 0.
As it had no publicly accessible setter, and the exception classes had no public constructor that allowed setting `HResult`, it was of little use for a developer to identify the exceptions in his own code, unless you did it the hard way, by overwriting the property entirely.
In .NET 4.5, things were improved, because `Exception.HResult` got a protected setter, which allowed developers to modify the property.
Still, there are no constructors to set the property, so developers are forced to create their own exceptions, which may mean a lot of work.

To leverage the new behavior, and relieve the developer's burden, this library aims to provide a set of the most common exceptions, with constructors allowing to set the `HResult` value.
In addition, it provides tools to create standardized error codes, and to discover if an exception is able to provide custom error codes.

### The project
#### Platforms
The project is aimed to be used with **Universal Windows Platform (UWP)** projects, as well as **classic desktop applications**.
To use the full potential of both platforms, while avoiding redundant duplication of code, the source code of all classes resides in the UWP project (*CodedExceptionsUniversal*) and is linked into the desktop project (*CodedExceptionsDesktop*), with the exception of the classes in the *Configuration* namespace, which are stored in the desktop project.
Differences between the platforms (mainly the ability to serialize classes in desktop applications) are included using preprocessor directives and the *WINDOWS_DESKTOP* and *WINDOWS_UWP* conditional compilation symbols.

#### Namespaces
Most classes reside in the *NerdyDuck.CodedExceptions* namespace.
Exception related to I/O operation can be found in the *NerdyDuck.CodedExceptions.IO* namespace.
The 'NerdyDuck.CodedExceptions.Configuration* namespace contains types to override facility identifiers (see [Using the library](#HowTo))

#### Types
Exceptions that represent the "enhanced" version of an existing framework exception are prefixed with "Coded".
If possible, the classes are derived from its corresponding framework exceptions, so it is easy to catch them even without the knowing the derived class.
The following code would catch a `CodedFormatException` as well as a `FormatException`:

```csharp
try
{
    // Your code here
}
catch (FormatException ex)
{
    // Do the error handling
}
```

#### Documentation
The *CodedExceptionsDoc* project uses [Sandcastle help file builder](https://shfb.codeplex.com/) (SHFB) to compile a help website, a compiled help manual (*.chm), and markdown pages for the wiki on GitHub.

#### Other projects in the solution
*CodedExceptionsNuGet* creates a NuGet package, (using the [NuGet Packager extension](https://visualstudiogallery.msdn.microsoft.com/daf5c6db-386b-4994-bdd7-b6cd52f11b72)) including the library for both the UWP and the desktop platforms, as well as the CHM help manual.
*CodedExceptionTestsDesktop* and *CodedExceptionsTestsUniversal* provide complete unit tests for both platforms.

## <a name="HowTo"></a>Using the library
You can either compile the libraries yourself (see below), or download the NuGet package from https://www.myget.org/F/nerdyduck-staging/api/v2 .
The package references the library appropriate for your project's platform and adds a source file named *Errors.cs* to your project.
Sorry, currently only C# is supported, but I'm sure you can port the code into the language of your choice.
Also, the package adds a line of code to your *AssemblyInfo.cs/.vb* file (at least two languages supported!).
The first thing to do, if you want to use standardized HRESULT values, is to modify the new line in your *AssemblyInfo* file.

```csharp
// [assembly: NerdyDuck.CodedExceptions.AssemblyFacilityIdentifier(0x0000)]
```

Uncomment the line, and change the argument of the `AssemblyFacilityIdentifierAttribute` to a value between 0 and 2047.
This value is used to fill the lower bits of the higher two bytes of the HRESULT value if you use the `Errors.CreateHResult` method.
An example:

*AssemblyInfo.cs*
```csharp
[assembly: NerdyDuck.CodedExceptions.AssemblyFacilityIdentifier(0x0305)]
```

*In your code:*
```csharp
int i = Errors.CreateHResult(0x42);
// Value of i is 0xa3050042
// All custom HRESULT values begin with 0xa, followed by the facility identifier (0x305), and the individual error code (0x0042).
```

See [here](https://msdn.microsoft.com/en-us/library/cc231198.aspx) for more information on standard-conform HRESULT values.

Finally, an example on how to use the resulting HResult value in an exception:
```csharp
using NerdyDuck.CodedExceptions;

public double Divide(double dividend, double divisor)
{
    if (divisor == 0.0)
    {
        throw new CodedArgumentOutOfRangeException(Errors.CreateHResult(0x17), "divisor", divisor, "Divisor may not be 0.");
    }
    return dividend / divisor;
}
```

But there is no explicit need to adhere to Microsoft's HRESULT format. If you prefer simple numbers, just use the exceptions without using the `Errors` class (you can simply delete the file if you don't plan to use it):

```csharp
using NerdyDuck.CodedExceptions;

public double Divide(double dividend, double divisor)
{
    if (divisor == 0.0)
    {
        throw new CodedArgumentOutOfRangeException(42, "divisor");
    }
    return dividend / divisor;
}
```

If you need to know whether the exception may include a custom error code, include the *NerdyDuck.CodedExceptions* namespace in a `using` directive, and use the extension method for the `Exception` class:

```csharp
using NerdyDuck.CodedExceptions;

...

try
{
    // Your code here
}
catch (Exception ex)
{
    if (ex.IsCodedException())
    {
        Console.WriteLine("Custom error code: 0x{0:x}", ex.HResult);
    }
}
```

If you need to determine if a value is a custom error code, and not a Microsoft exception, use the `HResultHelper.IsCustomHResult` method.

### Overriding facility identifiers
TODO
If you have two libraries that use the same facility identifier (maybe a third-party library, where you cannot simply change the attribute) you can change the facility identifier using the app.config.
For information about the usage, please consult the section for the *NerdyDuck.CodedExceptions.Configuration* namespace in the project wiki or the documentation created by the project.


## Compiling the solution
The libraries receive a strong name during compilation, but the strong name key (*NerdyDuck.snk*) is not part of the repository.
So, compiling the solution without modification will fail. You can either specify your own strong name key, or remove strong naming altogether.
In any case, remember to update the `[assembly: InternalsVisibleTo("...")]` line in the *AssemblyInfo.cs* file in the *CodedExceptionsUniversal* project.
If you do not have the [Sandcastle help file builder](https://shfb.codeplex.com/) or the (NuGet Packager extension)[https://visualstudiogallery.msdn.microsoft.com/daf5c6db-386b-4994-bdd7-b6cd52f11b72] installed, deactivate the projects, if Visual Studio did not do this autmatically when loading the solution.
After that, compiling should work.

## Creating own exceptions
If you derive a new exception from an exception in the library, or create a new coded exception from scratch, you should remember the following steps:
- Decorate your new exception with the `CodedExceptionAttribute` to make clear that the exception provides custom error codes.
- To adhere Microsoft's coding conventions, implement the default constructors for exceptions, in addition to the constructors requiring the HRESULT as argument: a parameterless constructor, a constructor requiring a message, and a constructor requiring a message and an inner exception.
- If you create an exception for desktop applications, decorate the exception with the `SerializableAttribute`, create an appropriate constructor, and override the `GetObjectData` method, if necessary.
