#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.CodedExceptions - Unit tests for the
 * NerdyDuck.CodedExceptions assembly
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) Daniel Kopp, dak@nerdyduck.de
 *
 * All rights reserved.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ******************************************************************************/
#endregion

using System.Diagnostics.CodeAnalysis;
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
