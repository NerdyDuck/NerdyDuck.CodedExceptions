// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Ignore Spelling: json

namespace NerdyDuck.CodedExceptions.Configuration;

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
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the default JSON file, if the file exists.</returns>
	/// <remarks>The default file is named 'FacilityIdentifierOverrides.json' and must reside in the working directory of the application.</remarks>
	public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache) => ExtensionHelper.LoadJson(cache, DefaultFileName + ".json", FromJsonInternal);

	/// <summary>
	/// Loads a list of facility identifier overrides from the JSON file at the specified path and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="path">The path to the JSON file containing the overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the JSON file.</returns>
	public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, string path) => ExtensionHelper.LoadJson(cache, path, FromJsonInternal);

	/// <summary>
	/// Loads a list of facility identifier overrides from the specified stream containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="stream">A stream containing JSON-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the JSON stream data.</returns>
	public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, Stream stream) => ExtensionHelper.LoadJson(cache, stream, FromJsonInternal);

	/// <summary>
	/// Loads a list of facility identifier overrides from the specified TextReader containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="reader">A <see cref="TextReader"/> containing JSON-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the JSON data of the <paramref name="reader"/>.</returns>
	public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, TextReader reader) => ExtensionHelper.LoadJson(cache, reader ?? throw new ArgumentNullException(nameof(reader)), FromJsonInternal);

#if !NETFRAMEWORK
	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the JSON data of the byte sequence.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
	/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
	public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, ReadOnlySequence<byte> json) => ExtensionHelper.LoadJson(cache, json, FromJsonInternal);

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified sequence of bytes containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="json">A sequence of bytes containing UTF8-encoded, JSON-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the JSON data of the byte memory.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="cache"/> is <see langword="null"/>.</exception>
	/// <exception cref="IOException">The JSON document or contents are invalid.</exception>
	public static AssemblyFacilityOverrideCache LoadJson(this AssemblyFacilityOverrideCache cache, ReadOnlyMemory<byte> json) => ExtensionHelper.LoadJson(cache, json, FromJsonInternal);
#endif

	/// <summary>
	/// Parses a list of facility identifier overrides from the specified string containing JSON data, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="content">A string containing JSON-formatted data representing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the JSON string.</returns>
	public static AssemblyFacilityOverrideCache ParseJson(this AssemblyFacilityOverrideCache cache, string content) => ExtensionHelper.ParseJson(cache, content, FromJsonInternal);

	/// <summary>
	/// Loads a list of facility identifier overrides from the specified JSON object, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="jsonElement">A <see cref="JsonElement"/> containing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the <see cref="JsonElement"/>.</returns>
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
		cache.AddRange(ExtensionHelper.FromJsonInternal(jsonElement, CheckAndConvert, (assembly, identifier) => new AssemblyFacilityOverride(assembly, identifier)));

		static int CheckAndConvert(JsonProperty jsonProperty) => jsonProperty.Value.ValueKind != JsonValueKind.Number ? throw NotANumberException() : jsonProperty.Value.GetInt32();
		static FormatException NotANumberException() => new(TextResources.Global_FromJson_NotANumber);
	}
}
