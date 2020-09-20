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
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Xml;
using System.Linq;
#if NET50
using System.Buffers;
using System.Text.Json;
#else
using System.Json;
#endif

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Provides methods to read configurations for <see cref="AssemblyDebugModeCache" /> and <see cref="AssemblyFacilityOverrideCache" /> from various sources.
	/// </summary>
	internal static class ExtensionHelper
	{
		internal static readonly XmlReaderSettings SecureSettings = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true, CloseInput = false, DtdProcessing = DtdProcessing.Prohibit, XmlResolver = null };

		/// <summary>
		/// Loads configuration data into a cache from the specified file path, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="path">The path to the XML file containing the configuration data.</param>
		/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
		internal static void LoadXml<T>(T cache, string path, Action<T, XmlReader> parser) where T : class
		{
			AssertCache(cache);
			AssertPath(path);

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
				LoadXml(cache, stream, parser);
			}
			finally
			{
				stream.Close();
			}
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified stream, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="stream">A stream containing XML-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
		internal static void LoadXml<T>(T cache, Stream stream, Action<T, XmlReader> parser) where T : class
		{
			AssertCache(cache);
			AssertStream(stream);

			using XmlReader reader = XmlReader.Create(stream, SecureSettings);
			parser(cache, reader);
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified TextReader, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
		internal static void LoadXml<T>(T cache, TextReader reader, Action<T, XmlReader> parser) where T : class
		{
			AssertCache(cache);
			AssertTextReader(reader);

			using XmlReader xreader = XmlReader.Create(reader, SecureSettings);
			parser(cache, xreader);
		}

#if NET50
		/// <summary>
		/// Loads configuration data into a cache from the specified sequence of bytes, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
		internal static void LoadXml<T>(T cache, ReadOnlySequence<byte> utf8Json, Action<T, XmlReader> parser) where T : class
		{
			AssertCache(cache);

			using MemoryStream stream = new MemoryStream(utf8Json.ToArray());
			using XmlReader xreader = XmlReader.Create(stream, SecureSettings);
			parser(cache, xreader);
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified sequence of bytes, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
		internal static void LoadXml<T>(T cache, ReadOnlyMemory<byte> utf8Json, Action<T, XmlReader> parser) where T : class
		{
			AssertCache(cache);

			using MemoryStream stream = new MemoryStream(utf8Json.ToArray());
			using XmlReader xreader = XmlReader.Create(stream, SecureSettings);
			parser(cache, xreader);
		}
#endif

		/// <summary>
		/// Loads configuration data into a cache from the specified string, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="content">A string containing XML-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
		internal static void ParseXml<T>(T cache, string content, Action<T, XmlReader> parser) where T : class
		{
			AssertCache(cache);
			AssertContent(content);

			using StringReader reader = new StringReader(content);
			using XmlReader xreader = XmlReader.Create(reader, SecureSettings);
			parser(cache, xreader);
		}

#if NET50
		/// <summary>
		/// Loads configuration data into a cache from the specified file path, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="path">The path to the JSON file containing the configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void LoadJson<T>(T cache, string path, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);
			AssertPath(path);

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
				LoadJson(cache, stream, parser);
			}
			finally
			{
				stream.Close();
			}
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified stream, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void LoadJson<T>(T cache, Stream stream, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);
			AssertStream(stream);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(stream, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}

			try
			{
				parser(cache, jsonDocument.RootElement);
			}
			finally
			{
				jsonDocument.Dispose();
			}
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified TextReader, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void LoadJson<T>(T cache, TextReader reader, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);
			AssertTextReader(reader);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(reader.ReadToEnd(), new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}

			try
			{
				parser(cache, jsonDocument.RootElement);
			}
			finally
			{
				jsonDocument.Dispose();
			}
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified sequence of bytes, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void LoadJson<T>(T cache, ReadOnlySequence<byte> utf8Json, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(utf8Json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}

			try
			{
				parser(cache, jsonDocument.RootElement);
			}
			finally
			{
				jsonDocument.Dispose();
			}
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified sequence of bytes, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void LoadJson<T>(T cache, ReadOnlyMemory<byte> utf8Json, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(utf8Json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}

			try
			{
				parser(cache, jsonDocument.RootElement);
			}
			finally
			{
				jsonDocument.Dispose();
			}
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified string, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="content">A string containing JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void ParseJson<T>(T cache, string content, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);
			AssertContent(content);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(content, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is JsonException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}

			try
			{
				parser(cache, jsonDocument.RootElement);
			}
			finally
			{
				jsonDocument.Dispose();
			}
		}
#else
		/// <summary>
		/// Loads configuration data into a cache from the specified file path, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="path">The path to the JSON file containing the configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void LoadJson<T>(T cache, string path, Action<T, JsonValue> parser) where T : class
		{
			AssertCache(cache);
			AssertPath(path);

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
				LoadJson(cache, stream, parser);
			}
			finally
			{
				stream.Close();
			}
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified stream, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void LoadJson<T>(T cache, Stream stream, Action<T, JsonValue> parser) where T : class
		{
			AssertCache(cache);
			AssertStream(stream);

			JsonValue jsonValue;
			try
			{
				jsonValue = JsonValue.Load(stream);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}

			parser(cache, jsonValue);
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified TextReader, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void LoadJson<T>(T cache, TextReader reader, Action<T, JsonValue> parser) where T : class
		{
			AssertCache(cache);
			AssertTextReader(reader);

			JsonValue jsonValue;
			try
			{
				jsonValue = JsonValue.Load(reader);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}

			parser(cache, jsonValue);
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified string, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="content">A string containing JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static void ParseJson<T>(T cache, string content, Action<T, JsonValue> parser) where T : class
		{
			AssertCache(cache);
			AssertContent(content);

			JsonValue jsonValue;
			try
			{
				jsonValue = JsonValue.Parse(content);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
			{
				throw new IOException(TextResources.Global_FromJson_ParseFailed, ex);
			}

			parser(cache, jsonValue);
		}
#endif

		internal static FormatException InvalidAssemblyNameException(string assemblyName, Exception ex) => new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, assemblyName), ex);

		internal static XmlException AssemblyNameAttributeMissingException(string parentNodeName) => new XmlException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_FromXml_AttributeMissing, parentNodeName, Globals.AssemblyNameKey));

		/// <summary>
		/// Checks if the object is null.
		/// </summary>
		/// <exception cref="ArgumentNullException">The object is null.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AssertCache(object cache)
		{
			if (cache == null)
			{
				throw new ArgumentNullException(nameof(cache));
			}
		}

		/// <summary>
		/// Checks if the reader is null.
		/// </summary>
		/// <param name="reader">The reader to check.</param>
		/// <exception cref="ArgumentNullException">The reader is null.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AssertXmlReader(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AssertSectionName(string sectionName)
		{
			if (string.IsNullOrWhiteSpace(sectionName))
			{
				throw new ArgumentException(TextResources.Global_FromApplicationConfiguration_NoSection, nameof(sectionName));
			}
		}

#if NET50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AssertJsonValueKindObject(JsonElement jsonElement)
		{
			if (jsonElement.ValueKind != JsonValueKind.Object)
			{
				throw new ArgumentException(TextResources.Global_FromJson_NotAnObject, nameof(jsonElement));
			}
		}

		internal static JsonElement GetParentElement(JsonElement jsonElement)
		{
			if (jsonElement.EnumerateObject().Count() == 1)
			{
				JsonElement jsonTemp = jsonElement.EnumerateObject().First().Value;
				if (jsonTemp.ValueKind == JsonValueKind.Object)
				{
					return jsonTemp;
				}
			}
			return jsonElement;
		}
#else
		/// <summary>
		/// Checks if the element is null.
		/// </summary>
		/// <param name="jsonElement">The element to check.</param>
		/// <exception cref="ArgumentNullException">The element is null.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AssertJsonValue(JsonValue jsonValue)
		{
			if (jsonValue == null)
			{
				throw new ArgumentNullException(nameof(jsonValue));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static JsonObject AssertJsonObject(JsonValue jsonValue)
		{
			if (!(jsonValue is JsonObject jsonObject))
			{
				throw new ArgumentException(TextResources.Global_FromJson_NotAnObject, nameof(jsonValue));
			}
			return jsonObject;
		}

		internal static JsonObject GetParentElement(JsonObject jsonObject)
		{
			if (jsonObject.Count == 1 && jsonObject.Values.First().JsonType == JsonType.Object)
			{
				jsonObject = (JsonObject)jsonObject.Values.First();
			}

			return jsonObject;
		}
#endif
#if !NET472
		/// <summary>
		/// Checks if the element is null.
		/// </summary>
		/// <param name="jsonElement">The element to check.</param>
		/// <exception cref="ArgumentNullException">The element is null.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AssertConfiguration(Microsoft.Extensions.Configuration.IConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}
		}
#endif

		/// <summary>
		/// Checks if the reader is null.
		/// </summary>
		/// <param name="reader">The reader to check.</param>
		/// <exception cref="ArgumentNullException">The reader is null.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AssertTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}
		}

		/// <summary>
		/// Checks if the path string is null or empty.
		/// </summary>
		/// <param name="path">The path to check.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		/// <exception cref="ArgumentException">The path is null or empty.</exception>
		private static void AssertPath(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException(TextResources.Global_NoPath, nameof(path));
			}
		}

		/// <summary>
		/// Checks if the stream is null or not readable.
		/// </summary>
		/// <param name="stream">The stream to check.</param>
		/// <exception cref="ArgumentNullException">The stream is null.</exception>
		/// <exception cref="ArgumentException">The stream is not readable.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AssertStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(TextResources.Global_StreamNoRead, nameof(stream));
			}
		}

		/// <summary>
		/// Checks if the content string is null or empty.
		/// </summary>
		/// <param name="content">The content to check.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AssertContent(string content)
		{
			if (string.IsNullOrWhiteSpace(content))
			{
				throw new ArgumentException(TextResources.Global_NoContent, nameof(content));
			}
		}
	}
}
