// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NerdyDuck.CodedExceptions.IO;

namespace NerdyDuck.Tests.CodedExceptions.IO;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.IO.CodedIOException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedIOExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedIOException();
		}
		catch (CodedIOException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_IO, ex.HResult);
			Assert.IsNull(ex.InnerException);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedIOException(GlobalConstants.TestMessage);
		}
		catch (CodedIOException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_IO, ex.HResult);
			Assert.IsNull(ex.InnerException);
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
				throw new CodedIOException(GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedIOException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_IO, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedIOException(GlobalConstants.CustomHResult);
		}
		catch (CodedIOException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new CodedIOException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage);
		}
		catch (CodedIOException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
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
				throw new CodedIOException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedIOException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
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
				throw new CodedIOException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedIOException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedIOException ex2 = SerializationHelper.Deserialize<CodedIOException>(buffer);

			Assert.AreEqual(GlobalConstants.CustomHResult, ex2.HResult);
			Assert.IsNotNull(ex2.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex2.Message);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new CodedIOException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage);
		}
		catch (CodedIOException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedIOException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
		}
	}
}
