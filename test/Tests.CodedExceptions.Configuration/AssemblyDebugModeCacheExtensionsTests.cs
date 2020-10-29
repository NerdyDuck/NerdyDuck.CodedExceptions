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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
#if NET50
using System.Buffers;
using System.Text.Json;
#else
using System.Json;
#endif
using System.Xml;
#if !NET48
using Microsoft.Extensions.Configuration;
#endif
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
		/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCacheExtensions class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class AssemblyDebugModeCacheExtensionsTests
		{
			[TestMethod]
			public void AssertCache_Void_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					AssemblyDebugModeCacheAppConfigExtensions.LoadApplicationConfiguration(null);
				});
			}

			[TestMethod]
			public void LoadApplicationConfiguration_Void_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.LoadApplicationConfiguration();
				Assert.AreEqual(1, cache.Count);
			}

			[TestMethod]
			public void LoadApplicationConfiguration_String_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.LoadApplicationConfiguration("testSections/goodOverrides");
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadApplicationConfiguration_StringEmpty_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadApplicationConfiguration(string.Empty);
				});
			}

			[TestMethod]
			public void LoadJson_Void_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.LoadJson();
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_String_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.LoadJson(@"TestFiles\AssemblyDebugModes.json");
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_StringEmpty_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadJson(string.Empty);
				});
			}

			[TestMethod]
			public void LoadJson_StringInvalid_Throw()
			{
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadJson("NoFileHere.json");
				});
			}

			[TestMethod]
			public void LoadJson_Stream_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				using (FileStream stream = new FileStream(@"TestFiles\AssemblyDebugModes.json", FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					cache.LoadJson(stream);
				}
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_StreamNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadJson((Stream)null);
				});
			}

			[TestMethod]
			public void LoadJson_StreamNoRead_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadJson(new NoReadStream());
				});
			}

			[TestMethod]
			public void LoadJson_TextReader_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				using (FileStream stream = new FileStream(@"TestFiles\AssemblyDebugModes.json", FileMode.Open, FileAccess.Read, FileShare.Read))
				using (TextReader reader = new StreamReader(stream))
				{
					cache.LoadJson(reader);
				}
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_TextReaderNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadJson((TextReader)null);
				});
			}

			[TestMethod]
			public void LoadJson_TextReaderInvIsEnabled_Throw()
			{
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					using FileStream stream = new FileStream(@"TestFiles\AssemblyDebugModesInvIsEnabled.json", FileMode.Open, FileAccess.Read, FileShare.Read);
					using TextReader reader = new StreamReader(stream);
					cache.LoadJson(reader);
				});
			}

			[TestMethod]
			public void LoadJson_InvAssemblyName_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadJson(@"TestFiles\AssemblyDebugModesInvAssemblyName.json");
				});
			}

			[TestMethod]
			public void LoadJson_InvIsEnabled_Throw()
			{
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadJson(@"TestFiles\AssemblyDebugModesInvIsEnabled.json");
				});
			}

			[TestMethod]
			public void LoadJson_NoAssemblyName_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.LoadJson(@"TestFiles\AssemblyDebugModesNoAssemblyName.json");
				Assert.AreEqual(1, cache.Count);
			}

			[TestMethod]
			public void LoadJson_NotBool_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadJson(@"TestFiles\AssemblyDebugModesNotBool.json");
				});
			}

			[TestMethod]
			public void ParseJson_String_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				string json = File.ReadAllText(@"TestFiles\AssemblyDebugModes.json");
				cache.ParseJson(json);
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void ParseJson_StringNull_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.ParseJson(null);
				});
			}

			[TestMethod]
			public void ParseJson_StringInvIsEnabled_Throw()
			{
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					string json = File.ReadAllText(@"TestFiles\AssemblyDebugModesInvIsEnabled.json");
					cache.ParseJson(json);
				});
			}

#if NET50
			[TestMethod]
			public void LoadJson_ReadOnlySequence_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				byte[] fileBytes = File.ReadAllBytes(@"TestFiles\AssemblyDebugModes.json"); // Has BOM, so we need to remove first three bytes.
				ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>(fileBytes, 3, fileBytes.Length - 3);
				cache.LoadJson(buffer);
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_ReadOnlyMemory_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				byte[] fileBytes = File.ReadAllBytes(@"TestFiles\AssemblyDebugModes.json"); // Has BOM, so we need to remove first three bytes.
				ReadOnlyMemory<byte> buffer = new ReadOnlyMemory<byte>(fileBytes, 3, fileBytes.Length - 3);
				cache.LoadJson(buffer);
				Assert.AreEqual(7, cache.Count);
			}
#endif

#if !NET50
			[TestMethod]
			public void FromJson_JsonValueNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.FromJson((JsonValue)null);
				});
			}

			[TestMethod]
			public void FromJson_JsonValueNotObject_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					JsonValue jsonValue = new JsonPrimitive("foo");
					cache.FromJson(jsonValue);
				});
			}
#endif

			[TestMethod]
			public void LoadJson_ParentObj_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.LoadJson(@"TestFiles\AssemblyDebugModesParent.json");
				Assert.AreEqual(7, cache.Count);
			}
#if !NET48
			[TestMethod]
			public void FromConfigurationSection_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				IConfiguration config = new ConfigurationBuilder().AddJsonFile(@"TestFiles\AssemblyDebugModesParent.json").Build();
				cache.LoadConfigurationSection(config.GetSection("debugModes"));
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void FromConfigurationSection_Null_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadConfigurationSection(null);
				});
			}

			[TestMethod]
			public void FromConfigurationSection_AssemblyNameInv_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					Dictionary<string, string> modes = new Dictionary<string, string>
					{
						 ["debugModes:NerdyDuck.CodedExceptions, Version=9.x.x.x"] = "True"
					};
					IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(modes).Build();
					cache.LoadConfigurationSection(config.GetSection("debugModes"));
				});
			}

			[TestMethod]
			public void FromConfigurationSection_IsEnabledInv_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					Dictionary<string, string> modes = new Dictionary<string, string>
					{
						["debugModes:NerdyDuck.CodedExceptions"] = "Foo"
					};
					IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(modes).Build();
					cache.LoadConfigurationSection(config.GetSection("debugModes"));
				});
			}


			[TestMethod]
			public void FromConfigurationSection_IsEnabledEmpty_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					Dictionary<string, string> modes = new Dictionary<string, string>
					{
						["debugModes:NerdyDuck.CodedExceptions"] = ""
					};
					IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(modes).Build();
					cache.LoadConfigurationSection(config.GetSection("debugModes"));
				});
			}
#endif
		}
	}
}
