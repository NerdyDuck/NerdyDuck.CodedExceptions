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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions.Configuration;

#pragma warning disable CA1707
namespace NerdyDuck.Tests.CodedExceptions.Configuration
{
#if NETCORE21
	namespace NetCore21
#elif NETCORE31
	namespace NetCore31
#elif NET48
	namespace Net48
#elif NET50
	namespace Net50
#endif
	{

		/// <summary>
		/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class AssemblyDebugModeCacheTests
		{
			private static readonly AssemblyIdentity s_thisAssemblyIdentity = new AssemblyIdentity(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
			private static readonly AssemblyIdentity s_otherAssemblyIdentity = new AssemblyIdentity(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.All);
			private static readonly Assembly s_thirdAssembly = typeof(System.Exception).Assembly;

			#region Global
			[TestMethod]
			public void Global_Success()
			{
				Assert.IsNotNull(AssemblyDebugModeCache.Global);
			}
			#endregion

			#region Constructor
			[TestMethod]
			public void Ctor_Success()
			{
				AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.Dispose();
			}
			#endregion

			#region IsDebugModeEnabled
			[TestMethod]
			public void IsDebugModeEnabled_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
				cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));

				Assert.IsTrue(cache.IsDebugModeEnabled(Globals.ThisAssembly));
				Assert.IsFalse(cache.IsDebugModeEnabled(Globals.OtherAssembly));
				Assert.IsFalse(cache.IsDebugModeEnabled(s_thirdAssembly));

				Assert.IsTrue(cache.IsDebugModeEnabled(typeof(AssemblyDebugModeCacheTests)));
				Assert.IsFalse(cache.IsDebugModeEnabled(typeof(Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute)));
				Assert.IsFalse(cache.IsDebugModeEnabled(typeof(System.Exception)));
			}

			[TestMethod]
			public void IsDebugModeEnabled_ArgNull_Throw()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
				cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));

				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					cache.IsDebugModeEnabled((Assembly)null);
				});

				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					cache.IsDebugModeEnabled((Type)null);
				});
			}
			#endregion

			#region BeginEndUpdate
			[TestMethod]
			public void BeginEndUpdate_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				bool hasUpdate = false;
				cache.CacheChanged += Cache_CacheChanged;
				cache.BeginUpdate();
				cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
				Assert.IsFalse(hasUpdate);
				cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));
				Assert.IsFalse(hasUpdate);
				cache.EndUpdate();

				Assert.IsTrue(hasUpdate);

				void Cache_CacheChanged(object sender, EventArgs e)
				{
					hasUpdate = true;
				}
			}

			[TestMethod]
			public void BeginEndUpdateNoChange_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				bool hasUpdate = false;
				cache.CacheChanged += Cache_CacheChanged;
				cache.BeginUpdate();
				cache.EndUpdate();
				Assert.IsFalse(hasUpdate);

				void Cache_CacheChanged(object sender, EventArgs e)
				{
					hasUpdate = true;
				}
			}
			#endregion

			#region Add
			[TestMethod]
			public void Add_Various_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
				cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));

				Assert.IsTrue(cache.IsDebugModeEnabled(Globals.ThisAssembly), "this");
				Assert.IsFalse(cache.IsDebugModeEnabled(Globals.OtherAssembly), "other");

				cache.Add(s_otherAssemblyIdentity, true);

				Assert.IsTrue(cache.IsDebugModeEnabled(Globals.OtherAssembly), "other updated");
			}

			[TestMethod]
			public void Add_Null_Throw()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					cache.Add(null);
				});
			}

			[TestMethod]
			public void AddRange_Success()
			{
				AssemblyDebugMode[] modes = new AssemblyDebugMode[]
				{
					new AssemblyDebugMode(s_thisAssemblyIdentity, true),
					new AssemblyDebugMode(s_otherAssemblyIdentity, false),
					new AssemblyDebugMode(s_otherAssemblyIdentity, true)
				};

				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.AddRange(null);
				Assert.IsFalse(cache.IsDebugModeEnabled(Globals.ThisAssembly), "empty");

				cache.AddRange(modes);
				Assert.IsTrue(cache.IsDebugModeEnabled(Globals.ThisAssembly), "this");
				Assert.IsTrue(cache.IsDebugModeEnabled(Globals.OtherAssembly), "other");
				Assert.IsFalse(cache.IsDebugModeEnabled(s_thirdAssembly), "third");
			}
			#endregion

			#region Clear
			[TestMethod]
			public void Clear_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
				Assert.IsTrue(cache.IsDebugModeEnabled(Globals.ThisAssembly), "this");

				cache.Clear();
				Assert.IsFalse(cache.IsDebugModeEnabled(Globals.ThisAssembly), "this cleared");
			}
			#endregion

			#region Remove
			[TestMethod]
			public void Remove_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));

				Assert.IsTrue(cache.IsDebugModeEnabled(Globals.ThisAssembly));
				cache.Remove(s_thisAssemblyIdentity);
				Assert.IsFalse(cache.IsDebugModeEnabled(Globals.ThisAssembly));
			}

			[TestMethod]
			public void Remove_ArgNull_Throw()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
				cache.Add(new AssemblyDebugMode(s_otherAssemblyIdentity, false));

				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					cache.Remove(null);
				});
			}
			#endregion

			#region Dispose
			[TestMethod]
			public void Dispose_Throw()
			{
				AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.Dispose();
				cache.Dispose();

				Assert.ThrowsException<ObjectDisposedException>(() =>
				{
					cache.IsDebugModeEnabled(Globals.ThisAssembly);
				});

				Assert.ThrowsException<ObjectDisposedException>(() =>
				{
					cache.Add(new AssemblyDebugMode(s_thisAssemblyIdentity, true));
				});

				Assert.ThrowsException<ObjectDisposedException>(() =>
				{
					cache.Clear();
				});
			}
			#endregion

			#region Finalizer
			[TestMethod]
			[SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Intentional for testing.")]
			[SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "Intentional for testing.")]
			public void Finalizer_Success()
			{
				if (true)
				{
					_ = new AssemblyDebugModeCache();
				}
				GC.Collect();
				GC.WaitForPendingFinalizers();
				
			}
			#endregion
		}
	}
}
