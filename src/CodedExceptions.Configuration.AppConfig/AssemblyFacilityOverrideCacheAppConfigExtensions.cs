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

using System.Collections.Generic;
using System.ComponentModel;

namespace NerdyDuck.CodedExceptions.Configuration
{
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
}
