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
using System.Buffers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions.Configuration
{
#if NET60
	namespace Net60
#elif NET50
	namespace Net50
#elif NETCORE31
	namespace NetCore31
#elif NET48
	namespace Net48
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
				_ = Assert.ThrowsException<ArgumentNullException>(() => AssemblyFacilityOverrideCacheAppConfigExtensions.LoadApplicationConfiguration(null));
			}

			[TestMethod]
			public void LoadApplicationConfiguration_Void_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				_ = cache.LoadApplicationConfiguration();
				Assert.AreEqual(1, cache.Count);
			}

			[TestMethod]
			public void LoadApplicationConfiguration_String_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				_ = cache.LoadApplicationConfiguration("testSections/goodOverrides");
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadApplicationConfiguration_StringEmpty_Throw()
			{
				_ = Assert.ThrowsException<ArgumentException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadApplicationConfiguration(string.Empty);
				  });
			}

			[TestMethod]
			public void FromJson_JsonElement_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				using FileStream stream = File.OpenRead(@"TestFiles\FacilityIdentifierOverrides.json");
				System.Text.Json.JsonElement root = System.Text.Json.JsonDocument.Parse(stream).RootElement;
				_ = cache.FromJson(root);
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_Void_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				_ = cache.LoadJson();
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_String_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				_ = cache.LoadJson(@"TestFiles\FacilityIdentifierOverrides.json");
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_StringEmpty_Throw()
			{
				_ = Assert.ThrowsException<ArgumentException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadJson(string.Empty);
				  });
			}

			[TestMethod]
			public void LoadJson_StringInvalid_Throw()
			{
				_ = Assert.ThrowsException<IOException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadJson("NoFileHere.json");
				  });
			}

			[TestMethod]
			public void LoadJson_Stream_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				using (FileStream stream = new(@"TestFiles\FacilityIdentifierOverrides.json", FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					_ = cache.LoadJson(stream);
				}
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_StreamNull_Throw()
			{
				_ = Assert.ThrowsException<ArgumentNullException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadJson((Stream)null);
				  });
			}

			[TestMethod]
			public void LoadJson_StreamNoRead_Throw()
			{
				_ = Assert.ThrowsException<ArgumentException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadJson(new NoReadStream());
				  });
			}

			[TestMethod]
			public void LoadJson_TextReader_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				using (FileStream stream = new(@"TestFiles\FacilityIdentifierOverrides.json", FileMode.Open, FileAccess.Read, FileShare.Read))
				using (TextReader reader = new StreamReader(stream))
				{
					_ = cache.LoadJson(reader);
				}
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_TextReaderNull_Throw()
			{
				_ = Assert.ThrowsException<ArgumentNullException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadJson((TextReader)null);
				  });
			}

			[TestMethod]
			public void LoadJson_TextReaderInvIsEnabled_Throw()
			{
				_ = Assert.ThrowsException<IOException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  using FileStream stream = new(@"TestFiles\FacilityIdentifierOverridesInvIdentifier.json", FileMode.Open, FileAccess.Read, FileShare.Read);
					  using TextReader reader = new StreamReader(stream);
					  _ = cache.LoadJson(reader);
				  });
			}

			[TestMethod]
			public void LoadJson_InvAssemblyName_Throw()
			{
				_ = Assert.ThrowsException<FormatException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesInvAssemblyName.json");
				  });
			}

			[TestMethod]
			public void LoadJson_InvIsEnabled_Throw()
			{
				_ = Assert.ThrowsException<IOException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesInvIdentifier.json");
				  });
			}

			[TestMethod]
			public void LoadJson_NoAssemblyName_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				_ = cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesNoAssemblyName.json");
				Assert.AreEqual(1, cache.Count);
			}

			[TestMethod]
			public void LoadJson_NotBool_Throw()
			{
				_ = Assert.ThrowsException<FormatException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesNotInt.json");
				  });
			}

			[TestMethod]
			public void ParseJson_String_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				string json = File.ReadAllText(@"TestFiles\FacilityIdentifierOverrides.json");
				_ = cache.ParseJson(json);
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void ParseJson_StringNull_Throw()
			{
				_ = Assert.ThrowsException<ArgumentException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.ParseJson(null);
				  });
			}

			[TestMethod]
			public void ParseJson_InvAssemblyName_Throw()
			{
				_ = Assert.ThrowsException<FormatException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  string json = File.ReadAllText(@"TestFiles\FacilityIdentifierOverridesInvAssemblyName.json");
					  _ = cache.ParseJson(json);
				  });
			}

			[TestMethod]
			public void LoadJson_ParentObj_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				_ = cache.LoadJson(@"TestFiles\FacilityIdentifierOverridesParent.json");
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_ReadOnlySequence_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				byte[] fileBytes = File.ReadAllBytes(@"TestFiles\FacilityIdentifierOverrides.json"); // Has BOM, so we need to remove first three bytes.
				ReadOnlySequence<byte> buffer = new(fileBytes, 3, fileBytes.Length - 3);
				_ = cache.LoadJson(buffer);
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadJson_ReadOnlyMemory_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				byte[] fileBytes = File.ReadAllBytes(@"TestFiles\FacilityIdentifierOverrides.json"); // Has BOM, so we need to remove first three bytes.
				ReadOnlyMemory<byte> buffer = new(fileBytes, 3, fileBytes.Length - 3);
				_ = cache.LoadJson(buffer);
				Assert.AreEqual(7, cache.Count);
			}

#if !NET48
			[TestMethod]
			public void LoadConfigurationSection_Success()
			{
				using AssemblyFacilityOverrideCache cache = new();
				IConfiguration config = new ConfigurationBuilder().AddJsonFile(@"TestFiles\FacilityIdentifierOverridesParent.json").Build();
				_ = cache.LoadConfigurationSection(config.GetSection("facilityIdentifierOverrides"));
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadConfigurationSection_Null_Throw()
			{
				_ = Assert.ThrowsException<ArgumentNullException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  _ = cache.LoadConfigurationSection(null);
				  });
			}

			[TestMethod]
			public void LoadConfigurationSection_AssemblyNameInv_Throw()
			{
				_ = Assert.ThrowsException<FormatException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  Dictionary<string, string> modes = new()
					  {
						  ["facilityIdentifierOverrides:NerdyDuck.CodedExceptions, Version=9.x.x.x"] = "42"
					  };
					  IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(modes).Build();
					  _ = cache.LoadConfigurationSection(config.GetSection("facilityIdentifierOverrides"));
				  });
			}

			[TestMethod]
			public void LoadConfigurationSection_IsEnabledInv_Throw()
			{
				_ = Assert.ThrowsException<FormatException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  Dictionary<string, string> modes = new()
					  {
						  ["facilityIdentifierOverrides:NerdyDuck.CodedExceptions"] = "Foo"
					  };
					  IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(modes).Build();
					  _ = cache.LoadConfigurationSection(config.GetSection("facilityIdentifierOverrides"));
				  });
			}


			[TestMethod]
			public void LoadConfigurationSection_IsEnabledEmpty_Throw()
			{
				_ = Assert.ThrowsException<FormatException>(() =>
				  {
					  using AssemblyFacilityOverrideCache cache = new();
					  Dictionary<string, string> modes = new()
					  {
						  ["facilityIdentifierOverrides:NerdyDuck.CodedExceptions"] = ""
					  };
					  IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(modes).Build();
					  _ = cache.LoadConfigurationSection(config.GetSection("facilityIdentifierOverrides"));
				  });
			}
#endif
		}
	}
}
