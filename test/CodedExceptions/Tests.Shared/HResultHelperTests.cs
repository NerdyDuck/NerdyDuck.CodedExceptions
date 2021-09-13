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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions;

namespace NerdyDuck.Tests.CodedExceptions
{
#if NET60
	namespace Net60
#elif NET50
	namespace Net50
#elif NETCORE31
	namespace NetCore31
#elif NET48
	namespace Net48
#endif
	{

		/// <summary>
		/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedException class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class HResultHelperTests
		{
			[TestMethod]
			public void GetFacilityId_Success()
			{
				int i = HResultHelper.GetFacilityId(Globals.CustomHResult);
				Assert.AreEqual(2047, i);
			}

			[TestMethod]
			public void GetErrorId_Success()
			{
				int i = HResultHelper.GetErrorId(Globals.CustomHResult);
				Assert.AreEqual(0x1234, i);
			}

			[TestMethod]
			public void GetBaseHResult_Success()
			{
				int i = HResultHelper.GetBaseHResult(0x7ff);
				Assert.AreEqual(unchecked((int)0xa7ff0000), i);
			}

			[TestMethod]
			public void IsCustomHResult_True()
			{
				Assert.IsTrue(HResultHelper.IsCustomHResult(Globals.CustomHResult));
			}

			[TestMethod]
			public void IsCustomHResult_False()
			{
				Assert.IsFalse(HResultHelper.IsCustomHResult(Globals.MicrosoftHResult));
			}

			[TestMethod]
			public void EnumToInt32_Int_Success()
			{
				Assert.AreEqual(1, HResultHelper.EnumToInt32(Int32Enumeration.One));
			}

			[TestMethod]
			public void EnumToInt32_Byte_Success()
			{
				Assert.AreEqual(1, HResultHelper.EnumToInt32(ByteEnumeration.One));
			}

			[TestMethod]
			public void EnumToInt32_UInt_Success()
			{
				Assert.AreEqual(1, HResultHelper.EnumToInt32(UInt32Enumeration.One));
			}

			[TestMethod]
			public void EnumToInt32_Null_Throw()
			{
				_ = Assert.ThrowsException<ArgumentNullException>(() => HResultHelper.EnumToInt32(null));
			}

			[TestMethod]
			public void HexStringToByteArray_NullOrEmpty_Success()
			{
				Assert.IsNull(HResultHelper.HexStringToByteArray(null));
				Assert.IsNull(HResultHelper.HexStringToByteArray(string.Empty));
			}

			[TestMethod]
			public void HexStringToByteArray_OneByte_Success()
			{
				byte[] buffer = HResultHelper.HexStringToByteArray("0a");
				Assert.IsNotNull(buffer);
				Assert.AreEqual(1, buffer.Length);
				Assert.AreEqual(10, buffer[0]);
			}

			[TestMethod]
			public void HexStringToByteArray_MultiByte_Success()
			{
				byte[] buffer = HResultHelper.HexStringToByteArray("2a2a2a");
				Assert.IsNotNull(buffer);
				Assert.AreEqual(3, buffer.Length);
				CollectionAssert.AreEqual(new byte[] { 42, 42, 42 }, buffer);
			}

			[TestMethod]
			public void HexStringToByteArray_MultiBytePrefix_Success()
			{
				byte[] buffer = HResultHelper.HexStringToByteArray("0x2a2a2a");
				Assert.IsNotNull(buffer);
				Assert.AreEqual(3, buffer.Length);
				CollectionAssert.AreEqual(new byte[] { 42, 42, 42 }, buffer);
			}

			[TestMethod]
			public void HexStringToByteArray_MultiBytePrefixCased_Success()
			{
				byte[] buffer = HResultHelper.HexStringToByteArray("0X2A2A2A");
				Assert.IsNotNull(buffer);
				Assert.AreEqual(3, buffer.Length);
				CollectionAssert.AreEqual(new byte[] { 42, 42, 42 }, buffer);
			}

			[TestMethod]
			public void HexStringToByteArray_InvalidString_Throw()
			{
				_ = Assert.ThrowsException<FormatException>(() =>
				  {
					  byte[] buffer = HResultHelper.HexStringToByteArray("Not hex");
				  });
			}

			[TestMethod]
			public void CreateToString_SimpleException_Success()
			{
				try
				{
					throw new CodedException(Globals.CustomHResult, Globals.TestMessage);
				}
				catch (CodedException ex)
				{
					string str = HResultHelper.CreateToString(ex, null);
					StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedException).FullName, Globals.CustomHResultString, Globals.TestMessage));
					StringAssert.Contains(str, nameof(CreateToString_SimpleException_Success));
				}
			}

			[TestMethod]
			public void CreateToString_ExtendedMessage_Success()
			{
				try
				{
					throw new CodedException(Globals.CustomHResult, Globals.TestMessage);
				}
				catch (CodedException ex)
				{
					string str = HResultHelper.CreateToString(ex, "[ExtendedMessage]");
					StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedException).FullName, Globals.CustomHResultString, Globals.TestMessage));
					StringAssert.Contains(str, nameof(CreateToString_ExtendedMessage_Success));
					StringAssert.Contains(str, Environment.NewLine + "[ExtendedMessage]");
				}
			}

			public void CreateToString_InnerException_Success()
			{
				try
				{
					try
					{
						throw new InvalidOperationException();
					}
					catch (InvalidOperationException ex)
					{
						throw new CodedException(Globals.CustomHResult, Globals.TestMessage, ex);
					}
				}
				catch (CodedException ex)
				{
					string str = HResultHelper.CreateToString(ex, null);
					StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedException).FullName, Globals.CustomHResultString, Globals.TestMessage));
					StringAssert.Contains(str, nameof(CreateToString_InnerException_Success));
					StringAssert.Contains(str, "System.InvalidOperationException");
				}
			}

			[TestMethod]
			public void CreateToString_ExceptionNull_Throw()
			{
				_ = Assert.ThrowsException<ArgumentNullException>(() => HResultHelper.CreateToString(null, null));
			}
		}
	}
}
