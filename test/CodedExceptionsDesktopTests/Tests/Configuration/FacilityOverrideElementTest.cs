#region Copyright
/*******************************************************************************
 * <copyright file="FacilityOverrideElementTest.cs" owner="Daniel Kopp">
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
 * <file name="FacilityOverrideElementTest.cs" date="2015-08-19">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.Configuration.FacilityOverrideElement class.
 * </file>
 ******************************************************************************/
#endregion

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using NerdyDuck.CodedExceptions;
using NerdyDuck.CodedExceptions.Configuration;
using System;
using System.Reflection;

namespace NerdyDuck.Tests.CodedExceptions.Configuration
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.FacilityOverrideElement class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class FacilityOverrideElementTest
	{
		#region IsReadOnly
		[TestMethod]
		public void IsReadOnly_Success()
		{
			FacilityOverrideElement foe = new FacilityOverrideElement();
			Assert.IsFalse(foe.IsReadOnly);
		}
		#endregion

		#region ToOverride
		[TestMethod]
		public void ToOverride_Success()
		{
			AssemblyName name = typeof(FacilityOverrideElement).Assembly.GetName();
			FacilityOverrideElement foe = new FacilityOverrideElement();
			foe.AssemblyName = name.FullName;
			foe.Identifier = 42;

			FacilityOverride fo = foe.ToOverride();
			Assert.AreEqual(name.FullName, fo.AssemblyName.FullName);
			Assert.AreEqual(42, fo.Identifier);
		}

		[TestMethod]
		public void ToOverride_IdentifierInvalid_Throw()
		{
			CustomAssert.ThrowsException<CodedFormatException>(() =>
			{
				AssemblyName name = typeof(FacilityOverrideElement).Assembly.GetName();
				FacilityOverrideElement foe = new FacilityOverrideElement();
				foe.AssemblyName = name.FullName;
				foe.Identifier = -1;

				FacilityOverride fo = foe.ToOverride();
			});
		}
		#endregion
	}
}
