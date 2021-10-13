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
using System.Configuration;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Represents the application configuration file section that can modify the handling of coded exceptions.
	/// </summary>
	internal class CodedExceptionsSection : ConfigurationSection
	{
		internal const string DefaultConfigSectionName = "nerdyDuck/codedExceptions";

		private static readonly ConfigurationPropertyCollection s_properties = new();
		private static readonly ConfigurationProperty s_facilityOverridesProp = new(Globals.OverridesNode, typeof(AssemblyFacilityOverrideCollection), new AssemblyFacilityOverrideCollection());
		private static readonly ConfigurationProperty s_debugModesProp = new(Globals.DebugModesNode, typeof(AssemblyDebugModeCollection), new AssemblyDebugModeCollection());

		/// <summary>
		/// Gets a collection of facility identifier override configurations.
		/// </summary>
		[ConfigurationProperty(Globals.DebugModesNode, IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(AssemblyDebugModeElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
		public AssemblyDebugModeCollection DebugModes => (AssemblyDebugModeCollection)base[s_debugModesProp];
		
		/// <summary>
		/// Gets a collection of facility identifier override configurations.
		/// </summary>
		[ConfigurationProperty(Globals.OverridesNode, IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(AssemblyFacilityOverrideElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
		public AssemblyFacilityOverrideCollection FacilityOverrides => (AssemblyFacilityOverrideCollection)base[s_facilityOverridesProp];

		/// <summary>
		/// Gets a value indicating if the element can be modified.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required to override behaviour.")]
		public new bool IsReadOnly => false;

		/// <summary>
		/// Gets a collection of configuration properties.
		/// </summary>
		protected override ConfigurationPropertyCollection Properties => s_properties;

		/// <summary>
		/// Static constructor.
		/// </summary>
		static CodedExceptionsSection()
		{
			s_properties.Add(s_facilityOverridesProp);
			s_properties.Add(s_debugModesProp);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedExceptionsSection" /> class.
		/// </summary>
		public CodedExceptionsSection()
			: base()
		{
		}

		/// <summary>
		/// Converts the <see cref="CodedExceptionsSection.FacilityOverrides"/> collection into a list of <see cref="AssemblyFacilityOverride"/> instances.
		/// </summary>
		/// <returns>A list of <see cref="AssemblyFacilityOverride"/> instances, that can be easily compared to assemblies.</returns>
		public List<AssemblyFacilityOverride>? CreateFacilityOverrides()
		{
			if (FacilityOverrides != null && FacilityOverrides.Count != 0)
			{
				List<AssemblyFacilityOverride> returnValue = new();
				foreach (AssemblyFacilityOverrideElement element in FacilityOverrides)
				{
					returnValue.Add(element.ToOverride());
				}

				return returnValue;
			}

			return null;
		}

		/// <summary>
		/// Converts the <see cref="CodedExceptionsSection.DebugModes"/> collection into a list of <see cref="AssemblyDebugMode"/> instances.
		/// </summary>
		/// <returns>A list of <see cref="AssemblyDebugMode"/> instances, that can be easily compared to assemblies.</returns>
		public List<AssemblyDebugMode>? CreateDebugModes()
		{
			if (DebugModes != null && DebugModes.Count != 0)
			{
				List<AssemblyDebugMode> returnValue = new();
				foreach (AssemblyDebugModeElement element in DebugModes)
				{
					returnValue.Add(element.ToAssemblyDebugMode());
				}

				return returnValue;
			}

			return null;
		}

		/// <summary>
		/// Gets a list of <see cref="AssemblyFacilityOverride"/>s from the default section in the application configuration file.
		/// </summary>
		/// <returns>A list of <see cref="AssemblyFacilityOverride"/>s, or <see langword="null"/>, if the default section was not found in the application configuration file, or no configuration file exists.</returns>
		internal static List<AssemblyFacilityOverride>? GetFacilityOverrides() => GetFacilityOverrides(DefaultConfigSectionName);

		/// <summary>
		/// Gets a list of <see cref="AssemblyFacilityOverride"/>s from the specified section in the application configuration file.
		/// </summary>
		/// <param name="sectionName">The name of the configuration section containing the override list.</param>
		/// <returns>A list of <see cref="AssemblyFacilityOverride"/>s, or <see langword="null"/>, if the default section was not found in the application configuration file, or no configuration file exists.</returns>
		internal static List<AssemblyFacilityOverride>? GetFacilityOverrides(string sectionName) => ((CodedExceptionsSection)ConfigurationManager.GetSection(sectionName))?.CreateFacilityOverrides();

		/// <summary>
		/// Gets a list of <see cref="AssemblyDebugMode"/>s from the default section in the application configuration file.
		/// </summary>
		/// <returns>A list of <see cref="AssemblyDebugMode"/>s, or <see langword="null"/>, if the default section was not found in the application configuration file, or no configuration file exists.</returns>
		internal static List<AssemblyDebugMode>? GetDebugModes() => GetDebugModes(DefaultConfigSectionName);

		/// <summary>
		/// Gets a list of <see cref="AssemblyDebugMode"/>s from the specified section in the application configuration file.
		/// </summary>
		/// <param name="sectionName">The name of the configuration section containing the override list.</param>
		/// <returns>A list of <see cref="AssemblyDebugMode"/>s, or <see langword="null"/>, if the default section was not found in the application configuration file, or no configuration file exists.</returns>
		internal static List<AssemblyDebugMode>? GetDebugModes(string sectionName) => ((CodedExceptionsSection)ConfigurationManager.GetSection(sectionName))?.CreateDebugModes();
	}
}
