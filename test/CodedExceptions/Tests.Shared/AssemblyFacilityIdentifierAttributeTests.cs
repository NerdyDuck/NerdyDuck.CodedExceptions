#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.CodedExceptions - Unit tests for the
 * NerdyDuck.CodedExceptions assembly
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) Daniel Kopp, dak@nerdyduck.de
 *
 * All rights reserved.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ******************************************************************************/
#endregion

using System.Reflection;

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyFacilityIdentifierAttributeTests
{
	[TestMethod]
	public void Ctor_Success()
	{
		AssemblyFacilityIdentifierAttribute att = new(42);
		Assert.AreEqual(42, att.FacilityId);
	}

	[TestMethod]
	public void Ctor_IdLessThan0_Throw()
	{
		_ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _ = new AssemblyFacilityIdentifierAttribute(-1));
	}

	[TestMethod]
	public void Ctor_IdGreaterThan2047_Throw()
	{
		_ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _ = new AssemblyFacilityIdentifierAttribute(2048));
	}

	[TestMethod]
	public void FromAssembly_Success()
	{
		Assembly assembly = typeof(AssemblyFacilityIdentifierAttributeTests).GetTypeInfo().Assembly;
		AssemblyFacilityIdentifierAttribute att = AssemblyFacilityIdentifierAttribute.FromAssembly(assembly);
		Assert.IsNotNull(att);
		Assert.AreEqual(42, att.FacilityId);
	}

	[TestMethod]
	public void FromAssembly_AssemblyNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => AssemblyFacilityIdentifierAttribute.FromAssembly(null));
	}

	[TestMethod]
	public void FromType_Success()
	{
		Type type = typeof(AssemblyFacilityIdentifierAttributeTests);
		AssemblyFacilityIdentifierAttribute att = AssemblyFacilityIdentifierAttribute.FromType(type);
		Assert.IsNotNull(att);
		Assert.AreEqual(42, att.FacilityId);
	}

	[TestMethod]
	public void FromType_AssemblyNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => AssemblyFacilityIdentifierAttribute.FromType(null));
	}
}
