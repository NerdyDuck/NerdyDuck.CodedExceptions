// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Provides methods to read configurations for <see cref="AssemblyDebugModeCache" /> and <see cref="AssemblyFacilityOverrideCache" /> from various sources.
/// </summary>
internal static class ExtensionHelper
{
	/// <summary>
	/// Checks if the object is null.
	/// </summary>
	/// <exception cref="ArgumentNullException">The object is null.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void AssertCache(object cache) =>
#if NETFRAMEWORK
		_ = (cache == null) ? throw new ArgumentNullException(nameof(cache)) : 0;
#else
		ArgumentNullException.ThrowIfNull(cache, nameof(cache));
#endif

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void AssertSectionName(string sectionName)
	{
		if (string.IsNullOrWhiteSpace(sectionName))
		{
			throw new ArgumentException(TextResources.Global_FromApplicationConfiguration_NoSection, nameof(sectionName));
		}
	}
}
