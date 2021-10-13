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

using System.Collections.Generic;

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedAggregateException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedAggregateExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedAggregateException();
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(0, ex.InnerExceptions.Count);
		}
	}

	[TestMethod]
	public void Ctor_IEnumerableException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				try
				{
					throw new NotSupportedException();
				}
				catch (Exception ex2)
				{
					List<Exception> exs = new() { ex, ex2 };
					throw new CodedAggregateException(exs);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
		}
	}

	[TestMethod]
	public void Ctor_ParamsException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				try
				{
					throw new NotSupportedException();
				}
				catch (Exception ex2)
				{
					throw new CodedAggregateException(ex, ex2);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedAggregateException(Globals.TestMessage);
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(0, ex.InnerExceptions.Count);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
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
				throw new CodedAggregateException(Globals.TestMessage, ex);
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(1, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
		}
	}

	[TestMethod]
	public void Ctor_StringIEnumerableException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				try
				{
					throw new NotSupportedException();
				}
				catch (Exception ex2)
				{
					List<Exception> exs = new() { ex, ex2 };
					throw new CodedAggregateException(Globals.TestMessage, exs);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
		}
	}

	[TestMethod]
	public void Ctor_StringParamsException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				try
				{
					throw new NotSupportedException();
				}
				catch (Exception ex2)
				{
					throw new CodedAggregateException(Globals.TestMessage, ex, ex2);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedAggregateException(Globals.CustomHResult);
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.AreEqual(0, ex.InnerExceptions.Count);
		}
	}

	[TestMethod]
	public void Ctor_Int32IEnumerableException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				try
				{
					throw new NotSupportedException();
				}
				catch (Exception ex2)
				{
					List<Exception> exs = new() { ex, ex2 };
					throw new CodedAggregateException(Globals.CustomHResult, exs);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
		}
	}

	[TestMethod]
	public void Ctor_Int32ParamsException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				try
				{
					throw new NotSupportedException();
				}
				catch (Exception ex2)
				{
					throw new CodedAggregateException(Globals.CustomHResult, ex, ex2);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedAggregateException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.AreEqual(0, ex.InnerExceptions.Count);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
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
				throw new CodedAggregateException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.AreEqual(1, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
		}
	}

	[TestMethod]
	public void Ctor_Int32StringIEnumerableException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				try
				{
					throw new NotSupportedException();
				}
				catch (Exception ex2)
				{
					List<Exception> exs = new() { ex, ex2 };
					throw new CodedAggregateException(Globals.CustomHResult, Globals.TestMessage, exs);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
		}
	}

	[TestMethod]
	public void Ctor_Int32StringParamsException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				try
				{
					throw new NotSupportedException();
				}
				catch (Exception ex2)
				{
					throw new CodedAggregateException(Globals.CustomHResult, Globals.TestMessage, ex, ex2);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, Globals.TestMessage);
		}
	}

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
				throw new CodedAggregateException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedAggregateException ex)
		{
			using System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
			CodedAggregateException ex2 = SerializationHelper.Deserialize<CodedAggregateException>(Buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.AreEqual(1, ex2.InnerExceptions.Count);
#if NET48
					Assert.AreEqual(Globals.TestMessage, ex2.Message);
#else
			StringAssert.StartsWith(ex2.Message, Globals.TestMessage);
#endif
		}
	}

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				try
				{
					throw new NotSupportedException();
				}
				catch (Exception ex2)
				{
					throw new CodedAggregateException(Globals.CustomHResult, Globals.TestMessage, ex, ex2);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedAggregateException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, typeof(FormatException).FullName);
			StringAssert.Contains(str, typeof(NotSupportedException).FullName);
		}
	}
}
