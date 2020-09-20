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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Xml;
#if NET50
using System.Text.Json;
using System.Buffers;
#else
using System.Json;
#endif
#if !NET472
using Microsoft.Extensions.Configuration;
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
		/// Loads a list of facility identifier overrides from the application configuration file (app.config / web.config) and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <remarks>The overrides are loaded from the default section 'nerdyDuck/codedExceptions'.</remarks>
		public static void LoadApplicationConfiguration(this AssemblyFacilityOverrideCache cache)
		{
			ExtensionHelper.AssertCache(cache);
			List<AssemblyFacilityOverride>? afc = CodedExceptionsSection.GetFacilityOverrides();
			if (afc is not null)
			{
				cache.AddRange(afc);
			}
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified section of the application configuration file (app.config / web.config) and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="sectionName">The name of the section in the application configuration file.</param>
		public static void LoadApplicationConfiguration(this AssemblyFacilityOverrideCache cache, string sectionName)
		{
			ExtensionHelper.AssertCache(cache);
			ExtensionHelper.AssertSectionName(sectionName);
			List<AssemblyFacilityOverride>? afc = CodedExceptionsSection.GetFacilityOverrides(sectionName);
			if (afc is not null)
			{
				cache.AddRange(afc);
			}
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the default XML file and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <remarks>The default file is named 'FacilityIdentifierOverrides.xml' and must reside in the working directory of the application.</remarks>
		public static void LoadXml(this AssemblyFacilityOverrideCache cache) => ExtensionHelper.LoadXml(cache, DefaultFileName + ".xml", (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the XML file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="path">The path to the XML file containing the overrides.</param>
		public static void LoadXml(this AssemblyFacilityOverrideCache cache, string path) => ExtensionHelper.LoadXml(cache, path, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified stream containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="stream">A stream containing XML-formatted data representing overrides.</param>
		public static void LoadXml(this AssemblyFacilityOverrideCache cache, Stream stream) => ExtensionHelper.LoadXml(cache, stream, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified TextReader containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing overrides.</param>
		public static void LoadXml(this AssemblyFacilityOverrideCache cache, TextReader reader) => ExtensionHelper.LoadXml(cache, reader, (cache, reader) => FromXmlInternal(cache, reader));

#if NET50
		/// <summary>
		/// Loads a list of facility identifier overrides from the specified sequence of bytes containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing overrides.</param>
		public static void LoadXml(this AssemblyFacilityOverrideCache cache, ReadOnlySequence<byte> utf8Json) => ExtensionHelper.LoadXml(cache, utf8Json, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified sequence of bytes containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing overrides.</param>
		public static void LoadXml(this AssemblyFacilityOverrideCache cache, ReadOnlyMemory<byte> utf8Json) => ExtensionHelper.LoadXml(cache, utf8Json, (cache, reader) => FromXmlInternal(cache, reader));

#endif
		/// <summary>
		/// Parses a list of facility identifier overrides from the specified string containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="content">A string containing XML-formatted data representing overrides.</param>
		public static void ParseXml(this AssemblyFacilityOverrideCache cache, string content) => ExtensionHelper.ParseXml(cache, content, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified XmlReader, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="XmlReader"/> containing overrides.</param>
		public static void FromXml(this AssemblyFacilityOverrideCache cache, XmlReader reader)
		{
			ExtensionHelper.AssertCache(cache);
			ExtensionHelper.AssertXmlReader(reader);

			FromXmlInternal(cache, reader);
		}


		/// <summary>
		/// Loads a list of facility identifier overrides from the specified XmlReader, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="XmlReader"/> containing overrides.</param>
		private static void FromXmlInternal(AssemblyFacilityOverrideCache cache, XmlReader reader)
		{
			reader.ReadStartElement(Globals.OverridesNode);
			List<AssemblyFacilityOverride> facilityOverrides = new List<AssemblyFacilityOverride>();
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
						throw IdentifierInvalidException(assemblyString, ex);
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

#if NET50
		/// <summary>
		/// Loads a list of facility identifier overrides from the default JSON file and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <remarks>The default file is named 'FacilityIdentifierOverrides.json' and must reside in the working directory of the application.</remarks>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache) => ExtensionHelper.LoadJson(cache, DefaultFileName + ".json", (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of facility identifier overrides from the JSON file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="path">The path to the JSON file containing the overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache, string path) => ExtensionHelper.LoadJson(cache, path, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache, Stream stream) => ExtensionHelper.LoadJson(cache, stream, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache, TextReader reader) => ExtensionHelper.LoadJson(cache, reader, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing overrides.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache, ReadOnlySequence<byte> utf8Json) => ExtensionHelper.LoadJson(cache, utf8Json, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing overrides.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache, ReadOnlyMemory<byte> utf8Json) => ExtensionHelper.LoadJson(cache, utf8Json, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Parses a list of facility identifier overrides from the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="content">A string containing JSON-formatted data representing overrides.</param>
		public static void ParseJson(this AssemblyFacilityOverrideCache cache, string content) => ExtensionHelper.ParseJson(cache, content, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="jsonElement">A <see cref="JsonElement"/> containing overrides.</param>
		public static void FromJson(this AssemblyFacilityOverrideCache cache, JsonElement jsonElement)
		{
			ExtensionHelper.AssertCache(cache);
			FromJsonInternal(cache, jsonElement);
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="jsonElement">A <see cref="JsonElement"/> containing overrides.</param>
		private static void FromJsonInternal(AssemblyFacilityOverrideCache cache, JsonElement jsonElement)
		{
			ExtensionHelper.AssertJsonValueKindObject(jsonElement);

			jsonElement = ExtensionHelper.GetParentElement(jsonElement);

			List<AssemblyFacilityOverride> facilityOverrides = new List<AssemblyFacilityOverride>();
			AssemblyIdentity assembly;
			int identifier;
			
			foreach (JsonProperty jsonProperty in jsonElement.EnumerateObject())
			{
				try
				{
					assembly = new AssemblyIdentity(jsonProperty.Name);
				}
				catch (FormatException ex)
				{
					throw ExtensionHelper.InvalidAssemblyNameException(jsonProperty.Name, ex);
				}

				if (jsonProperty.Value.ValueKind != JsonValueKind.Number)
				{
					throw NotANumberException();
				}
				identifier = jsonProperty.Value.GetInt32();

				facilityOverrides.Add(new AssemblyFacilityOverride(assembly, identifier));
			}

			cache.AddRange(facilityOverrides);
		}
#else
		/// <summary>
		/// Loads a list of facility identifier overrides from the default JSON file and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <remarks>The default file is named 'FacilityIdentifierOverrides.json' and must reside in the working directory of the application.</remarks>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache) => ExtensionHelper.LoadJson(cache, DefaultFileName + ".json", (cache, jsonObject) => FromJsonInternal(cache, jsonObject));

		/// <summary>
		/// Loads a list of facility identifier overrides from the JSON file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="path">The path to the JSON file containing the overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache, string path) => ExtensionHelper.LoadJson(cache, path, (cache, jsonObject) => FromJsonInternal(cache, jsonObject));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache, Stream stream) => ExtensionHelper.LoadJson(cache, stream, (cache, jsonObject) => FromJsonInternal(cache, jsonObject));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache cache, TextReader reader) => ExtensionHelper.LoadJson(cache, reader, (cache, jsonObject) => FromJsonInternal(cache, jsonObject));

		/// <summary>
		/// Parses a list of facility identifier overrides from the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="content">A string containing JSON-formatted data representing debug mode settings.</param>
		public static void ParseJson(this AssemblyFacilityOverrideCache cache, string content) => ExtensionHelper.ParseJson(cache, content, (cache, jsonObject) => FromJsonInternal(cache, jsonObject));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="jsonValue">A <see cref="JsonValue"/> containing overrides.</param>
		public static void FromJson(this AssemblyFacilityOverrideCache cache, JsonValue jsonValue)
		{
			ExtensionHelper.AssertCache(cache);
			ExtensionHelper.AssertJsonValue(jsonValue);

			FromJsonInternal(cache, jsonValue);
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="jsonValue">A <see cref="JsonValue"/> containing overrides.</param>
		private static void FromJsonInternal(AssemblyFacilityOverrideCache cache, JsonValue jsonValue)
		{
			JsonObject jsonObject = ExtensionHelper.AssertJsonObject(jsonValue);

			jsonObject = ExtensionHelper.GetParentElement(jsonObject);

			List<AssemblyFacilityOverride> facilityOverrides = new List<AssemblyFacilityOverride>();
			AssemblyIdentity assembly;
			int identifier;
			foreach (KeyValuePair<string, JsonValue> pair in jsonObject)
			{
				try
				{
					assembly = new AssemblyIdentity(pair.Key);
				}
				catch (FormatException ex)
				{
					throw ExtensionHelper.InvalidAssemblyNameException(pair.Key, ex);
				}

				if (pair.Value.JsonType != JsonType.Number)
				{
					throw NotANumberException();
				}
				identifier = pair.Value;

				facilityOverrides.Add(new AssemblyFacilityOverride(assembly, identifier));
			}

			cache.AddRange(facilityOverrides);
		}
#endif

#if !NET472
		/// <summary>
		/// Loads a list of facility identifier overrides from the specified <see cref="IConfiguration"/>, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="configuration">A <see cref="IConfiguration"/> containing overrides.</param>
		/// <remarks>The section must be keyed by the assembly names (deserializable into an <see cref="AssemblyIdentity"/>), while the values specify the identifier overrides.</remarks>
		[CLSCompliant(false)]
		public static void LoadConfigurationSection(this AssemblyFacilityOverrideCache cache, IConfiguration configuration)
		{
			ExtensionHelper.AssertCache(cache);
			ExtensionHelper.AssertConfiguration(configuration);

			List<AssemblyFacilityOverride> facilityOverrides = new List<AssemblyFacilityOverride>();
			AssemblyIdentity assembly;
			int identifier;

			foreach (KeyValuePair<string, string> pair in configuration.AsEnumerable(true))
			{
				try
				{
					assembly = new AssemblyIdentity(pair.Key);
				}
				catch (FormatException ex)
				{
					throw ExtensionHelper.InvalidAssemblyNameException(pair.Key, ex);
				}

				if (string.IsNullOrWhiteSpace(pair.Value))
				{
					throw new FormatException(TextResources.Global_IdentifierEmpty);
				}
				try
				{
					identifier = XmlConvert.ToInt32(pair.Value);
				}
				catch (FormatException ex)
				{
					throw IdentifierInvalidException(pair.Key, ex);
				}

				facilityOverrides.Add(new AssemblyFacilityOverride(assembly, identifier));
			}

			cache.AddRange(facilityOverrides);
		}
#endif

		private static FormatException IdentifierInvalidException(string assemblyName, Exception ex) => new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_IdentifierInvalid, assemblyName), ex);

		private static FormatException NotANumberException() => new FormatException(TextResources.Global_FromJson_NotANumber);
	}
}
