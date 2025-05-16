// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedArgumentNullOrWhiteSpaceException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedArgumentNullOrWhiteSpaceExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedArgumentNullOrWhiteSpaceException();
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
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
			throw new CodedArgumentNullOrWhiteSpaceException(GlobalConstants.ParamName);
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
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
				throw new CodedArgumentNullOrWhiteSpaceException(GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
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
			throw new CodedArgumentNullOrWhiteSpaceException(GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
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
			throw new CodedArgumentNullOrWhiteSpaceException(GlobalConstants.CustomHResult);
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
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
			throw new CodedArgumentNullOrWhiteSpaceException(GlobalConstants.CustomHResult, GlobalConstants.ParamName);
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
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
				throw new CodedArgumentNullOrWhiteSpaceException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
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
			throw new CodedArgumentNullOrWhiteSpaceException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
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
			throw new CodedArgumentNullOrWhiteSpaceException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedArgumentNullOrWhiteSpaceException ex2 = SerializationHelper.Deserialize<CodedArgumentNullOrWhiteSpaceException>(buffer);

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
			throw new CodedArgumentNullOrWhiteSpaceException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullOrWhiteSpaceException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedArgumentNullOrWhiteSpaceException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, GlobalConstants.ParamName);
		}
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_Success()
	{
		string text = "Hi!";
		CodedArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(text);
		CodedArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(text, GlobalConstants.CustomHResult);
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringNull_FailNull()
	{
		string text = null;
		CodedArgumentNullOrWhiteSpaceException ex = Assert.ThrowsExactly<CodedArgumentNullOrWhiteSpaceException>(() => CodedArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(text));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringNull_FailEmpty()
	{
		string text = string.Empty;
		CodedArgumentNullOrWhiteSpaceException ex = Assert.ThrowsExactly<CodedArgumentNullOrWhiteSpaceException>(() => CodedArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(text));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringNull_FailWhite()
	{
		string text = "   ";
		CodedArgumentNullOrWhiteSpaceException ex = Assert.ThrowsExactly<CodedArgumentNullOrWhiteSpaceException>(() => CodedArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(text));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringString_FailEmpty()
	{
		string argName = "myArg";
		string text = string.Empty;
		CodedArgumentNullOrWhiteSpaceException ex = Assert.ThrowsExactly<CodedArgumentNullOrWhiteSpaceException>(() => CodedArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(text, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENT, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringIntNull_FailNull()
	{
		string text = null;
		CodedArgumentNullOrWhiteSpaceException ex = Assert.ThrowsExactly<CodedArgumentNullOrWhiteSpaceException>(() => CodedArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(text, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringIntString_FailNull()
	{
		string argName = "myArg";
		string text = null;
		CodedArgumentNullOrWhiteSpaceException ex = Assert.ThrowsExactly<CodedArgumentNullOrWhiteSpaceException>(() => CodedArgumentNullOrWhiteSpaceException.ThrowIfNullOrWhiteSpace(text, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}
}
