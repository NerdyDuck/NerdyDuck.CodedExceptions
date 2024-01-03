// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;

namespace NerdyDuck.CodedExceptions.Configuration;

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
			throw new IOException(string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(TextResources.Global_OpenFileFailed), path), ex);
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
	internal static T LoadJson<T>(T cache, ReadOnlyMemory<byte> utf8Json, Action<T, JsonElement> parser) where T : class => LoadJson(cache, new ReadOnlySequence<byte>(utf8Json), parser);

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

	internal static FormatException InvalidAssemblyNameException(string assemblyName, Exception ex) => new(string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(TextResources.Global_AssemblyNameInvalid), assemblyName), ex);

#if NETFRAMEWORK
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
#else

	/// <summary>
	/// Checks if the object is null.
	/// </summary>
	/// <exception cref="ArgumentNullException">The object is null.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void AssertCache(object cache) => ArgumentNullException.ThrowIfNull(cache, nameof(cache));
#endif

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

#if NETFRAMEWORK
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
#else

	/// <summary>
	/// Checks if the reader is null.
	/// </summary>
	/// <param name="reader">The reader to check.</param>
	/// <exception cref="ArgumentNullException">The reader is null.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void AssertTextReader(TextReader reader) => ArgumentNullException.ThrowIfNull(reader, nameof(reader));
#endif

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
#if NETFRAMEWORK
		if (stream == null)
		{
			throw new ArgumentNullException(nameof(stream));
		}
#else
		ArgumentNullException.ThrowIfNull(stream, nameof(stream));
#endif
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

	internal static List<T> FromJsonInternal<T, TValue>(JsonElement jsonElement, Func<JsonProperty, TValue> converter, Func<AssemblyIdentity, TValue, T> constructor)
	{
		ExtensionHelper.AssertJsonValueKindObject(jsonElement);

		jsonElement = ExtensionHelper.GetParentElement(jsonElement);

		List<T> result = [];
		AssemblyIdentity assembly;
		TValue convertedValue;

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

			convertedValue = converter(jsonProperty);

			result.Add(constructor(assembly, convertedValue));
		}

		return result;
	}
}
