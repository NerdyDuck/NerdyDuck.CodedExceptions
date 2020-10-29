﻿#region Copyright
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
#endif
using System.Xml;
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
			public void LoadXml_Void_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				cache.LoadXml();
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadXml_String_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				cache.LoadXml(@"TestFiles\FacilityIdentifierOverrides.xml");
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadXml_StringEmpty_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadXml(string.Empty);
				});
			}

			[TestMethod]
			public void LoadXml_StringInvalid_Throw()
			{
				Assert.ThrowsException<IOException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadXml("NoFileHere.xml");
				});
			}

			[TestMethod]
			public void LoadXml_Stream_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				using (FileStream stream = new FileStream(@"TestFiles\FacilityIdentifierOverrides.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
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
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadXml((Stream)null);
				});
			}

			[TestMethod]
			public void LoadXml_StreamNoRead_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadXml(new NoReadStream());
				});
			}

			[TestMethod]
			public void LoadXml_TextReader_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				using (FileStream stream = new FileStream(@"TestFiles\FacilityIdentifierOverrides.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
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
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadXml((TextReader)null);
				});
			}

			[TestMethod]
			public void FromXml_XmlReader_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				using (FileStream stream = new FileStream(@"TestFiles\FacilityIdentifierOverrides.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
				using (XmlReader reader = XmlReader.Create(stream))
				{
					cache.FromXml(reader);
				}
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void FromXml_XmlReaderNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.FromXml((XmlReader)null);
				});
			}

			[TestMethod]
			public void ParseXml_String_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				string xml = File.ReadAllText(@"TestFiles\FacilityIdentifierOverrides.xml");
				cache.ParseXml(xml);
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void ParseXml_StringNull_Throw()
			{
				Assert.ThrowsException<ArgumentException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.ParseXml(null);
				});
			}

			[TestMethod]
			public void LoadXml_InvAssemblyName_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadXml(@"TestFiles\FacilityIdentifierOverridesInvAssemblyName.xml");
				});
			}

			[TestMethod]
			public void LoadXml_InvIsEnabled_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadXml(@"TestFiles\FacilityIdentifierOverridesInvIdentifier.xml");
				});
			}

			[TestMethod]
			public void LoadXml_MissingAssemblyName_Throw()
			{
				Assert.ThrowsException<XmlException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadXml(@"TestFiles\FacilityIdentifierOverridesMissingAssemblyName.xml");
				});
			}

			[TestMethod]
			public void LoadXml_MissingIdentifier_Throw()
			{
				Assert.ThrowsException<XmlException>(() =>
				{
					using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
					cache.LoadXml(@"TestFiles\FacilityIdentifierOverridesNoIdentifier.xml");
				});
			}

			[TestMethod]
			public void LoadXml_NoAssemblyName_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				cache.LoadXml(@"TestFiles\FacilityIdentifierOverridesNoAssemblyName.xml");
				Assert.AreEqual(1, cache.Count);
			}

#if NET50
			[TestMethod]
			public void LoadXml_ReadOnlySequence_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>(File.ReadAllBytes(@"TestFiles\FacilityIdentifierOverrides.xml"));
				cache.LoadXml(buffer);
				Assert.AreEqual(7, cache.Count);
			}

			[TestMethod]
			public void LoadXml_ReadOnlyMemory_Success()
			{
				using AssemblyFacilityOverrideCache cache = new AssemblyFacilityOverrideCache();
				ReadOnlyMemory<byte> buffer = new ReadOnlyMemory<byte>(File.ReadAllBytes(@"TestFiles\FacilityIdentifierOverrides.xml"));
				cache.LoadXml(buffer);
				Assert.AreEqual(7, cache.Count);
			}
#endif
		}
	}
}
