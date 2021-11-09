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

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
#if NET5_0_OR_GREATER
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
	/// <remarks>The default file is named 'AssemblyDebugModes.xml' and must reside in the working directory of the application.</remarks>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache) => ExtensionHelper.LoadXml(cache, DefaultFileName + ".xml", (cache, reader) => FromXmlInternal(cache, reader));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the XML file at the specified path and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="path">The path to the XML file containing the settings.</param>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, string path) => ExtensionHelper.LoadXml(cache, path, (cache, reader) => FromXmlInternal(cache, reader));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified stream containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="stream">A stream containing XML-formatted data representing debug mode settings.</param>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, Stream stream) => ExtensionHelper.LoadXml(cache, stream, (cache, reader) => FromXmlInternal(cache, reader));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified TextReader containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="reader">A <see cref="TextReader"/> containing XML-formatted data representing debug mode settings.</param>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, TextReader reader) => ExtensionHelper.LoadXml(cache, reader, (cache, reader) => FromXmlInternal(cache, reader));

#if NET5_0_OR_GREATER
	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing debug mode settings.</param>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, ReadOnlySequence<byte> utf8Json) => ExtensionHelper.LoadXml(cache, utf8Json, (cache, reader) => FromXmlInternal(cache, reader));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, XML-formatted data representing debug mode settings.</param>
	public static AssemblyDebugModeCache LoadXml(this AssemblyDebugModeCache cache, ReadOnlyMemory<byte> utf8Json) => ExtensionHelper.LoadXml(cache, utf8Json, (cache, reader) => FromXmlInternal(cache, reader));
#endif

	/// <summary>
	/// Parses a list of assembly debug mode settings from the specified string containing XML data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="content">A string containing XML-formatted data representing debug mode settings.</param>
	public static AssemblyDebugModeCache ParseXml(this AssemblyDebugModeCache cache, string content) => ExtensionHelper.ParseXml(cache, content, (cache, reader) => FromXmlInternal(cache, reader));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified XmlReader, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="reader">A <see cref="XmlReader"/> containing debug mode settings.</param>
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
	private static void FromXmlInternal(this AssemblyDebugModeCache cache, XmlReader reader) => cache.AddRange(ExtensionHelper.FromXmlInternal(reader, Globals.DebugModesNode, Globals.DebugModeNode, Globals.IsEnabledKey, nameof(TextResources.Global_IsEnabledInvalid), (stringValue) => string.IsNullOrWhiteSpace(stringValue) || XmlConvert.ToBoolean(stringValue), (assembly, convertedValue) => new AssemblyDebugMode(assembly, convertedValue)));
}
