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
#pragma warning disable MSTEST0032 // Assertion condition is always true
		Assert.IsNotNull(section.DebugModes);
#pragma warning restore MSTEST0032 // Assertion condition is always true
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

		List<AssemblyFacilityOverride> overrides = section.CreateFacilityOverrides();
		Assert.AreEqual(2, overrides.Count);
	}

	[TestMethod]
	public void CreateFacilityOverrides_Null_Success()
	{
		CodedExceptionsSection section = new();

		List<AssemblyFacilityOverride> overrides = section.CreateFacilityOverrides();
		Assert.IsNull(overrides);
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

		List<AssemblyDebugMode> debugModes = section.CreateDebugModes();
		Assert.AreEqual(2, debugModes.Count);
	}

	[TestMethod]
	public void CreateDebugModes_Null_Success()
	{
		CodedExceptionsSection section = new();

		List<AssemblyDebugMode> debugModes = section.CreateDebugModes();
		Assert.IsNull(debugModes);
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
		List<AssemblyFacilityOverride> overrides = CodedExceptionsSection.GetFacilityOverrides("testSections/goodOverrides");
		Assert.IsNotNull(overrides);
		Assert.AreEqual(7, overrides.Count);
	}

	[TestMethod]
	public void GetFacilityOverrides_StringNoName_Success()
	{
		List<AssemblyFacilityOverride> overrides = CodedExceptionsSection.GetFacilityOverrides("testSections/noAssemblyOverrides");
		Assert.IsNotNull(overrides);
		Assert.AreEqual(1, overrides.Count);
	}

	[TestMethod]
	public void GetFacilityOverrides_StringBadIdentifier_Throw()
	{
		_ = Assert.ThrowsExactly<System.Configuration.ConfigurationErrorsException>(() => CodedExceptionsSection.GetFacilityOverrides("testSections/badOverrides"));
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
		List<AssemblyDebugMode> debugModes = CodedExceptionsSection.GetDebugModes("testSections/goodOverrides");
		Assert.IsNotNull(debugModes);
		Assert.AreEqual(7, debugModes.Count);
	}

	[TestMethod]
	public void GetDebugModes_StringEmptyName_Success()
	{
		List<AssemblyDebugMode> debugModes = CodedExceptionsSection.GetDebugModes("testSections/noAssemblyOverrides");
		Assert.IsNotNull(debugModes);
		Assert.AreEqual(1, debugModes.Count);
	}

	[TestMethod]
	public void GetDebugModes_StringBadName_Throw()
	{
		_ = Assert.ThrowsExactly<System.Configuration.ConfigurationErrorsException>(() => CodedExceptionsSection.GetDebugModes("testSections/badOverrides"));
	}
}
