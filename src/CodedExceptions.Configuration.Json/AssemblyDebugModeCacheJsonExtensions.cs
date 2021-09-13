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

using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Extends the <see cref="AssemblyDebugModeCache" /> class with methods to easily add debug mode settings from various sources.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class AssemblyDebugModeCacheJsonExtensions
	{
		private const string DefaultFileName = "AssemblyDebugModes";

		/// <summary>
		/// Loads a list of assembly debug mode settings from the default JSON file and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <remarks>The default file is named 'AssemblyDebugModes.json' and must reside in the working directory of the application.</remarks>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The file could not be opened or read.</exception>
		public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache) => ExtensionHelper.LoadJson(cache, DefaultFileName + ".json", (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the JSON file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="path">The path to the JSON file containing the settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="path"/> is <see langword="null"/> or white-space.</exception>
		/// <exception cref="IOException">The file could not be opened or read.</exception>
		public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, string path) => ExtensionHelper.LoadJson(cache, path, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="stream"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="stream"/> is is not readable.</exception>
		/// <exception cref="IOException">The stream data could not be read.</exception>
		public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, Stream stream) => ExtensionHelper.LoadJson(cache, stream, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="reader"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The stream data could not be read.</exception>
		public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, TextReader reader) => ExtensionHelper.LoadJson(cache, reader ?? throw new ArgumentNullException(nameof(reader)), (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
		public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, ReadOnlySequence<byte> utf8Json) => ExtensionHelper.LoadJson(cache, utf8Json, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
		public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, ReadOnlyMemory<byte> utf8Json) => ExtensionHelper.LoadJson(cache, utf8Json, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="content">A string containing JSON-formatted data representing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/> or <paramref name="content"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="IOException">The stream data could not be read.</exception>
		public static AssemblyDebugModeCache ParseJson(this AssemblyDebugModeCache cache, string content) => ExtensionHelper.ParseJson(cache, content, (cache, jsonElement) => FromJson(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="jsonElement">The <see cref="JsonElement"/> containing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="jsonElement"/> is not a JSON object.</exception>
		/// <exception cref="FormatException">The JSON data is invalid.</exception>
		public static AssemblyDebugModeCache FromJson(this AssemblyDebugModeCache cache, JsonElement jsonElement)
		{
			ExtensionHelper.AssertCache(cache);
			FromJsonInternal(cache, jsonElement);
			return cache;
		}

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the settings to.</param>
		/// <param name="jsonElement">The <see cref="JsonElement"/> containing debug mode settings.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="jsonElement"/> is not a JSON object.</exception>
		/// <exception cref="FormatException">The JSON data is invalid.</exception>
		private static void FromJsonInternal(this AssemblyDebugModeCache cache, JsonElement jsonElement)
		{
			ExtensionHelper.AssertJsonValueKindObject(jsonElement);

			jsonElement = ExtensionHelper.GetParentElement(jsonElement);

			List<AssemblyDebugMode> debugModes = new();
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
					throw ExtensionHelper.InvalidAssemblyNameException(jsonProperty.Name, ex);
				}

				if (jsonProperty.Value.ValueKind is not JsonValueKind.True and not JsonValueKind.False)
				{
					throw NotABoolException();
				}
				isEnabled = jsonProperty.Value.GetBoolean();

				debugModes.Add(new AssemblyDebugMode(assemblyName, isEnabled));
			}

			cache.AddRange(debugModes);
		}

		private static FormatException NotABoolException() => new(TextResources.Global_FromJson_NotABool);

	}
}
