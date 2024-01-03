// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedExceptionAttribute class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedExceptionAttributeTests
{
	[TestMethod]
	public void Ctor_Success()
	{
		_ = new CodedExceptionAttribute();
	}

	[TestMethod]
	public void IsCodedException_True()
	{
		Assert.IsTrue(CodedExceptionAttribute.IsCodedException(new CodedException()));
	}

	[TestMethod]
	public void IsCodedException_False()
	{
		Assert.IsFalse(CodedExceptionAttribute.IsCodedException(new ArgumentException()));
	}

	[TestMethod]
	public void IsCodedException_ExceptionNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => CodedExceptionAttribute.IsCodedException(null));
	}
}
