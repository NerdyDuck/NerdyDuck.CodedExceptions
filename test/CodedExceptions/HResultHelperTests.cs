// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions;

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
		int i = HResultHelper.GetFacilityId(GlobalConstants.CustomHResult);
		Assert.AreEqual(2047, i);
	}

	[TestMethod]
	public void GetErrorId_Success()
	{
		int i = HResultHelper.GetErrorId(GlobalConstants.CustomHResult);
		Assert.AreEqual(0x1234, i);
	}

	[TestMethod]
	public void GetBaseHResult_Success()
	{
		int i = HResultHelper.GetBaseHResult(0x7ff);
		Assert.AreEqual(unchecked((int)0x27ff0000), i);
	}

	[TestMethod]
	public void GetBaseHResultError_Success()
	{
		int i = HResultHelper.GetBaseHResultError(0x7ff);
		Assert.AreEqual(unchecked((int)0xa7ff0000), i);
	}

	[TestMethod]
	public void IsCustomHResult_True()
	{
		Assert.IsTrue(HResultHelper.IsCustomHResult(GlobalConstants.CustomHResult));
	}

	[TestMethod]
	public void IsCustomHResult_False()
	{
		Assert.IsFalse(HResultHelper.IsCustomHResult(GlobalConstants.MicrosoftHResult));
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
		_ = Assert.ThrowsExactly<ArgumentNullException>(() => HResultHelper.EnumToInt32(null));
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
		byte[] buffer = HResultHelper.HexStringToByteArray("102afe");
		Assert.IsNotNull(buffer);
		Assert.AreEqual(3, buffer.Length);
		CollectionAssert.AreEqual(new byte[] { 16, 42, 254 }, buffer);
	}

	[TestMethod]
	public void HexStringToByteArray_MultiBytePrefix_Success()
	{
		byte[] buffer = HResultHelper.HexStringToByteArray("0x102afe");
		Assert.IsNotNull(buffer);
		Assert.AreEqual(3, buffer.Length);
		CollectionAssert.AreEqual(new byte[] { 16, 42, 254 }, buffer);
	}

	[TestMethod]
	public void HexStringToByteArray_MultiBytePrefixCased_Success()
	{
		byte[] buffer = HResultHelper.HexStringToByteArray("0X102AFE");
		Assert.IsNotNull(buffer);
		Assert.AreEqual(3, buffer.Length);
		CollectionAssert.AreEqual(new byte[] { 16, 42, 254 }, buffer);
	}

	[TestMethod]
	public void HexStringToByteArray_InvalidString_Throw()
	{
		_ = Assert.ThrowsExactly<FormatException>(() =>
		  {
			  byte[] buffer = HResultHelper.HexStringToByteArray("Not hex");
		  });
	}

	[TestMethod]
	public void CreateToString_SimpleException_Success()
	{
		try
		{
			throw new CodedException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage);
		}
		catch (CodedException ex)
		{
			string str = HResultHelper.CreateToString(ex, null);
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(CreateToString_SimpleException_Success));
		}
	}

	[TestMethod]
	public void CreateToString_ExtendedMessage_Success()
	{
		try
		{
			throw new CodedException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage);
		}
		catch (CodedException ex)
		{
			string str = HResultHelper.CreateToString(ex, "[ExtendedMessage]");
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
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
				throw new CodedException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedException ex)
		{
			string str = HResultHelper.CreateToString(ex, null);
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(CreateToString_InnerException_Success));
			StringAssert.Contains(str, "System.InvalidOperationException");
		}
	}

	[TestMethod]
	public void CreateToString_ExceptionNull_Throw()
	{
		_ = Assert.ThrowsExactly<ArgumentNullException>(() => HResultHelper.CreateToString(null, null));
	}
}
