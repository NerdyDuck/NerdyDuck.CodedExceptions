// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideElement class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyFacilityOverrideElementTests
{
	[TestMethod]
	public void IsReadOnly_Success()
	{
		AssemblyFacilityOverrideElement overrideElement = new();
		Assert.IsFalse(overrideElement.IsReadOnly);
	}

	[TestMethod]
	public void ToOverride_Success()
	{
		AssemblyFacilityOverrideElement overrideElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			Identifier = 42
		};

		AssemblyFacilityOverride facilityOverride = overrideElement.ToOverride();
		Assert.AreEqual(Globals.ThisAssemblyNameString, facilityOverride.AssemblyName.ToString());
		Assert.AreEqual(42, facilityOverride.Identifier);
	}

	[TestMethod]
	public void ToOverride_IdentifierInvalid_Throw()
	{
		_ = Assert.ThrowsException<FormatException>(() =>
		  {
			  AssemblyFacilityOverrideElement overrideElement = new()
			  {
				  AssemblyName = Globals.ThisAssemblyNameString,
				  Identifier = -1
			  };

			  _ = overrideElement.ToOverride();
		  });
	}
}
