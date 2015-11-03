#region Copyright
/*******************************************************************************
 * <copyright file="Constants.cs" owner="Daniel Kopp">
 * Copyright 2015 Daniel Kopp
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * </copyright>
 * <author name="Daniel Kopp" email="dak@nerdyduck.de" />
 * <assembly name="NerdyDuck.Tests.CodedExceptions">
 * Unit tests for NerdyDuck.CodedExceptions assembly.
 * </assembly>
 * <file name="Constants.cs" date="2015-08-13">
 * Constant values for recurring test scenarios.
 * </file>
 ******************************************************************************/
#endregion

namespace NerdyDuck.Tests.CodedExceptions
{
	/// <summary>
	/// Constant values for recurring test scenarios.
	/// </summary>
#if WINDOWS_DESKTOP
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
	public static class Constants
	{
		public const int COR_E_EXCEPTION = unchecked((int)0x80131500);
		public const int COR_E_ARGUMENT = unchecked((int)0x80070057);
		public const int COR_E_ARGUMENTOUTOFRANGE = unchecked((int)0x80131502);
		public const int COR_E_NULLREFERENCE = unchecked((int)0x80004003);
		public const int COR_E_NOTSUPPORTED = unchecked((int)0x80131515);
		public const int COR_E_SYSTEM = unchecked((int)0x80131501);
		public const int COR_E_FORMAT = unchecked((int)0x80131537);
		public const int COR_E_INVALIDOPERATION = unchecked((int)0x80131509);
		public const int COR_E_SERIALIZATION = unchecked((int)0x8013150C);
		public const int COR_E_TIMEOUT = unchecked((int)0x80131505);
		public const int COR_E_DIRECTORYNOTFOUND = unchecked((int)0x80070003);
		public const int COR_E_IO = unchecked((int)0x80131620);
		public const int COR_E_FILENOTFOUND = unchecked((int)0x80070002);
		public const int DataHResult = unchecked((int)0x80131920);
		public const int XmlHResult = unchecked((int)0x80131940);
		public const int CustomHResult = unchecked((int)0xa7ff1234);
		public const int MicrosoftHResult = unchecked((int)0x87ff1234);
		public const string CustomHResultString = "0xa7ff1234";
		public const string TestMessage = "[TestMessage]";
		public const string ParamName = "parameter1";
		public const string DirectoryName = "c:\\temp";
		public const string FileName = @"c:\temp\test.dat";
	}
}
