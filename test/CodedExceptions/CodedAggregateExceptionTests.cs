// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
				throw new CodedAggregateException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedAggregateException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedAggregateException ex2 = SerializationHelper.Deserialize<CodedAggregateException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.AreEqual(1, ex2.InnerExceptions.Count);
			Assert.AreEqual(Globals.TestMessage, ex2.Message);
		}
	}
#endif

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
