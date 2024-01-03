// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Extends the <see cref="AssemblyDebugModeCache" /> class with methods to easily add debug mode settings from various sources.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class AssemblyDebugModeCacheConfigSectionExtensions
{
	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified <see cref="IConfiguration"/>, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="configuration">A <see cref="IConfiguration"/> containing debug mode settings.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the <paramref name="configuration"/>.</returns>
	/// <remarks>The section must be keyed by the assembly names (deserializable into an <see cref="AssemblyIdentity"/>), while the values specify the whether the debug mode is enabled or not (true/false).</remarks>
	[CLSCompliant(false)]
	public static AssemblyDebugModeCache LoadConfigurationSection(this AssemblyDebugModeCache cache, IConfiguration configuration)
	{
#if NETFRAMEWORK
		if (cache == null)
		{
			throw new ArgumentNullException(nameof(cache));
		}
		if (configuration == null)
		{
			throw new ArgumentNullException(nameof(configuration));
		}
#else
		ArgumentNullException.ThrowIfNull(cache, nameof(cache));
		ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
#endif
		cache.AddRange(ExtensionHelper.LoadConfigurationSection(configuration, (assembly, debugMode) => new AssemblyDebugMode(assembly, debugMode), (stringValue) => Convert.ToBoolean(stringValue, CultureInfo.InvariantCulture)));
		return cache;
	}
}
