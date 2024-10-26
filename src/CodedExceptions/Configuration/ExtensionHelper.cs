// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Xml;

#if NET5_0_OR_GREATER
using System.Buffers;
#endif

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Provides methods to read configurations for <see cref="AssemblyDebugModeCache" /> and <see cref="AssemblyFacilityOverrideCache" /> from various sources.
/// </summary>
internal static class ExtensionHelper
{
	internal static readonly XmlReaderSettings s_secureSettings = new() { IgnoreComments = true, IgnoreWhitespace = true, CloseInput = false, DtdProcessing = DtdProcessing.Prohibit, XmlResolver = null };

	/// <summary>
	/// Loads configuration data into a cache from the specified file path, using the specified method.
	/// </summary>
	/// <typeparam name="T">The type of the cache.</typeparam>
	/// <param name="cache">The cache to add the configuration data to.</param>
	/// <param name="path">The path to the XML file containing the configuration data.</param>
	/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
	internal static T LoadXml<T>(T cache, string path, Action<T, XmlReader> parser) where T : class
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
		ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));
#else
		if (string.IsNullOrWhiteSpace(path))
		{
			throw new ArgumentException(SR.Load_NullOrEmpty, nameof(path));
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
			_ = LoadXml(cache, stream, parser);
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
	/// <param name="stream">A stream containing XML-formatted data representing configuration data.</param>
	/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
	internal static T LoadXml<T>(T cache, Stream stream, Action<T, XmlReader> parser) where T : class
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

		using XmlReader reader = XmlReader.Create(stream, s_secureSettings);
		parser(cache, reader);
		return cache;
	}

	/// <summary>
	/// Loads configuration data into a cache from the specified TextReader, using the specified method.
	/// </summary>
	/// <typeparam name="T">The type of the cache.</typeparam>
	/// <param name="cache">The cache to add the configuration data to.</param>
	/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing configuration data.</param>
	/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
	internal static T LoadXml<T>(T cache, TextReader reader, Action<T, XmlReader> parser) where T : class
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

		using XmlReader xmlReader = XmlReader.Create(reader, s_secureSettings);
		parser(cache, xmlReader);
		return cache;
	}

#if NET5_0_OR_GREATER
	/// <summary>
	/// Loads configuration data into a cache from the specified sequence of bytes, using the specified method.
	/// </summary>
	/// <typeparam name="T">The type of the cache.</typeparam>
	/// <param name="cache">The cache to add the configuration data to.</param>
	/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing configuration data.</param>
	/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
	internal static T LoadXml<T>(T cache, ReadOnlySequence<byte> utf8Json, Action<T, XmlReader> parser) where T : class
	{
		ArgumentNullException.ThrowIfNull(cache);

		using MemoryStream stream = new(utf8Json.ToArray());
		using XmlReader xmlReader = XmlReader.Create(stream, s_secureSettings);
		parser(cache, xmlReader);
		return cache;
	}

	/// <summary>
	/// Loads configuration data into a cache from the specified sequence of bytes, using the specified method.
	/// </summary>
	/// <typeparam name="T">The type of the cache.</typeparam>
	/// <param name="cache">The cache to add the configuration data to.</param>
	/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing configuration data.</param>
	/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
	internal static T LoadXml<T>(T cache, ReadOnlyMemory<byte> utf8Json, Action<T, XmlReader> parser) where T : class
	{
		ArgumentNullException.ThrowIfNull(cache);

		using MemoryStream stream = new(utf8Json.ToArray());
		using XmlReader xmlReader = XmlReader.Create(stream, s_secureSettings);
		parser(cache, xmlReader);
		return cache;
	}
#endif

	/// <summary>
	/// Loads configuration data into a cache from the specified string, using the specified method.
	/// </summary>
	/// <typeparam name="T">The type of the cache.</typeparam>
	/// <param name="cache">The cache to add the configuration data to.</param>
	/// <param name="content">A string containing XML-formatted data representing configuration data.</param>
	/// <param name="parser">The method that parses the XML data and adds the configuration data to the cache.</param>
	internal static T ParseXml<T>(T cache, string content, Action<T, XmlReader> parser) where T : class
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
			throw new ArgumentException(SR.Load_NullOrEmpty, nameof(content));
		}
#endif

		using StringReader reader = new(content);
		using XmlReader xmlReader = XmlReader.Create(reader, s_secureSettings);
		parser(cache, xmlReader);
		return cache;
	}

	/// <summary>
	/// Finds the object in a list that has the best match of it's <see cref="AssemblyIdentity" /> field to the specified <see cref="Assembly" />.
	/// </summary>
	/// <typeparam name="TSource">The type of the <paramref name="source"/> list to search.</typeparam>
	/// <param name="source">The list of objects to search.</param>
	/// <param name="assembly">The <see cref="Assembly" /> to match the <see cref="AssemblyIdentity" />s to.</param>
	/// <param name="selector">A function to select the <see cref="AssemblyIdentity" /> of the <typeparamref name="TSource"/> objects.</param>
	/// <returns>The <typeparamref name="TSource"/> object having the best match to <paramref name="assembly"/>, or <see langword="null" />, it no match was found.</returns>
	internal static TSource? GetMaximumMatch<TSource>(List<TSource> source, Assembly assembly, Func<TSource, AssemblyIdentity> selector)
	{
		int match, highestMatch = -1;
		TSource? result = default;
		foreach (TSource sourceObject in source)
		{
			if ((match = selector(sourceObject).Match(assembly)) > 0 && match > highestMatch)
			{
				highestMatch = match;
				result = sourceObject;
				if (match == AssemblyIdentity.MaximumMatchValue)
				{
					break; // Can't get any better.
				}
			}
		}

		return result;
	}

	internal static List<T> FromXmlInternal<T, TValue>(XmlReader reader, string rootNodeName, string nodesName, string valueKey, string valueInvalidResourceKey, Func<string?, TValue> converter, Func<AssemblyIdentity, TValue, T> constructor)
	{
		reader.ReadStartElement(rootNodeName);
		List<T> result = [];
		string? assemblyString, valueString;
		AssemblyIdentity assembly;
		TValue convertedValue;

		while (!(reader.Name == rootNodeName && reader.NodeType == XmlNodeType.EndElement))
		{
			if (reader.Name == nodesName && reader.NodeType == XmlNodeType.Element)
			{
				assemblyString = reader.GetAttribute(GlobalStrings.AssemblyNameKey);
				if (assemblyString == null)
				{
					throw new XmlException(string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(SR.FromXml_AttributeMissing), reader.Name, GlobalStrings.AssemblyNameKey));
				}

				try
				{
					assembly = new AssemblyIdentity(assemblyString);
				}
				catch (FormatException ex)
				{
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(SR.Load_AssemblyNameInvalid), assemblyString), ex);
				}

				valueString = reader.GetAttribute(valueKey);
				try
				{
					convertedValue = converter(valueString);
				}
				catch (XmlException)
				{
					throw;
				}
				catch (FormatException ex)
				{
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, SR.ResourceManager.GetString(valueInvalidResourceKey, CultureInfo.CurrentCulture) ?? string.Empty, assemblyString), ex);
				}

				result.Add(constructor(assembly, convertedValue));
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

		return result;
	}
}
