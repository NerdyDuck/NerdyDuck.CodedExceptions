#region Copyright
/*******************************************************************************
 * <copyright file="XmlFacilityOverridesTest.cs" owner="Daniel Kopp">
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
 * <file name="XmlFacilityOverridesTest.cs" date="2015-08-19">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.Configuration.XmlFacilityOverrides class.
 * </file>
 ******************************************************************************/
#endregion

#if WINDOWS_UWP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif
#if WINDOWS_DESKTOP || NETCORE
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
#endif
using NerdyDuck.CodedExceptions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace NerdyDuck.Tests.CodedExceptions.Configuration
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.XmlFacilityOverrides class.
	/// </summary>
	[TestClass]
	public class XmlFacilityOverridesTest
	{
		[TestMethod]
		public void GetFacilityOverrides_FileNameNull_Throw()
		{
			CustomAssert.ThrowsException<ArgumentNullException>(() =>
			{
				XmlFacilityOverrides.GetFacilityOverrides(null);
			});
		}

		[TestMethod]
		public void GetFacilityOverrides_FileNotExists_Null()
		{
			List<FacilityOverride> ovrds = XmlFacilityOverrides.GetFacilityOverrides("IDoNotExist.xml");
			Assert.IsNull(ovrds);
		}

		[TestMethod]
		public void GetFacilityOverrides_Void_Success()
		{
			List<FacilityOverride> ovrds = XmlFacilityOverrides.GetFacilityOverrides();
			Assert.IsNotNull(ovrds);
			Assert.AreEqual(2, ovrds.Count);
		}

		[TestMethod]
		public void GetFacilityOverrides_String_Success()
		{
			List<FacilityOverride> ovrds = XmlFacilityOverrides.GetFacilityOverrides("FacilityIdentifierOverrides.xml");
			Assert.IsNotNull(ovrds);
			Assert.AreEqual(2, ovrds.Count);
		}

		[TestMethod]
		public void GetFacilityOverrides_WrongXML_Throw()
		{
			CustomAssert.ThrowsException<IOException>(() =>
			{
				XmlFacilityOverrides.GetFacilityOverrides("WrongXml.xml");
			});
		}

		[TestMethod]
		public void GetFacilityOverrides_InvalidXML_Throw()
		{
			CustomAssert.ThrowsException<IOException>(() =>
			{
				XmlFacilityOverrides.GetFacilityOverrides("InvalidXml.xml");
			});
		}

		[TestMethod]
		public void GetFacilityOverrides_InvalidIdentifier_Throw()
		{
			CustomAssert.ThrowsException<IOException>(() =>
			{
				XmlFacilityOverrides.GetFacilityOverrides("InvalidIdentifier.xml");
			});
		}
	}
}
