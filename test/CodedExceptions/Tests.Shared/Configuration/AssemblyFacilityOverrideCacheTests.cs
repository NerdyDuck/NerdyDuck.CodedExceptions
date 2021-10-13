#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.CodedExceptions - Unit tests for the
 * NerdyDuck.CodedExceptions assembly
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

using System.Reflection;
using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideCache class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyFacilityOverrideCacheTests
{
	private static readonly AssemblyIdentity s_thisAssemblyIdentity = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
	private static readonly AssemblyIdentity s_otherAssemblyIdentity = new(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.All);
	private static readonly Assembly s_thirdAssembly = typeof(System.Exception).Assembly;

	[TestMethod]
	public void Global_Success()
	{
		Assert.IsNotNull(AssemblyFacilityOverrideCache.Global);
	}

	[TestMethod]
	public void Ctor_Success()
	{
		AssemblyFacilityOverrideCache cache = new();
		cache.Dispose();
	}

	[TestMethod]
	public void TryGetOverride_Success()
	{
		using AssemblyFacilityOverrideCache cache = new();
		cache.Add(new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42));
		cache.Add(new AssemblyFacilityOverride(s_otherAssemblyIdentity, 17));

		Assert.IsTrue(cache.TryGetOverride(Globals.ThisAssembly, out int i1));
		Assert.AreEqual(42, i1);
		Assert.IsTrue(cache.TryGetOverride(Globals.OtherAssembly, out int i2));
		Assert.AreEqual(17, i2);
		Assert.IsFalse(cache.TryGetOverride(s_thirdAssembly, out int i3));
		Assert.AreEqual(0, i3);

		Assert.IsTrue(cache.TryGetOverride(typeof(AssemblyDebugModeCacheTests), out int i4));
		Assert.AreEqual(42, i4);
		Assert.IsTrue(cache.TryGetOverride(typeof(Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute), out int i5));
		Assert.AreEqual(17, i2);
		Assert.IsFalse(cache.TryGetOverride(typeof(System.Exception), out int i6));
	}

	[TestMethod]
	public void TryGetOverride_ArgNull_Throw()
	{
		using AssemblyFacilityOverrideCache cache = new();
		cache.Add(new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42));
		cache.Add(new AssemblyFacilityOverride(s_otherAssemblyIdentity, 17));

		_ = Assert.ThrowsException<ArgumentNullException>(() => cache.TryGetOverride((Assembly)null, out int i1));

		_ = Assert.ThrowsException<ArgumentNullException>(() => cache.TryGetOverride((Type)null, out int i2));
	}

	[TestMethod]
	public void Add_Various_Success()
	{
		using AssemblyFacilityOverrideCache cache = new();
		cache.Add(new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42));
		cache.Add(new AssemblyFacilityOverride(s_otherAssemblyIdentity, 17));

		Assert.IsTrue(cache.TryGetOverride(Globals.ThisAssembly, out int i1));
		Assert.AreEqual(42, i1);
		Assert.IsTrue(cache.TryGetOverride(Globals.OtherAssembly, out int i2));
		Assert.AreEqual(17, i2);

		cache.Add(s_otherAssemblyIdentity, 10);

		Assert.IsTrue(cache.TryGetOverride(Globals.OtherAssembly, out int i3));
		Assert.AreEqual(10, i3);
	}

	[TestMethod]
	public void Add_Null_Throw()
	{
		using AssemblyFacilityOverrideCache cache = new();
		_ = Assert.ThrowsException<ArgumentNullException>(() => cache.Add(null));
	}

	[TestMethod]
	public void AddRange_Success()
	{
		AssemblyFacilityOverride[] overrides = new AssemblyFacilityOverride[]
		{
					new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42),
					new AssemblyFacilityOverride(s_otherAssemblyIdentity, 17),
					new AssemblyFacilityOverride(s_otherAssemblyIdentity, 10)
		};

		using AssemblyFacilityOverrideCache cache = new();
		cache.AddRange(null);
		Assert.IsFalse(cache.TryGetOverride(Globals.ThisAssembly, out int i1), "empty");

		cache.AddRange(overrides);
		Assert.IsTrue(cache.TryGetOverride(Globals.ThisAssembly, out int i2));
		Assert.AreEqual(42, i2);
		Assert.IsTrue(cache.TryGetOverride(Globals.OtherAssembly, out int i3));
		Assert.AreEqual(10, i3);
	}

	[TestMethod]
	public void Clear_Success()
	{
		using AssemblyFacilityOverrideCache cache = new();
		cache.Add(new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42));
		Assert.IsTrue(cache.TryGetOverride(Globals.ThisAssembly, out int i1));
		Assert.AreEqual(42, i1);

		cache.Clear();
		Assert.IsFalse(cache.TryGetOverride(Globals.ThisAssembly, out int i2));
	}

	[TestMethod]
	public void Dispose_Throw()
	{
		AssemblyFacilityOverrideCache cache = new();
		cache.Dispose();
		cache.Dispose();

		_ = Assert.ThrowsException<ObjectDisposedException>(() => cache.TryGetOverride(Globals.ThisAssembly, out int i1));

		_ = Assert.ThrowsException<ObjectDisposedException>(() => cache.Add(new AssemblyFacilityOverride(s_thisAssemblyIdentity, 42)));

		_ = Assert.ThrowsException<ObjectDisposedException>(() => cache.Clear());
	}

	[TestMethod]
	//[SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Intentional for testing.")]
	public void Finalizer_Success()
	{
		if (true)
		{
			_ = new AssemblyFacilityOverrideCache();
		}
		GC.Collect();
		GC.WaitForPendingFinalizers();

	}
}
