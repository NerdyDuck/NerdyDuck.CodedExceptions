﻿// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
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
}