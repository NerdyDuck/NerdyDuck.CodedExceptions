// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideCollection class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyFacilityOverrideCollectionTests
{
	[TestMethod]
	public void Add_Index_Remove_ByElement_Success()
	{
		AssemblyFacilityOverrideCollection overrideCollection = new();
		AssemblyFacilityOverrideElement overrideElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			Identifier = 42
		};
		overrideCollection.Add(overrideElement);

		Assert.AreEqual(0, overrideCollection.IndexOf(overrideElement));
		Assert.IsNotNull(overrideCollection[0]);
		overrideCollection.Remove(overrideElement);
		Assert.AreEqual(-1, overrideCollection.IndexOf(overrideElement));
	}

	[TestMethod]
	public void Add_Remove_ByName_Success()
	{
		AssemblyFacilityOverrideCollection overrideCollection = new();
		AssemblyFacilityOverrideElement overrideElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			Identifier = 42
		};
		overrideCollection.Add(overrideElement);

		Assert.IsNotNull(overrideCollection[Globals.ThisAssemblyNameString]);
		overrideCollection.Remove(Globals.ThisAssemblyNameString);
		Assert.IsNull(overrideCollection[Globals.ThisAssemblyNameString]);
	}

	[TestMethod]
	public void Add_Remove_ByIndex_Success()
	{

		AssemblyFacilityOverrideCollection overrideCollection = new();
		AssemblyFacilityOverrideElement overrideElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			Identifier = 42
		};
		overrideCollection.Add(overrideElement);

		Assert.AreEqual(0, overrideCollection.IndexOf(overrideElement));
		overrideCollection.RemoveAt(0);
		Assert.AreEqual(-1, overrideCollection.IndexOf(overrideElement));
	}

	[TestMethod]
	public void Clear_Success()
	{
		AssemblyFacilityOverrideCollection overrideCollection = new();
		AssemblyFacilityOverrideElement overrideElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			Identifier = 42
		};
		overrideCollection.Add(overrideElement);

		overrideCollection[0] = overrideElement;
		Assert.AreEqual(0, overrideCollection.IndexOf(overrideElement));
		overrideCollection.Clear();
		Assert.AreEqual(-1, overrideCollection.IndexOf(overrideElement));
	}
}
