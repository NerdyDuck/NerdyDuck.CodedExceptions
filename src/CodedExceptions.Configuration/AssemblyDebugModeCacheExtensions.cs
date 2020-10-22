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
	/// Extends the <see cref="AssemblyDebugModeCache" /> class with methods to easily add debug mode settings from various sources.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class AssemblyDebugModeCacheExtensions
	{
		private const string DefaultFileName = "AssemblyDebugModes";

		/// <summary>
		/// Loads a list of assembly debug mode settings from the application configuration file (app.config / web.config) and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <remarks>The settings are loaded from the default section 'nerdyDuck/codedExceptions'.</remarks>
		public static void LoadApplicationConfiguration(this AssemblyDebugModeCache cache)
		{
			ExtensionHelper.AssertCache(cache);
			List<AssemblyDebugMode>? adm = CodedExceptionsSection.GetDebugModes();
			if (adm is not null)
			{
				cache.AddRange(adm);
			}
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified section of the application configuration file (app.config / web.config) and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="sectionName">The name of the section in the application configuration file.</param>
		public static void LoadApplicationConfiguration(this AssemblyDebugModeCache cache, string sectionName)
		{
			ExtensionHelper.AssertCache(cache);
			ExtensionHelper.AssertSectionName(sectionName);
			List<AssemblyDebugMode>? adm = CodedExceptionsSection.GetDebugModes(sectionName);
			if (adm is not null)
			{
				cache.AddRange(adm);
			}
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the default XML file and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <remarks>The default file is named 'AssemblyDebugModes.xml' and must reside in the working directory of the application.</remarks>
		public static void LoadXml(this AssemblyDebugModeCache cache) => ExtensionHelper.LoadXml(cache, DefaultFileName + ".xml", (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the XML file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="path">The path to the XML file containing the settings.</param>
		public static void LoadXml(this AssemblyDebugModeCache cache, string path) => ExtensionHelper.LoadXml(cache, path, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified stream containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="stream">A stream containing XML-formatted data representing debug mode settings.</param>
		public static void LoadXml(this AssemblyDebugModeCache cache, Stream stream) => ExtensionHelper.LoadXml(cache, stream, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified TextReader containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing debug mode settings.</param>
		public static void LoadXml(this AssemblyDebugModeCache cache, TextReader reader) => ExtensionHelper.LoadXml(cache, reader, (cache, reader) => FromXmlInternal(cache, reader));

#if NET50
		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing debug mode settings.</param>
		public static void LoadXml(this AssemblyDebugModeCache cache, ReadOnlySequence<byte> utf8Json) => ExtensionHelper.LoadXml(cache, utf8Json, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing debug mode settings.</param>
		public static void LoadXml(this AssemblyDebugModeCache cache, ReadOnlyMemory<byte> utf8Json) => ExtensionHelper.LoadXml(cache, utf8Json, (cache, reader) => FromXmlInternal(cache, reader));
#endif

		/// <summary>
		/// Parses a list of assembly debug mode settings from the specified string containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="content">A string containing XML-formatted data representing debug mode settings.</param>
		public static void ParseXml(this AssemblyDebugModeCache cache, string content) => ExtensionHelper.ParseXml(cache, content, (cache, reader) => FromXmlInternal(cache, reader));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified XmlReader, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="XmlReader"/> containing debug mode settings.</param>
		public static void FromXml(this AssemblyDebugModeCache cache, XmlReader reader)
		{
			ExtensionHelper.AssertCache(cache);
			ExtensionHelper.AssertXmlReader(reader);

			FromXmlInternal(cache, reader);
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified XmlReader, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="XmlReader"/> containing debug mode settings.</param>
		private static void FromXmlInternal(this AssemblyDebugModeCache cache, XmlReader reader)
		{
			reader.ReadStartElement(Globals.DebugModesNode);
			List<AssemblyDebugMode> debugModes = new List<AssemblyDebugMode>();
			string? assemblyString, isEnabledString;
			AssemblyIdentity assembly;
			bool isEnabled;

			while (!(reader.Name == Globals.DebugModesNode && reader.NodeType == XmlNodeType.EndElement))
			{
				if (reader.Name == Globals.DebugModeNode && reader.NodeType == XmlNodeType.Element)
				{
					assemblyString = reader.GetAttribute(Globals.AssemblyNameKey);
					if (assemblyString == null)
					{
						throw ExtensionHelper.AssemblyNameAttributeMissingException(Globals.DebugModeNode);
					}
					try
					{
						assembly = new AssemblyIdentity(assemblyString);
					}
					catch (FormatException ex)
					{
						throw ExtensionHelper.InvalidAssemblyNameException(assemblyString, ex);
					}

					isEnabledString = reader.GetAttribute(Globals.IsEnabledKey);
					if (string.IsNullOrWhiteSpace(isEnabledString))
					{
						isEnabled = true;
					}
					else
					{
						try
						{
							isEnabled = XmlConvert.ToBoolean(isEnabledString);
						}
						catch (FormatException ex)
						{
							throw new XmlException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_IsEnabledInvalid, assemblyString), ex);
						}
					}

					debugModes.Add(new AssemblyDebugMode(assembly, isEnabled));
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

			cache.AddRange(debugModes);
		}

#if NET50
		/// <summary>
		/// Loads a list of assembly debug mode settings from the default JSON file and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <remarks>The default file is named 'AssemblyDebugModes.json' and must reside in the working directory of the application.</remarks>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The file could not be opened or read.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache) => ExtensionHelper.LoadJson(cache, DefaultFileName + ".json", (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the JSON file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="path">The path to the JSON file containing the settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="path"/> is <see langword="null"/> or white-space.</exception>
		/// <exception cref="IOException">The file could not be opened or read.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache, string path) => ExtensionHelper.LoadJson(cache, path, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="stream"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="stream"/> is is not readable.</exception>
		/// <exception cref="IOException">The stream data could not be read.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache, Stream stream) => ExtensionHelper.LoadJson(cache, stream, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="reader"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The stream data could not be read.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache, TextReader reader) => ExtensionHelper.LoadJson(cache, reader ?? throw new ArgumentNullException(nameof(reader)), (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache, ReadOnlySequence<byte> utf8Json) => ExtensionHelper.LoadJson(cache, utf8Json, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache, ReadOnlyMemory<byte> utf8Json) => ExtensionHelper.LoadJson(cache, utf8Json, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="content">A string containing JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/> or <paramref name="content"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="IOException">The stream data could not be read.</exception>
		public static void ParseJson(this AssemblyDebugModeCache cache, string content) => ExtensionHelper.ParseJson(cache, content, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="jsonElement">The <see cref="JsonElement"/> containing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="jsonElement"/> is not a JSON object.</exception>
		/// <exception cref="FormatException">The JSON data is invalid.</exception>
		public static void FromJson(this AssemblyDebugModeCache cache, JsonElement jsonElement)
		{
			ExtensionHelper.AssertCache(cache);
			FromJsonInternal(cache, jsonElement);
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="jsonElement">The <see cref="JsonElement"/> containing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="jsonElement"/> is not a JSON object.</exception>
		/// <exception cref="FormatException">The JSON data is invalid.</exception>
		private static void FromJsonInternal(this AssemblyDebugModeCache cache, JsonElement jsonElement)
		{
			ExtensionHelper.AssertJsonValueKindObject(jsonElement);

			jsonElement = ExtensionHelper.GetParentElement(jsonElement);

			List<AssemblyDebugMode> debugModes = new List<AssemblyDebugMode>();
			AssemblyIdentity assemblyName;
			bool isEnabled;

			foreach (JsonProperty jsonProperty in jsonElement.EnumerateObject())
			{
				try
				{
					assemblyName = new AssemblyIdentity(jsonProperty.Name);
				}
				catch (FormatException ex)
				{
					throw ExtensionHelper.InvalidAssemblyNameException(jsonProperty.Name, ex);
				}

				if (jsonProperty.Value.ValueKind != JsonValueKind.True && jsonProperty.Value.ValueKind != JsonValueKind.False)
				{
					throw NotABoolException();
				}
				isEnabled = jsonProperty.Value.GetBoolean();

				debugModes.Add(new AssemblyDebugMode(assemblyName, isEnabled));
			}

			cache.AddRange(debugModes);
		}
#else
		/// <summary>
		/// Loads a list of assembly debug mode settings from the default JSON file and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <remarks>The default file is named 'AssemblyDebugModes.json' and must reside in the working directory of the application.</remarks>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The file could not be opened or read.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache) => ExtensionHelper.LoadJson(cache, DefaultFileName + ".json", (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the JSON file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="path">The path to the JSON file containing the settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="path"/> is <see langword="null"/> or white-space.</exception>
		/// <exception cref="IOException">The file could not be opened or read.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache, string path) => ExtensionHelper.LoadJson(cache, path, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="stream"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="stream"/> is is not readable.</exception>
		/// <exception cref="IOException">The stream data could not be read.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache, Stream stream) => ExtensionHelper.LoadJson(cache, stream, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="reader"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The stream data could not be read.</exception>
		public static void LoadJson(this AssemblyDebugModeCache cache, TextReader reader) => ExtensionHelper.LoadJson(cache, reader, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="content">A string containing JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/> or <paramref name="content"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="IOException">The stream data could not be read.</exception>
		public static void ParseJson(this AssemblyDebugModeCache cache, string content) => ExtensionHelper.ParseJson(cache, content, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="jsonValue">The <see cref="JsonValue"/> containing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="jsonValue"/> is not a JSON object.</exception>
		/// <exception cref="FormatException">The JSON data is invalid.</exception>
		public static void FromJson(this AssemblyDebugModeCache cache, JsonValue jsonValue)
		{
			ExtensionHelper.AssertCache(cache);
			ExtensionHelper.AssertJsonValue(jsonValue);
			FromJsonInternal(cache, jsonValue);
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="jsonValue">A <see cref="JsonValue"/> containing debug mode settings.</param>
		private static void FromJsonInternal(this AssemblyDebugModeCache cache, JsonValue jsonValue)
		{
			JsonObject jsonObject = ExtensionHelper.AssertJsonObject(jsonValue);

			jsonObject = ExtensionHelper.GetParentElement(jsonObject);

			List<AssemblyDebugMode> debugModes = new List<AssemblyDebugMode>();
			AssemblyIdentity assembly;
			bool isEnabled;
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

				if (pair.Value.JsonType != JsonType.Boolean)
				{
					throw NotABoolException();
				}
				isEnabled = pair.Value;

				debugModes.Add(new AssemblyDebugMode(assembly, isEnabled));
			}

			cache.AddRange(debugModes);
		}
#endif

#if !NET472
		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified <see cref="IConfiguration"/>, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="configuration">A <see cref="IConfiguration"/> containing debug mode settings.</param>
		/// <remarks>The section must be keyed by the assembly names (deserializable into an <see cref="AssemblyIdentity"/>), while the values specify the whether the debug mode is enabled or not (true/false).</remarks>
		[CLSCompliant(false)]
		public static void LoadConfigurationSection(this AssemblyDebugModeCache cache, IConfiguration configuration)
		{
			ExtensionHelper.AssertCache(cache);
			ExtensionHelper.AssertConfiguration(configuration);

			List<AssemblyDebugMode> debugModes = new List<AssemblyDebugMode>();
			AssemblyIdentity assembly;
			bool isEnabled;

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
					isEnabled = Convert.ToBoolean(pair.Value, CultureInfo.InvariantCulture);
				}
				catch (FormatException ex)
				{
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_IsEnabledInvalid, pair.Key), ex);
				}

				debugModes.Add(new AssemblyDebugMode(assembly, isEnabled));
			}

			cache.AddRange(debugModes);
		}
#endif

		private static FormatException NotABoolException() => new FormatException(TextResources.Global_FromJson_NotABool);

	}
}
