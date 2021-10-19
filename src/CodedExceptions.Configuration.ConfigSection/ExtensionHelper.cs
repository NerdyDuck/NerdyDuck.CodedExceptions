#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.CodedExceptions.Configuration - Unit tests for the
 * NerdyDuck.CodedExceptions.Configuration assembly
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

using System.Runtime.CompilerServices;

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Provides methods to read configurations for <see cref="AssemblyDebugModeCache" /> and <see cref="AssemblyFacilityOverrideCache" /> from various sources.
/// </summary>
internal static class ExtensionHelper
{
	internal static FormatException InvalidAssemblyNameException(string assemblyName, Exception ex) => new(string.Format(CultureInfo.CurrentCulture, TextResources.Global_AssemblyNameInvalid, assemblyName), ex);

	/// <summary>
	/// Checks if the object is null.
	/// </summary>
	/// <exception cref="ArgumentNullException">The object is null.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void AssertCache(object cache)
	{
		if (cache == null)
		{
			throw new ArgumentNullException(nameof(cache));
		}
	}

	/// <summary>
	/// Checks if the element is null.
	/// </summary>
	/// <param name="configuration">The element to check.</param>
	/// <exception cref="ArgumentNullException">The element is null.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void AssertConfiguration(IConfiguration configuration)
	{
		if (configuration == null)
		{
			throw new ArgumentNullException(nameof(configuration));
		}
	}

	internal static List<TTarget> LoadConfigurationSection<TTarget, TValue>(IConfiguration source, Func<AssemblyIdentity, TValue, TTarget> constructor, Func<string, TValue> converter)
	{
		List<TTarget> result = new();
		AssemblyIdentity assembly;
		TValue convertedValue;

		foreach (KeyValuePair<string, string> pair in source.AsEnumerable(true))
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
				convertedValue = converter(pair.Value);
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
