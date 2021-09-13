#region Copyright
/*******************************************************************************
 * NerdyDuck.CodedExceptions.Configuration - Configures facility identifier
 * overrides and debug mode flags implemented in NerdyDuck.CodedExceptions.
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

#if !NET50
#pragma warning disable CS8632
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml;
#if NET50
using System.Buffers;
#endif

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Extends the <see cref="AssemblyFacilityOverrideCache" /> class with methods to easily add assembly facility identifier overrides from various sources.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class AssemblyFacilityOverrideCacheExtensions
	{
		private const string DefaultFileName = "FacilityIdentifierOverrides";

		/// <summary>
		/// Loads a list of facility identifier overrides from the default XML file and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <remarks>The default file is named 'FacilityIdentifierOverrides.xml' and must reside in the working directory of the application.</remarks>
		public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache) => ExtensionHelper.LoadXml(cache, DefaultFileName + ".xml", (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the XML file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="path">The path to the XML file containing the overrides.</param>
		public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, string path) => ExtensionHelper.LoadXml(cache, path, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified stream containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="stream">A stream containing XML-formatted data representing overrides.</param>
		public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, Stream stream) => ExtensionHelper.LoadXml(cache, stream, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified TextReader containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing overrides.</param>
		public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, TextReader reader) => ExtensionHelper.LoadXml(cache, reader, (cache, reader) => FromXmlInternal(cache, reader));

#if NET50
		/// <summary>
		/// Loads a list of facility identifier overrides from the specified sequence of bytes containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing overrides.</param>
		public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, ReadOnlySequence<byte> utf8Json) => ExtensionHelper.LoadXml(cache, utf8Json, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified sequence of bytes containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing overrides.</param>
		public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, ReadOnlyMemory<byte> utf8Json) => ExtensionHelper.LoadXml(cache, utf8Json, (cache, reader) => FromXmlInternal(cache, reader));

#endif
		/// <summary>
		/// Parses a list of facility identifier overrides from the specified string containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="content">A string containing XML-formatted data representing overrides.</param>
		public static AssemblyFacilityOverrideCache ParseXml(this AssemblyFacilityOverrideCache cache, string content) => ExtensionHelper.ParseXml(cache, content, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified XmlReader, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="XmlReader"/> containing overrides.</param>
		public static AssemblyFacilityOverrideCache FromXml(this AssemblyFacilityOverrideCache cache, XmlReader reader)
		{
			ExtensionHelper.AssertCache(cache);
			ExtensionHelper.AssertXmlReader(reader);

			FromXmlInternal(cache, reader);
			return cache;
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified XmlReader, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="XmlReader"/> containing overrides.</param>
		private static void FromXmlInternal(AssemblyFacilityOverrideCache cache, XmlReader reader)
		{
			reader.ReadStartElement(Globals.OverridesNode);
			List<AssemblyFacilityOverride> facilityOverrides = new();
			string? assemblyString, identifierString;
			AssemblyIdentity assembly;
			int identifier;

			while (!(reader.Name == Globals.OverridesNode && reader.NodeType == XmlNodeType.EndElement))
			{
				if (reader.Name == Globals.OverrideNode && reader.NodeType == XmlNodeType.Element)
				{
					assemblyString = reader.GetAttribute(Globals.AssemblyNameKey);
					if (assemblyString == null)
					{
						throw ExtensionHelper.AssemblyNameAttributeMissingException(Globals.OverrideNode);
					}

					try
					{
						assembly = new AssemblyIdentity(assemblyString);
					}
					catch (FormatException ex)
					{
						throw ExtensionHelper.InvalidAssemblyNameException(assemblyString, ex);
					}

					identifierString = reader.GetAttribute(Globals.IdentifierKey);
					if (string.IsNullOrWhiteSpace(identifierString))
					{
						throw new XmlException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_FromXml_AttributeMissing, Globals.OverrideNode, Globals.IdentifierKey));
					}
					try
					{
						identifier = XmlConvert.ToInt32(identifierString);
					}
					catch (FormatException ex)
					{
						throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_IdentifierInvalid, assemblyString), ex);
					}

					facilityOverrides.Add(new AssemblyFacilityOverride(assembly, identifier));
					reader.Skip();
				}
				else
				{
					if (!reader.Read())
					{
						break;
					}
				}
			}

			cache.AddRange(facilityOverrides);
		}
	}
}
