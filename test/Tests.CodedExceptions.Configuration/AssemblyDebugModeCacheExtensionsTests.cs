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
#region AssertCache
			[TestMethod]
			public void AssertCache_Void_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					AssemblyDebugModeCacheExtensions.LoadApplicationConfiguration(null);
				});
			}
#endregion

#region ApplicationConfiguration
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
#endregion

#region Xml

			[TestMethod]
			public void LoadXml_Void_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.LoadXml();
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadXml_String_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.LoadXml("AssemblyDebugModes.xml");
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadXml_StringEmpty_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadXml(string.Empty);
				});
			}

			[TestMethod]
			public void LoadXml_StringInvalid_Throw()
			{
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadXml("NoFileHere.xml");
				});
			}

			[TestMethod]
			public void LoadXml_Stream_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				using (FileStream stream = new FileStream("AssemblyDebugModes.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					cache.LoadXml(stream);
				}
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadXml_StreamNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadXml((Stream)null);
				});
			}

			[TestMethod]
			public void LoadXml_StreamNoRead_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadXml(new NoReadStream());
				});
			}

			[TestMethod]
			public void LoadXml_TextReader_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				using (FileStream stream = new FileStream("AssemblyDebugModes.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
				using (TextReader reader = new StreamReader(stream))
				{
					cache.LoadXml(reader);
				}
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadXml_TextReaderNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadXml((TextReader)null);
				});
			}

			[TestMethod]
			public void FromXml_XmlReaderNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.FromXml((XmlReader)null);
				});
			}

			[TestMethod]
			public void LoadXml_InvAssemblyName_Throw()
			{
				Assert.ThrowsException<XmlException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadXml(@"TestFiles\AssemblyDebugModesInvAssemblyName.xml");
				});
			}

			[TestMethod]
			public void LoadXml_InvIsEnabled_Throw()
			{
				Assert.ThrowsException<XmlException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadXml(@"TestFiles\AssemblyDebugModesInvIsEnabled.xml");
				});
			}

			[TestMethod]
			public void LoadXml_MissingAssemblyName_Throw()
			{
				Assert.ThrowsException<XmlException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadXml(@"TestFiles\AssemblyDebugModesMissingAssemblyName.xml");
				});
			}

			[TestMethod]
			public void LoadXml_NoAssemblyName_Success()
			{
				using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
				cache.LoadXml(@"TestFiles\AssemblyDebugModesNoAssemblyName.xml");
				Assert.AreEqual(1, cache.Count);
			}
#endregion

#region Json

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
				cache.LoadJson("AssemblyDebugModes.json");
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
				using (FileStream stream = new FileStream("AssemblyDebugModes.json", FileMode.Open, FileAccess.Read, FileShare.Read))
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
				using (FileStream stream = new FileStream("AssemblyDebugModes.json", FileMode.Open, FileAccess.Read, FileShare.Read))
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
				Assert.ThrowsException<IOException>(() =>
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
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyDebugModeCache cache = new AssemblyDebugModeCache();
					cache.LoadJson(@"TestFiles\AssemblyDebugModesNotBool.json");
				});
			}

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
#endregion
#if !NET48
#region LoadConfigurationSection
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
#endregion
#endif
		}
	}
}
