// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedCommunicationException class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedCommunicationExceptionTests
{
	[TestMethod]
	public void Ctor_Void_Success()
	{
		try
		{
			throw new CodedCommunicationException();
		}
		catch (CodedCommunicationException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
			Assert.IsNull(ex.InnerException);
		}
	}

	[TestMethod]
	public void Ctor_String_Success()
	{
		try
		{
			throw new CodedCommunicationException(GlobalConstants.TestMessage);
		}
		catch (CodedCommunicationException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
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
				throw new CodedCommunicationException(GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedCommunicationException ex)
		{
			Assert.AreEqual(GlobalConstants.COR_E_EXCEPTION, ex.HResult);
			Assert.IsNotNull(ex.InnerException);
			Assert.AreEqual(GlobalConstants.TestMessage, ex.Message);
		}
	}

	[TestMethod]
	public void Ctor_Int32_Success()
	{
		try
		{
			throw new CodedCommunicationException(GlobalConstants.CustomHResult);
		}
		catch (CodedCommunicationException ex)
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
			throw new CodedCommunicationException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage);
		}
		catch (CodedCommunicationException ex)
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
				throw new CodedCommunicationException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedCommunicationException ex)
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
				throw new CodedCommunicationException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage, ex);
			}
		}
		catch (CodedCommunicationException ex)
		{
			using System.IO.MemoryStream buffer = SerializationHelper.Serialize(ex);
			CodedCommunicationException ex2 = SerializationHelper.Deserialize<CodedCommunicationException>(buffer);

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
			throw new CodedCommunicationException(GlobalConstants.CustomHResult, GlobalConstants.TestMessage);
		}
		catch (CodedCommunicationException ex)
		{
			string str = ex.ToString();
			StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, GlobalConstants.DefaultToStringFormat, typeof(CodedCommunicationException).FullName, GlobalConstants.CustomHResultString, GlobalConstants.TestMessage));
			StringAssert.Contains(str, nameof(ToString_Success));
		}
	}
}
