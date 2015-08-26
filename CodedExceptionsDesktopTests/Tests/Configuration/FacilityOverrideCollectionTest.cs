#region Copyright
/*******************************************************************************
 * <copyright file="FacilityOverrideCollectionTest.cs" owner="Daniel Kopp">
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
 * <file name="FacilityOverrideCollectionTest.cs" date="2015-08-19">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.Configuration.FacilityOverrideCollection class.
 * </file>
 ******************************************************************************/
#endregion

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace NerdyDuck.Tests.CodedExceptions.Configuration
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.FacilityOverrideCollection class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class FacilityOverrideCollectionTest
	{
		private const string TestAssembly = "NerdyDuck.CodedExceptions";

		[TestMethod]
		public void Add_Index_Remove_ByElement_Success()
		{
			FacilityOverrideCollection c = new FacilityOverrideCollection();
			FacilityOverrideElement e = new FacilityOverrideElement() { AssemblyName = TestAssembly, Identifier = 42 };
			c.Add(e);
			Assert.AreEqual(0, c.IndexOf(e));
			Assert.IsNotNull(c[0]);
			c.Remove(e);
			Assert.AreEqual(-1, c.IndexOf(e));
		}

		[TestMethod]
		public void Add_Remove_ByName_Success()
		{
			FacilityOverrideCollection c = new FacilityOverrideCollection();
			FacilityOverrideElement e = new FacilityOverrideElement() { AssemblyName = TestAssembly, Identifier = 42 };
			c.Add(e);
			Assert.IsNotNull(c[TestAssembly]);
			c.Remove(TestAssembly);
			Assert.IsNull(c[TestAssembly]);
		}

		[TestMethod]
		public void Add_Remove_ByIndex_Success()
		{
			FacilityOverrideCollection c = new FacilityOverrideCollection();
			FacilityOverrideElement e = new FacilityOverrideElement() { AssemblyName = TestAssembly, Identifier = 42 };
			c.Add(e);
			Assert.AreEqual(0, c.IndexOf(e));
			c.RemoveAt(0);
			Assert.AreEqual(-1, c.IndexOf(e));
		}

		[TestMethod]
		public void Clear_Success()
		{
			FacilityOverrideCollection c = new FacilityOverrideCollection();
			FacilityOverrideElement e = new FacilityOverrideElement() { AssemblyName = TestAssembly, Identifier = 42 };
			c.Add(e);
			c[0] = e;
			Assert.AreEqual(0, c.IndexOf(e));
			c.Clear();
			Assert.AreEqual(-1, c.IndexOf(e));
		}
	}
}
