// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedNotSupportedException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedNotSupportedExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedNotSupportedException();
		}
		catch (CodedNotSupportedException ex)
		{
			Assert.AreEqual(Globals.COR_E_NOTSUPPORTED, ex.HResult);
			Assert.IsNull(ex.InnerException);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedNotSupportedException(Globals.TestMessage);
		}
		catch (CodedNotSupportedException ex)
		{
			Assert.AreEqual(Globals.COR_E_NOTSUPPORTED, ex.HResult);
			Assert.IsNull(ex.InnerException);
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
				throw new CodedNotSupportedException(Globals.TestMessage, ex);
			}
		}
		catch (CodedNotSupportedException ex)
		{
			Assert.AreEqual(Globals.COR_E_NOTSUPPORTED, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedNotSupportedException(Globals.CustomHResult);
		}
		catch (CodedNotSupportedException ex)
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
			throw new CodedNotSupportedException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedNotSupportedException ex)
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
				throw new CodedNotSupportedException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedNotSupportedException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
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
				throw new CodedNotSupportedException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedNotSupportedException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedNotSupportedException ex2 = SerializationHelper.Deserialize<CodedNotSupportedException>(buffer);

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
			throw new CodedNotSupportedException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedNotSupportedException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, "{0}: ({1}) {2}", typeof(CodedNotSupportedException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
		}
	}
}
