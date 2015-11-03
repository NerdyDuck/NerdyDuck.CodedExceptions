#region Copyright
/*******************************************************************************
 * <copyright file="AssemblyFacilityIdentifierAttributeTest.cs"
 * owner="Daniel Kopp">
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
 * <file name="AssemblyFacilityIdentifierAttributeTest.cs" date="2015-08-12">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute class.
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
using System;
using System.Reflection;

namespace NerdyDuck.Tests.CodedExceptions
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedException class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class AssemblyFacilityIdentifierAttributeTest
	{
		#region Constructors
		[TestMethod]
		public void Ctor_Success()
		{
			AssemblyFacilityIdentifierAttribute att = new AssemblyFacilityIdentifierAttribute(42);
			Assert.AreEqual(42, att.FacilityId);
		}

		[TestMethod]
		public void Ctor_IdLessThan0_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentOutOfRangeException>(() =>
			{
				AssemblyFacilityIdentifierAttribute att = new AssemblyFacilityIdentifierAttribute(-1);
			});
		}

		[TestMethod]
		public void Ctor_IdGreaterThan2047_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentOutOfRangeException>(() =>
			{
				AssemblyFacilityIdentifierAttribute att = new AssemblyFacilityIdentifierAttribute(2048);
			});
		}
		#endregion

		#region FromAssembly
		[TestMethod]
		public void FromAssembly_Success()
		{
			Assembly assembly = typeof(AssemblyFacilityIdentifierAttributeTest).GetTypeInfo().Assembly;
			AssemblyFacilityIdentifierAttribute att = AssemblyFacilityIdentifierAttribute.FromAssembly(assembly);
			Assert.IsNotNull(att);
			Assert.AreEqual(42, att.FacilityId);
		}

		[TestMethod]
		public void FromAssembly_AssemblyNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				AssemblyFacilityIdentifierAttribute.FromAssembly(null);
			});
		}
		#endregion

		#region FromType
		[TestMethod]
		public void FromType_Success()
		{
			Type type = typeof(AssemblyFacilityIdentifierAttributeTest);
			AssemblyFacilityIdentifierAttribute att = AssemblyFacilityIdentifierAttribute.FromType(type);
			Assert.IsNotNull(att);
			Assert.AreEqual(42, att.FacilityId);
		}

		[TestMethod]
		public void FromType_AssemblyNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				AssemblyFacilityIdentifierAttribute.FromType(null);
			});
		}
		#endregion

		#region TryGetOverride
		[TestMethod]
		public void TryGetOverride_Assembly_False()
		{
			Assembly assembly = typeof(AssemblyFacilityIdentifierAttributeTest).GetTypeInfo().Assembly;
			int i;
			Assert.IsFalse(AssemblyFacilityIdentifierAttribute.TryGetOverride(assembly, out i));
		}

		[TestMethod]
		public void TryGetOverride_Assembly_True()
		{
			Assembly assembly = typeof(int).GetTypeInfo().Assembly;
			int i;
			Assert.IsTrue(AssemblyFacilityIdentifierAttribute.TryGetOverride(assembly, out i));
		}

		[TestMethod]
		public void TryGetOverride_AssemblyNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				int i;
				Assert.IsFalse(AssemblyFacilityIdentifierAttribute.TryGetOverride((Assembly)null, out i));
			});
		}

		[TestMethod]
		public void TryGetOverride_Type_False()
		{
			Type type = typeof(AssemblyFacilityIdentifierAttributeTest);
			int i;
			Assert.IsFalse(AssemblyFacilityIdentifierAttribute.TryGetOverride(type, out i));
		}

		[TestMethod]
		public void TryGetOverride_TypeNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				int i;
				Assert.IsFalse(AssemblyFacilityIdentifierAttribute.TryGetOverride((Type)null, out i));
			});
		}
		#endregion
	}
}
