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
			Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(GlobalConstants.ParamName);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ObjectName);
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
				throw new CodedObjectDisposedException(GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
		}
	}

	[TestMethod]
	public void Ctor_StringString_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ObjectName);
		}
	}

	[TestMethod]
	public void Ctor_Int32Null_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(GlobalConstants.CustomHResult, null);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(GlobalConstants.CustomHResult, GlobalConstants.ParamName);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ObjectName);
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
				throw new CodedObjectDisposedException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedObjectDisposedException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ObjectName);
		}
	}

#if NETFRAMEWORK
	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedObjectDisposedException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedObjectDisposedException ex2 = SerializationHelper.Deserialize<CodedObjectDisposedException>(buffer);

			Assert.AreEqual(GlobalConstants.CustomHResult, ex2.HResult);
			Assert.IsNull(ex2.InnerException);
			StringAssert.StartsWith(ex2.Message, GlobalConstants.TestMessage);
			Assert.AreEqual(GlobalConstants.ParamName, ex2.ObjectName);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedObjectDisposedException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedObjectDisposedException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedObjectDisposedException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, GlobalConstants.ParamName);
		}
	}

	[TestMethod]
	public void ThrowIf_Success()
	{
		CodedObjectDisposedException.ThrowIf(false, GlobalConstants.ParamName);
		CodedObjectDisposedException.ThrowIf(false, new object());
		CodedObjectDisposedException.ThrowIf(false, typeof(string));
		CodedObjectDisposedException.ThrowIf(false, GlobalConstants.CustomHResult, GlobalConstants.ParamName);
		CodedObjectDisposedException.ThrowIf(false, GlobalConstants.CustomHResult, new object());
		CodedObjectDisposedException.ThrowIf(false, GlobalConstants.CustomHResult, typeof(string));
		CodedObjectDisposedException.ThrowIf(0, GlobalConstants.ParamName);
		CodedObjectDisposedException.ThrowIf(0, new object());
		CodedObjectDisposedException.ThrowIf(0, typeof(string));
		CodedObjectDisposedException.ThrowIf(0, GlobalConstants.CustomHResult, GlobalConstants.ParamName);
		CodedObjectDisposedException.ThrowIf(0, GlobalConstants.CustomHResult, new object());
		CodedObjectDisposedException.ThrowIf(0, GlobalConstants.CustomHResult, typeof(string));
	}

	[TestMethod]
	public void ThrowIf_BoolNull_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, null));
		Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
	}

	[TestMethod]
	public void ThrowIf_BoolString_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, typeof(CultureInfo)));
		Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolObject_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, CultureInfo.InvariantCulture));
		Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolType_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, typeof(string)));
		Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual("String", ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolIntNull_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, GlobalConstants.CustomHResult, null));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
	}

	[TestMethod]
	public void ThrowIf_BoolIntString_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, GlobalConstants.CustomHResult, typeof(CultureInfo)));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolIntObject_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, GlobalConstants.CustomHResult, CultureInfo.InvariantCulture));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_BoolIntType_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(true, GlobalConstants.CustomHResult, typeof(string)));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual("String", ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntNull_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, null));
		Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
	}

	[TestMethod]
	public void ThrowIf_IntString_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, typeof(CultureInfo)));
		Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntObject_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, CultureInfo.InvariantCulture));
		Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntType_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, typeof(string)));
		Assert.AreEqual(GlobalConstants.COR_E_OBJECTDISPOSED, ex.HResult);
		Assert.AreEqual("String", ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntIntNull_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, GlobalConstants.CustomHResult, null));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.IsTrue(string.IsNullOrEmpty(ex.ObjectName));
	}

	[TestMethod]
	public void ThrowIf_IntIntString_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, GlobalConstants.CustomHResult, typeof(CultureInfo)));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntIntObject_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, GlobalConstants.CustomHResult, CultureInfo.InvariantCulture));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(nameof(CultureInfo), ex.ObjectName);
	}

	[TestMethod]
	public void ThrowIf_IntIntType_Fail()
	{
		CodedObjectDisposedException ex = Assert.ThrowsExactly<CodedObjectDisposedException>(() => CodedObjectDisposedException.ThrowIf(1, GlobalConstants.CustomHResult, typeof(string)));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual("String", ex.ObjectName);
	}
}
