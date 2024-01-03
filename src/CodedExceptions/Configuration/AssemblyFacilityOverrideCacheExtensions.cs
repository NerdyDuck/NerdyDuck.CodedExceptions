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
/// Extends the <see cref="AssemblyFacilityOverrideCache" /> class with methods to easily add assembly facility identifier overrides from various sources.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class AssemblyFacilityOverrideCacheExtensions
{
	private const string DefaultFileName = "FacilityIdentifierOverrides";

	/// <summary>
	/// Loads a list of facility identifier overrides from the default XML file and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the default XML file, if the file exists.</returns>
	/// <remarks>The default file is named 'FacilityIdentifierOverrides.xml' and must reside in the working directory of the application.</remarks>
	public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache) => ExtensionHelper.LoadXml(cache, DefaultFileName + ".xml", FromXmlInternal);

	/// <summary>
	/// Loads a list of facility identifier overrides from the XML file at the specified path and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="path">The path to the XML file containing the overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the XML file.</returns>
	public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, string path) => ExtensionHelper.LoadXml(cache, path, FromXmlInternal);

	/// <summary>
	/// Loads a list of facility identifier overrides from the specified stream containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="stream">A stream containing XML-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the XML stream data.</returns>
	public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, Stream stream) => ExtensionHelper.LoadXml(cache, stream, FromXmlInternal);

	/// <summary>
	/// Loads a list of facility identifier overrides from the specified TextReader containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the XML data of the <paramref name="reader"/>.</returns>
	public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, TextReader reader) => ExtensionHelper.LoadXml(cache, reader, FromXmlInternal);

#if !NETFRAMEWORK
	/// <summary>
	/// Loads a list of facility identifier overrides from the specified sequence of bytes containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the XML data of the byte sequence.</returns>
	public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, ReadOnlySequence<byte> json) => ExtensionHelper.LoadXml(cache, json, FromXmlInternal);

	/// <summary>
	/// Loads a list of facility identifier overrides from the specified sequence of bytes containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the XML data of the byte memory.</returns>
	public static AssemblyFacilityOverrideCache LoadXml(this AssemblyFacilityOverrideCache cache, ReadOnlyMemory<byte> json) => ExtensionHelper.LoadXml(cache, json, FromXmlInternal);

#endif
	/// <summary>
	/// Parses a list of facility identifier overrides from the specified string containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="content">A string containing XML-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the XML string.</returns>
	public static AssemblyFacilityOverrideCache ParseXml(this AssemblyFacilityOverrideCache cache, string content) => ExtensionHelper.ParseXml(cache, content, FromXmlInternal);

	/// <summary>
	/// Loads a list of facility identifier overrides from the specified XmlReader, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="reader">A <see cref="XmlReader"/> containing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the XML data of the <paramref name="reader"/>.</returns>
	public static AssemblyFacilityOverrideCache FromXml(this AssemblyFacilityOverrideCache cache, XmlReader reader)
	{
		ExtensionHelper.AssertCache(cache);
		ExtensionHelper.AssertXmlReader(reader);

		FromXmlInternal(cache, reader);
		return cache;
	}

	/// <summary>
	/// Loads a list of facility identifier overrides from the specified XmlReader, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="reader">A <see cref="XmlReader"/> containing overrides.</param>
	private static void FromXmlInternal(AssemblyFacilityOverrideCache cache, XmlReader reader) => cache.AddRange(ExtensionHelper.FromXmlInternal(reader, GlobalStrings.OverridesNode, GlobalStrings.OverrideNode, GlobalStrings.IdentifierKey, nameof(TextResources.Global_FromXml_AttributeMissing), (stringValue) => string.IsNullOrWhiteSpace(stringValue) ? throw new XmlException(string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(TextResources.Global_FromXml_AttributeMissing), GlobalStrings.OverrideNode, GlobalStrings.IdentifierKey)) : XmlConvert.ToInt32(stringValue), (assembly, convertedValue) => new AssemblyFacilityOverride(assembly, convertedValue)));
}
