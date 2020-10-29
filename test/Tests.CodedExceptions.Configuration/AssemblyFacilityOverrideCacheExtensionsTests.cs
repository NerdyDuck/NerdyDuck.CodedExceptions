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
using Microsoft.Extensions.Configuration;
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
		/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideCacheExtensions class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class AssemblyFacilityOverrideCacheExtensionsTests
		{
			[TestMethod]
			public void AssertCache_Void_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					AssemblyFacilityOverrideCacheAppConfigExtensions.LoadApplicationConfiguration(null);
				});
			}

			[TestMethod]
			public void LoadApplicationConfiguration_Void_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				cache.LoadApplicationConfiguration();
				Assert.AreEqual(1, cache.Count);
			}

			[TestMethod]
			public void LoadApplicationConfiguration_String_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				cache.LoadApplicationConfiguration("testSections/goodOverrides");
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadApplicationConfiguration_StringEmpty_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadApplicationConfiguration(string.Empty);
				});
			}

			[TestMethod]
			public void LoadJson_Void_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				cache.LoadJson();
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_String_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				cache.LoadJson(@"TestFiles\FacilityIdentifierOverrides.json");
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_StringEmpty_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadJson(string.Empty);
				});
			}

			[TestMethod]
			public void LoadJson_StringInvalid_Throw()
			{
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadJson("NoFileHere.json");
				});
			}

			[TestMethod]
			public void LoadJson_Stream_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				using (FileStream stream = new FileStream(@"TestFiles\FacilityIdentifierOverrides.json", FileMode.Open, FileAccess.Read, FileShare.Read))
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
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadJson((Stream)null);
				});
			}

			[TestMethod]
			public void LoadJson_StreamNoRead_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadJson(new NoReadStream());
				});
			}

			[TestMethod]
			public void LoadJson_TextReader_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				using (FileStream stream = new FileStream(@"TestFiles\FacilityIdentifierOverrides.json", FileMode.Open, FileAccess.Read, FileShare.Read))
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
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadJson((TextReader)null);
				});
			}

			[TestMethod]
			public void LoadJson_TextReaderInvIsEnabled_Throw()
			{
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					using FileStream stream = new FileStream(@"TestFiles\FacilityIdentifierOverridesInvIdentifier.json", FileMode.Open, FileAccess.Read, FileShare.Read);
					using TextReader reader = new StreamReader(stream);
					cache.LoadJson(reader);
				});
			}

			[TestMethod]
			public void LoadJson_InvAssemblyName_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesInvAssemblyName.json");
				});
			}

			[TestMethod]
			public void LoadJson_InvIsEnabled_Throw()
			{
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesInvIdentifier.json");
				});
			}

			[TestMethod]
			public void LoadJson_NoAssemblyName_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesNoAssemblyName.json");
				Assert.AreEqual(1, cache.Count);
			}

			[TestMethod]
			public void LoadJson_NotBool_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesNotInt.json");
				});
			}

			[TestMethod]
			public void ParseJson_String_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				string json = File.ReadAllText(@"TestFiles\FacilityIdentifierOverrides.json");
				cache.ParseJson(json);
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void ParseJson_StringNull_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.ParseJson(null);
				});
			}

			[TestMethod]
			public void ParseJson_InvAssemblyName_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					string json = File.ReadAllText(@"TestFiles\FacilityIdentifierOverridesInvAssemblyName.json");
					cache.ParseJson(json);
				});
			}

#if !NET50
			[TestMethod]
			public void FromJson_JsonValueNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.FromJson((JsonValue)null);
				});
			}

			[TestMethod]
			public void FromJson_JsonValueNotObject_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					JsonValue jsonValue = new JsonPrimitive("foo");
					cache.FromJson(jsonValue);
				});
			}
#endif
			[TestMethod]
			public void LoadJson_ParentObj_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesParent.json");
				Assert.AreEqual(7, cache.Count);
			}

#if NET50
			[TestMethod]
			public void LoadJson_ReadOnlySequence_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				byte[] fileBytes = File.ReadAllBytes(@"TestFiles\FacilityIdentifierOverrides.json"); // Has BOM, so we need to remove first three bytes.
				ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>(fileBytes, 3, fileBytes.Length - 3);
				cache.LoadJson(buffer);
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_ReadOnlyMemory_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				byte[] fileBytes = File.ReadAllBytes(@"TestFiles\FacilityIdentifierOverrides.json"); // Has BOM, so we need to remove first three bytes.
				ReadOnlyMemory<byte> buffer = new ReadOnlyMemory<byte>(fileBytes, 3, fileBytes.Length - 3);
				cache.LoadJson(buffer);
				Assert.AreEqual(7, cache.Count);
			}
#endif

#if !NET48
			[TestMethod]
			public void LoadConfigurationSection_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				IConfiguration config = new ConfigurationBuilder().AddJsonFile(@"TestFiles\FacilityIdentifierOverridesParent.json").Build();
				cache.LoadConfigurationSection(config.GetSection("facilityIdentifierOverrides"));
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadConfigurationSection_Null_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadConfigurationSection(null);
				});
			}

			[TestMethod]
			public void LoadConfigurationSection_AssemblyNameInv_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					Dictionary<string, string> modes = new Dictionary<string, string>
					{
						["facilityIdentifierOverrides:NerdyDuck.CodedExceptions, Version=9.x.x.x"] = "42"
					};
					IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(modes).Build();
					cache.LoadConfigurationSection(config.GetSection("facilityIdentifierOverrides"));
				});
			}

			[TestMethod]
			public void LoadConfigurationSection_IsEnabledInv_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					Dictionary<string, string> modes = new Dictionary<string, string>
					{
						["facilityIdentifierOverrides:NerdyDuck.CodedExceptions"] = "Foo"
					};
					IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(modes).Build();
					cache.LoadConfigurationSection(config.GetSection("facilityIdentifierOverrides"));
				});
			}


			[TestMethod]
			public void LoadConfigurationSection_IsEnabledEmpty_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					Dictionary<string, string> modes = new Dictionary<string, string>
					{
						["facilityIdentifierOverrides:NerdyDuck.CodedExceptions"] = ""
					};
					IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(modes).Build();
					cache.LoadConfigurationSection(config.GetSection("facilityIdentifierOverrides"));
				});
			}
#endif
		}
	}
}
