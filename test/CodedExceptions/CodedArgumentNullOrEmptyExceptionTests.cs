// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedArgumentNullOrEmptyException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedArgumentNullOrEmptyExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedArgumentNullOrEmptyException();
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedArgumentNullOrEmptyException(GlobalConstants.ParamName);
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ParamName);
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
				throw new CodedArgumentNullOrEmptyException(GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_StringString_Success()
	{
		try
		{
			throw new CodedArgumentNullOrEmptyException(GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedArgumentNullOrEmptyException(GlobalConstants.CustomHResult);
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedArgumentNullOrEmptyException(GlobalConstants.CustomHResult, GlobalConstants.ParamName);
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ParamName);
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
				throw new CodedArgumentNullOrEmptyException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_Success()
	{
		try
		{
			throw new CodedArgumentNullOrEmptyException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ParamName);
		}
	}

#if NETFRAMEWORK
	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		try
		{
			throw new CodedArgumentNullOrEmptyException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedArgumentNullOrEmptyException ex2 = SerializationHelper.Deserialize<CodedArgumentNullOrEmptyException>(buffer);

			Assert.AreEqual(GlobalConstants.CustomHResult, ex2.HResult);
			StringAssert.StartsWith(ex2.Message, GlobalConstants.TestMessage);
			Assert.AreEqual(GlobalConstants.ParamName, ex2.ParamName);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedArgumentNullOrEmptyException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullOrEmptyException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedArgumentNullOrEmptyException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, GlobalConstants.ParamName);
		}
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_Success()
	{
		string text = "Hi!";
		CodedArgumentNullOrEmptyException.ThrowIfNullOrEmpty(text);
		CodedArgumentNullOrEmptyException.ThrowIfNullOrEmpty(text, GlobalConstants.CustomHResult);
		text = "   ";
		CodedArgumentNullOrEmptyException.ThrowIfNullOrEmpty(text);
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringNull_Fail()
	{
		string text = string.Empty;
		CodedArgumentNullOrEmptyException ex = Assert.ThrowsExactly<CodedArgumentNullOrEmptyException>(() => CodedArgumentNullOrEmptyException.ThrowIfNullOrEmpty(text));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringString_Fail()
	{
		string argName = "myArg";
		string text = string.Empty;
		CodedArgumentNullOrEmptyException ex = Assert.ThrowsExactly<CodedArgumentNullOrEmptyException>(() => CodedArgumentNullOrEmptyException.ThrowIfNullOrEmpty(text, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringIntNull_Fail()
	{
		string text = string.Empty;
		CodedArgumentNullOrEmptyException ex = Assert.ThrowsExactly<CodedArgumentNullOrEmptyException>(() => CodedArgumentNullOrEmptyException.ThrowIfNullOrEmpty(text, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringIntString_Fail()
	{
		string argName = "myArg";
		string text = string.Empty;
		CodedArgumentNullOrEmptyException ex = Assert.ThrowsExactly<CodedArgumentNullOrEmptyException>(() => CodedArgumentNullOrEmptyException.ThrowIfNullOrEmpty(text, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}
}
