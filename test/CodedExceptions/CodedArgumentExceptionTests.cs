// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedArgumentException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedArgumentExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedArgumentException();
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENT, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedArgumentException(Globals.TestMessage);
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENT, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
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
				throw new CodedArgumentException(Globals.TestMessage, ex);
			}
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENT, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_StringString_Success()
	{
		try
		{
			throw new CodedArgumentException(Globals.TestMessage, Globals.ParamName);
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENT, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_StringStringException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedArgumentException(Globals.TestMessage, Globals.ParamName, ex);
			}
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.COR_E_ARGUMENT, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedArgumentException(Globals.CustomHResult);
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedArgumentException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
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
				throw new CodedArgumentException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_Success()
	{
		try
		{
			throw new CodedArgumentException(Globals.CustomHResult, Globals.TestMessage, Globals.ParamName);
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringStringException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedArgumentException(Globals.CustomHResult, Globals.TestMessage, Globals.ParamName, ex);
			}
		}
		catch (CodedArgumentException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
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
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedArgumentException(Globals.CustomHResult, Globals.TestMessage, Globals.ParamName, ex);
			}
		}
		catch (CodedArgumentException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedArgumentException ex2 = SerializationHelper.Deserialize<CodedArgumentException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNotNull(ex2.InnerException);
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
			throw new CodedArgumentException(Globals.CustomHResult, Globals.TestMessage, Globals.ParamName);
		}
		catch (CodedArgumentException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedArgumentException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, Globals.ParamName);
		}
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_Success()
	{
		string text = "Hi!";
		CodedArgumentException.ThrowIfNullOrEmpty(text);
		text = "   ";
		CodedArgumentException.ThrowIfNullOrEmpty(text);
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringNull_FailNull()
	{
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentException.ThrowIfNullOrEmpty(text));
		Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringString_FailNull()
	{
		string argName = "myArg";
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentException.ThrowIfNullOrEmpty(text, argName));
		Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringNull_FailArg()
	{
		string text = string.Empty;
		CodedArgumentException ex = Assert.ThrowsException<CodedArgumentException>(() => CodedArgumentException.ThrowIfNullOrEmpty(text));
		Assert.AreEqual(Globals.COR_E_ARGUMENT, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringString_FailArg()
	{
		string argName = "myArg";
		string text = string.Empty;
		CodedArgumentException ex = Assert.ThrowsException<CodedArgumentException>(() => CodedArgumentException.ThrowIfNullOrEmpty(text, argName));
		Assert.AreEqual(Globals.COR_E_ARGUMENT, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringIntNull_FailNull()
	{
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentException.ThrowIfNullOrEmpty(text, Globals.CustomHResult));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringIntString_FailNull()
	{
		string argName = "myArg";
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentException.ThrowIfNullOrEmpty(text, Globals.CustomHResult, argName));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringIntNull_FailArg()
	{
		string text = string.Empty;
		CodedArgumentException ex = Assert.ThrowsException<CodedArgumentException>(() => CodedArgumentException.ThrowIfNullOrEmpty(text, Globals.CustomHResult));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrEmpty_StringIntString_FailArg()
	{
		string argName = "myArg";
		string text = string.Empty;
		CodedArgumentException ex = Assert.ThrowsException<CodedArgumentException>(() => CodedArgumentException.ThrowIfNullOrEmpty(text, Globals.CustomHResult, argName));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_Success()
	{
		string text = "Hi!";
		CodedArgumentException.ThrowIfNullOrWhiteSpace(text);
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringNull_FailNull()
	{
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentException.ThrowIfNullOrWhiteSpace(text));
		Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringString_FailNull()
	{
		string argName = "myArg";
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentException.ThrowIfNullOrWhiteSpace(text, argName));
		Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringNull_FailArg()
	{
		string text = string.Empty;
		CodedArgumentException ex = Assert.ThrowsException<CodedArgumentException>(() => CodedArgumentException.ThrowIfNullOrWhiteSpace(text));
		Assert.AreEqual(Globals.COR_E_ARGUMENT, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringString_FailArg()
	{
		string argName = "myArg";
		string text = string.Empty;
		CodedArgumentException ex = Assert.ThrowsException<CodedArgumentException>(() => CodedArgumentException.ThrowIfNullOrWhiteSpace(text, argName));
		Assert.AreEqual(Globals.COR_E_ARGUMENT, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringIntNull_FailNull()
	{
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentException.ThrowIfNullOrWhiteSpace(text, Globals.CustomHResult));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringIntString_FailNull()
	{
		string argName = "myArg";
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentException.ThrowIfNullOrWhiteSpace(text, Globals.CustomHResult, argName));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringIntNull_FailArg()
	{
		string text = "   ";
		CodedArgumentException ex = Assert.ThrowsException<CodedArgumentException>(() => CodedArgumentException.ThrowIfNullOrWhiteSpace(text, Globals.CustomHResult));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNullOrWhitespace_StringIntString_FailArg()
	{
		string argName = "myArg";
		string text = "   ";
		CodedArgumentException ex = Assert.ThrowsException<CodedArgumentException>(() => CodedArgumentException.ThrowIfNullOrWhiteSpace(text, Globals.CustomHResult, argName));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}
}
