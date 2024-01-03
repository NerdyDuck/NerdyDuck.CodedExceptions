// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Extends the <see cref="AssemblyFacilityOverrideCache" /> class with methods to easily add assembly facility identifier overrides from various sources.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class AssemblyFacilityOverrideCacheAppConfigExtensions
{
	/// <summary>
	/// Loads a list of facility identifier overrides from the application configuration file (app.config / web.config) and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the default configuration section.</returns>
	/// <remarks>The overrides are loaded from the default section 'nerdyDuck/codedExceptions'.</remarks>
	public static AssemblyFacilityOverrideCache LoadApplicationConfiguration(this AssemblyFacilityOverrideCache cache)
	{
		ExtensionHelper.AssertCache(cache);
		List<AssemblyFacilityOverride>? afc = CodedExceptionsSection.GetFacilityOverrides();
		if (afc is not null)
		{
			cache.AddRange(afc);
		}

		return cache;
	}

	/// <summary>
	/// Loads a list of facility identifier overrides from the specified section of the application configuration file (app.config / web.config) and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the overrides to.</param>
	/// <param name="sectionName">The name of the section in the application configuration file.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the overrides specified in the specified configuration section.</returns>
	public static AssemblyFacilityOverrideCache LoadApplicationConfiguration(this AssemblyFacilityOverrideCache cache, string sectionName)
	{
		ExtensionHelper.AssertCache(cache);
		ExtensionHelper.AssertSectionName(sectionName);
		List<AssemblyFacilityOverride>? afc = CodedExceptionsSection.GetFacilityOverrides(sectionName);
		if (afc is not null)
		{
			cache.AddRange(afc);
		}

		return cache;
	}
}
