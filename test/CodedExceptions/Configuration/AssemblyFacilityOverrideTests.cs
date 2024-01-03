// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverride class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyFacilityOverrideTests
{
	private static readonly AssemblyIdentity s_thisAssemblyIdentity = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
	private static readonly AssemblyIdentity s_otherAssemblyIdentity = new(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);

	[TestMethod]
	public void Ctor_AssemblyIdentityInt_Success()
	{
		AssemblyFacilityOverride assemblyFacilityOverride = new(s_thisAssemblyIdentity, 42);
		Assert.AreEqual(s_thisAssemblyIdentity, assemblyFacilityOverride.AssemblyName);
		Assert.AreEqual(42, assemblyFacilityOverride.Identifier);

	}

	[TestMethod]
	public void Ctor_AssemblyIdentityNullInt_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => new AssemblyFacilityOverride((AssemblyIdentity)null, 42));
	}

	[TestMethod]
	public void Ctor_AssemblyIdentityIntOOR_Throw()
	{
		_ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AssemblyFacilityOverride(s_thisAssemblyIdentity, -1), "i<0");
		_ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AssemblyFacilityOverride(s_thisAssemblyIdentity, 7711), "i>2047");
	}

	[TestMethod]
	public void Ctor_StringInt_Success()
	{
		AssemblyFacilityOverride assemblyFacilityOverride = new(s_thisAssemblyIdentity.ToString(), 42);
		Assert.AreEqual(s_thisAssemblyIdentity, assemblyFacilityOverride.AssemblyName);
		Assert.AreEqual(42, assemblyFacilityOverride.Identifier);
	}

#if NETFRAMEWORK
	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		AssemblyFacilityOverride assemblyFacilityOverride1 = new(s_thisAssemblyIdentity, 42);
		using System.IO.MemoryStream buffer = SerializationHelper.Serialize(assemblyFacilityOverride1);
		AssemblyFacilityOverride assemblyFacilityOverride2 = SerializationHelper.Deserialize<AssemblyFacilityOverride>(buffer);

		Assert.AreEqual(s_thisAssemblyIdentity, assemblyFacilityOverride2.AssemblyName);
		Assert.AreEqual(42, assemblyFacilityOverride2.Identifier);
	}

	[TestMethod]
	public void Ctor_SerializationInfoNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => SerializationHelper.InvokeSerializationConstructorWithNullContext(typeof(AssemblyFacilityOverride)));
	}

	[TestMethod]
	public void GetObjectData_SerializationInfoNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() =>
		  {
			  System.Runtime.Serialization.ISerializable assemblyFacilityOverride = new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42);
			  assemblyFacilityOverride.GetObjectData(null, new System.Runtime.Serialization.StreamingContext());
		  });
	}
#endif

	[TestMethod]
	public void GetHashCode_Success()
	{
		AssemblyFacilityOverride assemblyFacilityOverride = new(s_thisAssemblyIdentity, 42);
		int i = assemblyFacilityOverride.GetHashCode();
		Assert.AreNotEqual(0, i);
	}

	[TestMethod]
	public void Equals_Various_Success()
	{
		AssemblyFacilityOverride assemblyFacilityOverride1a = new(s_thisAssemblyIdentity, 42);
		AssemblyFacilityOverride assemblyFacilityOverride1b = new(s_thisAssemblyIdentity, 42);
		AssemblyFacilityOverride assemblyFacilityOverride1c = new(s_thisAssemblyIdentity, 24);
		AssemblyFacilityOverride assemblyFacilityOverride2 = new(s_otherAssemblyIdentity, 17);

		Assert.IsFalse(assemblyFacilityOverride1a.Equals((AssemblyFacilityOverride)null), "1a=null");
		Assert.IsFalse(assemblyFacilityOverride1a.Equals((object)null), "1a=objnull");
		Assert.IsFalse(assemblyFacilityOverride1a.Equals(new object()), "1a=obj");
		Assert.IsTrue(assemblyFacilityOverride1a.Equals((object)assemblyFacilityOverride1a), "1a=obj1a");

		Assert.IsTrue(assemblyFacilityOverride1a.Equals(assemblyFacilityOverride1a), "1a=1a");
		Assert.IsTrue(assemblyFacilityOverride1a.Equals(assemblyFacilityOverride1b), "1a=1b");
		Assert.IsFalse(assemblyFacilityOverride1a.Equals(assemblyFacilityOverride2), "1a!=2");
		Assert.IsFalse(assemblyFacilityOverride1a.Equals(assemblyFacilityOverride1c), "1a!=1c");
	}
}
