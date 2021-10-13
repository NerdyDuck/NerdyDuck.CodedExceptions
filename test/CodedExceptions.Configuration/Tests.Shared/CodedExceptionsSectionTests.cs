#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.CodedExceptions.Configuration - Unit tests for the
 * NerdyDuck.CodedExceptions.Configuration assembly
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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideElement class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class CodedExceptionsSectionTests
{
	[TestMethod]
	public void FacilityOverrides_Success()
	{
		CodedExceptionsSection section = new();
		Assert.IsNotNull(section.FacilityOverrides);
		Assert.AreEqual(0, section.FacilityOverrides.Count);
		Assert.IsNotNull(section.DebugModes);
		Assert.AreEqual(0, section.DebugModes.Count);
	}

	[TestMethod]
	public void IsReadOnly_Success()
	{
		CodedExceptionsSection section = new();
		Assert.IsFalse(section.IsReadOnly);
	}

	[TestMethod]
	public void CreateFacilityOverrides_Success()
	{
		CodedExceptionsSection section = new();
		section.FacilityOverrides.Add(new AssemblyFacilityOverrideElement
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			Identifier = 42
		});
		section.FacilityOverrides.Add(new AssemblyFacilityOverrideElement
		{
			AssemblyName = Globals.OtherAssembly.FullName,
			Identifier = 17
		});

		List<AssemblyFacilityOverride> ovrds = section.CreateFacilityOverrides();
		Assert.AreEqual(2, ovrds.Count);
	}

	[TestMethod]
	public void CreateFacilityOverrides_Null_Success()
	{
		CodedExceptionsSection section = new();

		List<AssemblyFacilityOverride> ovrds = section.CreateFacilityOverrides();
		Assert.IsNull(ovrds);
	}

	[TestMethod]
	public void CreateDebugModes_Success()
	{
		CodedExceptionsSection section = new();
		section.DebugModes.Add(new AssemblyDebugModeElement
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			IsEnabled = true
		});
		section.DebugModes.Add(new AssemblyDebugModeElement
		{
			AssemblyName = Globals.OtherAssembly.FullName,
			IsEnabled = false
		});

		List<AssemblyDebugMode> dbgmds = section.CreateDebugModes();
		Assert.AreEqual(2, dbgmds.Count);
	}

	[TestMethod]
	public void CreateDebugModes_Null_Success()
	{
		CodedExceptionsSection section = new();

		List<AssemblyDebugMode> dbgmds = section.CreateDebugModes();
		Assert.IsNull(dbgmds);
	}

	[TestMethod]
	public void GetFacilityOverrides_Void_Success()
	{
		Assert.IsNotNull(CodedExceptionsSection.GetFacilityOverrides());
	}

	[TestMethod]
	public void GetFacilityOverrides_String_Null()
	{
		Assert.IsNull(CodedExceptionsSection.GetFacilityOverrides("NoSuchSection"));
	}

	[TestMethod]
	public void GetFacilityOverrides_String_Success()
	{
		List<AssemblyFacilityOverride> ovrds = CodedExceptionsSection.GetFacilityOverrides("testSections/goodOverrides");
		Assert.IsNotNull(ovrds);
		Assert.AreEqual(7, ovrds.Count);
	}

	[TestMethod]
	public void GetFacilityOverrides_StringNoName_Success()
	{
		List<AssemblyFacilityOverride> ovrds = CodedExceptionsSection.GetFacilityOverrides("testSections/noAssemblyOverrides");
		Assert.IsNotNull(ovrds);
		Assert.AreEqual(1, ovrds.Count);
	}

	[TestMethod]
	public void GetFacilityOverrides_StringBadIdentifier_Throw()
	{
		_ = Assert.ThrowsException<System.Configuration.ConfigurationErrorsException>(() => CodedExceptionsSection.GetFacilityOverrides("testSections/badOverrides"));
	}

	[TestMethod]
	public void GetDebugModes_Void_Success()
	{
		Assert.IsNotNull(CodedExceptionsSection.GetDebugModes());
	}

	[TestMethod]
	public void GetDebugModes_String_Null()
	{
		Assert.IsNull(CodedExceptionsSection.GetDebugModes("NoSuchSection"));
	}

	[TestMethod]
	public void GetDebugModes_String_Success()
	{
		List<AssemblyDebugMode> dbgmds = CodedExceptionsSection.GetDebugModes("testSections/goodOverrides");
		Assert.IsNotNull(dbgmds);
		Assert.AreEqual(7, dbgmds.Count);
	}

	[TestMethod]
	public void GetDebugModes_StringEmptyName_Success()
	{
		List<AssemblyDebugMode> dbgmds = CodedExceptionsSection.GetDebugModes("testSections/noAssemblyOverrides");
		Assert.IsNotNull(dbgmds);
		Assert.AreEqual(1, dbgmds.Count);
	}

	[TestMethod]
	public void GetDebugModes_StringBadName_Throw()
	{
		_ = Assert.ThrowsException<System.Configuration.ConfigurationErrorsException>(() => CodedExceptionsSection.GetDebugModes("testSections/badOverrides"));
	}
}
