// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedArgumentNullOrEmptyException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedArgumentOutOfRangeExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException();
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.ParamName);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
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
				throw new CodedArgumentOutOfRangeException(Globals.TestMessage, ex);
			}
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_StringString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_StringObjectString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.ParamName, 42, Globals.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(42, ex.ActualValue);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.CustomHResult);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.CustomHResult, Globals.ParamName);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
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
				throw new CodedArgumentOutOfRangeException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.CustomHResult, Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringObjectString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.CustomHResult, Globals.ParamName, 42, Globals.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(42, ex.ActualValue);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
		}
	}

#if NETFRAMEWORK
	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.CustomHResult, Globals.ParamName, 42, Globals.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedArgumentOutOfRangeException ex2 = SerializationHelper.Deserialize<CodedArgumentOutOfRangeException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNull(ex2.InnerException);
			Assert.AreEqual(42, ex2.ActualValue);
			StringAssert.StartsWith(ex2.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex2.ParamName);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.CustomHResult, Globals.ParamName, 42, Globals.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedArgumentOutOfRangeException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, "ToString_Success");
			StringAssert.Contains(str, Globals.ParamName);
		}
	}
}
