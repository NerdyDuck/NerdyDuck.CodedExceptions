#region Copyright
/*******************************************************************************
 * <copyright file="CodedExceptionsSectionTest.cs" owner="Daniel Kopp">
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
 * <file name="CodedExceptionsSectionTest.cs" date="2015-08-19">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.Configuration.CodedExceptionsSection class.
 * </file>
 ******************************************************************************/
#endregion

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions.Configuration;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NerdyDuck.Tests.CodedExceptions.Configuration
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.CodedExceptionsSection class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class CodedExceptionsSectionTest
	{
		#region Properties
		[TestMethod]
		public void FacilityOverrides_Success()
		{
			CodedExceptionsSection section = new CodedExceptionsSection();
			Assert.IsNotNull(section.FacilityOverrides);
			Assert.AreEqual(0, section.FacilityOverrides.Count);
		}

		[TestMethod]
		public void IsReadOnly_Success()
		{
			CodedExceptionsSection section = new CodedExceptionsSection();
			Assert.IsFalse(section.IsReadOnly);
		}
		#endregion

		#region CreateFacilityOverrides
		[TestMethod]
		public void CreateFacilityOverrides_Success()
		{
			CodedExceptionsSection section = new CodedExceptionsSection();
			section.FacilityOverrides.Add(new FacilityOverrideElement() { AssemblyName = "NerdyDuck.CodedExceptions", Identifier = 42 });
			section.FacilityOverrides.Add(new FacilityOverrideElement() { AssemblyName = "MyAssembly", Identifier = 1 });

			List<FacilityOverride> ovrds = section.CreateFacilityOverrides();
			Assert.AreEqual(2, ovrds.Count);
		}

		#region GetFacilityOverrides
		[TestMethod]
		public void GetFacilityOverrides_Void_Null()
		{
			Assert.IsNull(CodedExceptionsSection.GetFacilityOverrides());
		}

		[TestMethod]
		public void GetFacilityOverrides_String_Success()
		{
			List<FacilityOverride> ovrds = CodedExceptionsSection.GetFacilityOverrides("testSections/goodOverrides");
			Assert.IsNotNull(ovrds);
			Assert.AreEqual(7, ovrds.Count);
		}

		[TestMethod]
		public void GetFacilityOverrides_StringBadIdentifier_Throw()
		{
			CustomAssert.ThrowsException<System.Configuration.ConfigurationErrorsException>(() =>
			{
				List<FacilityOverride> ovrds = CodedExceptionsSection.GetFacilityOverrides("testSections/badOverrides");
			});
		}
		#endregion
		#endregion
	}
}
