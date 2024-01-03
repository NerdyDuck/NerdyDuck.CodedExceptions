// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NerdyDuck.CodedExceptions.IO;

namespace NerdyDuck.Tests.CodedExceptions.IO;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.IO.CodedDirectoryNotFoundException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedDirectoryNotFoundExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedDirectoryNotFoundException();
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_DIRECTORYNOTFOUND, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.DirectoryName);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedDirectoryNotFoundException(Globals.TestMessage);
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_DIRECTORYNOTFOUND, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.DirectoryName);
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
				throw new CodedDirectoryNotFoundException(Globals.TestMessage, ex);
			}
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_DIRECTORYNOTFOUND, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.DirectoryName);
		}
	}

	[TestMethod]
	public void Ctor_StringString_Success()
	{
		try
		{
			throw new CodedDirectoryNotFoundException(Globals.TestMessage, Globals.DirectoryName);
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_DIRECTORYNOTFOUND, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.DirectoryName, ex.DirectoryName);
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
				throw new CodedDirectoryNotFoundException(Globals.TestMessage, Globals.DirectoryName, ex);
			}
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_DIRECTORYNOTFOUND, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.DirectoryName, ex.DirectoryName);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedDirectoryNotFoundException(Globals.CustomHResult);
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.DirectoryName);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedDirectoryNotFoundException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.DirectoryName);
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
				throw new CodedDirectoryNotFoundException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.DirectoryName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_Success()
	{
		try
		{
			throw new CodedDirectoryNotFoundException(Globals.CustomHResult, Globals.TestMessage, Globals.DirectoryName);
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.DirectoryName, ex.DirectoryName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_MsgNull_Success()
	{
		try
		{
			throw new CodedDirectoryNotFoundException(Globals.CustomHResult, null, Globals.DirectoryName);
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNotNull(ex.Message);
			Assert.AreEqual(Globals.DirectoryName, ex.DirectoryName);
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
				throw new CodedDirectoryNotFoundException(Globals.CustomHResult, Globals.TestMessage, Globals.DirectoryName, ex);
			}
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.DirectoryName, ex.DirectoryName);
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
				throw new CodedDirectoryNotFoundException(Globals.CustomHResult, Globals.TestMessage, Globals.DirectoryName, ex);
			}
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedDirectoryNotFoundException ex2 = SerializationHelper.Deserialize<CodedDirectoryNotFoundException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNotNull(ex2.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex2.Message);
			Assert.AreEqual(Globals.DirectoryName, ex2.DirectoryName);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedDirectoryNotFoundException(Globals.CustomHResult, Globals.TestMessage, Globals.DirectoryName);
		}
		catch (CodedDirectoryNotFoundException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedDirectoryNotFoundException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, ex.DirectoryName);
		}
	}
}
