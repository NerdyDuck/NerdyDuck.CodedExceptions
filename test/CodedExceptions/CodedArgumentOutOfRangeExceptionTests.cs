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
			Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
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
			throw new CodedArgumentOutOfRangeException(GlobalConstants.ParamName);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
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
				throw new CodedArgumentOutOfRangeException(GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_StringString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_StringObjectString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(GlobalConstants.ParamName, 42, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(42, ex.ActualValue);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(GlobalConstants.CustomHResult);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
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
			throw new CodedArgumentOutOfRangeException(GlobalConstants.CustomHResult, GlobalConstants.ParamName);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
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
				throw new CodedArgumentOutOfRangeException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsNull(ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.ActualValue);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
			Assert.AreEqual(GlobalConstants.ParamName, ex.ParamName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringObjectString_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, 42, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(42, ex.ActualValue);
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
			throw new CodedArgumentOutOfRangeException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, 42, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedArgumentOutOfRangeException ex2 = SerializationHelper.Deserialize<CodedArgumentOutOfRangeException>(buffer);

			Assert.AreEqual(GlobalConstants.CustomHResult, ex2.HResult);
			Assert.IsNull(ex2.InnerException);
			Assert.AreEqual(42, ex2.ActualValue);
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
			throw new CodedArgumentOutOfRangeException(GlobalConstants.CustomHResult, GlobalConstants.ParamName, 42, GlobalConstants.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedArgumentOutOfRangeException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, "ToString_Success");
			StringAssert.Contains(str, GlobalConstants.ParamName);
		}
	}

	[TestMethod]
	public void ThrowIfEqual_Success()
	{
		int arg = 42;
		CodedArgumentOutOfRangeException.ThrowIfEqual(arg, 43);
		CodedArgumentOutOfRangeException.ThrowIfEqual(arg, 43, nameof(arg));
		CodedArgumentOutOfRangeException.ThrowIfEqual(arg, 43, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfEqual(arg, 43, GlobalConstants.CustomHResult, nameof(arg));
	}

	[TestMethod]
	public void ThrowIfEqual_IntIntNull_Fail()
	{
		int arg = 42;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfEqual(arg, arg));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfEqual_IntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfEqual(arg, arg, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfEqual_IntIntIntNull_Fail()
	{
		int arg = 42;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfEqual(arg, arg, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfEqual_IntIntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfEqual(arg, arg, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfGreaterThan_Success()
	{
		int arg = 42;
		CodedArgumentOutOfRangeException.ThrowIfGreaterThan(arg, 43);
		CodedArgumentOutOfRangeException.ThrowIfGreaterThan(arg, 43, nameof(arg));
		CodedArgumentOutOfRangeException.ThrowIfGreaterThan(arg, 43, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfGreaterThan(arg, 43, GlobalConstants.CustomHResult, nameof(arg));
	}

	[TestMethod]
	public void ThrowIfGreaterThan_IntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfGreaterThan(arg, compValue));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfGreaterThan_IntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfGreaterThan(arg, compValue, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfGreaterThan_IntIntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfGreaterThan(arg, compValue, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfGreaterThan_IntIntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfGreaterThan(arg, compValue, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfGreaterThanOrEqual_Success()
	{
		int arg = 42;
		CodedArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arg, 43);
		CodedArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arg, 43, nameof(arg));
		CodedArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arg, 43, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arg, 43, GlobalConstants.CustomHResult, nameof(arg));
	}

	[TestMethod]
	public void ThrowIfGreaterThanOrEqual_IntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arg, compValue));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfGreaterThanOrEqual_IntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arg, compValue, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfGreaterThanOrEqual_IntIntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arg, compValue, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfGreaterThanOrEqual_IntIntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arg, compValue, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfLessThan_Success()
	{
		int arg = 42;
		CodedArgumentOutOfRangeException.ThrowIfLessThan(arg, 41);
		CodedArgumentOutOfRangeException.ThrowIfLessThan(arg, 41, nameof(arg));
		CodedArgumentOutOfRangeException.ThrowIfLessThan(arg, 41, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfLessThan(arg, 41, GlobalConstants.CustomHResult, nameof(arg));
	}

	[TestMethod]
	public void ThrowIfLessThan_IntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessThan(arg, compValue));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfLessThan_IntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessThan(arg, compValue, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfLessThan_IntIntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessThan(arg, compValue, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfLessThan_IntIntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessThan(arg, compValue, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfLessThanOrEqual_Success()
	{
		int arg = 42;
		CodedArgumentOutOfRangeException.ThrowIfLessThanOrEqual(arg, 41);
		CodedArgumentOutOfRangeException.ThrowIfLessThanOrEqual(arg, 41, nameof(arg));
		CodedArgumentOutOfRangeException.ThrowIfLessThanOrEqual(arg, 41, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfLessThanOrEqual(arg, 41, GlobalConstants.CustomHResult, nameof(arg));
	}

	[TestMethod]
	public void ThrowIfLessThanOrEqual_IntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessThanOrEqual(arg, compValue));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfLessThanOrEqual_IntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessThanOrEqual(arg, compValue, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfLessThanOrEqual_IntIntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessThanOrEqual(arg, compValue, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfLessThanOrEqual_IntIntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessThanOrEqual(arg, compValue, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNotEqual_Success()
	{
		int arg = 42;
		CodedArgumentOutOfRangeException.ThrowIfNotEqual(arg, 42);
		CodedArgumentOutOfRangeException.ThrowIfNotEqual(arg, 42, nameof(arg));
		CodedArgumentOutOfRangeException.ThrowIfNotEqual(arg, 42, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNotEqual(arg, 42, GlobalConstants.CustomHResult, nameof(arg));
	}

	[TestMethod]
	public void ThrowIfNotEqual_IntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotEqual(arg, compValue));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNotEqual_IntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotEqual(arg, compValue, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNotEqual_IntIntIntNull_Fail()
	{
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotEqual(arg, compValue, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNotEqual_IntIntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int compValue = 43;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotEqual(arg, compValue, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfLessOrGreaterThan_Success()
	{
		int arg = 42;
		CodedArgumentOutOfRangeException.ThrowIfLessOrGreaterThan(arg, 41, 43);
		CodedArgumentOutOfRangeException.ThrowIfLessOrGreaterThan(arg, 42, 43, nameof(arg));
		CodedArgumentOutOfRangeException.ThrowIfLessOrGreaterThan(arg, 41, 42, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfLessOrGreaterThan(arg, 41, 43, GlobalConstants.CustomHResult, nameof(arg));
	}

	[TestMethod]
	public void ThrowIfLessOrGreaterThan_IntIntIntNull_Fail()
	{
		int arg = 42;
		int minValue = 43;
		int maxValue = 44;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessOrGreaterThan(arg, minValue, maxValue));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfLessOrGreaterThan_IntIntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int minValue = 40;
		int maxValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessOrGreaterThan(arg, minValue, maxValue, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfLessOrGreaterThan_IntIntIntIntNull_Fail()
	{
		int arg = 42;
		int minValue = 43;
		int maxValue = 44;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessOrGreaterThan(arg, minValue, maxValue, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(arg), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfLessOrGreaterThan_IntIntIntIntString_Fail()
	{
		string argName = "myArg";
		int arg = 42;
		int minValue = 40;
		int maxValue = 41;
		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfLessOrGreaterThan(arg, minValue, maxValue, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(arg, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNegative_Success()
	{
		sbyte sb = 1;
		short s = 1;
		int i = 1;
		long l = 1;
		float f = 1;
		double d = 1;
		CodedArgumentOutOfRangeException.ThrowIfNegative(sb);
		CodedArgumentOutOfRangeException.ThrowIfNegative(sb, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegative(s);
		CodedArgumentOutOfRangeException.ThrowIfNegative(s, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegative(i);
		CodedArgumentOutOfRangeException.ThrowIfNegative(i, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegative(l);
		CodedArgumentOutOfRangeException.ThrowIfNegative(l, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegative(f);
		CodedArgumentOutOfRangeException.ThrowIfNegative(f, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegative(d);
		CodedArgumentOutOfRangeException.ThrowIfNegative(d, GlobalConstants.CustomHResult);
	}

	[TestMethod]
	public void ThrowIfNegative_DivNull_Fail()
	{
		sbyte sb = -1;
		short s = -1;
		int i = -1;
		long l = -1;
		float f = -1;
		double d = -1;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(sb));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(sb), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(s));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(s), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(i));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(i), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(l));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(l), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(f));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(f), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(d));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(d), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNegative_DivString_Fail()
	{
		string argName = "myArg";
		sbyte sb = -1;
		short s = -1;
		int i = -1;
		long l = -1;
		float f = -1;
		double d = -1;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(sb, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(s, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(i, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(l, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(f, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(d, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
	}

	[TestMethod]
	public void ThrowIfNegative_DivIntNull_Fail()
	{
		sbyte sb = -1;
		short s = -1;
		int i = -1;
		long l = -1;
		float f = -1;
		double d = -1;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(sb, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(sb), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(s, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(s), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(i, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(i), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(l, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(l), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(f, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(f), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(d, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(d), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNegative_DivIntString_Fail()
	{
		string argName = "myArg";
		sbyte sb = -1;
		short s = -1;
		int i = -1;
		long l = -1;
		float f = -1;
		double d = -1;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(sb, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(s, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(i, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(l, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(f, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegative(d, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
	}

	[TestMethod]
	public void ThrowIfNegativeOrZero_Success()
	{
		sbyte sb = 1;
		short s = 1;
		int i = 1;
		long l = 1;
		float f = 1;
		double d = 1;
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(sb);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(sb, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(s);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(s, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(i);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(i, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(l);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(l, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(f);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(f, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(d);
		CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(d, GlobalConstants.CustomHResult);
	}

	[TestMethod]
	public void ThrowIfNegativeOrZero_DivNull_Fail()
	{
		sbyte sb = 0;
		short s = 0;
		int i = 0;
		long l = 0;
		float f = 0;
		double d = 0;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(sb));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(sb), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(s));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(s), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(i));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(i), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(l));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(l), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(f));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(f), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(d));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(d), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNegativeOrZero_DivString_Fail()
	{
		string argName = "myArg";
		sbyte sb = -1;
		short s = -1;
		int i = -1;
		long l = -1;
		float f = -1;
		double d = -1;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(sb, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(s, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(i, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(l, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(f, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(d, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNegativeOrZero_DivIntNull_Fail()
	{
		sbyte sb = 0;
		short s = 0;
		int i = 0;
		long l = 0;
		float f = 0;
		double d = 0;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(sb, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(sb), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(s, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(s), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(i, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(i), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(l, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(l), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(f, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(f), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(d, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(d), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNegativeOrZero_DivIntString_Fail()
	{
		string argName = "myArg";
		sbyte sb = -1;
		short s = -1;
		int i = -1;
		long l = -1;
		float f = -1;
		double d = -1;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(sb, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(s, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(i, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(l, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(f, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNegativeOrZero(d, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfZero_Success()
	{
		sbyte sb = 1;
		short s = 1;
		int i = 1;
		long l = 1;
		float f = 1;
		double d = 1;
		byte b = 1;
		ushort us = 1;
		uint ui = 1;
		ulong ul = 1;
		char c = 'X';
		CodedArgumentOutOfRangeException.ThrowIfZero(sb);
		CodedArgumentOutOfRangeException.ThrowIfZero(sb, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(s);
		CodedArgumentOutOfRangeException.ThrowIfZero(s, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(i);
		CodedArgumentOutOfRangeException.ThrowIfZero(i, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(l);
		CodedArgumentOutOfRangeException.ThrowIfZero(l, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(f);
		CodedArgumentOutOfRangeException.ThrowIfZero(f, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(d);
		CodedArgumentOutOfRangeException.ThrowIfZero(d, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(b);
		CodedArgumentOutOfRangeException.ThrowIfZero(b, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(us);
		CodedArgumentOutOfRangeException.ThrowIfZero(us, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(ul);
		CodedArgumentOutOfRangeException.ThrowIfZero(ul, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(ui);
		CodedArgumentOutOfRangeException.ThrowIfZero(ui, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfZero(c);
		CodedArgumentOutOfRangeException.ThrowIfZero(c, GlobalConstants.CustomHResult);
	}

	[TestMethod]
	public void ThrowIfZero_DivNull_Fail()
	{
		sbyte sb = 0;
		short s = 0;
		int i = 0;
		long l = 0;
		float f = 0;
		double d = 0;
		byte b = 0;
		ushort us = 0;
		uint ui = 0;
		ulong ul = 0;
		char c = '\0';

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(sb));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(sb), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(s));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(s), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(i));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(i), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(l));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(l), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(f));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(f), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(d));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(d), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(b));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(b, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(b), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(us));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(us, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(us), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(ui));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(ui, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(ui), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(ul));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(ul, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(ul), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(c));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(c, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(c), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfZero_DivString_Fail()
	{
		string argName = "myArg";
		sbyte sb = 0;
		short s = 0;
		int i = 0;
		long l = 0;
		float f = 0;
		double d = 0;
		byte b = 0;
		ushort us = 0;
		uint ui = 0;
		ulong ul = 0;
		char c = '\0';

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(sb, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(s, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(i, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(l, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(f, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(d, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(b, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(b, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(us, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(us, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(ui, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(ui, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(ul, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(ul, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(c, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(c, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfZero_DivIntNull_Fail()
	{
		sbyte sb = 0;
		short s = 0;
		int i = 0;
		long l = 0;
		float f = 0;
		double d = 0;
		byte b = 0;
		ushort us = 0;
		uint ui = 0;
		ulong ul = 0;
		char c = '\0';

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(sb, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(sb), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(s, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(s), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(i, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(i), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(l, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(l), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(f, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(f), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(d, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(d), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(b, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(b, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(b), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(us, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(us, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(us), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(ui, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(ui, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(ui), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(ul, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(ul, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(ul), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(c, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(c, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(c), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfZero_DivIntString_Fail()
	{
		string argName = "myArg";
		sbyte sb = 0;
		short s = 0;
		int i = 0;
		long l = 0;
		float f = 0;
		double d = 0;
		byte b = 0;
		ushort us = 0;
		uint ui = 0;
		ulong ul = 0;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(sb, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(s, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(i, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(l, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(f, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(f, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(d, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(d, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(b, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(b, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(us, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(us, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(ui, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(ui, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfZero(ul, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(ul, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNotMultiple_Success()
	{
		sbyte sb = 16;
		sbyte sbm = 4;
		short s = 16;
		short sm = 4;
		int i = 16;
		int im = 4;
		long l = 16;
		long lm = 4;
		byte b = 16;
		byte bm = 4;
		ushort us = 16;
		ushort usm = 4;
		uint ui = 16;
		uint uim = 4;
		ulong ul = 16;
		ulong ulm = 4;
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(sb, sbm);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(sb, sbm, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(s,sm);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(s, sm, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(i, im);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(i, im, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(l, lm);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(l, lm, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(b, bm);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(b, bm, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(us, usm);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(us, usm, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ul, ulm);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ul, ulm, GlobalConstants.CustomHResult);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ui, uim);
		CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ui, uim, GlobalConstants.CustomHResult);
	}

	[TestMethod]
	public void ThrowIfNotMultiple_DivNull_Fail()
	{
		sbyte sb = 15;
		sbyte sbm = 4;
		short s = 15;
		short sm = 4;
		int i = 15;
		int im = 4;
		long l = 15;
		long lm = 4;
		byte b = 15;
		byte bm = 4;
		ushort us = 15;
		ushort usm = 4;
		uint ui = 15;
		uint uim = 4;
		ulong ul = 15;
		ulong ulm = 4;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(sb, sbm));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(sb), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(s, sm));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(s), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(i, im));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(i), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(l, lm));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(l), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(b, bm));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(b, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(b), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(us, usm));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(us, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(us), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ui, uim));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(ui, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(ui), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ul, ulm));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(ul, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(ul), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNotMultiple_DivString_Fail()
	{
		string argName = "myArg";
		sbyte sb = 15;
		sbyte sbm = 4;
		short s = 15;
		short sm = 4;
		int i = 15;
		int im = 4;
		long l = 15;
		long lm = 4;
		byte b = 15;
		byte bm = 4;
		ushort us = 15;
		ushort usm = 4;
		uint ui = 15;
		uint uim = 4;
		ulong ul = 15;
		ulong ulm = 4;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(sb, sbm, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(s, sm, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(i, im, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(l, lm, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(b, bm, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(b, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(us, usm, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(us, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ui, uim, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(ui, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ul, ulm, argName));
		Assert.AreEqual(GlobalConstants.COR_E_ARGUMENTOUTOFRANGE, ex.HResult);
		Assert.AreEqual(ul, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}

	[TestMethod]
	public void ThrowIfNotMultiple_DivIntNull_Fail()
	{
		sbyte sb = 15;
		sbyte sbm = 4;
		short s = 15;
		short sm = 4;
		int i = 15;
		int im = 4;
		long l = 15;
		long lm = 4;
		byte b = 15;
		byte bm = 4;
		ushort us = 15;
		ushort usm = 4;
		uint ui = 15;
		uint uim = 4;
		ulong ul = 15;
		ulong ulm = 4;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(sb, sbm, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(sb), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(s, sm, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(s), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(i, im, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(i), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(l, lm, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(l), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(b, bm, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(b, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(b), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(us, usm, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(us, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(us), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ui, uim, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(ui, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(ui), ex.ParamName);
#endif

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ul, ulm, GlobalConstants.CustomHResult));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(ul, ex.ActualValue);
#if !NETFRAMEWORK && !NETSTANDARD2_0 && !NETSTANDARD2_1
		Assert.AreEqual(nameof(ul), ex.ParamName);
#endif
	}

	[TestMethod]
	public void ThrowIfNotMultiple_DivIntString_Fail()
	{
		string argName = "myArg";
		sbyte sb = 15;
		sbyte sbm = 4;
		short s = 15;
		short sm = 4;
		int i = 15;
		int im = 4;
		long l = 15;
		long lm = 4;
		byte b = 15;
		byte bm = 4;
		ushort us = 15;
		ushort usm = 4;
		uint ui = 15;
		uint uim = 4;
		ulong ul = 15;
		ulong ulm = 4;

		CodedArgumentOutOfRangeException ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(sb, sbm, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(sb, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(s, sm, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(s, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(i, im, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(i, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(l, lm, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(l, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(b, bm, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(b, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(us, usm, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(us, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ui, uim, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(ui, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);

		ex = Assert.ThrowsExactly<CodedArgumentOutOfRangeException>(() => CodedArgumentOutOfRangeException.ThrowIfNotMultiple(ul, ulm, GlobalConstants.CustomHResult, argName));
		Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
		Assert.AreEqual(ul, ex.ActualValue);
		Assert.AreEqual(argName, ex.ParamName);
	}
}
