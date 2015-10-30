# NerdyDuck.CodedExceptions
#### Exceptions with custom HRESULTs for .NET 4.6+ and UAP

This project provides a library of classes derived from [`System.Exception`](https://msdn.microsoft.com/en-us/library/System.Exception.aspx) that offer constructors to set the `HResult` property with a custom value.
It also includes helper classes to create standardized HRESULT values compliant to Microsoft's usage of HRESULT. See [here](https://msdn.microsoft.com/en-us/library/cc231198.aspx) for more information.

#### Platforms
- .NET Framework 4.6 or newer for desktop applications
- Universal App Platform (UAP) or Universal Windows Platform (UWP) 10.0 (Windows 10) or newer for Windows Store Apps and [Windows 10 IoT](https://dev.windows.com/en-us/iot)

#### Languages
The neutral resource language for all texts is English (en-US). Currently, the only localization available is German (de-DE). If you like to add other languages, feel free to send a pull request with the translated resources!

#### How to get
- Use the NuGet package from my [MyGet](https://www.myget.org) feed: [https://www.myget.org/F/nerdyduck-release/api/v2](https://www.myget.org/F/nerdyduck-release/api/v2). If you need to debug the library, get the debug symbols from my [SymbolSource](http://www.symbolsource.org) feed: [http://srv.symbolsource.org/pdb/MyGet/nerdyduck/c789556a-0301-4af8-8f5b-a3bbc806645d](http://srv.symbolsource.org/pdb/MyGet/nerdyduck/c789556a-0301-4af8-8f5b-a3bbc806645d).
- Download the binaries from the [Releases](../../releases/) page.
- You can clone the repositoy and compile the libraries yourself (see the [Wiki](../../wiki/) for requirements).

#### More information
For examples and a complete class reference, please see the [Wiki](../../wiki/).

#### History
#####TBD / v1.1.0 / DAK
- Added `CodedArgumentNullOrEmptyException` and `CodedArgumentNullOrWhiteSpaceException`.
- Changed version numbers to align file versions with NuGet version.
- Changed target platform of desktop library to .NET 4.6.
- Added missing AssemblyMetadataAttribute to UAP assemblies.
- On release compilation, output files are signed with a SPC.
- Replaced manually updated Resources.cs by T4 template. Added new build targets to UAP project to run the template at every build. VS Modelling SDK required.
- Fixed localization errors.

#####2015-09-24 / v1.0.1 / DAK
- Fixed bug in desktop library resource generation.
- Added documentation.
- Added example projects.
- Changed project URL in NuGet package to point to GitHub.

#####2015-08-27 / v1.0.0 / DAK
- Initial release. No class documentation yet.
