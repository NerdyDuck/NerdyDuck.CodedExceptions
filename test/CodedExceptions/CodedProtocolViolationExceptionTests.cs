// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedProtocolViolationException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedProtocolViolationExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedProtocolViolationException();
		}
		catch (CodedProtocolViolationException ex)
		{
			Assert.AreEqual(Globals.COR_E_INVALIDOPERATION, ex.HResult);
			Assert.IsNull(ex.InnerException);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedProtocolViolationException(Globals.TestMessage);
		}
		catch (CodedProtocolViolationException ex)
		{
			Assert.AreEqual(Globals.COR_E_INVALIDOPERATION, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedProtocolViolationException(Globals.CustomHResult);
		}
		catch (CodedProtocolViolationException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedProtocolViolationException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedProtocolViolationException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

#if NETFRAMEWORK
	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		try
		{
			throw new CodedProtocolViolationException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedProtocolViolationException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedProtocolViolationException ex2 = SerializationHelper.Deserialize<CodedProtocolViolationException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNull(ex2.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex2.Message);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedProtocolViolationException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedProtocolViolationException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedProtocolViolationException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
		}
	}
}
