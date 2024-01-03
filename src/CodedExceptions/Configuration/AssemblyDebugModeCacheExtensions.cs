// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Ignore Spelling: json

using System.ComponentModel;
using System.IO;
using System.Xml;
#if !NETFRAMEWORK
using System.Buffers;
#endif

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Extends the <see cref="AssemblyDebugModeCache" /> class with methods to easily add debug mode settings from various sources.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class AssemblyDebugModeCacheExtensions
{
	private const string DefaultFileName = "AssemblyDebugModes";

	/// <summary>
	/// Loads a list of assembly debug mode settings from the default XML file and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the default XML file, if the file exists.</returns>
	/// <remarks>The default file is named 'AssemblyDebugModes.xml' and must reside in the working directory of the application.</remarks>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache) => ExtensionHelper.LoadXml(cache, DefaultFileName + ".xml", FromXmlInternal);

	/// <summary>
	/// Loads a list of assembly debug mode settings from the XML file at the specified path and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="path">The path to the XML file containing the settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the XML file.</returns>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, string path) => ExtensionHelper.LoadXml(cache, path, FromXmlInternal);

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified stream containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="stream">A stream containing XML-formatted data representing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the XML stream data.</returns>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, Stream stream) => ExtensionHelper.LoadXml(cache, stream, FromXmlInternal);

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified TextReader containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the XML data of the <paramref name="reader"/>.</returns>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, TextReader reader) => ExtensionHelper.LoadXml(cache, reader, FromXmlInternal);

#if !NETFRAMEWORK
	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the XML data of the byte sequence.</returns>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, ReadOnlySequence<byte> json) => ExtensionHelper.LoadXml(cache, json, FromXmlInternal);

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the XML data of the byte memory.</returns>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, ReadOnlyMemory<byte> json) => ExtensionHelper.LoadXml(cache, json, FromXmlInternal);
#endif

	/// <summary>
	/// Parses a list of assembly debug mode settings from the specified string containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="content">A string containing XML-formatted data representing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the XML string.</returns>
	public static AssemblyDebugModeCache ParseXml(this AssemblyDebugModeCache cache, string content) => ExtensionHelper.ParseXml(cache, content, FromXmlInternal);

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified XmlReader, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="reader">A <see cref="XmlReader"/> containing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the XML data of the <paramref name="reader"/>.</returns>
	public static AssemblyDebugModeCache FromXml(this AssemblyDebugModeCache cache, XmlReader reader)
	{
		ExtensionHelper.AssertCache(cache);
		ExtensionHelper.AssertXmlReader(reader);

		FromXmlInternal(cache, reader);
		return cache;
	}

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified XmlReader, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="reader">A <see cref="XmlReader"/> containing debug mode settings.</param>
	private static void FromXmlInternal(this AssemblyDebugModeCache cache, XmlReader reader) => cache.AddRange(ExtensionHelper.FromXmlInternal(reader, GlobalStrings.DebugModesNode, GlobalStrings.DebugModeNode, GlobalStrings.IsEnabledKey, nameof(TextResources.Global_IsEnabledInvalid), (stringValue) => string.IsNullOrWhiteSpace(stringValue) || XmlConvert.ToBoolean(stringValue), (assembly, convertedValue) => new AssemblyDebugMode(assembly, convertedValue)));
}
