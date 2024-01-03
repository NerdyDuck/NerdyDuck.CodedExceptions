// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeElement class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyDebugModeElementTests
{
	[TestMethod]
	public void IsReadOnly_Success()
	{
		AssemblyDebugModeElement debugModeElement = new();
		Assert.IsFalse(debugModeElement.IsReadOnly);
	}

	[TestMethod]
	public void ToOverride_Success()
	{
		AssemblyDebugModeElement debugModeElement = new()
		{
			AssemblyName = Globals.ThisAssemblyNameString
		};

		AssemblyDebugMode debugMode = debugModeElement.ToAssemblyDebugMode();
		Assert.AreEqual(Globals.ThisAssemblyNameString, debugMode.AssemblyName.ToString());
		Assert.IsTrue(debugMode.IsEnabled);
	}

	[TestMethod]
	public void ToOverride_IdentifierInvalid_Throw()
	{
		_ = Assert.ThrowsException<FormatException>(() =>
		  {
			  AssemblyDebugModeElement debugModeElement = new()
			  {
				  AssemblyName = "TestAssembly, Version=1.0.xxx",
				  IsEnabled = true
			  };

			  _ = debugModeElement.ToAssemblyDebugMode();
		  });
	}
}
