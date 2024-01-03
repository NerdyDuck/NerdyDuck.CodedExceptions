// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net.Sockets;

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedSocketException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedSocketExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedSocketException();
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.E_FAIL, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
		}
	}

	[TestMethod]
	public void Ctor_SocketError_Success()
	{
		try
		{
			throw new CodedSocketException(SocketError.VersionNotSupported);
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.E_FAIL, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedSocketException(Globals.TestMessage);
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.E_FAIL, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
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
				throw new CodedSocketException(Globals.TestMessage, ex);
			}
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.E_FAIL, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
		}
	}

	[TestMethod]
	public void Ctor_SocketErrorString_Success()
	{
		try
		{
			throw new CodedSocketException(SocketError.VersionNotSupported, Globals.TestMessage);
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.E_FAIL, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
		}
	}

	[TestMethod]
	public void Ctor_SocketErrorStringException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedSocketException(SocketError.VersionNotSupported, Globals.TestMessage, ex);
			}
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.E_FAIL, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedSocketException(Globals.CustomHResult);
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
		}
	}

	[TestMethod]
	public void Ctor_Int32SocketError_Success()
	{
		try
		{
			throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported);
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedSocketException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
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
				throw new CodedSocketException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
		}
	}

	[TestMethod]
	public void Ctor_IntSocketErrorString_Success()
	{
		try
		{
			throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported, Globals.TestMessage);
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
		}
	}

	[TestMethod]
	public void Ctor_IntSocketErrorStringException_Success()
	{
		try
		{
			try
			{
				throw new FormatException();
			}
			catch (Exception ex)
			{
				throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported, Globals.TestMessage, ex);
			}
		}
		catch (CodedSocketException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
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
				throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported, Globals.TestMessage, ex);
			}
		}
		catch (CodedSocketException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedSocketException ex2 = SerializationHelper.Deserialize<CodedSocketException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNotNull(ex2.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex2.Message);
			Assert.AreEqual(SocketError.VersionNotSupported, ex2.SocketErrorCode);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported, Globals.TestMessage);
		}
		catch (CodedSocketException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedSocketException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, ((int)SocketError.VersionNotSupported).ToString(CultureInfo.InvariantCulture));
		}
	}
}
