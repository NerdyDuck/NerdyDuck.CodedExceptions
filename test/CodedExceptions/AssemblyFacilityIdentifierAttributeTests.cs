// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
