// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
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
	public void Finalizer_Success()
	{
		WeakReference<AssemblyFacilityOverrideCache> weak = null;

		void dispose()
		{
			AssemblyFacilityOverrideCache disposable = new();
			weak = new WeakReference<AssemblyFacilityOverrideCache>(disposable, true);
		}

		dispose();
		GC.Collect(0, GCCollectionMode.Forced);
		GC.WaitForPendingFinalizers();
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
		Assert.IsTrue(cache.TryGetOverride(typeof(TestClassAttribute), out int i5));
		Assert.AreEqual(17, i2);
		Assert.IsFalse(cache.TryGetOverride(typeof(Exception), out int i6));
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
}
