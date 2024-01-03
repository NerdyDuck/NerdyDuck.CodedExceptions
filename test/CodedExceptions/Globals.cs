// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Global test values.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Globals
{
	public const int COR_E_EXCEPTION = unchecked((int)0x80131500);
	public const int COR_E_ARGUMENT = unchecked((int)0x80070057);
	public const int COR_E_ARGUMENTOUTOFRANGE = unchecked((int)0x80131502);
	public const int COR_E_NULLREFERENCE = unchecked((int)0x80004003);
	public const int COR_E_INVALIDCAST = unchecked((int)0x80004002);
	public const int COR_E_NOTSUPPORTED = unchecked((int)0x80131515);
	public const int COR_E_SYSTEM = unchecked((int)0x80131501);
	public const int COR_E_FORMAT = unchecked((int)0x80131537);
	public const int COR_E_INVALIDOPERATION = unchecked((int)0x80131509);
	public const int COR_E_SERIALIZATION = unchecked((int)0x8013150C);
	public const int COR_E_TIMEOUT = unchecked((int)0x80131505);
	public const int COR_E_DIRECTORYNOTFOUND = unchecked((int)0x80070003);
	public const int COR_E_IO = unchecked((int)0x80131620);
	public const int COR_E_FILENOTFOUND = unchecked((int)0x80070002);
	public const int COR_E_TYPELOAD = unchecked((int)0x80131522);
	public const int E_FAIL = unchecked((int)0x80004005);
	public const int DataHResult = unchecked((int)0x80131920);
	public const int XmlHResult = unchecked((int)0x80131940);
	public const int CustomHResult = unchecked((int)0xa7ff1234);
	public const int MicrosoftHResult = unchecked((int)0x87ff1234);
	public const string CustomHResultString = "0xa7ff1234";
	public const string TestMessage = "[TestMessage]";
	public const string ParamName = "parameter1";
	public const string DirectoryName = "c:\\temp";
	public const string FileName = @"c:\temp\test.dat";
	public const string DefaultToStringFormat = "{0}: ({1}) {2}";

	public static readonly Assembly ThisAssembly = typeof(Globals).Assembly;
	public static readonly AssemblyName ThisAssemblyName = ThisAssembly.GetName();
	public static readonly string ThisAssemblyNameString = ThisAssembly.FullName;
	public static readonly Assembly OtherAssembly = typeof(Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute).Assembly;
}
