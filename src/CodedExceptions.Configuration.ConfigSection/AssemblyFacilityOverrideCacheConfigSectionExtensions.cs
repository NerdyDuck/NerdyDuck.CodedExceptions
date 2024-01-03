// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Xml;

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Extends the <see cref="AssemblyFacilityOverrideCache" /> class with methods to easily add assembly facility identifier overrides from various sources.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class AssemblyFacilityOverrideCacheConfigSectionExtensions
{
	/// <summary>
	/// Loads a list of facility identifier overrides from the specified <see cref="IConfiguration"/>, and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="configuration">A <see cref="IConfiguration"/> containing overrides.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the <paramref name="configuration"/>.</returns>
	/// <remarks>The section must be keyed by the assembly names (deserializable into an <see cref="AssemblyIdentity"/>), while the values specify the identifier overrides.</remarks>
	[CLSCompliant(false)]
	public static AssemblyFacilityOverrideCache LoadConfigurationSection(this AssemblyFacilityOverrideCache cache, IConfiguration configuration)
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
		cache.AddRange(ExtensionHelper.LoadConfigurationSection(configuration, (assembly, identifier) => new AssemblyFacilityOverride(assembly, identifier), (stringValue) => Convert.ToInt32(stringValue, CultureInfo.InvariantCulture)));
		return cache;
	}
}
