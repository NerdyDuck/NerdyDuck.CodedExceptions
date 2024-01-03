// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideElement class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyDebugModeCollectionTests
{
	[TestMethod]
	public void Add_Index_Remove_ByElement_Success()
	{
		AssemblyDebugModeCollection debugModeCollection = new();
		AssemblyDebugModeElement debugModeElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			IsEnabled = true
		};
		debugModeCollection.Add(debugModeElement);

		Assert.AreEqual(0, debugModeCollection.IndexOf(debugModeElement));
		Assert.IsNotNull(debugModeCollection[0]);
		debugModeCollection.Remove(debugModeElement);
		Assert.AreEqual(-1, debugModeCollection.IndexOf(debugModeElement));
	}

	[TestMethod]
	public void Add_Remove_ByName_Success()
	{
		AssemblyDebugModeCollection debugModeCollection = new();
		AssemblyDebugModeElement debugModeElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			IsEnabled = true
		};
		debugModeCollection.Add(debugModeElement);

		Assert.IsNotNull(debugModeCollection[Globals.ThisAssemblyNameString]);
		debugModeCollection.Remove(Globals.ThisAssemblyNameString);
		Assert.IsNull(debugModeCollection[Globals.ThisAssemblyNameString]);
	}

	[TestMethod]
	public void Add_Remove_ByIndex_Success()
	{

		AssemblyDebugModeCollection debugModeCollection = new();
		AssemblyDebugModeElement debugModeElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			IsEnabled = true
		};
		debugModeCollection.Add(debugModeElement);

		Assert.AreEqual(0, debugModeCollection.IndexOf(debugModeElement));
		debugModeCollection.RemoveAt(0);
		Assert.AreEqual(-1, debugModeCollection.IndexOf(debugModeElement));
	}

	[TestMethod]
	public void Clear_Success()
	{
		AssemblyDebugModeCollection debugModeCollection = new();
		AssemblyDebugModeElement debugModeElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString,
			IsEnabled = true
		};
		debugModeCollection.Add(debugModeElement);

		debugModeCollection[0] = debugModeElement;
		Assert.AreEqual(0, debugModeCollection.IndexOf(debugModeElement));
		debugModeCollection.Clear();
		Assert.AreEqual(-1, debugModeCollection.IndexOf(debugModeElement));
	}
}
