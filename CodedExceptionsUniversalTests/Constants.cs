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
		public const int CustomHResult = unchecked((int)0xa7ff1234);
		public const int MicrosoftHResult = unchecked((int)0x87ff1234);
		public const string CustomHResultString = "0xa7ff1234";
        public const string TestMessage = "[TestMessage]";
		public const string ParamName = "parameter1";
	}
}
