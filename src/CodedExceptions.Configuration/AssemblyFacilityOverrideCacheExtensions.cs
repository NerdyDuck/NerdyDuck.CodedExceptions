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
	/// Extends the <see cref="AssemblyFacilityOverrideCache" /> class with methods to easily add assembly facility identifier overrides from various sources.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class AssemblyFacilityOverrideCacheExtensions
	{
		#region Constants
		private const string DefaultFileName = "FacilityIdentifierOverrides";
		#endregion

		#region ApplicationConfiguration
		/// <summary>
		/// Loads a list of facility identifier overrides from the application configuration file (app.config / web.config) and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <remarks>The overrides are loaded from the default section 'nerdyDuck/codedExceptions'.</remarks>
		public static void LoadApplicationConfiguration(this AssemblyFacilityOverrideCache overrideCache)
		{
			AssertCache(overrideCache);
			List<AssemblyFacilityOverride>? afc = CodedExceptionsSection.GetFacilityOverrides();
			if (afc is not null)
			{
				overrideCache.AddRange(afc);
			}
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified section of the application configuration file (app.config / web.config) and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="sectionName">The name of the section in the application configuration file.</param>
		public static void LoadApplicationConfiguration(this AssemblyFacilityOverrideCache overrideCache, string sectionName)
		{
			AssertCache(overrideCache);
			if (string.IsNullOrWhiteSpace(sectionName))
			{
				throw new ArgumentException(TextResources.Global_FromApplicationConfiguration_NoSection, nameof(sectionName));
			}
			List<AssemblyFacilityOverride>? afc = CodedExceptionsSection.GetFacilityOverrides(sectionName);
			if (afc is not null)
			{
				overrideCache.AddRange(afc);
			}
		}
		#endregion

		#region Xml
		/// <summary>
		/// Loads a list of facility identifier overrides from the default XML file and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <remarks>The default file is named 'FacilityIdentifierOverrides.xml' and must reside in the working directory of the application.</remarks>
		public static void LoadXml(this AssemblyFacilityOverrideCache overrideCache)
		{
			AssertCache(overrideCache);
			LoadXml(overrideCache, DefaultFileName + ".xml");
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the XML file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="path">The path to the XML file containing the overrides.</param>
		public static void LoadXml(this AssemblyFacilityOverrideCache overrideCache, string path)
		{
			AssertCache(overrideCache);
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
				LoadXml(overrideCache, stream);
			}
			finally
			{
				stream.Close();
			}
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified stream containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="stream">A stream containing XML-formatted data representing overrides.</param>
		public static void LoadXml(this AssemblyFacilityOverrideCache overrideCache, Stream stream)
		{
			AssertCache(overrideCache);
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(TextResources.Global_StreamNoRead, nameof(stream));
			}
			using XmlReader reader = XmlReader.Create(stream, Globals.SecureSettings);
			FromXml(overrideCache, reader);
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified TextReader containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing overrides.</param>
		public static void LoadXml(this AssemblyFacilityOverrideCache overrideCache, TextReader reader)
		{
			AssertCache(overrideCache);
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}
			using XmlReader xreader = XmlReader.Create(reader, Globals.SecureSettings);
			FromXml(overrideCache, xreader);
		}

		/// <summary>
		/// Parses a list of facility identifier overrides from the specified string containing XML data, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="content">A string containing XML-formatted data representing overrides.</param>
		public static void ParseXml(this AssemblyFacilityOverrideCache overrideCache, string content)
		{
			AssertCache(overrideCache);
			if (string.IsNullOrEmpty(content))
			{
				throw new ArgumentNullException(nameof(content));
			}
			using StringReader reader = new StringReader(content);
			using XmlReader xreader = XmlReader.Create(reader, Globals.SecureSettings);
			FromXml(overrideCache, xreader);
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified XmlReader, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="XmlReader"/> containing overrides.</param>
		public static void FromXml(this AssemblyFacilityOverrideCache overrideCache, XmlReader reader)
		{
			AssertCache(overrideCache);
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			reader.ReadStartElement(Globals.OverridesNode);
			List<AssemblyFacilityOverride> facilityOverrides = new List<AssemblyFacilityOverride>();
			string assemblyString, identifierString;
			AssemblyIdentity assembly;
			int identifier;

			while (!(reader.Name == Globals.OverridesNode && reader.NodeType == XmlNodeType.EndElement))
			{
				if (reader.Name == Globals.OverrideNode && reader.NodeType == XmlNodeType.Element)
				{
					assemblyString = reader.GetAttribute(Globals.AssemblyNameKey);
					if (assemblyString == null)
					{
						throw new XmlException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_FromXml_AttributeMissing, Globals.OverrideNode, Globals.AssemblyNameKey));
					}
					try
					{
						assembly = new AssemblyIdentity(assemblyString);
					}
					catch (FormatException ex)
					{
						throw new XmlException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, assemblyString), ex);
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
						throw new XmlException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_IdentifierInvalid, assemblyString), ex);
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

			overrideCache.AddRange(facilityOverrides);
		}
		#endregion

		#region Json
		/// <summary>
		/// Loads a list of facility identifier overrides from the default JSON file and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <remarks>The default file is named 'FacilityIdentifierOverrides.json' and must reside in the working directory of the application.</remarks>
		public static void LoadJson(this AssemblyFacilityOverrideCache overrideCache)
		{
			AssertCache(overrideCache);
			LoadJson(overrideCache, DefaultFileName + ".json");
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the JSON file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="path">The path to the JSON file containing the overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache overrideCache, string path)
		{
			AssertCache(overrideCache);
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
				LoadJson(overrideCache, stream);
			}
			finally
			{
				stream.Close();
			}
		}

#if NET50
		/// <summary>
		/// Loads a list of facility identifier overrides from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache overrideCache, Stream stream)
		{
			AssertCache(overrideCache);
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
				FromJson(overrideCache, jsonDocument.RootElement);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache overrideCache, TextReader reader)
		{
			AssertCache(overrideCache);
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				using JsonDocument jsonDocument = JsonDocument.Parse(reader.ReadToEnd(), new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
				FromJson(overrideCache, jsonDocument.RootElement);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Parses a list of facility identifier overrides from the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="content">A string containing JSON-formatted data representing debug mode settings.</param>
		public static void ParseJson(this AssemblyFacilityOverrideCache overrideCache, string content)
		{
			AssertCache(overrideCache);
			if (string.IsNullOrEmpty(content))
			{
				throw new ArgumentNullException(nameof(content));
			}

			try
			{
				using JsonDocument jsonDocument = JsonDocument.Parse(content, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
				FromJson(overrideCache, jsonDocument.RootElement);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="jsonElement">A <see cref="JsonElement"/> containing overrides.</param>
		public static void FromJson(this AssemblyFacilityOverrideCache overrideCache, JsonElement jsonElement)
		{
			AssertCache(overrideCache);

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
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, jsonProperty.Name), ex);
				}

				if (jsonProperty.Value.ValueKind != JsonValueKind.Number)
				{
					throw new FormatException(TextResources.Global_FromJson_NotANumber);
				}
				identifier = jsonProperty.Value.GetInt32();

				facilityOverrides.Add(new AssemblyFacilityOverride(assembly, identifier));
			}

			overrideCache.AddRange(facilityOverrides);
		}
#else
		/// <summary>
		/// Loads a list of facility identifier overrides from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache overrideCache, Stream stream)
		{
			AssertCache(overrideCache);
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
				FromJson(overrideCache, jsonValue);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
		public static void LoadJson(this AssemblyFacilityOverrideCache overrideCache, TextReader reader)
		{
			AssertCache(overrideCache);
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			JsonValue jsonValue;
			try
			{
				jsonValue = JsonValue.Load(reader);
				FromJson(overrideCache, jsonValue);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Parses a list of facility identifier overrides from the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="content">A string containing JSON-formatted data representing debug mode settings.</param>
		public static void ParseJson(this AssemblyFacilityOverrideCache overrideCache, string content)
		{
			AssertCache(overrideCache);
			if (string.IsNullOrEmpty(content))
			{
				throw new ArgumentNullException(nameof(content));
			}

			JsonValue jsonValue;
			try
			{
				jsonValue = JsonValue.Parse(content);
				FromJson(overrideCache, jsonValue);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="jsonValue">A <see cref="JsonValue"/> containing overrides.</param>
		public static void FromJson(this AssemblyFacilityOverrideCache overrideCache, JsonValue jsonValue)
		{
			AssertCache(overrideCache);
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
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, pair.Key), ex);
				}

				if (pair.Value.JsonType != JsonType.Number)
				{
					throw new FormatException(TextResources.Global_FromJson_NotANumber);
				}
				identifier = pair.Value;

				facilityOverrides.Add(new AssemblyFacilityOverride(assembly, identifier));
			}

			overrideCache.AddRange(facilityOverrides);
		}
#endif
		#endregion

#if !NET472
		#region ConfigurationSection
		/// <summary>
		/// Loads a list of facility identifier overrides from the specified <see cref="IConfiguration"/>, and adds them to the cache.
		/// </summary>
		/// <param name="overrideCache">The cache to add the overrides to.</param>
		/// <param name="configuration">A <see cref="IConfiguration"/> containing overrides.</param>
		/// <remarks>The section must be keyed by the assembly names (deserializable into an <see cref="AssemblyIdentity"/>), while the values specify the identifier overrides.</remarks>
		[CLSCompliant(false)]
		public static void LoadConfigurationSection(this AssemblyFacilityOverrideCache overrideCache, IConfiguration configuration)
		{
			AssertCache(overrideCache);
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

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
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, pair.Key), ex);
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
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_IdentifierInvalid, pair.Key), ex);
				}

				facilityOverrides.Add(new AssemblyFacilityOverride(assembly, identifier));
			}

			overrideCache.AddRange(facilityOverrides);
		}
		#endregion
#endif

		#region Private methods
		/// <summary>
		/// Checks if the object has already been disposed.
		/// </summary>
		/// <exception cref="ObjectDisposedException">The object is already disposed.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AssertCache(AssemblyFacilityOverrideCache? overrideCache)
		{
			if (overrideCache == null)
			{
				throw new ArgumentNullException(nameof(overrideCache));
			}
		}
		#endregion
	}
}
