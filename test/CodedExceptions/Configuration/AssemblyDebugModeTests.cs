// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyDebugMode class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyDebugModeTests
{
	private static readonly AssemblyIdentity s_thisAssemblyIdentity = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
	private static readonly AssemblyIdentity s_otherAssemblyIdentity = new(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);

	[TestMethod]
	public void Ctor_AssemblyIdentityBool_Success()
	{
		AssemblyDebugMode assemblyDebugMode = new(s_thisAssemblyIdentity, true);
		Assert.AreEqual(s_thisAssemblyIdentity, assemblyDebugMode.AssemblyName);
		Assert.IsTrue(assemblyDebugMode.IsEnabled);

		assemblyDebugMode = new AssemblyDebugMode(s_thisAssemblyIdentity, false);
		Assert.AreEqual(s_thisAssemblyIdentity, assemblyDebugMode.AssemblyName);
		Assert.IsFalse(assemblyDebugMode.IsEnabled);
	}

	[TestMethod]
	public void Ctor_AssemblyIdentityNullBool_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => new AssemblyDebugMode((AssemblyIdentity)null, true));
	}

	[TestMethod]
	public void Ctor_StringBool_Success()
	{
		AssemblyDebugMode assemblyDebugMode = new(s_thisAssemblyIdentity.ToString(), true);
		Assert.AreEqual(s_thisAssemblyIdentity, assemblyDebugMode.AssemblyName);
		Assert.IsTrue(assemblyDebugMode.IsEnabled);
	}

#if NETFRAMEWORK
	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		AssemblyDebugMode assemblyDebugMode1 = new(s_thisAssemblyIdentity, true);
		using System.IO.MemoryStream buffer = SerializationHelper.Serialize(assemblyDebugMode1);
		AssemblyDebugMode assemblyDebugMode2 = SerializationHelper.Deserialize<AssemblyDebugMode>(buffer);

		Assert.AreEqual(s_thisAssemblyIdentity, assemblyDebugMode2.AssemblyName);
		Assert.IsTrue(assemblyDebugMode2.IsEnabled);
	}

	[TestMethod]
	public void Ctor_SerializationInfoNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => SerializationHelper.InvokeSerializationConstructorWithNullContext(typeof(AssemblyDebugMode)));
	}

	[TestMethod]
	public void GetObjectData_SerializationInfoNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() =>
		  {
			  System.Runtime.Serialization.ISerializable assemblyDebugMode = new AssemblyDebugMode(s_thisAssemblyIdentity, true);
			  assemblyDebugMode.GetObjectData(null, new System.Runtime.Serialization.StreamingContext());
		  });
	}
#endif

	[TestMethod]
	public void GetHashCode_Success()
	{
		AssemblyDebugMode assemblyDebugMode = new(s_thisAssemblyIdentity, true);
		int i = assemblyDebugMode.GetHashCode();
		Assert.AreNotEqual(0, i);
	}

	[TestMethod]
	public void Equals_Various_Success()
	{
		AssemblyDebugMode assemblyDebugMode1a = new(s_thisAssemblyIdentity, true);
		AssemblyDebugMode assemblyDebugMode1b = new(s_thisAssemblyIdentity, true);
		AssemblyDebugMode assemblyDebugMode1c = new(s_thisAssemblyIdentity, false);
		AssemblyDebugMode assemblyDebugMode2 = new(s_otherAssemblyIdentity, true);

		Assert.IsFalse(assemblyDebugMode1a.Equals((AssemblyDebugMode)null), "1a=null");
		Assert.IsFalse(assemblyDebugMode1a.Equals((object)null), "1a=objnull");
		Assert.IsFalse(assemblyDebugMode1a.Equals(new object()), "1a=obj");
		Assert.IsTrue(assemblyDebugMode1a.Equals((object)assemblyDebugMode1a), "1a=obj1a");

		Assert.IsTrue(assemblyDebugMode1a.Equals(assemblyDebugMode1a), "1a=1a");
		Assert.IsTrue(assemblyDebugMode1a.Equals(assemblyDebugMode1b), "1a=1b");
		Assert.IsFalse(assemblyDebugMode1a.Equals(assemblyDebugMode1c), "1a!=1c");
		Assert.IsFalse(assemblyDebugMode1a.Equals(assemblyDebugMode2), "1a!=2");
	}
}
