// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Represents the application configuration file section that can modify the handling of coded exceptions.
/// </summary>
internal class CodedExceptionsSection : ConfigurationSection
{
	internal const string DefaultConfigSectionName = "nerdyDuck/codedExceptions";

	private static readonly ConfigurationPropertyCollection s_properties = [];
	private static readonly ConfigurationProperty s_facilityOverridesProp = new(GlobalStrings.OverridesNode, typeof(AssemblyFacilityOverrideCollection), new AssemblyFacilityOverrideCollection());
	private static readonly ConfigurationProperty s_debugModesProp = new(GlobalStrings.DebugModesNode, typeof(AssemblyDebugModeCollection), new AssemblyDebugModeCollection());

	/// <summary>
	/// Gets a collection of facility identifier override configurations.
	/// </summary>
	[ConfigurationProperty(GlobalStrings.DebugModesNode, IsDefaultCollection = false)]
	[ConfigurationCollection(typeof(AssemblyDebugModeElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public AssemblyDebugModeCollection DebugModes => (AssemblyDebugModeCollection)base[s_debugModesProp];

	/// <summary>
	/// Gets a collection of facility identifier override configurations.
	/// </summary>
	[ConfigurationProperty(GlobalStrings.OverridesNode, IsDefaultCollection = false)]
	[ConfigurationCollection(typeof(AssemblyFacilityOverrideElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public AssemblyFacilityOverrideCollection FacilityOverrides => (AssemblyFacilityOverrideCollection)base[s_facilityOverridesProp];

	/// <summary>
	/// Gets a value indicating if the element can be modified.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required to override behavior.")]
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
			List<AssemblyFacilityOverride> returnValue = [];
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
			List<AssemblyDebugMode> returnValue = [];
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
