// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.ExceptionExtensions class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class ExceptionExtensionsTests
{
	[TestMethod]
	public void GetErrorId_Success()
	{
		try
		{
			throw new CodedException(Globals.CustomHResult);
		}
		catch (CodedException ex)
		{
			Assert.AreEqual(0x1234, ExceptionExtensions.GetErrorId(ex));
		}
	}

	[TestMethod]
	public void GetErrorId_ExceptionNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => ExceptionExtensions.GetErrorId(null));
	}

	[TestMethod]
	public void GetFacilityId_Success()
	{
		try
		{
			throw new CodedException(Globals.CustomHResult);
		}
		catch (CodedException ex)
		{
			Assert.AreEqual(2047, ExceptionExtensions.GetFacilityId(ex));
		}
	}

	[TestMethod]
	public void GetFacilityId_ExceptionNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => ExceptionExtensions.GetFacilityId(null));
	}

	[TestMethod]
	public void IsCodedException_True()
	{
		try
		{
			throw new CodedException(Globals.CustomHResult);
		}
		catch (CodedException ex)
		{
			Assert.IsTrue(ExceptionExtensions.IsCodedException(ex));
		}
	}

	[TestMethod]
	public void IsCodedException_False()
	{
		try
		{
			throw new NotImplementedException();
		}
		catch (NotImplementedException ex)
		{
			Assert.IsFalse(ExceptionExtensions.IsCodedException(ex));
		}
	}

	[TestMethod]
	public void IsCodedException_ExceptionNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => ExceptionExtensions.IsCodedException(null));
	}

	[TestMethod]
	public void IsCustomHResult_True()
	{
		try
		{
			throw new CodedException(Globals.CustomHResult);
		}
		catch (CodedException ex)
		{
			Assert.IsTrue(ExceptionExtensions.IsCustomHResult(ex));
		}
	}

	[TestMethod]
	public void IsCustomHResult_False()
	{
		try
		{
			throw new NotImplementedException();
		}
		catch (NotImplementedException ex)
		{
			Assert.IsFalse(ExceptionExtensions.IsCustomHResult(ex));
		}
	}

	[TestMethod]
	public void IsCustomHResult_ExceptionNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => ExceptionExtensions.IsCustomHResult(null));
	}
}
