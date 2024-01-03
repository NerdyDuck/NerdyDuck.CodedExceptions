// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedInvalidOperationException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedInvalidOperationExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedInvalidOperationException();
		}
		catch (CodedInvalidOperationException ex)
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
			throw new CodedInvalidOperationException(Globals.TestMessage);
		}
		catch (CodedInvalidOperationException ex)
		{
			Assert.AreEqual(Globals.COR_E_INVALIDOPERATION, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_StringException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedInvalidOperationException(Globals.TestMessage, ex);
			}
		}
		catch (CodedInvalidOperationException ex)
		{
			Assert.AreEqual(Globals.COR_E_INVALIDOPERATION, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedInvalidOperationException(Globals.CustomHResult);
		}
		catch (CodedInvalidOperationException ex)
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
			throw new CodedInvalidOperationException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedInvalidOperationException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_IntStringException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedInvalidOperationException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedInvalidOperationException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

#if NETFRAMEWORK
	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedInvalidOperationException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedInvalidOperationException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedInvalidOperationException ex2 = SerializationHelper.Deserialize<CodedInvalidOperationException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNotNull(ex2.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex2.Message);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedInvalidOperationException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedInvalidOperationException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedInvalidOperationException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
		}
	}
}
