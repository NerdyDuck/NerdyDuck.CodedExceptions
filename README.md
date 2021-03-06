# NerdyDuck.CodedExceptions
#### Exceptions with custom HRESULTs for .NET 4.6+, UWP and .NET Core 1.0

This project provides a library of classes derived from [`System.Exception`](https://msdn.microsoft.com/en-us/library/System.Exception.aspx) that offer constructors to set the `HResult` property with a custom value.
It also includes helper classes to create standardized HRESULT values compliant to Microsoft's usage of HRESULT. See [here](https://msdn.microsoft.com/en-us/library/cc231198.aspx) for more information.

#### Platforms
- .NET Framework 4.6 or newer for desktop applications
- Universal Windows Platform (UWP) 10.0 (Windows 10) or newer for Windows Store Apps and [Windows 10 IoT](https://dev.windows.com/en-us/iot)
- .NET Core 1.0 (netstandard1.6).

#### Languages
The neutral resource language for all texts is English (en-US). Currently, the only localization available is German (de-DE). If you like to add other languages, feel free to send a pull request with the translated resources!

#### How to get
- Use the NuGet package from my [MyGet](https://www.myget.org) feed: [https://www.myget.org/F/nerdyduck-release/api/v3/index.json](https://www.myget.org/F/nerdyduck-release/api/v3/index.json). If you need to debug the library, get the debug symbols from the same feed: [https://www.myget.org/F/nerdyduck-release/symbols/](https://www.myget.org/F/nerdyduck-release/symbols/).
- Download the binaries from the [Releases](../../releases/) page.
- You can clone the repository and compile the libraries yourself (see the [Wiki](../../wiki/) for requirements).

#### More information
For examples and a complete class reference, please see the [Wiki](../../wiki/).

#### Licence
The project is licensed under the [Apache License, Version 2.0](LICENSE).

#### History
#####2016-08-04 / 1.3.1 / DAK
- Universal project compiled against Microsoft.NETCore.UniversalWindowsPlatform 5.2.2 .

#####2016-08-02 / 1.3.0 / DAK
- Added new target platform .NET Core 1.0. Compiled against netstandard1.6 .
- Added new exceptions: `CodedInvalidCastException`, `CodedSocketException`, `InsufficientStorageSpaceException`.
- New project *CodedExceptionsCore* and unit test project *CodedExceptionsCoreTests*. New compilation symbol `NETCORE` used to for platform-specific code.
- All code files (except platform-specific files) moved to *CodedExceptionsCore* and *CodedExceptionsCoreTests* projects, because .xproj project type does not support links.
- Handling of resource files has changed. Source are now .resx files, that are copied and renamed to .resw to be used in the UWP project.
- Strong name key file `NerdyDuck.CodedExceptions.snk` and certificate `NerdyDuck.CodedExceptions.pfx` are now included in the project to make it easier to clone and compile.
- Signing of output assemblies with SPC certificate has moved from the library projects to the deploy project *CodedExceptionsDeploy*. Libraries will only be signed when the NuGet package is created and pushed (compiled as Release).

#####2016-04-12 / 1.2.1 / DAK
- Added `HResultHelper.GetEnumInt32Value(Enum)` and `HResultHelper.GetEnumUnderlyingType(Type)` to facilitate new method in Errors.cs: `Errors.CreateHResult(Enum)`.
- Switched internal error codes from integers to `ErrorCodes` enumeration.
- Universal project compiled against Microsoft.NETCore.UniversalWindowsPlatform 5.1.0 .

#####2016-04-06 / 1.2.0 / DAK
- Added `CodedTypeLoadException`.
- Added deployment project to compile all projects and create/push the NuGet package in one go. Removed separate NuGet project. Removes also dependency on NuGet Packager Template.
- Extracted file signing into its own reusable MSBuild target file.
- Extracted resource generation for desktop project into its own reusable MSBuild target file.
- Created a MSBuild target for automatic T4 transformations on build. Removes dependency on Visual Studio Modeling SDK.

#####2016-01-08 / v1.1.2 / DAK
- Changed target version for UWP library to Windows 10 build 10586; minimum version remains build 10240.
- Changed automatic signing of assemblies from post-compiler batch script to msbuild task.

#####2015-12-04 / v1.1.1 / DAK
- Restructured solution into separate folders for sources, tests, examples and deployment/documentation.
- Fixed bug in NuGet package that prevented the PRI file to be deployed by a UWP project referencing the package.
- Some cleanup

#####2015-11-03 / v1.1.0 / DAK
- Added `CodedArgumentNullOrEmptyException` and `CodedArgumentNullOrWhiteSpaceException`.
- Changed version numbers to align file versions with NuGet version.
- Changed target platform of desktop library to .NET 4.6.
- Added missing AssemblyMetadataAttribute to UAP assemblies.
- On release compilation, output files are signed with a SPC.
- Replaced manually updated Resources.cs by T4 template. Added new build targets to UAP project to run the template at every build. VS Modeling SDK required.
- Fixed localization errors.
- Completed unit tests. Test coverage > 98%

#####2015-09-24 / v1.0.1 / DAK
- Fixed bug in desktop library resource generation.
- Added documentation.
- Added example projects.
- Changed project URL in NuGet package to point to GitHub.

#####2015-08-27 / v1.0.0 / DAK
- Initial release. No class documentation yet.
