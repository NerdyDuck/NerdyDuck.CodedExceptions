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
		#region Constants
		private const string DefaultFileName = "AssemblyDebugModes";
		#endregion

		#region ApplicationConfiguration
		/// <summary>
		/// Loads a list of assembly debug mode settings from the application configuration file (app.config / web.config) and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <remarks>The settings are loaded from the default section 'nerdyDuck/codedExceptions'.</remarks>
		public static void LoadApplicationConfiguration(this AssemblyDebugModeCache debugModeCache)
		{
			AssertCache(debugModeCache);
			List<AssemblyDebugMode>? adm = CodedExceptionsSection.GetDebugModes();
			if (adm is not null)
			{
				debugModeCache.AddRange(adm);
			}
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified section of the application configuration file (app.config / web.config) and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="sectionName">The name of the section in the application configuration file.</param>
		public static void LoadApplicationConfiguration(this AssemblyDebugModeCache debugModeCache, string sectionName)
		{
			AssertCache(debugModeCache);
			if (string.IsNullOrWhiteSpace(sectionName))
			{
				throw new ArgumentException(TextResources.Global_FromApplicationConfiguration_NoSection, nameof(sectionName));
			}
			List<AssemblyDebugMode>? adm = CodedExceptionsSection.GetDebugModes(sectionName);
			if (adm is not null)
			{
				debugModeCache.AddRange(adm);
			}
		}
		#endregion

		#region Xml
		/// <summary>
		/// Loads a list of assembly debug mode settings from the default XML file and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <remarks>The default file is named 'AssemblyDebugModes.xml' and must reside in the working directory of the application.</remarks>
		public static void LoadXml(this AssemblyDebugModeCache debugModeCache)
		{
			AssertCache(debugModeCache);
			LoadXml(debugModeCache, DefaultFileName + ".xml");
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the XML file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="path">The path to the XML file containing the settings.</param>
		public static void LoadXml(this AssemblyDebugModeCache debugModeCache, string path)
		{
			AssertCache(debugModeCache);
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException(TextResources.Global_NoPath, nameof(path));
			}

			FileStream stream;
			try
			{
				stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (Exception ex) when (ex is IOException || ex is ArgumentException || ex is NotSupportedException || ex is SecurityException || ex is UnauthorizedAccessException)
			{
				throw new IOException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_OpenFileFailed, path), ex);
			}

			try
			{
				LoadXml(debugModeCache, stream);
			}
			finally
			{
				stream.Close();
			}
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified stream containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="stream">A stream containing XML-formatted data representing debug mode settings.</param>
		public static void LoadXml(this AssemblyDebugModeCache debugModeCache, Stream stream)
		{
			AssertCache(debugModeCache);
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(TextResources.Global_StreamNoRead, nameof(stream));
			}
			using XmlReader reader = XmlReader.Create(stream, Globals.SecureSettings);
			FromXml(debugModeCache, reader);
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified TextReader containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing debug mode settings.</param>
		public static void LoadXml(this AssemblyDebugModeCache debugModeCache, TextReader reader)
		{
			AssertCache(debugModeCache);
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}
			using XmlReader xreader = XmlReader.Create(reader, Globals.SecureSettings);
			FromXml(debugModeCache, xreader);
		}

		/// <summary>
		/// Parses a list of assembly debug mode settings from the specified string containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="content">A string containing XML-formatted data representing debug mode settings.</param>
		public static void ParseXml(this AssemblyDebugModeCache debugModeCache, string content)
		{
			AssertCache(debugModeCache);
			if (string.IsNullOrEmpty(content))
			{
				throw new ArgumentNullException(nameof(content));
			}
			using StringReader reader = new StringReader(content);
			using XmlReader xreader = XmlReader.Create(reader, Globals.SecureSettings);
			FromXml(debugModeCache, xreader);
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified XmlReader, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="XmlReader"/> containing debug mode settings.</param>
		public static void FromXml(this AssemblyDebugModeCache debugModeCache, XmlReader reader)
		{
			AssertCache(debugModeCache);
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			reader.ReadStartElement(Globals.DebugModesNode);
			List<AssemblyDebugMode> debugModes = new List<AssemblyDebugMode>();
			string assemblyString, isEnabledString;
			AssemblyIdentity assembly;
			bool isEnabled;

			while (!(reader.Name == Globals.DebugModesNode && reader.NodeType == XmlNodeType.EndElement))
			{
				if (reader.Name == Globals.DebugModeNode && reader.NodeType == XmlNodeType.Element)
				{
					assemblyString = reader.GetAttribute(Globals.AssemblyNameKey);
					if (assemblyString == null)
					{
						throw new XmlException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_FromXml_AttributeMissing, Globals.DebugModeNode, Globals.AssemblyNameKey));
					}
					try
					{
						assembly = new AssemblyIdentity(assemblyString);
					}
					catch (FormatException ex)
					{
						throw new XmlException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, assemblyString), ex);
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

			debugModeCache.AddRange(debugModes);
		}
		#endregion

		#region Json
		/// <summary>
		/// Loads a list of assembly debug mode settings from the default JSON file and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <remarks>The default file is named 'AssemblyDebugModes.json' and must reside in the working directory of the application.</remarks>
		public static void LoadJson(this AssemblyDebugModeCache debugModeCache)
		{
			AssertCache(debugModeCache);
			LoadJson(debugModeCache, DefaultFileName + ".json");
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the JSON file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="path">The path to the JSON file containing the settings.</param>
		public static void LoadJson(this AssemblyDebugModeCache debugModeCache, string path)
		{
			AssertCache(debugModeCache);
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException(TextResources.Global_NoPath, nameof(path));
			}

			FileStream stream;
			try
			{
				stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (Exception ex) when (ex is IOException || ex is ArgumentException || ex is NotSupportedException || ex is SecurityException || ex is UnauthorizedAccessException)
			{
				throw new IOException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_OpenFileFailed, path), ex);
			}

			try
			{
				LoadJson(debugModeCache, stream);
			}
			finally 
			{
				stream.Close(); 
			}
		}

#if NET50
		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing debug mode settings.</param>
		public static void LoadJson(this AssemblyDebugModeCache debugModeCache, Stream stream)
		{
			AssertCache(debugModeCache);
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(TextResources.Global_StreamNoRead, nameof(stream));
			}

			try
			{
				using JsonDocument jsonDocument = JsonDocument.Parse(stream, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
				FromJson(debugModeCache, jsonDocument.RootElement);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
		public static void LoadJson(this AssemblyDebugModeCache debugModeCache, TextReader reader)
		{
			AssertCache(debugModeCache);
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				using JsonDocument jsonDocument = JsonDocument.Parse(reader.ReadToEnd(), new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
				FromJson(debugModeCache, jsonDocument.RootElement);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="json">A string containing JSON-formatted data representing debug mode settings.</param>
		public static void ParseJson(this AssemblyDebugModeCache debugModeCache, string json)
		{
			AssertCache(debugModeCache);
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException(nameof(json));
			}

			try
			{
				using JsonDocument jsonDocument = JsonDocument.Parse(json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
				FromJson(debugModeCache, jsonDocument.RootElement);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="jsonElement">The <see cref="JsonElement"/> containing debug mode settings.</param>
		public static void FromJson(this AssemblyDebugModeCache debugModeCache, JsonElement jsonElement)
		{
			AssertCache(debugModeCache);
			if (jsonElement.ValueKind != JsonValueKind.Object)
			{
				throw new ArgumentException(TextResources.Global_FromJson_NotAnObject, nameof(jsonElement));
			}

			if (jsonElement.EnumerateObject().Count() == 1)
			{
				JsonElement jsonTemp = jsonElement.EnumerateObject().First().Value;
				if (jsonTemp.ValueKind == JsonValueKind.Object)
				{
					jsonElement = jsonTemp;
				}
			}

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
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, jsonProperty.Name), ex);
				}

				if (jsonProperty.Value.ValueKind != JsonValueKind.True && jsonProperty.Value.ValueKind != JsonValueKind.False)
				{
					throw new FormatException(TextResources.Global_FromJson_NotABool);
				}
				isEnabled = jsonProperty.Value.GetBoolean();

				debugModes.Add(new AssemblyDebugMode(assemblyName, isEnabled));
			}

			debugModeCache.AddRange(debugModes);
		}
#else
		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing debug mode settings.</param>
		public static void LoadJson(this AssemblyDebugModeCache debugModeCache, Stream stream)
		{
			AssertCache(debugModeCache);
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(TextResources.Global_StreamNoRead, nameof(stream));
			}

			JsonValue jsonValue;
			try
			{
				jsonValue = JsonValue.Load(stream);
				FromJson(debugModeCache, jsonValue);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing debug mode settings.</param>
		public static void LoadJson(this AssemblyDebugModeCache debugModeCache, TextReader reader)
		{
			AssertCache(debugModeCache);
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			JsonValue jsonValue;
			try
			{
				jsonValue = JsonValue.Load(reader);
				FromJson(debugModeCache, jsonValue);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Parses a list of assembly debug mode settingsfrom the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="content">A string containing JSON-formatted data representing debug mode settings.</param>
		public static void ParseJson(this AssemblyDebugModeCache debugModeCache, string content)
		{
			AssertCache(debugModeCache);
			if (string.IsNullOrEmpty(content))
			{
				throw new ArgumentNullException(nameof(content));
			}

			JsonValue jsonValue;
			try
			{
				jsonValue = JsonValue.Parse(content);
				FromJson(debugModeCache, jsonValue);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="jsonValue">A <see cref="JsonValue"/> containing debug mode settings.</param>
		public static void FromJson(this AssemblyDebugModeCache debugModeCache, JsonValue jsonValue)
		{
			AssertCache(debugModeCache);
			if (jsonValue == null)
			{
				throw new ArgumentNullException(nameof(jsonValue));
			}

			if (!(jsonValue is JsonObject jsonObject))
			{
				throw new ArgumentException(TextResources.Global_FromJson_NotAnObject, nameof(jsonValue));
			}

			if (jsonObject.Count == 1 && jsonObject.Values.First().JsonType == JsonType.Object)
			{
				jsonObject = (JsonObject)jsonObject.Values.First();
			}

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
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, pair.Key), ex);
				}

				if (pair.Value.JsonType != JsonType.Boolean)
				{
					throw new FormatException(TextResources.Global_FromJson_NotABool);
				}
				isEnabled = pair.Value;

				debugModes.Add(new AssemblyDebugMode(assembly, isEnabled));
			}

			debugModeCache.AddRange(debugModes);
		}
#endif
		#endregion

#if !NET472
		#region ConfigurationSection
		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified <see cref="IConfiguration"/>, and adds them to the cache.
		/// </summary>
		/// <param name="debugModeCache">The cache to add the settings to.</param>
		/// <param name="configuration">A <see cref="IConfiguration"/> containing debug mode settings.</param>
		/// <remarks>The section must be keyed by the assembly names (deserializable into an <see cref="AssemblyIdentity"/>), while the values specify the whether the debug mode is enabled or not (true/false).</remarks>
		[CLSCompliant(false)]
		public static void LoadConfigurationSection(this AssemblyDebugModeCache debugModeCache, IConfiguration configuration)
		{
			AssertCache(debugModeCache);
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

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
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, pair.Key), ex);
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

			debugModeCache.AddRange(debugModes);
		}
		#endregion
#endif

		#region Private methods
		/// <summary>
		/// Checks if the object has already been disposed.
		/// </summary>
		/// <exception cref="ObjectDisposedException">The object is already disposed.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AssertCache(AssemblyDebugModeCache debugModeCache)
		{
			if (debugModeCache == null)
			{
				throw new ArgumentNullException(nameof(debugModeCache));
			}
		}
		#endregion
	}
}
