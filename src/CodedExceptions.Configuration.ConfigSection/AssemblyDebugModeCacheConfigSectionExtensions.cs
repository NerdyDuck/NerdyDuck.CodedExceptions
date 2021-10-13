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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;

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
	/// <remarks>The section must be keyed by the assembly names (deserializable into an <see cref="AssemblyIdentity"/>), while the values specify the whether the debug mode is enabled or not (true/false).</remarks>
	[CLSCompliant(false)]
	public static AssemblyDebugModeCache LoadConfigurationSection(this AssemblyDebugModeCache cache, IConfiguration configuration)
	{
		ExtensionHelper.AssertCache(cache);
		ExtensionHelper.AssertConfiguration(configuration);

		List<AssemblyDebugMode> debugModes = new();
		AssemblyIdentity assembly;
		bool isEnabled;

		foreach (KeyValuePair<string, string> pair in configuration.AsEnumerable(true))
		{
			try
			{
				assembly = new AssemblyIdentity(pair.Key);
			}
			catch (FormatException ex)
			{
				throw ExtensionHelper.InvalidAssemblyNameException(pair.Key, ex);
			}

			if (string.IsNullOrWhiteSpace(pair.Value))
			{
				throw new FormatException(TextResources.Global_IdentifierEmpty);
			}
			try
			{
				isEnabled = Convert.ToBoolean(pair.Value, CultureInfo.InvariantCulture);
			}
			catch (FormatException ex)
			{
				throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.Global_IsEnabledInvalid, pair.Key), ex);
			}

			debugModes.Add(new AssemblyDebugMode(assembly, isEnabled));
		}

		cache.AddRange(debugModes);
		return cache;
	}
}
