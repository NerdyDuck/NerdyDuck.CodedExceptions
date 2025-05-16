// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyDebugModeCacheTests
{
	private static readonly AssemblyIdentity s_thisAssemblyIdentity = new(GlobalConstants.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
	private static readonly AssemblyIdentity s_otherAssemblyIdentity = new(GlobalConstants.OtherAssembly, AssemblyIdentity.AssemblyNameElements.All);
	private static readonly Assembly s_thirdAssembly = typeof(System.Exception).Assembly;

	[TestMethod]
	public void Global_Success()
	{
#pragma warning disable MSTEST0032 // Assertion condition is always true
		Assert.IsNotNull(AssemblyDebugModeCache.Global);
#pragma warning restore MSTEST0032 // Assertion condition is always true
	}

	[TestMethod]
	public void Ctor_Success()
	{
		AssemblyDebugModeCache cache = new();
		cache.Dispose();
	}

	[TestMethod]
	public void Finalizer_Success()
	{
#pragma warning disable IDE0059 // Unnecessary assignment of a value
		WeakReference<AssemblyDebugModeCache> weak = null;

		void dispose()
		{
			AssemblyDebugModeCache disposable = new();
			weak = new WeakReference<AssemblyDebugModeCache>(disposable, true);
		}

		dispose();
		GC.Collect(0, GCCollectionMode.Forced);
		GC.WaitForPendingFinalizers();
#pragma warning restore IDE0059 // Unnecessary assignment of a value
	}

	[TestMethod]
	public void IsDebugModeEnabled_Success()
	{
		using AssemblyDebugModeCache cache = new();
		cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
		cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));

		Assert.IsTrue(cache.IsDebugModeEnabled(GlobalConstants.ThisAssembly));
		Assert.IsFalse(cache.IsDebugModeEnabled(GlobalConstants.OtherAssembly));
		Assert.IsFalse(cache.IsDebugModeEnabled(s_thirdAssembly));

		Assert.IsTrue(cache.IsDebugModeEnabled(typeof(AssemblyDebugModeCacheTests)));
		Assert.IsFalse(cache.IsDebugModeEnabled(typeof(Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute)));
		Assert.IsFalse(cache.IsDebugModeEnabled(typeof(System.Exception)));
	}

	[TestMethod]
	public void IsDebugModeEnabled_ArgNull_Throw()
	{
		using AssemblyDebugModeCache cache = new();
		cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
		cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));

		_ = Assert.ThrowsExactly<ArgumentNullException>(() => cache.IsDebugModeEnabled((Assembly)null));
		_ = Assert.ThrowsExactly<ArgumentNullException>(() => cache.IsDebugModeEnabled((Type)null));
	}

	[TestMethod]
	public void BeginEndUpdate_Success()
	{
		using AssemblyDebugModeCache cache = new();
		bool hasUpdate = false;
		cache.CacheChanged += Cache_CacheChanged;
		cache.BeginUpdate();
		cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
		Assert.IsFalse(hasUpdate);
		cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));
		Assert.IsFalse(hasUpdate);
		cache.EndUpdate();

		Assert.IsTrue(hasUpdate);

		void Cache_CacheChanged(object sender, EventArgs e) => hasUpdate = true;
	}

	[TestMethod]
	public void BeginEndUpdateNoChange_Success()
	{
		using AssemblyDebugModeCache cache = new();
		bool hasUpdate = false;
		cache.CacheChanged += Cache_CacheChanged;
		cache.BeginUpdate();
		cache.EndUpdate();
		Assert.IsFalse(hasUpdate);

		void Cache_CacheChanged(object sender, EventArgs e) => hasUpdate = true;
	}

	[TestMethod]
	public void Add_Various_Success()
	{
		using AssemblyDebugModeCache cache = new();
		cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
		cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));

		Assert.IsTrue(cache.IsDebugModeEnabled(GlobalConstants.ThisAssembly), "this");
		Assert.IsFalse(cache.IsDebugModeEnabled(GlobalConstants.OtherAssembly), "other");

		cache.Add(s_otherAssemblyIdentity, true);

		Assert.IsTrue(cache.IsDebugModeEnabled(GlobalConstants.OtherAssembly), "other updated");
	}

	[TestMethod]
	public void Add_Null_Throw()
	{
		using AssemblyDebugModeCache cache = new();
		_ = Assert.ThrowsExactly<ArgumentNullException>(() => cache.Add(null));
	}

	[TestMethod]
	public void AddRange_Success()
	{
		AssemblyDebugMode[] modes =
		[
					new AssemblyDebugMode(s_thisAssemblyIdentity, true),
					new AssemblyDebugMode(s_otherAssemblyIdentity, false),
					new AssemblyDebugMode(s_otherAssemblyIdentity, true)
		];

		using AssemblyDebugModeCache cache = new();
		cache.AddRange(null);
		Assert.IsFalse(cache.IsDebugModeEnabled(GlobalConstants.ThisAssembly), "empty");

		cache.AddRange(modes);
		Assert.IsTrue(cache.IsDebugModeEnabled(GlobalConstants.ThisAssembly), "this");
		Assert.IsTrue(cache.IsDebugModeEnabled(GlobalConstants.OtherAssembly), "other");
		Assert.IsFalse(cache.IsDebugModeEnabled(s_thirdAssembly), "third");
	}

	[TestMethod]
	public void Clear_Success()
	{
		using AssemblyDebugModeCache cache = new();
		cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
		Assert.IsTrue(cache.IsDebugModeEnabled(GlobalConstants.ThisAssembly), "this");

		cache.Clear();
		Assert.IsFalse(cache.IsDebugModeEnabled(GlobalConstants.ThisAssembly), "this cleared");
	}

	[TestMethod]
	public void Remove_Success()
	{
		using AssemblyDebugModeCache cache = new();
		cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));

		Assert.IsTrue(cache.IsDebugModeEnabled(GlobalConstants.ThisAssembly));
		cache.Remove(s_thisAssemblyIdentity);
		Assert.IsFalse(cache.IsDebugModeEnabled(GlobalConstants.ThisAssembly));
	}

	[TestMethod]
	public void Remove_ArgNull_Throw()
	{
		using AssemblyDebugModeCache cache = new();
		cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
		cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));

		_ = Assert.ThrowsExactly<ArgumentNullException>(() => cache.Remove(null));
	}

	[TestMethod]
	public void Dispose_Throw()
	{
		AssemblyDebugModeCache cache = new();
		cache.Dispose();
		cache.Dispose();

		_ = Assert.ThrowsExactly<ObjectDisposedException>(() => cache.IsDebugModeEnabled(GlobalConstants.ThisAssembly));
		_ = Assert.ThrowsExactly<ObjectDisposedException>(() => cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true)));
		_ = Assert.ThrowsExactly<ObjectDisposedException>(cache.Clear);
	}
}
