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
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
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
					List<Exception> exs = [ex, ex2];
					throw new CodedAggregateException(exs);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
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
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedAggregateException(GlobalConstants.TestMessage);
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(0, ex.InnerExceptions.Count);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
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
				throw new CodedAggregateException(GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(1, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
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
					List<Exception> exs = [ex, ex2];
					throw new CodedAggregateException(GlobalConstants.TestMessage, exs);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
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
					throw new CodedAggregateException(GlobalConstants.TestMessage, ex, ex2);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedAggregateException(GlobalConstants.CustomHResult);
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
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
					List<Exception> exs = [ex, ex2];
					throw new CodedAggregateException(GlobalConstants.CustomHResult, exs);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
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
					throw new CodedAggregateException(GlobalConstants.CustomHResult, ex, ex2);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedAggregateException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage);
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.AreEqual(0, ex.InnerExceptions.Count);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
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
				throw new CodedAggregateException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.AreEqual(1, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
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
					List<Exception> exs = [ex, ex2];
					throw new CodedAggregateException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, exs);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
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
					throw new CodedAggregateException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex, ex2);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.AreEqual(2, ex.InnerExceptions.Count);
			StringAssert.StartsWith(ex.Message, GlobalConstants.TestMessage);
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
				throw new CodedAggregateException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedAggregateException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedAggregateException ex2 = SerializationHelper.Deserialize<CodedAggregateException>(buffer);

			Assert.AreEqual(GlobalConstants.CustomHResult, ex2.HResult);
			Assert.AreEqual(1, ex2.InnerExceptions.Count);
			Assert.AreEqual(GlobalConstants.TestMessage, ex2.Message);
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
					throw new CodedAggregateException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex, ex2);
				}
			}
		}
		catch (CodedAggregateException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedAggregateException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, typeof(FormatException).FullName);
			StringAssert.Contains(str, typeof(NotSupportedException).FullName);
		}
	}
}
