// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Ignore Spelling: json

namespace NerdyDuck.CodedExceptions.Configuration;

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
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the default JSON file, if the file exists.</returns>
	/// <remarks>The default file is named 'AssemblyDebugModes.json' and must reside in the working directory of the application.</remarks>
	/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
	/// <exception cref="IOException">The file could not be opened or read.</exception>
	public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache) => ExtensionHelper.LoadJson(cache, DefaultFileName + ".json", (cache, jsonElement) => FromJson(cache, jsonElement));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the JSON file at the specified path and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="path">The path to the JSON file containing the settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the JSON file.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="path"/> is <see langword="null"/> or white-space.</exception>
	/// <exception cref="IOException">The file could not be opened or read.</exception>
	public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, string path) => ExtensionHelper.LoadJson(cache, path, (cache, jsonElement) => FromJson(cache, jsonElement));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified stream containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="stream">A stream containing JSON-formatted data representing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the JSON stream data.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="stream"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="stream"/> is not readable.</exception>
	/// <exception cref="IOException">The stream data could not be read.</exception>
	public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, Stream stream) => ExtensionHelper.LoadJson(cache, stream, (cache, jsonElement) => FromJson(cache, jsonElement));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified TextReader containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the JSON data of the <paramref name="reader"/>.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="cache"/> or <paramref name="reader"/> is <see langword="null"/>.</exception>
	/// <exception cref="IOException">The stream data could not be read.</exception>
	public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, TextReader reader) => ExtensionHelper.LoadJson(cache, reader ?? throw new ArgumentNullException(nameof(reader)), (cache, jsonElement) => FromJson(cache, jsonElement));

#if !NETFRAMEWORK
	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the JSON data of the byte sequence.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
	/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
	public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, ReadOnlySequence<byte> json) => ExtensionHelper.LoadJson(cache, json, (cache, jsonElement) => FromJson(cache, jsonElement));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the JSON data of the byte memory.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
	/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
	public static AssemblyDebugModeCache LoadJson(this AssemblyDebugModeCache cache, ReadOnlyMemory<byte> json) => ExtensionHelper.LoadJson(cache, json, (cache, jsonElement) => FromJson(cache, jsonElement));
#endif

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified string containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="content">A string containing JSON-formatted data representing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the JSON string.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/> or <paramref name="content"/> is <see langword="null"/> or empty.</exception>
	/// <exception cref="IOException">The stream data could not be read.</exception>
	public static AssemblyDebugModeCache ParseJson(this AssemblyDebugModeCache cache, string content) => ExtensionHelper.ParseJson(cache, content, (cache, jsonElement) => FromJson(cache, jsonElement));

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified JSON object, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="jsonElement">The <see cref="JsonElement"/> containing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the <see cref="JsonElement"/>.</returns>
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
		cache.AddRange(ExtensionHelper.FromJsonInternal(jsonElement, CheckAndConvert, (assembly, debugMode) => new AssemblyDebugMode(assembly, debugMode)));
		static bool CheckAndConvert(JsonProperty jsonProperty) => jsonProperty.Value.ValueKind is not JsonValueKind.True and not JsonValueKind.False ? throw NotABoolException() : jsonProperty.Value.GetBoolean();
		static FormatException NotABoolException() => new(TextResources.Global_FromJson_NotABool);
	}
}
