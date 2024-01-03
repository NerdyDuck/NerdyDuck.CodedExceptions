// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NerdyDuck.CodedExceptions.IO;

namespace NerdyDuck.Tests.CodedExceptions.IO;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.IO.CodedFileNotFoundException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedFileNotFoundExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedFileNotFoundException();
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_FILENOTFOUND, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.FileName);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedFileNotFoundException(Globals.TestMessage);
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_FILENOTFOUND, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.FileName);
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
				throw new CodedFileNotFoundException(Globals.TestMessage, ex);
			}
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_FILENOTFOUND, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.FileName);
		}
	}

	[TestMethod]
	public void Ctor_StringString_Success()
	{
		try
		{
			throw new CodedFileNotFoundException(Globals.TestMessage, Globals.FileName);
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_FILENOTFOUND, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.FileName, ex.FileName);
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
				throw new CodedFileNotFoundException(Globals.TestMessage, Globals.FileName, ex);
			}
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.COR_E_FILENOTFOUND, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.FileName, ex.FileName);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedFileNotFoundException(Globals.CustomHResult);
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.FileName);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedFileNotFoundException(Globals.CustomHResult, Globals.TestMessage);
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.FileName);
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
				throw new CodedFileNotFoundException(Globals.CustomHResult, Globals.TestMessage, ex);
			}
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.IsNull(ex.FileName);
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_Success()
	{
		try
		{
			throw new CodedFileNotFoundException(Globals.CustomHResult, Globals.TestMessage, Globals.FileName);
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.FileName, ex.FileName);
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
				throw new CodedFileNotFoundException(Globals.CustomHResult, Globals.TestMessage, Globals.FileName, ex);
			}
		}
		catch (CodedFileNotFoundException ex)
		{
			Assert.AreEqual(Globals.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex.Message);
			Assert.AreEqual(Globals.FileName, ex.FileName);
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
				throw new CodedFileNotFoundException(Globals.CustomHResult, Globals.TestMessage, Globals.FileName, ex);
			}
		}
		catch (CodedFileNotFoundException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedFileNotFoundException ex2 = SerializationHelper.Deserialize<CodedFileNotFoundException>(buffer);

			Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
			Assert.IsNotNull(ex2.InnerException);
			Assert.AreEqual(Globals.TestMessage, ex2.Message);
			Assert.AreEqual(Globals.FileName, ex2.FileName);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedFileNotFoundException(Globals.CustomHResult, Globals.TestMessage, Globals.FileName);
		}
		catch (CodedFileNotFoundException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedFileNotFoundException).FullName, Globals.CustomHResultString, Globals.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, ex.FileName);
		}
	}
}
