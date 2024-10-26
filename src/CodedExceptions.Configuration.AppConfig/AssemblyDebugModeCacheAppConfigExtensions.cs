// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Extends the <see cref="AssemblyDebugModeCache" /> class with methods to easily add debug mode settings from various sources.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class AssemblyDebugModeCacheAppConfigExtensions
{
	/// <summary>
	/// Loads a list of assembly debug mode settings from the application configuration file (app.config / web.config) and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the default configuration section.</returns>
	/// <remarks>The settings are loaded from the default section 'nerdyDuck/codedExceptions'.</remarks>
	public static AssemblyDebugModeCache LoadApplicationConfiguration(this AssemblyDebugModeCache cache)
	{
#if NET5_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(cache);
#else
		if (cache == null)
		{
			throw new ArgumentNullException(nameof(cache));
		}
#endif
		List<AssemblyDebugMode>? adm = CodedExceptionsSection.GetDebugModes();
		if (adm is not null)
		{
			cache.AddRange(adm);
		}

		return cache;
	}

	/// <summary>
	/// Loads a list of assembly debug mode settings from the specified section of the application configuration file (app.config / web.config) and adds them to the cache.
	/// </summary>
	/// <param name="cache">The cache to add the settings to.</param>
	/// <param name="sectionName">The name of the section in the application configuration file.</param>
	/// <returns>The specified <paramref name="cache"/> object, containing the debug mode settings specified in the specified configuration section.</returns>
	public static AssemblyDebugModeCache LoadApplicationConfiguration(this AssemblyDebugModeCache cache, string sectionName)
	{
#if NET5_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(cache);
#else
		if (cache == null)
		{
			throw new ArgumentNullException(nameof(cache));
		}
#endif
#if NET7_0_OR_GREATER
		ArgumentException.ThrowIfNullOrEmpty(sectionName);
#else
		if (string.IsNullOrWhiteSpace(sectionName))
		{
			throw new ArgumentException(SR.Load_NullOrEmpty, nameof(sectionName));
		}
#endif

		List<AssemblyDebugMode>? adm = CodedExceptionsSection.GetDebugModes(sectionName);
		if (adm is not null)
		{
			cache.AddRange(adm);
		}

		return cache;
	}
}
