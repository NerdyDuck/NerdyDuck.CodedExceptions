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
using System.Buffers;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text.Json;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Provides methods to read configurations for <see cref="AssemblyDebugModeCache" /> and <see cref="AssemblyFacilityOverrideCache" /> from various sources.
	/// </summary>
	internal static class ExtensionHelper
	{
		/// <summary>
		/// Loads configuration data into a cache from the specified file path, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="path">The path to the JSON file containing the configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static T LoadJson<T>(T cache, string path, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);
			AssertPath(path);

			FileStream stream;
			try
			{
				stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (Exception ex) when (ex is IOException or ArgumentException or NotSupportedException or SecurityException or UnauthorizedAccessException)
			{
				throw new IOException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_OpenFileFailed, path), ex);
			}

			try
			{
				_ = LoadJson(cache, stream, parser);
			}
			finally
			{
				stream.Close();
			}
			return cache;
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified stream, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static T LoadJson<T>(T cache, Stream stream, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);
			AssertStream(stream);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(stream, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException or FormatException or JsonException)
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
			return cache;
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified TextReader, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static T LoadJson<T>(T cache, TextReader reader, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);
			AssertTextReader(reader);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(reader.ReadToEnd(), new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException or FormatException or JsonException)
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
			return cache;
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified sequence of bytes, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static T LoadJson<T>(T cache, ReadOnlySequence<byte> utf8Json, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(utf8Json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException or FormatException or JsonException)
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
			return cache;
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified sequence of bytes, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static T LoadJson<T>(T cache, ReadOnlyMemory<byte> utf8Json, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(utf8Json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException or FormatException or JsonException)
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
			return cache;
		}

		/// <summary>
		/// Loads configuration data into a cache from the specified string, using the specified method.
		/// </summary>
		/// <typeparam name="T">The type of the cache.</typeparam>
		/// <param name="cache">The cache to add the configuration data to.</param>
		/// <param name="content">A string containing JSON-formatted data representing configuration data.</param>
		/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
		internal static T ParseJson<T>(T cache, string content, Action<T, JsonElement> parser) where T : class
		{
			AssertCache(cache);
			AssertContent(content);

			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(content, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
			}
			catch (Exception ex) when (ex is ArgumentException or FormatException or JsonException)
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
			return cache;
		}

		internal static FormatException InvalidAssemblyNameException(string assemblyName, Exception ex) => new(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, assemblyName), ex);

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
		/// <exception cref="ArgumentException">The path is null or empty.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
