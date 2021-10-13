#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.CodedExceptions - Unit tests for the
 * NerdyDuck.CodedExceptions assembly
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) Daniel Kopp, dak@nerdyduck.de
 *
 * All rights reserved.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ******************************************************************************/
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions;

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

	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		try
		{
			throw new CodedArgumentOutOfRangeException(Globals.CustomHResult, Globals.ParamName, 42, Globals.TestMessage);
		}
		catch (CodedArgumentOutOfRangeException ex)
		{
			using System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
			CodedArgumentOutOfRangeException ex2 = SerializationHelper.Deserialize<CodedArgumentOutOfRangeException>(Buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNull(ex2.InnerException);
			Assert.AreEqual(42, ex2.ActualValue);
			StringAssert.StartsWith(ex2.Message, Globals.TestMessage);
			Assert.AreEqual(Globals.ParamName, ex2.ParamName);
		}
	}

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
