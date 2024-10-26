// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using NerdyDuck.CodedExceptions;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedJsonException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedJsonExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedJsonException();
		}
		catch (CodedJsonException ex)
		{
			Assert.IsNull(ex.InnerException);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedJsonException(Globals.TestMessage);
		}
		catch (CodedJsonException ex)
		{
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_StringStringLongLong_Success()
	{
		try
		{
			throw new CodedJsonException(Globals.TestMessage, Globals.TestPath, 42, 27);
		}
		catch (CodedJsonException ex)
		{
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.TestPath, ex.Path);
			Assert.AreEqual(42, ex.LineNumber);
			Assert.AreEqual(27, ex.BytePositionInLine);
		}
	}

	[TestMethod]
	public void Ctor_StringStringLongLongException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedJsonException(Globals.TestMessage, Globals.TestPath, 42, 27, ex);
			}
		}
		catch (CodedJsonException ex)
		{
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.TestPath, ex.Path);
			Assert.AreEqual(42, ex.LineNumber);
			Assert.AreEqual(27, ex.BytePositionInLine);
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
				throw new CodedJsonException(Globals.TestMessage, ex);
			}
		}
		catch (CodedJsonException ex)
		{
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedJsonException(Globals.CustomHResult);
		}
		catch (CodedJsonException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedJsonException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedJsonException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
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
				throw new CodedJsonException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedJsonException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_IntStringStringLongLong_Success()
	{
		try
		{
			throw new CodedJsonException(Globals.CustomHResult, Globals.TestMessage, Globals.TestPath, 42, 27);
		}
		catch (CodedJsonException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.TestPath, ex.Path);
			Assert.AreEqual(42, ex.LineNumber);
			Assert.AreEqual(27, ex.BytePositionInLine);
		}
	}

	[TestMethod]
	public void Ctor_IntStringStringLongLongException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedJsonException(Globals.CustomHResult, Globals.TestMessage, Globals.TestPath, 42, 27, ex);
			}
		}
		catch (CodedJsonException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.TestPath, ex.Path);
			Assert.AreEqual(42, ex.LineNumber);
			Assert.AreEqual(27, ex.BytePositionInLine);
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
				throw new CodedJsonException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedJsonException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedJsonException ex2 = SerializationHelper.Deserialize<CodedJsonException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNotNull(ex2.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex2.Message);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedJsonException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedJsonException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedJsonException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
		}
	}
}
