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
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Extends the <see cref="AssemblyFacilityOverrideCache" /> class with methods to easily add assembly facility identifier overrides from various sources.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class AssemblyFacilityOverrideCacheJsonExtensions
	{
		private const string DefaultFileName = "FacilityIdentifierOverrides";

		/// <summary>
		/// Loads a list of facility identifier overrides from the default JSON file and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <remarks>The default file is named 'FacilityIdentifierOverrides.json' and must reside in the working directory of the application.</remarks>
		public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache) => ExtensionHelper.LoadJson(cache, DefaultFileName + ".json", (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of facility identifier overrides from the JSON file at the specified path and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="path">The path to the JSON file containing the overrides.</param>
		public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, string path) => ExtensionHelper.LoadJson(cache, path, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified stream containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="stream">A stream containing JSON-formatted data representing overrides.</param>
		public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, Stream stream) => ExtensionHelper.LoadJson(cache, stream, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified TextReader containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
		public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, TextReader reader) => ExtensionHelper.LoadJson(cache, reader ?? throw new ArgumentNullException(nameof(reader)), (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing overrides.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
		public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, ReadOnlySequence<byte> utf8Json) => ExtensionHelper.LoadJson(cache, utf8Json, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="utf8Json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing overrides.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
		/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
		public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, ReadOnlyMemory<byte> utf8Json) => ExtensionHelper.LoadJson(cache, utf8Json, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Parses a list of facility identifier overrides from the specified string containing JSON data, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="content">A string containing JSON-formatted data representing overrides.</param>
		public static AssemblyFacilityOverrideCache ParseJson(this AssemblyFacilityOverrideCache cache, string content) => ExtensionHelper.ParseJson(cache, content, (cache, jsonElement) => FromJsonInternal(cache, jsonElement));

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="jsonElement">A <see cref="JsonElement"/> containing overrides.</param>
		public static AssemblyFacilityOverrideCache FromJson(this AssemblyFacilityOverrideCache cache, JsonElement jsonElement)
		{
			ExtensionHelper.AssertCache(cache);
			FromJsonInternal(cache, jsonElement);
			return cache;
		}

		/// <summary>
		/// Loads a list of facility identifier overrides from the specified JSON object, and adds them to the cache.
		/// </summary>
		/// <param name="cache">The cache to add the overrides to.</param>
		/// <param name="jsonElement">A <see cref="JsonElement"/> containing overrides.</param>
		private static void FromJsonInternal(AssemblyFacilityOverrideCache cache, JsonElement jsonElement)
		{
			ExtensionHelper.AssertJsonValueKindObject(jsonElement);

			jsonElement = ExtensionHelper.GetParentElement(jsonElement);

			List<AssemblyFacilityOverride> facilityOverrides = new();
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
					throw ExtensionHelper.InvalidAssemblyNameException(jsonProperty.Name, ex);
				}

				if (jsonProperty.Value.ValueKind != JsonValueKind.Number)
				{
					throw NotANumberException();
				}
				identifier = jsonProperty.Value.GetInt32();

				facilityOverrides.Add(new AssemblyFacilityOverride(assembly, identifier));
			}

			cache.AddRange(facilityOverrides);
		}

		private static FormatException NotANumberException() => new(TextResources.Global_FromJson_NotANumber);
	}
}
