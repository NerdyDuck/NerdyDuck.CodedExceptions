// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedXmlException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedXmlExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedXmlException();
		}
		catch (CodedXmlException ex)
		{
			Assert.AreEqual(Globals.XmlHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(0, ex.LineNumber);
			Assert.AreEqual(0, ex.LinePosition);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedXmlException(Globals.TestMessage);
		}
		catch (CodedXmlException ex)
		{
			Assert.AreEqual(Globals.XmlHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(0, ex.LineNumber);
			Assert.AreEqual(0, ex.LinePosition);
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
				throw new CodedXmlException(Globals.TestMessage, ex);
			}
		}
		catch (CodedXmlException ex)
		{
			Assert.AreEqual(Globals.XmlHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(0, ex.LineNumber);
			Assert.AreEqual(0, ex.LinePosition);
		}
	}

	[TestMethod]
	public void Ctor_StringExceptionInt32Int32_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedXmlException(Globals.TestMessage, ex, 2710, 42);
			}
		}
		catch (CodedXmlException ex)
		{
			Assert.AreEqual(Globals.XmlHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			StringAssert.Contains(ex.Message, Globals.TestMessage);
			Assert.AreEqual(2710, ex.LineNumber);
			Assert.AreEqual(42, ex.LinePosition);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedXmlException(Globals.CustomHResult);
		}
		catch (CodedXmlException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(0, ex.LineNumber);
			Assert.AreEqual(0, ex.LinePosition);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedXmlException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedXmlException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(0, ex.LineNumber);
			Assert.AreEqual(0, ex.LinePosition);
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
				throw new CodedXmlException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedXmlException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(0, ex.LineNumber);
			Assert.AreEqual(0, ex.LinePosition);
		}
	}

	[TestMethod]
	public void Ctor_IntStringExceptionInt32Int32_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedXmlException(Globals.CustomHResult, Globals.TestMessage, ex, 2710, 42);
			}
		}
		catch (CodedXmlException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			StringAssert.Contains(ex.Message, Globals.TestMessage);
			Assert.AreEqual(2710, ex.LineNumber);
			Assert.AreEqual(42, ex.LinePosition);
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
				throw new CodedXmlException(Globals.CustomHResult, Globals.TestMessage, ex, 2710, 42);
			}
		}
		catch (CodedXmlException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedXmlException ex2 = SerializationHelper.Deserialize<CodedXmlException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNotNull(ex2.InnerException);
			StringAssert.StartsWith(ex2.Message, Globals.TestMessage);
			Assert.AreEqual(2710, ex2.LineNumber);
			Assert.AreEqual(42, ex2.LinePosition);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedXmlException(Globals.CustomHResult, Globals.TestMessage, null, 2710, 42);
		}
		catch (CodedXmlException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedXmlException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, "2710");
			StringAssert.Contains(str, "42");
		}
	}
}
