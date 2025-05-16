// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedArgumentNullException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedArgumentNullExceptionsTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedArgumentNullException();
		}
		catch (CodedArgumentNullException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_NULLREFERENCE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedArgumentNullException(GlobalConstants.ParamName);
		}
		catch (CodedArgumentNullException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_NULLREFERENCE, ex.HResult);
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
				throw new CodedArgumentNullException(GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedArgumentNullException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_NULLREFERENCE, ex.HResult);
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
			throw new CodedArgumentNullException(GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_NULLREFERENCE, ex.HResult);
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
			throw new CodedArgumentNullException(GlobalConstants.CustomHResult);
		}
		catch (CodedArgumentNullException ex)
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
			throw new CodedArgumentNullException(GlobalConstants.CustomHResult, GlobalConstants.ParamName);
		}
		catch (CodedArgumentNullException ex)
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
				throw new CodedArgumentNullException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedArgumentNullException ex)
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
			throw new CodedArgumentNullException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullException ex)
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
			throw new CodedArgumentNullException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedArgumentNullException ex2 = SerializationHelper.Deserialize<CodedArgumentNullException>(buffer);

			Assert.AreEqual(GlobalConstants.CustomHResult, ex2.HResult);
			Assert.IsNull(ex2.InnerException);
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
			throw new CodedArgumentNullException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentNullException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedArgumentNullException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, GlobalConstants.ParamName);
		}
	}

	[TestMethod]
	public void ThrowIfNull_Success()
	{
		CodedArgumentNullException.ThrowIfNull(new object());
		CodedArgumentNullException.ThrowIfNull(new object(), GlobalConstants.CustomHResult);
		CodedArgumentNullException.ThrowIfNull(new object(), "arg");
		CodedArgumentNullException.ThrowIfNull(new object(), GlobalConstants.CustomHResult, "arg");

		int[] i = [0];
		unsafe
		{
			fixed (int* p = &i[0])
			{
				CodedArgumentNullException.ThrowIfNull(p);
				CodedArgumentNullException.ThrowIfNull(p, GlobalConstants.CustomHResult);
				CodedArgumentNullException.ThrowIfNull(p, "arg");
				CodedArgumentNullException.ThrowIfNull(p, GlobalConstants.CustomHResult, "arg");
			}
		}

		IntPtr ptr = new(1);
		CodedArgumentNullException.ThrowIfNull(ptr);
		CodedArgumentNullException.ThrowIfNull(ptr, GlobalConstants.CustomHResult);
		CodedArgumentNullException.ThrowIfNull(ptr, "arg");
		CodedArgumentNullException.ThrowIfNull(ptr, GlobalConstants.CustomHResult, "arg");
	}

	[TestMethod]
	public void ThrowIfNull_ObjectNull_Fail()
	{
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsExactly<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(text));
		Assert.AreEqual(GlobalConstants.COR_E_NULLREFERENCE, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNull_VoidNull_Fail()
	{
		unsafe
		{
			void* p = null;
			CodedArgumentNullException ex = Assert.ThrowsExactly<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(p));
			Assert.AreEqual(GlobalConstants.COR_E_NULLREFERENCE, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
			Assert.AreEqual(nameof(p), ex.ParamName);
#endif
		}
	}

	[TestMethod]
	public void ThrowIfNull_IntPtrNull_Fail()
	{
		IntPtr p = IntPtr.Zero;
		CodedArgumentNullException ex = Assert.ThrowsExactly<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(p));
		Assert.AreEqual(GlobalConstants.COR_E_NULLREFERENCE, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(p), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNull_StringString_Fail()
	{
		string argName = "myArg";
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsExactly<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(text, argName));
		Assert.AreEqual(GlobalConstants.COR_E_NULLREFERENCE, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNull_StringIntNull_Fail()
	{
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsExactly<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(text, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNull_VoidIntNull_Fail()
	{
		unsafe
		{
			void* p = null;
			CodedArgumentNullException ex = Assert.ThrowsExactly<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(p, GlobalConstants.CustomHResult));
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
			Assert.AreEqual(nameof(p), ex.ParamName);
#endif
		}
	}

	[TestMethod]
	public void ThrowIfNull_IntPtrIntNull_Fail()
	{
		IntPtr p = IntPtr.Zero;
		CodedArgumentNullException ex = Assert.ThrowsExactly<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(p, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(p), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNull_StringIntString_Fail()
	{
		string argName = "myArg";
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsExactly<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(text, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}
}
