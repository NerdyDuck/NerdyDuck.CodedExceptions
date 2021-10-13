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

using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions.Configuration;

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
