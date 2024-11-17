// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedArgumentNullException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedObjectsDisposedExceptionTests
{
	[TestMethod]
	public void Ctor_Null_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(null);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(Globals.ParamName);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.ParamName, ex.ObjectName);
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
				throw new CodedObjectDisposedException(Globals.TestMessage, ex);
			}
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
		}
	}

	[TestMethod]
	public void Ctor_StringString_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ObjectName);
		}
	}

	[TestMethod]
	public void Ctor_Int32Null_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(Globals.CustomHResult, null);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(Globals.CustomHResult, Globals.ParamName);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.ParamName, ex.ObjectName);
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
				throw new CodedObjectDisposedException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(Globals.CustomHResult, Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex.ObjectName);
		}
	}

#if NETFRAMEWORK
	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(Globals.CustomHResult, Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedObjectDisposedException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedObjectDisposedException ex2 = SerializationHelper.Deserialize<CodedObjectDisposedException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNull(ex2.InnerException);
			StringAssert.StartsWith(ex2.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex2.ObjectName);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(Globals.CustomHResult, Globals.ParamName, Globals.TestMessage);
		}
		catch (CodedObjectDisposedException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedObjectDisposedException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, Globals.ParamName);
		}
	}

	[TestMethod]
	public void ThrowIf_Success()
	{
		CodedObjectDisposedException.ThrowIf(false, Globals.ParamName);
		CodedObjectDisposedException.ThrowIf(false, new object());
		CodedObjectDisposedException.ThrowIf(false, typeof(string));
		CodedObjectDisposedException.ThrowIf(false, Globals.CustomHResult, Globals.ParamName);
		CodedObjectDisposedException.ThrowIf(false, Globals.CustomHResult, new object());
		CodedObjectDisposedException.ThrowIf(false, Globals.CustomHResult, typeof(string));
		CodedObjectDisposedException.ThrowIf(0, Globals.ParamName);
		CodedObjectDisposedException.ThrowIf(0, new object());
		CodedObjectDisposedException.ThrowIf(0, typeof(string));
		CodedObjectDisposedException.ThrowIf(0, Globals.CustomHResult, Globals.ParamName);
		CodedObjectDisposedException.ThrowIf(0, Globals.CustomHResult, new object());
		CodedObjectDisposedException.ThrowIf(0, Globals.CustomHResult, typeof(string));
	}

	[TestMethod]
	public void ThrowIf_BoolNull_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, null));
		Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
	}

	[TestMethod]
	public void ThrowIf_BoolString_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, typeof(CultureInfo)));
		Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolObject_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, CultureInfo.InvariantCulture));
		Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolType_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, typeof(string)));
		Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual("String", ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolIntNull_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, Globals.CustomHResult, null));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
	}

	[TestMethod]
	public void ThrowIf_BoolIntString_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, Globals.CustomHResult, typeof(CultureInfo)));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolIntObject_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, Globals.CustomHResult, CultureInfo.InvariantCulture));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolIntType_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, Globals.CustomHResult, typeof(string)));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual("String", ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntNull_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, null));
		Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
	}

	[TestMethod]
	public void ThrowIf_IntString_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, typeof(CultureInfo)));
		Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntObject_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, CultureInfo.InvariantCulture));
		Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntType_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, typeof(string)));
		Assert.AreEqual(Globals.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual("String", ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntIntNull_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, Globals.CustomHResult, null));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
	}

	[TestMethod]
	public void ThrowIf_IntIntString_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, Globals.CustomHResult, typeof(CultureInfo)));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntIntObject_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, Globals.CustomHResult, CultureInfo.InvariantCulture));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntIntType_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsException<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, Globals.CustomHResult, typeof(string)));
		Assert.AreEqual(Globals.CustomHResult, ex.HResult);
		Assert.AreEqual("String", ex.ObjectName);
	}
}
