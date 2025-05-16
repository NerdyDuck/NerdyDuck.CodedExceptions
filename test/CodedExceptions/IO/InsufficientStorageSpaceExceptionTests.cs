// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NerdyDuck.CodedExceptions.IO;

namespace NerdyDuck.Tests.CodedExceptions.IO;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.IO.InsufficientStorageSpaceException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class InsufficientStorageSpaceExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new InsufficientStorageSpaceException();
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_IO, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.StoragePath);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new InsufficientStorageSpaceException(GlobalConstants.TestMessage);
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_IO, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsNull(ex.StoragePath);
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
				throw new InsufficientStorageSpaceException(GlobalConstants.TestMessage, ex);
			}
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_IO, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsNull(ex.StoragePath);
		}
	}

	[TestMethod]
	public void Ctor_StringString_Success()
	{
		try
		{
			throw new InsufficientStorageSpaceException(GlobalConstants.TestMessage, GlobalConstants.FileName);
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_IO, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.AreEqual(GlobalConstants.FileName, ex.StoragePath);
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
				throw new InsufficientStorageSpaceException(GlobalConstants.TestMessage, GlobalConstants.FileName, ex);
			}
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_IO, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.AreEqual(GlobalConstants.FileName, ex.StoragePath);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new InsufficientStorageSpaceException(GlobalConstants.CustomHResult);
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.IsNull(ex.StoragePath);
		}
	}

	[TestMethod]
	public void Ctor_IntString_Success()
	{
		try
		{
			throw new InsufficientStorageSpaceException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage);
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsNull(ex.StoragePath);
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
				throw new InsufficientStorageSpaceException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.IsNull(ex.StoragePath);
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_Success()
	{
		try
		{
			throw new InsufficientStorageSpaceException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, GlobalConstants.FileName);
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.AreEqual(GlobalConstants.FileName, ex.StoragePath);
		}
	}

	[TestMethod]
	public void Ctor_IntStringString_MsgNull_Success()
	{
		try
		{
			throw new InsufficientStorageSpaceException(GlobalConstants.CustomHResult, null, GlobalConstants.FileName);
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNull(ex.InnerException);
			StringAssert.Contains(ex.Message, GlobalConstants.FileName);
			Assert.AreEqual(GlobalConstants.FileName, ex.StoragePath);
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
				throw new InsufficientStorageSpaceException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, GlobalConstants.FileName, ex);
			}
		}
		catch (InsufficientStorageSpaceException ex)
		{
			Assert.AreEqual(GlobalConstants.CustomHResult, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
			Assert.AreEqual(GlobalConstants.FileName, ex.StoragePath);
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
				throw new InsufficientStorageSpaceException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, GlobalConstants.FileName, ex);
			}
		}
		catch (InsufficientStorageSpaceException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			InsufficientStorageSpaceException ex2 = SerializationHelper.Deserialize<InsufficientStorageSpaceException>(buffer);

			Assert.AreEqual(GlobalConstants.CustomHResult, ex2.HResult);
			Assert.IsNotNull(ex2.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex2.Message);
			Assert.AreEqual(GlobalConstants.FileName, ex2.StoragePath);
		}
	}
#endif

	[TestMethod]
	public void ToString_Success()
	{
		try
		{
			throw new InsufficientStorageSpaceException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, GlobalConstants.FileName);
		}
		catch (InsufficientStorageSpaceException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(InsufficientStorageSpaceException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
			StringAssert.Contains(str, ex.StoragePath);
		}
	}
}
