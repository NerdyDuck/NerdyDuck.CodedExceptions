// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Provides methods to read configurations for <see cref="AssemblyDebugModeCache" /> and <see cref="AssemblyFacilityOverrideCache" /> from various sources.
/// </summary>
internal static class ExtensionHelper
{
	internal static FormatException InvalidAssemblyNameException(string assemblyName, Exception ex) => new(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, assemblyName), ex);

	internal static List<TTarget> LoadConfigurationSection<TTarget, TValue>(IConfiguration source, Func<AssemblyIdentity, TValue, TTarget> constructor, Func<string, TValue> converter)
	{
		List<TTarget> result = [];
		AssemblyIdentity assembly;
		TValue convertedValue;

		foreach (KeyValuePair<string, string?> pair in source.AsEnumerable(true))
		{
			try
			{
				assembly = new AssemblyIdentity(pair.Key);
			}
			catch (FormatException ex)
			{
				throw InvalidAssemblyNameException(pair.Key, ex);
			}
			if (string.IsNullOrWhiteSpace(pair.Value))
			{
				throw new FormatException(TextResources.Global_IdentifierEmpty);
			}

			try
			{
				convertedValue = converter(pair.Value!); // pair.Value checked one block above
			}
			catch (FormatException ex)
			{
				throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_IdentifierInvalid, pair.Key), ex);
			}

			result.Add(constructor(assembly, convertedValue));
		}

		return result;
	}
}
