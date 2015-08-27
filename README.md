# NerdyDuck.CodedExceptions
#### Exceptions with custom HRESULTs for .NET 4.5+ and UAP

This project provides a library of classes derived from [`System.Exception`](https://msdn.microsoft.com/en-us/library/System.Exception.aspx) that offer constructors to set the `HResult` property with a custom value. It also includes helper classes to create standardized HRESULT values compliant to Microsoft's usage of HRESULT. See [here](https://msdn.microsoft.com/en-us/library/cc231198.aspx) for more information.

#### Platforms
- .NET 4.5 framework or newer for desktop applications
- Universal App Platform (UAP) or Universal Windows Platform (UWP) 10.0 (Windows 10) or newer for Windows Store Apps and [Windows 10 IoT](https://dev.windows.com/en-us/iot)

#### Languages
The neutral resource language for all texts is English (en-US). Currently, the only localization available is German (de-DE). If you like to add other languages, feel free to send a pull request with the translated resources!

#### How to get
- Use the NuGet package from my [MyGet](https://www.myget.org) feed: [https://www.myget.org/F/nerdyduck-release/api/v2](https://www.myget.org/F/nerdyduck-release/api/v2])
- Download the binaries from the [Releases](../../releases/) page.
- You can clone the repositoy and compile the libraries yourself (see the [Wiki](../../wiki/) for requirements).

#### More information
For examples and a complete class reference, please see the [Wiki](../../wiki/).

#### History
2015-08-27 / v.1.0.0+build.2 / DAK - Initial release. No class documentation yet.
