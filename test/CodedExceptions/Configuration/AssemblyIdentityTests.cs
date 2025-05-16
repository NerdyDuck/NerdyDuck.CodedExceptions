// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyIdentity class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyIdentityTests
{
	private const string NeutralLanguage = "neutral";
	private const string AssemblyNameOnly = "TestAssembly";
	private const string AssemblyNamePkt = "TestAssembly, PublicKeyToken=0123456789abcdef";
	private const string AssemblyNamePktOdd = "TestAssembly, PublicKeyToken=0123456789abcd";
	private const string AssemblyNamePktInv = "TestAssembly, PublicKeyToken=01234W6789abcdef";
	private const string AssemblyNameCulturePkt = "TestAssembly, Culture=neutral, PublicKeyToken=0123456789abcdef";
	private const string AssemblyNameVersionInv = "TestAssembly, Version=1.0.xxx";
	private static readonly byte[] s_pkt1 = [0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef];

#if NETFRAMEWORK
	private static readonly Assembly s_localizedAssembly = Assembly.ReflectionOnlyLoadFrom(@"de\NerdyDuck.CodedExceptions.resources.dll");
	private static readonly Assembly s_testAssembly = Assembly.ReflectionOnlyLoadFrom("TestAssembly.dll");
#else
	private static readonly Assembly s_localizedAssembly = Assembly.LoadFrom(@"de\NerdyDuck.CodedExceptions.resources.dll");
	private static readonly Assembly s_testAssembly = Assembly.LoadFrom("TestAssembly.dll");
#endif

	[TestMethod]
	public void Ctor_Void_Success()
	{
		AssemblyIdentity assemblyIdentity = new();
		Assert.IsNull(assemblyIdentity.Culture);
		Assert.IsNull(assemblyIdentity.Name);
		Assert.IsNull(assemblyIdentity.Version);
		Assert.IsNull(assemblyIdentity.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_StringFull_Success()
	{
		AssemblyIdentity assemblyIdentity = new(GlobalConstants.ThisAssemblyNameString);
		Assert.AreEqual(NeutralLanguage, assemblyIdentity.Culture);
		Assert.AreEqual(GlobalConstants.ThisAssemblyName.Name, assemblyIdentity.Name);
		Assert.AreEqual(GlobalConstants.ThisAssemblyName.Version, assemblyIdentity.Version);
		CollectionAssert.AreEqual(GlobalConstants.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_StringNameOnly_Success()
	{
		AssemblyIdentity assemblyIdentity = new(AssemblyNameOnly);
		Assert.IsNull(assemblyIdentity.Culture, nameof(assemblyIdentity.Culture));
		Assert.AreEqual(AssemblyNameOnly, assemblyIdentity.Name, true, nameof(assemblyIdentity.Name));
		Assert.IsNull(assemblyIdentity.Version, nameof(assemblyIdentity.Version));
		Assert.IsNull(assemblyIdentity.GetPublicKeyToken(), "PublicKeyToken");
	}

	[TestMethod]
	public void Ctor_StringNamePkt_Success()
	{
		AssemblyIdentity assemblyIdentity = new(AssemblyNamePkt);
		Assert.IsNull(assemblyIdentity.Culture, nameof(assemblyIdentity.Culture));
		Assert.AreEqual(AssemblyNameOnly, assemblyIdentity.Name, true, nameof(assemblyIdentity.Name));
		Assert.IsNull(assemblyIdentity.Version, nameof(assemblyIdentity.Version));
		CollectionAssert.AreEqual(s_pkt1, assemblyIdentity.GetPublicKeyToken(), "PublicKeyToken");
	}

	[TestMethod]
	public void Ctor_StringNamePktInv_Throw()
	{
		_ = Assert.ThrowsExactly<FormatException>(() => _ = new AssemblyIdentity(AssemblyNamePktInv));
	}

	[TestMethod]
	public void Ctor_StringNameCulturePkt_Success()
	{
		AssemblyIdentity assemblyIdentity = new(AssemblyNameCulturePkt);
		Assert.AreEqual(NeutralLanguage, assemblyIdentity.Culture, false, nameof(assemblyIdentity.Culture));
		Assert.AreEqual(AssemblyNameOnly, assemblyIdentity.Name, true, nameof(assemblyIdentity.Name));
		Assert.IsNull(assemblyIdentity.Version, nameof(assemblyIdentity.Version));
		CollectionAssert.AreEqual(s_pkt1, assemblyIdentity.GetPublicKeyToken(), "PublicKeyToken");
	}

	[TestMethod]
	public void Ctor_StringNameVersionInv_Throw()
	{
		_ = Assert.ThrowsExactly<FormatException>(() => _ = new AssemblyIdentity(AssemblyNameVersionInv));
	}

	[TestMethod]
	public void Ctor_AssemblyIdentity_Success()
	{
		AssemblyIdentity assemblyIdentity1 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity2 = new(assemblyIdentity1);
		Assert.AreEqual(assemblyIdentity1.Culture, assemblyIdentity2.Culture);
		Assert.AreEqual(assemblyIdentity1.Name, assemblyIdentity2.Name);
		Assert.AreEqual(assemblyIdentity1.Version, assemblyIdentity2.Version);
		CollectionAssert.AreEqual(assemblyIdentity1.GetPublicKeyToken(), assemblyIdentity2.GetPublicKeyToken());
		assemblyIdentity1 = new AssemblyIdentity(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Name);
		assemblyIdentity2 = new AssemblyIdentity(assemblyIdentity1);
		Assert.AreEqual(assemblyIdentity1.Culture, assemblyIdentity2.Culture);
		Assert.AreEqual(assemblyIdentity1.Name, assemblyIdentity2.Name);
		Assert.AreEqual(assemblyIdentity1.Version, assemblyIdentity2.Version);
		CollectionAssert.AreEqual(assemblyIdentity1.GetPublicKeyToken(), assemblyIdentity2.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_Assembly_Success()
	{
		AssemblyIdentity assemblyIdentity = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		Assert.AreEqual(NeutralLanguage, assemblyIdentity.Culture);
		Assert.AreEqual(GlobalConstants.ThisAssemblyName.Name, assemblyIdentity.Name);
		Assert.AreEqual(GlobalConstants.ThisAssemblyName.Version, assemblyIdentity.Version);
		CollectionAssert.AreEqual(GlobalConstants.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_AssemblyName_Success()
	{
		AssemblyIdentity assemblyIdentity = new(GlobalConstants.ThisAssembly.GetName(), AssemblyIdentity.AssemblyNameElements.All);
		Assert.AreEqual(NeutralLanguage, assemblyIdentity.Culture);
		Assert.AreEqual(GlobalConstants.ThisAssemblyName.Name, assemblyIdentity.Name);
		Assert.AreEqual(GlobalConstants.ThisAssemblyName.Version, assemblyIdentity.Version);
		CollectionAssert.AreEqual(GlobalConstants.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_AssemblyCulture_Success()
	{
		AssemblyIdentity assemblyIdentity = new(s_localizedAssembly, AssemblyIdentity.AssemblyNameElements.All);
		Assert.AreEqual("de", assemblyIdentity.Culture);
	}

	[TestMethod]
	public void Ctor_AssemblyNoVersion_Success()
	{
		AssemblyIdentity assemblyIdentity = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
		Assert.AreEqual(NeutralLanguage, assemblyIdentity.Culture);
		Assert.AreEqual(GlobalConstants.ThisAssemblyName.Name, assemblyIdentity.Name);
		Assert.IsNull(assemblyIdentity.Version);
		CollectionAssert.AreEqual(GlobalConstants.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_AssemblyNull_Throw()
	{
		_ = Assert.ThrowsExactly<ArgumentNullException>(() => _ = new AssemblyIdentity((Assembly)null, AssemblyIdentity.AssemblyNameElements.All));
	}

	[TestMethod]
	public void Ctor_AssemblyNameNull_Throw()
	{
		_ = Assert.ThrowsExactly<ArgumentNullException>(() => _ = new AssemblyIdentity((AssemblyName)null, AssemblyIdentity.AssemblyNameElements.All));
	}

#if NETFRAMEWORK
	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		AssemblyIdentity assemblyIdentity1 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		using System.IO.MemoryStream buffer = SerializationHelper.Serialize(assemblyIdentity1);
		AssemblyIdentity assemblyIdentity2 = SerializationHelper.Deserialize<AssemblyIdentity>(buffer);

		Assert.AreEqual(NeutralLanguage, assemblyIdentity2.Culture);
		Assert.AreEqual(GlobalConstants.ThisAssemblyName.Name, assemblyIdentity2.Name);
		Assert.AreEqual(GlobalConstants.ThisAssemblyName.Version, assemblyIdentity2.Version);
		CollectionAssert.AreEqual(GlobalConstants.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity2.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_SerializationInfoNull_Throw()
	{
		_ = Assert.ThrowsExactly<ArgumentNullException>(() => SerializationHelper.InvokeSerializationConstructorWithNullContext(typeof(AssemblyIdentity)));
	}

	[TestMethod]
	public void GetObjectData_SerializationInfoNull_Throw()
	{
		_ = Assert.ThrowsExactly<ArgumentNullException>(() =>
		{
			System.Runtime.Serialization.ISerializable assemblyIdentity = new AssemblyIdentity(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
			assemblyIdentity.GetObjectData(null, new System.Runtime.Serialization.StreamingContext());
		});
	}
#endif

	[TestMethod]
	public void GetHashCode_Success()
	{
		AssemblyIdentity assemblyIdentity = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		int i = assemblyIdentity.GetHashCode();
		Assert.AreNotEqual(0, i);
	}

	[TestMethod]
	public void Equals_Various_Success()
	{
		AssemblyIdentity assemblyIdentity0 = new();
		AssemblyIdentity assemblyIdentity1 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Name);
		AssemblyIdentity assemblyIdentity2 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity3 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity4 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.PublicKeyToken);
		AssemblyIdentity assemblyIdentity5 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity1a = new(GlobalConstants.OtherAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity2a = new(GlobalConstants.OtherAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity3a = new(s_localizedAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity4a = new(GlobalConstants.OtherAssembly, AssemblyIdentity.AssemblyNameElements.PublicKeyToken);
		AssemblyIdentity assemblyIdentity5a = new(GlobalConstants.OtherAssembly, AssemblyIdentity.AssemblyNameElements.All);

		Assert.IsFalse(assemblyIdentity0.Equals((AssemblyIdentity)null), "0=null");
		Assert.IsFalse(assemblyIdentity0.Equals((object)null), "0=(object)null");
		Assert.IsFalse(assemblyIdentity0.Equals(new object()), "0=object");
		Assert.IsFalse(assemblyIdentity0.Equals((object)assemblyIdentity1), "0=obj1");
		Assert.IsTrue(assemblyIdentity0.Equals((object)GlobalConstants.ThisAssembly), "0=obj0");

		Assert.IsTrue(assemblyIdentity0.Equals(assemblyIdentity0), "0=0");
		Assert.IsTrue(assemblyIdentity1.Equals(assemblyIdentity1), "1=1");
		Assert.IsTrue(assemblyIdentity2.Equals(assemblyIdentity2), "2=2");
		Assert.IsTrue(assemblyIdentity3.Equals(assemblyIdentity3), "3=3");
		Assert.IsTrue(assemblyIdentity4.Equals(assemblyIdentity4), "4=4");
		Assert.IsFalse(assemblyIdentity0.Equals(assemblyIdentity1), "0=1");
		Assert.IsFalse(assemblyIdentity0.Equals(assemblyIdentity2), "0=2");
		Assert.IsFalse(assemblyIdentity0.Equals(assemblyIdentity3), "0=3");
		Assert.IsFalse(assemblyIdentity0.Equals(assemblyIdentity4), "0=4");
		Assert.IsFalse(assemblyIdentity1.Equals(assemblyIdentity0), "1=0");
		Assert.IsFalse(assemblyIdentity2.Equals(assemblyIdentity0), "2=0");
		Assert.IsFalse(assemblyIdentity3.Equals(assemblyIdentity0), "3=0");
		Assert.IsFalse(assemblyIdentity4.Equals(assemblyIdentity0), "4=0");

		Assert.IsFalse(assemblyIdentity1.Equals(assemblyIdentity1a), "1=1a");
		Assert.IsFalse(assemblyIdentity2.Equals(assemblyIdentity2a), "2=2a");
		Assert.IsFalse(assemblyIdentity3.Equals(assemblyIdentity3a), "3=3a");
		Assert.IsFalse(assemblyIdentity4.Equals(assemblyIdentity4a), "4=4a");
		Assert.IsFalse(assemblyIdentity5.Equals(assemblyIdentity5a), "5=5a");

		Assert.IsTrue(assemblyIdentity5.Equals(GlobalConstants.ThisAssembly), "5=This");
	}

	[TestMethod]
	public void IsMatch_Various_Success()
	{
		AssemblyIdentity assemblyIdentity0 = new();
		AssemblyIdentity assemblyIdentity1 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Name);
		AssemblyIdentity assemblyIdentity2 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity3 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity3a = new(GlobalConstants.ThisAssemblyName.Name + ", Culture=de-DE");
		AssemblyIdentity assemblyIdentity4 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.PublicKeyToken);
		AssemblyIdentity assemblyIdentity5 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
		AssemblyIdentity assemblyIdentity6 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity7 = new(s_localizedAssembly, AssemblyIdentity.AssemblyNameElements.Culture);

		Assert.IsTrue(assemblyIdentity0.IsMatch(GlobalConstants.ThisAssembly), "Match 0");
		Assert.IsTrue(assemblyIdentity1.IsMatch(GlobalConstants.ThisAssembly), "Match 1");
		Assert.IsTrue(assemblyIdentity2.IsMatch(GlobalConstants.ThisAssembly), "Match 2");
		Assert.IsTrue(assemblyIdentity3.IsMatch(GlobalConstants.ThisAssembly), "Match 3");
		Assert.IsTrue(assemblyIdentity4.IsMatch(GlobalConstants.ThisAssembly), "Match 4");
		Assert.IsTrue(assemblyIdentity5.IsMatch(GlobalConstants.ThisAssembly), "Match 5");
		Assert.IsTrue(assemblyIdentity6.IsMatch(GlobalConstants.ThisAssembly), "Match 6");
		Assert.IsTrue(assemblyIdentity7.IsMatch(s_localizedAssembly), "Match 7");

		Assert.IsFalse(assemblyIdentity1.IsMatch((Assembly)null), "No match null");
		Assert.IsFalse(assemblyIdentity1.IsMatch((AssemblyName)null), "No match null");
		Assert.IsFalse(assemblyIdentity1.IsMatch(GlobalConstants.OtherAssembly), "No match 1");
		Assert.IsFalse(assemblyIdentity2.IsMatch(GlobalConstants.OtherAssembly), "No match 2");
		Assert.IsFalse(assemblyIdentity3a.IsMatch(GlobalConstants.ThisAssembly), "No match 3a");
		Assert.IsFalse(assemblyIdentity4.IsMatch(GlobalConstants.OtherAssembly), "No match 4");
		Assert.IsFalse(assemblyIdentity5.IsMatch(GlobalConstants.OtherAssembly), "No match 5");
		Assert.IsFalse(assemblyIdentity6.IsMatch(GlobalConstants.OtherAssembly), "No match 6");
		Assert.IsFalse(assemblyIdentity3.IsMatch(s_localizedAssembly), "No match 7");
	}

	[TestMethod]
	public void IsMatch_OddPkt_Success()
	{
		AssemblyIdentity assemblyIdentity = new(AssemblyNamePktOdd);
		Assert.IsFalse(assemblyIdentity.IsMatch(GlobalConstants.OtherAssembly));
	}

	[TestMethod]
	public void IsMatch_NoPkt_Success()
	{
		AssemblyIdentity assemblyIdentity = new(AssemblyNamePkt);
		Assert.IsFalse(assemblyIdentity.IsMatch(s_testAssembly));
	}

	[TestMethod]
	public void Match_Various_Success()
	{
		AssemblyIdentity assemblyIdentity0 = new();
		AssemblyIdentity assemblyIdentity1 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Name);
		AssemblyIdentity assemblyIdentity2 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity3 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity3a = new(GlobalConstants.ThisAssemblyName.Name + ", Culture=de-DE");
		AssemblyIdentity assemblyIdentity4 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.PublicKeyToken);
		AssemblyIdentity assemblyIdentity5 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
		AssemblyIdentity assemblyIdentity6 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity7 = new(s_localizedAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity8 = new(AssemblyNamePkt);

		Assert.AreEqual(-1, assemblyIdentity0.Match((Assembly)null), "Match0(null)");
		Assert.AreEqual(-1, assemblyIdentity0.Match((AssemblyName)null), "Match0(null)");
		Assert.AreEqual(0, assemblyIdentity0.Match(GlobalConstants.ThisAssembly), "Match0(This)");
		Assert.AreEqual(8, assemblyIdentity1.Match(GlobalConstants.ThisAssembly), "Match1(This)");
		Assert.AreEqual(4, assemblyIdentity2.Match(GlobalConstants.ThisAssembly), "Match2(This)");
		Assert.AreEqual(1, assemblyIdentity3.Match(GlobalConstants.ThisAssembly), "Match3(This)");
		Assert.AreEqual(2, assemblyIdentity4.Match(GlobalConstants.ThisAssembly), "Match4(This)");
		Assert.AreEqual(1, assemblyIdentity7.Match(s_localizedAssembly), "Match7(Local)");

		Assert.AreEqual(0, assemblyIdentity0.Match(GlobalConstants.OtherAssembly), "Match0(Other)");
		Assert.AreEqual(-1, assemblyIdentity1.Match(GlobalConstants.OtherAssembly), "Match1(Other)");
		Assert.AreEqual(-2, assemblyIdentity2.Match(GlobalConstants.OtherAssembly), "Match2(Other)");
		Assert.AreEqual(1, assemblyIdentity3.Match(GlobalConstants.OtherAssembly), "Match3(Other)");
		Assert.AreEqual(-3, assemblyIdentity3a.Match(GlobalConstants.ThisAssembly), "Match3a(This)");
		Assert.AreEqual(-4, assemblyIdentity4.Match(GlobalConstants.OtherAssembly), "Match4(Other)");

		Assert.AreEqual(-3, assemblyIdentity3.Match(s_localizedAssembly), "Match3(Localized)");

		Assert.AreEqual(11, assemblyIdentity5.Match(GlobalConstants.ThisAssembly), "Match5(This)");
		Assert.AreEqual(15, assemblyIdentity6.Match(GlobalConstants.ThisAssembly), "Match6(This)");
		Assert.AreEqual(-4, assemblyIdentity8.Match(s_testAssembly), "Match8(TestAssembly)");
	}

	[TestMethod]
	public void ToString_Success()
	{
		AssemblyIdentity assemblyIdentity = new(GlobalConstants.ThisAssemblyNameString);
		Assert.AreEqual(GlobalConstants.ThisAssemblyNameString, assemblyIdentity.ToString());
		assemblyIdentity = new AssemblyIdentity(s_localizedAssembly, AssemblyIdentity.AssemblyNameElements.All);
		Assert.AreEqual(s_localizedAssembly.FullName, assemblyIdentity.ToString());
		assemblyIdentity = new AssemblyIdentity(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		_ = assemblyIdentity.ToString();
	}

	[TestMethod]
	public void OpEqu_Success()
	{
		AssemblyIdentity assemblyIdentity1 = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity2 = null;
		AssemblyIdentity assemblyIdentity3 = null;
		AssemblyIdentity assemblyIdentity4 = new(GlobalConstants.OtherAssembly, AssemblyIdentity.AssemblyNameElements.All);

		Assert.IsFalse(assemblyIdentity1 == assemblyIdentity2, "1=2");
		Assert.IsTrue(assemblyIdentity2 == assemblyIdentity3, "2=3");
		Assert.IsFalse(assemblyIdentity1 == assemblyIdentity4, "1=4");

		Assert.IsTrue(assemblyIdentity1 != assemblyIdentity2, "1=2");
		Assert.IsFalse(assemblyIdentity2 != assemblyIdentity3, "2=3");
		Assert.IsTrue(assemblyIdentity1 != assemblyIdentity4, "1!=4");
	}
}
