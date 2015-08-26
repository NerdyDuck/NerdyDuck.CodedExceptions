#region Copyright
/*******************************************************************************
 * <copyright file="FacilityOverrideTest.cs" owner="Daniel Kopp">
 * Copyright 2015 Daniel Kopp
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * </copyright>
 * <author name="Daniel Kopp" email="dak@nerdyduck.de" />
 * <assembly name="NerdyDuck.Tests.CodedExceptions">
 * Unit tests for NerdyDuck.CodedExceptions assembly.
 * </assembly>
 * <file name="FacilityOverrideTest.cs" date="2015-08-18">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.Configuration.FacilityOverride class.
 * </file>
 ******************************************************************************/
#endregion

#if WINDOWS_UWP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif
#if WINDOWS_DESKTOP
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
#endif
using NerdyDuck.CodedExceptions;
using NerdyDuck.CodedExceptions.Configuration;
using System;
using System.Reflection;

namespace NerdyDuck.Tests.CodedExceptions.Configuration
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.FacilityOverride class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class FacilityOverrideTest
	{
		#region Constructors
		[TestMethod]
		public void Ctor_AssemblyNameInt_Success()
		{
			AssemblyName name = typeof(FacilityOverrideTest).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			Assert.AreEqual(name, fo.AssemblyName);
			Assert.AreEqual(42, fo.Identifier);
		}

		public void Ctor_AssemblyNameNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				FacilityOverride fo = new FacilityOverride((AssemblyName)null, 42);
			});
		}

		[TestMethod]
		public void Ctor_AssemblyNameIdLessThan0_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentOutOfRangeException>(() =>
			{
				AssemblyName name = typeof(FacilityOverrideTest).GetTypeInfo().Assembly.GetName();
				FacilityOverride fo = new FacilityOverride(name, -1);
			});
		}

		[TestMethod]
		public void Ctor_AssemblyNameIdGreaterThan2047_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentOutOfRangeException>(() =>
			{
				AssemblyName name = typeof(FacilityOverrideTest).GetTypeInfo().Assembly.GetName();
				FacilityOverride fo = new FacilityOverride(name, 2048);
			});
		}

		[TestMethod]
		public void Ctor_StringInt_Success()
		{
			AssemblyName asmblyname = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			string name = typeof(FacilityOverride).GetTypeInfo().Assembly.FullName;
			FacilityOverride fo = new FacilityOverride(name, 42);
			Assert.IsTrue(asmblyname.FullName == fo.AssemblyName.FullName);
			Assert.AreEqual(42, fo.Identifier);
		}

		public void Ctor_StringNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				FacilityOverride fo = new FacilityOverride((string)null, 42);
			});
		}

		[TestMethod]
		public void Ctor_StringIdLessThan0_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentOutOfRangeException>(() =>
			{
				string name = typeof(FacilityOverride).AssemblyQualifiedName;
				FacilityOverride fo = new FacilityOverride(name, -1);
			});
		}

		[TestMethod]
		public void Ctor_StringIdGreaterThan2047_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentOutOfRangeException>(() =>
			{
				string name = typeof(FacilityOverride).AssemblyQualifiedName;
				FacilityOverride fo = new FacilityOverride(name, 2048);
			});
		}
		#endregion

		#region Equals
		[TestMethod]
		public void Equals_ObjectNull_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			object obj = null;

			Assert.IsFalse(fo.Equals(obj));
		}

		[TestMethod]
		public void Equals_ObjectAssemblyName_True()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			object obj = name;

			Assert.IsTrue(fo.Equals(obj));
		}

		[TestMethod]
		public void Equals_ObjectFacilityOverride_True()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			object obj = fo;

			Assert.IsTrue(fo.Equals(obj));
		}

		[TestMethod]
		public void Equals_ObjectObject_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			object obj = new object();

			Assert.IsFalse(fo.Equals(obj));
		}

		[TestMethod]
		public void Equals_AssemblyNameNull_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			AssemblyName other = null;

			Assert.IsFalse(fo.Equals(other));
		}

		[TestMethod]
		public void Equals_AssemblyNameDifferentAssembly_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			AssemblyName other = typeof(object).GetTypeInfo().Assembly.GetName();

			Assert.IsFalse(fo.Equals(other));
		}

		[TestMethod]
		public void Equals_AssemblyNameNoVersion_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			AssemblyName other = new AssemblyName();
			other.Name = name.Name;
			CloneCulture(name, other);
			other.SetPublicKeyToken(name.GetPublicKeyToken());

			Assert.IsFalse(fo.Equals(other));
		}

		[TestMethod]
		public void Equals_AssemblyNameDifferentVersion_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			AssemblyName other = new AssemblyName();
			other.Name = name.Name;
			other.Version = new Version(name.Version.Major + 1, name.Version.Minor, name.Version.Build, name.Version.Revision);
			CloneCulture(name, other);
			other.SetPublicKeyToken(name.GetPublicKeyToken());

			Assert.IsFalse(fo.Equals(other));
		}

		[TestMethod]
		public void Equals_AssemblyNameCultureNull_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
#if WINDOWS_DESKTOP
			name.CultureInfo = new System.Globalization.CultureInfo("de-DE");
#endif
#if WINDOWS_UWP
			name.CultureName = "de-DE";
#endif
			FacilityOverride fo = new FacilityOverride(name, 42);
			AssemblyName other = new AssemblyName();
			other.Name = name.Name;
			other.Version = name.Version;
			other.SetPublicKeyToken(name.GetPublicKeyToken());

			Assert.IsFalse(fo.Equals(other));
		}

		[TestMethod]
		public void Equals_AssemblyNameDifferentCulture_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
#if WINDOWS_DESKTOP
			name.CultureInfo = new System.Globalization.CultureInfo("de-DE");
#endif
#if WINDOWS_UWP
			name.CultureName = "de-DE";
#endif
			FacilityOverride fo = new FacilityOverride(name, 42);
			AssemblyName other = new AssemblyName();
			other.Name = name.Name;
			other.Version = name.Version;
#if WINDOWS_DESKTOP
			other.CultureInfo = new System.Globalization.CultureInfo("fr-FR");
#endif
#if WINDOWS_UWP
			other.CultureName = "fr-FR";
#endif
			other.SetPublicKeyToken(name.GetPublicKeyToken());

			Assert.IsFalse(fo.Equals(other));
		}

		[TestMethod]
		public void Equals_AssemblyNameNoPublicKey_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			AssemblyName other = new AssemblyName();
			other.Name = name.Name;
			other.Version = name.Version;
			CloneCulture(name, other);

			Assert.IsFalse(fo.Equals(other));
		}

		[TestMethod]
		public void Equals_AssemblyNameDifferentPublicKey_False()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			AssemblyName other = new AssemblyName();
			other.Name = name.Name;
			other.Version = name.Version;
			CloneCulture(name, other);
			byte[] buffer = new byte[8];
			Array.Copy(name.GetPublicKeyToken(), buffer, 8);
			for (int i = 0; i < (int)(buffer.Length / 2); i++)
			{
				byte b = buffer[i];
				buffer[i] = buffer[buffer.Length - i - 1];
				buffer[buffer.Length - i - 1] = b;
			}
			other.SetPublicKeyToken(buffer);

			Assert.IsFalse(fo.Equals(other));
		}

		public void Equals_AssemblyName_True()
		{
			AssemblyName name = typeof(FacilityOverride).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);
			AssemblyName other = new AssemblyName();
			other.Name = name.Name;
			other.Version = name.Version;
			CloneCulture(name, other);
			other.SetPublicKeyToken(name.GetPublicKeyToken());

			Assert.IsTrue(fo.Equals(other));
		}
		#endregion

		#region GetHashCode
		[TestMethod]
		public void GetHashCode_Success()
		{
			AssemblyName name = typeof(FacilityOverrideTest).GetTypeInfo().Assembly.GetName();
			FacilityOverride fo = new FacilityOverride(name, 42);

			Assert.IsTrue(fo.GetHashCode() != 0);
		}
#endregion

		private void CloneCulture(AssemblyName source, AssemblyName target)
		{
#if WINDOWS_DESKTOP
			source.CultureInfo = target.CultureInfo;
#endif
#if WINDOWS_UWP
			source.CultureName = target.CultureName;
#endif
		}
	}
}
