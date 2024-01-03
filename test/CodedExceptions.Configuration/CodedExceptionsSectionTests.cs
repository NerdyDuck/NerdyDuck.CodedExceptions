// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

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
