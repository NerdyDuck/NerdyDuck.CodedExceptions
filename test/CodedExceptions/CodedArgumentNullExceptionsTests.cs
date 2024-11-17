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
			Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedArgumentNullException(Globals.ParamName);
		}
		catch (CodedArgumentNullException ex)
		{
			Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
			Assert.IsNull(ex.InnerException);
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
				throw new CodedArgumentNullException(Globals.TestMessage, ex);
			}
		}
		catch (CodedArgumentNullException ex)
		{
			Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
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
			throw new CodedArgumentNullException(Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedArgumentNullException ex)
		{
			Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedArgumentNullException(Globals.CustomHResult);
		}
		catch (CodedArgumentNullException ex)
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
			throw new CodedArgumentNullException(Globals.CustomHResult, Globals.ParamName);
		}
		catch (CodedArgumentNullException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
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
				throw new CodedArgumentNullException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedArgumentNullException ex)
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
			throw new CodedArgumentNullException(Globals.CustomHResult, Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedArgumentNullException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
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
			throw new CodedArgumentNullException(Globals.CustomHResult, Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedArgumentNullException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedArgumentNullException ex2 = SerializationHelper.Deserialize<CodedArgumentNullException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNull(ex2.InnerException);
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
			throw new CodedArgumentNullException(Globals.CustomHResult, Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedArgumentNullException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedArgumentNullException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, Globals.ParamName);
		}
	}

	[TestMethod]
	public void ThrowIfNull_Success()
	{
		CodedArgumentNullException.ThrowIfNull(new object());
		CodedArgumentNullException.ThrowIfNull(new object(), Globals.CustomHResult);
		CodedArgumentNullException.ThrowIfNull(new object(), "arg");
		CodedArgumentNullException.ThrowIfNull(new object(), Globals.CustomHResult, "arg");

		int[] i = { 0 };
		unsafe
		{
			fixed (int* p = &i[0])
			{
				CodedArgumentNullException.ThrowIfNull(p);
				CodedArgumentNullException.ThrowIfNull(p, Globals.CustomHResult);
				CodedArgumentNullException.ThrowIfNull(p, "arg");
				CodedArgumentNullException.ThrowIfNull(p, Globals.CustomHResult, "arg");
			}
		}

		IntPtr ptr = new IntPtr(1);
		CodedArgumentNullException.ThrowIfNull(ptr);
		CodedArgumentNullException.ThrowIfNull(ptr, Globals.CustomHResult);
		CodedArgumentNullException.ThrowIfNull(ptr, "arg");
		CodedArgumentNullException.ThrowIfNull(ptr, Globals.CustomHResult, "arg");
	}

	[TestMethod]
	public void ThrowIfNull_ObjectNull_Fail()
	{
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(text));
		Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
#if NET5_0_OR_GREATER
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNull_VoidNull_Fail()
	{
		unsafe
		{
			void* p = null;
			CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(p));
			Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
#if NET5_0_OR_GREATER
			Assert.AreEqual(nameof(p), ex.ParamName);
#endif
		}
	}

	[TestMethod]
	public void ThrowIfNull_IntPtrNull_Fail()
	{
		IntPtr p = IntPtr.Zero;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(p));
		Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
#if NET5_0_OR_GREATER
		Assert.AreEqual(nameof(p), ex.ParamName);
#endif
	}


	[TestMethod]
	public void ThrowIfNull_StringString_Fail()
	{
		string argName = "myArg";
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(text, argName));
		Assert.AreEqual(Globals.COR_E_NULLREFERENCE, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNull_StringIntNull_Fail()
	{
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(text, Globals.CustomHResult));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
#if NET5_0_OR_GREATER
		Assert.AreEqual(nameof(text), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNull_VoidIntNull_Fail()
	{
		unsafe
		{
			void* p = null;
			CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(p, Globals.CustomHResult));
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
#if NET5_0_OR_GREATER
			Assert.AreEqual(nameof(p), ex.ParamName);
#endif
		}
	}

	[TestMethod]
	public void ThrowIfNull_IntPtrIntNull_Fail()
	{
		IntPtr p = IntPtr.Zero;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(p, Globals.CustomHResult));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
#if NET5_0_OR_GREATER
		Assert.AreEqual(nameof(p), ex.ParamName);
#endif
	}


	[TestMethod]
	public void ThrowIfNull_StringIntString_Fail()
	{
		string argName = "myArg";
		string text = null;
		CodedArgumentNullException ex = Assert.ThrowsException<CodedArgumentNullException>(() => CodedArgumentNullException.ThrowIfNull(text, Globals.CustomHResult, argName));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual(argName, ex.ParamName);
	}
}
