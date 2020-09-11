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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions.Configuration;

#pragma warning disable CA1707
namespace NerdyDuck.Tests.CodedExceptions.Configuration
{
#if NETCORE21
	namespace NetCore21
#elif NETCORE31
	namespace NetCore31
#elif NET48
	namespace Net48
#elif NET50
	namespace Net50
#endif
	{

		/// <summary>
		/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverride class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class AssemblyFacilityOverrideTests
		{
			private static readonly AssemblyIdentity s_thisAssemblyIdentity = new AssemblyIdentity(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
			private static readonly AssemblyIdentity s_otherAssemblyIdentity = new AssemblyIdentity(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);

			#region Constructors
			[TestMethod]
			public void Ctor_AssemblyIdentityInt_Success()
			{
				AssemblyFacilityOverride assemblyFacilityOverride = new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42);
				Assert.AreEqual(s_thisAssemblyIdentity, assemblyFacilityOverride.AssemblyName);
				Assert.AreEqual(42, assemblyFacilityOverride.Identifier);

			}

			[TestMethod]
			public void Ctor_AssemblyIdentityNullInt_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					AssemblyFacilityOverride assemblyFacilityOverride = new AssemblyFacilityOverride((AssemblyIdentity)null, 42);
				});
			}

			[TestMethod]
			public void Ctor_AssemblyIdentityIntOOR_Throw()
			{
				Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
				{
					AssemblyFacilityOverride assemblyFacilityOverride = new AssemblyFacilityOverride(s_thisAssemblyIdentity, -1);
				}, "i<0");
				Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
				{
					AssemblyFacilityOverride assemblyFacilityOverride = new AssemblyFacilityOverride(s_thisAssemblyIdentity, 7711);
				}, "i>2047");
			}

			[TestMethod]
			public void Ctor_StringInt_Success()
			{
				AssemblyFacilityOverride assemblyFacilityOverride = new AssemblyFacilityOverride(s_thisAssemblyIdentity.ToString(), 42);
				Assert.AreEqual(s_thisAssemblyIdentity, assemblyFacilityOverride.AssemblyName);
				Assert.AreEqual(42, assemblyFacilityOverride.Identifier);
			}

			[TestMethod]
			public void Ctor_SerializationInfo_Success()
			{
				AssemblyFacilityOverride assemblyFacilityOverride1 = new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42);
				using System.IO.MemoryStream Buffer = SerializationHelper.Serialize(assemblyFacilityOverride1);
				AssemblyFacilityOverride assemblyFacilityOverride2 = SerializationHelper.Deserialize<AssemblyFacilityOverride>(Buffer);

				Assert.AreEqual(s_thisAssemblyIdentity, assemblyFacilityOverride2.AssemblyName);
				Assert.AreEqual(42, assemblyFacilityOverride2.Identifier);
			}

			[TestMethod]
			public void Ctor_SerializationInfoNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					SerializationHelper.InvokeSerializationConstructorWithNullContext(typeof(AssemblyFacilityOverride));
				});
			}
			#endregion

			#region GetHashCode
			[TestMethod]
			public void GetHashCode_Success()
			{
				AssemblyFacilityOverride assemblyFacilityOverride = new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42);
				int i = assemblyFacilityOverride.GetHashCode();
				Assert.AreNotEqual(0, i);
			}
			#endregion

			#region Equals
			[TestMethod]
			public void Equals_Various_Success()
			{
				AssemblyFacilityOverride assemblyFacilityOverride1a = new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42);
				AssemblyFacilityOverride assemblyFacilityOverride1b = new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42);
				AssemblyFacilityOverride assemblyFacilityOverride1c = new AssemblyFacilityOverride(s_thisAssemblyIdentity, 24);
				AssemblyFacilityOverride assemblyFacilityOverride2 = new AssemblyFacilityOverride(s_otherAssemblyIdentity, 17);


				Assert.IsFalse(assemblyFacilityOverride1a.Equals((AssemblyFacilityOverride)null), "1a=null");
				Assert.IsFalse(assemblyFacilityOverride1a.Equals((object)null), "1a=objnull");
				Assert.IsFalse(assemblyFacilityOverride1a.Equals(new object()), "1a=obj");
				Assert.IsTrue(assemblyFacilityOverride1a.Equals((object)assemblyFacilityOverride1a), "1a=obj1a");

				Assert.IsTrue(assemblyFacilityOverride1a.Equals(assemblyFacilityOverride1a), "1a=1a");
				Assert.IsTrue(assemblyFacilityOverride1a.Equals(assemblyFacilityOverride1b), "1a=1b");
				Assert.IsFalse(assemblyFacilityOverride1a.Equals(assemblyFacilityOverride2), "1a!=2");
				Assert.IsFalse(assemblyFacilityOverride1a.Equals(assemblyFacilityOverride1c), "1a!=1c");
			}
			#endregion

			#region GetObjectData
			[TestMethod]
			public void GetObjectData_SerializationInfoNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					System.Runtime.Serialization.ISerializable assemblyFacilityOverride = new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42);
					assemblyFacilityOverride.GetObjectData(null, new System.Runtime.Serialization.StreamingContext());
				});
			}
			#endregion
		}
	}
}
