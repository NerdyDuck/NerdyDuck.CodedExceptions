// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Linq;
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
#if NET5_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(cache);
#else
		if (cache == null)
		{
			throw new ArgumentNullException(nameof(cache));
		}
#endif
#if NET7_0_OR_GREATER
		ArgumentException.ThrowIfNullOrEmpty(path);
#else
		if (string.IsNullOrEmpty(path))
		{
			throw new ArgumentException(SR.LoadJson_NullOrEmpty, nameof(path));
		}
#endif

		FileStream stream;
		try
		{
			stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}
		catch (Exception ex) when (ex is IOException or ArgumentException or NotSupportedException or SecurityException or UnauthorizedAccessException)
		{
			throw new IOException(string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(SR.Load_OpenFileFailed), path), ex);
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
#if NET5_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(cache);
		ArgumentNullException.ThrowIfNull(stream);
#else
		if (cache == null)
		{
			throw new ArgumentNullException(nameof(cache));
		}
		if (stream == null)
		{
			throw new ArgumentNullException(nameof(stream));
		}
#endif
		if (!stream.CanRead)
		{
			throw new ArgumentException(SR.Load_StreamNoRead, nameof(stream));
		}

		JsonDocument jsonDocument;
		try
		{
			jsonDocument = JsonDocument.Parse(stream, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
		}
		catch (Exception ex) when (ex is ArgumentException or FormatException or JsonException)
		{
			throw new IOException(SR.FromJson_ParseFailed, ex);
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
#if NET5_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(cache);
		ArgumentNullException.ThrowIfNull(reader);
#else
		if (cache == null)
		{
			throw new ArgumentNullException(nameof(cache));
		}

		if (reader == null)
		{
			throw new ArgumentNullException(nameof(reader));
		}
#endif

		JsonDocument jsonDocument;
		try
		{
			jsonDocument = JsonDocument.Parse(reader.ReadToEnd(), new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
		}
		catch (Exception ex) when (ex is ArgumentException or FormatException or JsonException)
		{
			throw new IOException(SR.FromJson_ParseFailed, ex);
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

#if NET5_0_OR_GREATER
	/// <summary>
	/// Loads configuration data into a cache from the specified sequence of bytes, using the specified method.
	/// </summary>
	/// <typeparam name="T">The type of the cache.</typeparam>
	/// <param name="cache">The cache to add the configuration data to.</param>
	/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing configuration data.</param>
	/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
	internal static T LoadJson<T>(T cache, ReadOnlySequence<byte> utf8Json, Action<T, JsonElement> parser) where T : class
	{
		ArgumentNullException.ThrowIfNull(cache);

		JsonDocument jsonDocument;
		try
		{
			jsonDocument = JsonDocument.Parse(utf8Json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
		}
		catch (Exception ex) when (ex is ArgumentException or FormatException or JsonException)
		{
			throw new IOException(SR.FromJson_ParseFailed, ex);
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
#endif

	/// <summary>
	/// Loads configuration data into a cache from the specified string, using the specified method.
	/// </summary>
	/// <typeparam name="T">The type of the cache.</typeparam>
	/// <param name="cache">The cache to add the configuration data to.</param>
	/// <param name="content">A string containing JSON-formatted data representing configuration data.</param>
	/// <param name="parser">The method that parses the JSON data and adds the configuration data to the cache.</param>
	internal static T ParseJson<T>(T cache, string content, Action<T, JsonElement> parser) where T : class
	{
#if NET5_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(cache);
#else
		if (cache == null)
		{
			throw new ArgumentNullException(nameof(cache));
		}
#endif
#if NET7_0_OR_GREATER
		ArgumentException.ThrowIfNullOrEmpty(content);
#else
		if (string.IsNullOrEmpty(content))
		{
#if NET5_0_OR_GREATER
			ArgumentNullException.ThrowIfNull(content);
#else
			if (content == null)
			{
				throw new ArgumentNullException(nameof(content));
			}
#endif
			throw new ArgumentException(SR.LoadJson_NullOrEmpty, nameof(content));
		}
#endif

		JsonDocument jsonDocument;
		try
		{
			jsonDocument = JsonDocument.Parse(content, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
		}
		catch (Exception ex) when (ex is ArgumentException or FormatException or JsonException)
		{
			throw new IOException(SR.FromJson_ParseFailed, ex);
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

	internal static List<T> FromJsonInternal<T, TValue>(JsonElement jsonElement, Func<JsonProperty, TValue> converter, Func<AssemblyIdentity, TValue, T> constructor)
	{
		if (jsonElement.ValueKind != JsonValueKind.Object)
		{
			throw new ArgumentException(SR.FromJson_ParseFailed, nameof(jsonElement));
		}

		if (jsonElement.EnumerateObject().Count() == 1)
		{
			JsonElement jsonTemp = jsonElement.EnumerateObject().First().Value;
			if (jsonTemp.ValueKind == JsonValueKind.Object)
			{
				jsonElement = jsonTemp;
			}
		}

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
				throw new FormatException(string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(SR.FromJson_AssemblyNameInvalid), jsonProperty.Name), ex);
			}

			convertedValue = converter(jsonProperty);

			result.Add(constructor(assembly, convertedValue));
		}

		return result;
	}
}
