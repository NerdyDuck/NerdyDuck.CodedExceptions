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
	private static readonly byte[] s_pkt1 = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };

#if NET48
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
		Assert.AreEqual(string.Empty, assemblyIdentity.Name);
		Assert.IsNull(assemblyIdentity.Version);
		Assert.IsNull(assemblyIdentity.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_StringFull_Success()
	{
		AssemblyIdentity assemblyIdentity = new(Globals.ThisAssemblyNameString);
		Assert.AreEqual(NeutralLanguage, assemblyIdentity.Culture);
		Assert.AreEqual(Globals.ThisAssemblyName.Name, assemblyIdentity.Name);
		Assert.AreEqual(Globals.ThisAssemblyName.Version, assemblyIdentity.Version);
		CollectionAssert.AreEqual(Globals.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity.GetPublicKeyToken());
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
		_ = Assert.ThrowsException<FormatException>(() => _ = new AssemblyIdentity(AssemblyNamePktInv));
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
		_ = Assert.ThrowsException<FormatException>(() => _ = new AssemblyIdentity(AssemblyNameVersionInv));
	}

	[TestMethod]
	public void Ctor_AssemblyIdentity_Success()
	{
		AssemblyIdentity assemblyIdentity1 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity2 = new(assemblyIdentity1);
		Assert.AreEqual(assemblyIdentity1.Culture, assemblyIdentity2.Culture);
		Assert.AreEqual(assemblyIdentity1.Name, assemblyIdentity2.Name);
		Assert.AreEqual(assemblyIdentity1.Version, assemblyIdentity2.Version);
		CollectionAssert.AreEqual(assemblyIdentity1.GetPublicKeyToken(), assemblyIdentity2.GetPublicKeyToken());
		assemblyIdentity1 = new AssemblyIdentity(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Name);
		assemblyIdentity2 = new AssemblyIdentity(assemblyIdentity1);
		Assert.AreEqual(assemblyIdentity1.Culture, assemblyIdentity2.Culture);
		Assert.AreEqual(assemblyIdentity1.Name, assemblyIdentity2.Name);
		Assert.AreEqual(assemblyIdentity1.Version, assemblyIdentity2.Version);
		CollectionAssert.AreEqual(assemblyIdentity1.GetPublicKeyToken(), assemblyIdentity2.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_Assembly_Success()
	{
		AssemblyIdentity assemblyIdentity = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		Assert.AreEqual(NeutralLanguage, assemblyIdentity.Culture);
		Assert.AreEqual(Globals.ThisAssemblyName.Name, assemblyIdentity.Name);
		Assert.AreEqual(Globals.ThisAssemblyName.Version, assemblyIdentity.Version);
		CollectionAssert.AreEqual(Globals.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity.GetPublicKeyToken());
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
		AssemblyIdentity assemblyIdentity = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
		Assert.AreEqual(NeutralLanguage, assemblyIdentity.Culture);
		Assert.AreEqual(Globals.ThisAssemblyName.Name, assemblyIdentity.Name);
		Assert.IsNull(assemblyIdentity.Version);
		CollectionAssert.AreEqual(Globals.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_AssemblyNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => _ = new AssemblyIdentity((Assembly)null, AssemblyIdentity.AssemblyNameElements.All));
	}

	[TestMethod]
	public void Ctor_SerializationInfo_Success()
	{
		AssemblyIdentity assemblyIdentity1 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		using System.IO.MemoryStream Buffer = SerializationHelper.Serialize(assemblyIdentity1);
		AssemblyIdentity assemblyIdentity2 = SerializationHelper.Deserialize<AssemblyIdentity>(Buffer);

		Assert.AreEqual(NeutralLanguage, assemblyIdentity2.Culture);
		Assert.AreEqual(Globals.ThisAssemblyName.Name, assemblyIdentity2.Name);
		Assert.AreEqual(Globals.ThisAssemblyName.Version, assemblyIdentity2.Version);
		CollectionAssert.AreEqual(Globals.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity2.GetPublicKeyToken());
	}

	[TestMethod]
	public void Ctor_SerializationInfoNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() => SerializationHelper.InvokeSerializationConstructorWithNullContext(typeof(AssemblyIdentity)));
	}

	[TestMethod]
	public void GetHashCode_Success()
	{
		AssemblyIdentity assemblyIdentity = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		int i = assemblyIdentity.GetHashCode();
		Assert.AreNotEqual(0, i);
	}

	[TestMethod]
	public void Equals_Various_Success()
	{
		AssemblyIdentity assemblyIdentity0 = new();
		AssemblyIdentity assemblyIdentity1 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Name);
		AssemblyIdentity assemblyIdentity2 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity3 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity4 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.PublicKeyToken);
		AssemblyIdentity assemblyIdentity5 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity1a = new(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity2a = new(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity3a = new(s_localizedAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity4a = new(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.PublicKeyToken);
		AssemblyIdentity assemblyIdentity5a = new(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.All);

		Assert.IsFalse(assemblyIdentity0.Equals((AssemblyIdentity)null), "0=null");
		Assert.IsFalse(assemblyIdentity0.Equals((object)null), "0=objnull");
		Assert.IsFalse(assemblyIdentity0.Equals(new object()), "0=obj");
		Assert.IsFalse(assemblyIdentity0.Equals((object)assemblyIdentity1), "0=obj1");
		Assert.IsTrue(assemblyIdentity0.Equals((object)Globals.ThisAssembly), "0=obj0");

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

		Assert.IsTrue(assemblyIdentity5.Equals(Globals.ThisAssembly), "5=This");
	}

	[TestMethod]
	public void IsMatch_Various_Success()
	{
		AssemblyIdentity assemblyIdentity0 = new();
		AssemblyIdentity assemblyIdentity1 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Name);
		AssemblyIdentity assemblyIdentity2 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity3 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity3a = new(Globals.ThisAssemblyName.Name + ", Culture=de-DE");
		AssemblyIdentity assemblyIdentity4 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.PublicKeyToken);
		AssemblyIdentity assemblyIdentity5 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
		AssemblyIdentity assemblyIdentity6 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity7 = new(s_localizedAssembly, AssemblyIdentity.AssemblyNameElements.Culture);

		Assert.IsTrue(assemblyIdentity0.IsMatch(Globals.ThisAssembly), "Match 0");
		Assert.IsTrue(assemblyIdentity1.IsMatch(Globals.ThisAssembly), "Match 1");
		Assert.IsTrue(assemblyIdentity2.IsMatch(Globals.ThisAssembly), "Match 2");
		Assert.IsTrue(assemblyIdentity3.IsMatch(Globals.ThisAssembly), "Match 3");
		Assert.IsTrue(assemblyIdentity4.IsMatch(Globals.ThisAssembly), "Match 4");
		Assert.IsTrue(assemblyIdentity5.IsMatch(Globals.ThisAssembly), "Match 5");
		Assert.IsTrue(assemblyIdentity6.IsMatch(Globals.ThisAssembly), "Match 6");
		Assert.IsTrue(assemblyIdentity7.IsMatch(s_localizedAssembly), "Match 7");

		Assert.IsFalse(assemblyIdentity1.IsMatch(null), "No match null");
		Assert.IsFalse(assemblyIdentity1.IsMatch(Globals.OtherAssembly), "No match 1");
		Assert.IsFalse(assemblyIdentity2.IsMatch(Globals.OtherAssembly), "No match 2");
		Assert.IsFalse(assemblyIdentity3a.IsMatch(Globals.ThisAssembly), "No match 3a");
		Assert.IsFalse(assemblyIdentity4.IsMatch(Globals.OtherAssembly), "No match 4");
		Assert.IsFalse(assemblyIdentity5.IsMatch(Globals.OtherAssembly), "No match 5");
		Assert.IsFalse(assemblyIdentity6.IsMatch(Globals.OtherAssembly), "No match 6");
		Assert.IsFalse(assemblyIdentity3.IsMatch(s_localizedAssembly), "No match 7");
	}

	[TestMethod]
	public void IsMatch_OddPkt_Success()
	{
		AssemblyIdentity assemblyIdentity = new(AssemblyNamePktOdd);
		Assert.IsFalse(assemblyIdentity.IsMatch(Globals.OtherAssembly));
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
		AssemblyIdentity assemblyIdentity1 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Name);
		AssemblyIdentity assemblyIdentity2 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Version);
		AssemblyIdentity assemblyIdentity3 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity3a = new(Globals.ThisAssemblyName.Name + ", Culture=de-DE");
		AssemblyIdentity assemblyIdentity4 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.PublicKeyToken);
		AssemblyIdentity assemblyIdentity5 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
		AssemblyIdentity assemblyIdentity6 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity7 = new(s_localizedAssembly, AssemblyIdentity.AssemblyNameElements.Culture);
		AssemblyIdentity assemblyIdentity8 = new(AssemblyNamePkt);

		Assert.AreEqual(-1, assemblyIdentity0.Match(null), "Match0(null)");
		Assert.AreEqual(0, assemblyIdentity0.Match(Globals.ThisAssembly), "Match0(This)");
		Assert.AreEqual(8, assemblyIdentity1.Match(Globals.ThisAssembly), "Match1(This)");
		Assert.AreEqual(4, assemblyIdentity2.Match(Globals.ThisAssembly), "Match2(This)");
		Assert.AreEqual(1, assemblyIdentity3.Match(Globals.ThisAssembly), "Match3(This)");
		Assert.AreEqual(2, assemblyIdentity4.Match(Globals.ThisAssembly), "Match4(This)");
		Assert.AreEqual(1, assemblyIdentity7.Match(s_localizedAssembly), "Match7(Local)");

		Assert.AreEqual(0, assemblyIdentity0.Match(Globals.OtherAssembly), "Match0(Other)");
		Assert.AreEqual(-1, assemblyIdentity1.Match(Globals.OtherAssembly), "Match1(Other)");
		Assert.AreEqual(-2, assemblyIdentity2.Match(Globals.OtherAssembly), "Match2(Other)");
		Assert.AreEqual(1, assemblyIdentity3.Match(Globals.OtherAssembly), "Match3(Other)");
		Assert.AreEqual(-3, assemblyIdentity3a.Match(Globals.ThisAssembly), "Match3a(This)");
		Assert.AreEqual(-4, assemblyIdentity4.Match(Globals.OtherAssembly), "Match4(Other)");

		Assert.AreEqual(-3, assemblyIdentity3.Match(s_localizedAssembly), "Match3(Localized)");

		Assert.AreEqual(11, assemblyIdentity5.Match(Globals.ThisAssembly), "Match5(This)");
		Assert.AreEqual(15, assemblyIdentity6.Match(Globals.ThisAssembly), "Match6(This)");
		Assert.AreEqual(-4, assemblyIdentity8.Match(s_testAssembly), "Match8(TestAssembly)");
	}

	[TestMethod]
	public void SetPublicKeyToken_ByteArray_Success()
	{
		AssemblyIdentity assemblyIdentity = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		CollectionAssert.AreEqual(Globals.ThisAssemblyName.GetPublicKeyToken(), assemblyIdentity.GetPublicKeyToken());
		assemblyIdentity.SetPublicKeyToken(null);
		Assert.IsNull(assemblyIdentity.GetPublicKeyToken());
	}

	[TestMethod]
	public void ToString_Success()
	{
		AssemblyIdentity assemblyIdentity = new(Globals.ThisAssemblyNameString);
		Assert.AreEqual(Globals.ThisAssemblyNameString, assemblyIdentity.ToString());
		assemblyIdentity = new AssemblyIdentity(s_localizedAssembly, AssemblyIdentity.AssemblyNameElements.All);
		Assert.AreEqual(s_localizedAssembly.FullName, assemblyIdentity.ToString());
		assemblyIdentity = new AssemblyIdentity(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		_ = assemblyIdentity.ToString();
	}

	[TestMethod]
	public void GetObjectData_SerializationInfoNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() =>
		  {
			  System.Runtime.Serialization.ISerializable assemblyIdentity = new AssemblyIdentity(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
			  assemblyIdentity.GetObjectData(null, new System.Runtime.Serialization.StreamingContext());
		  });
	}

	[TestMethod]
	public void OpEqu_Success()
	{
		AssemblyIdentity assemblyIdentity1 = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.All);
		AssemblyIdentity assemblyIdentity2 = null;
		AssemblyIdentity assemblyIdentity3 = null;
		AssemblyIdentity assemblyIdentity4 = new(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.All);

		Assert.IsFalse(assemblyIdentity1 == assemblyIdentity2, "1=2");
		Assert.IsTrue(assemblyIdentity2 == assemblyIdentity3, "2=3");
		Assert.IsFalse(assemblyIdentity1 == assemblyIdentity4, "1=4");

		Assert.IsTrue(assemblyIdentity1 != assemblyIdentity2, "1=2");
		Assert.IsFalse(assemblyIdentity2 != assemblyIdentity3, "2=3");
		Assert.IsTrue(assemblyIdentity1 != assemblyIdentity4, "1!=4");
	}
}
