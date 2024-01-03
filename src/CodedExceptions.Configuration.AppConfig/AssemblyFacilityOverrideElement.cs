// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Represents an element in the &lt;facilityOverrides&gt; configuration section.
/// </summary>
internal class AssemblyFacilityOverrideElement : ConfigurationElement
{
	private static readonly ConfigurationPropertyCollection s_properties = [];
	private static readonly ConfigurationProperty s_assemblyNameProp = new(GlobalStrings.AssemblyNameKey, typeof(string), "", ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
	private static readonly ConfigurationProperty s_identifierProp = new(GlobalStrings.IdentifierKey, typeof(int), 0, ConfigurationPropertyOptions.IsRequired);

	/// <summary>
	/// Gets or sets the fully or partially qualified name of the assembly to override the facility id for.
	/// </summary>
	[ConfigurationProperty(GlobalStrings.AssemblyNameKey)]
	[StringValidator(InvalidCharacters = GlobalStrings.InvalidNameChars, MinLength = 1)]
	public string AssemblyName
	{
		get => (string)base[s_assemblyNameProp];
		set => base[s_assemblyNameProp] = value;
	}

	/// <summary>
	/// Gets a collection of configuration properties.
	/// </summary>
	protected override ConfigurationPropertyCollection Properties => s_properties;

	/// <summary>
	/// Gets or sets the overriding value for the facility id.
	/// </summary>
	[ConfigurationProperty(GlobalStrings.IdentifierKey)]
	[IntegerValidator(MinValue = 0, MaxValue = 2047)]
	public int Identifier
	{
		get => (int)base[s_identifierProp];
		set => base[s_identifierProp] = value;
	}

	/// <summary>
	/// Gets a value indicating if the element can be modified.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required to override behavior.")]
	public new bool IsReadOnly => false;

	/// <summary>
	/// Static constructor.
	/// </summary>
	static AssemblyFacilityOverrideElement()
	{
		s_properties.Add(s_assemblyNameProp);
		s_properties.Add(s_identifierProp);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyFacilityOverrideElement" /> class.
	/// </summary>
	public AssemblyFacilityOverrideElement()
		: base()
	{
	}

	/// <summary>
	/// Creates a <see cref="AssemblyFacilityOverride" /> object from the current element.
	/// </summary>
	/// <returns>A <see cref="AssemblyFacilityOverride" /> reflecting data of the current element.</returns>
	/// <exception cref="FormatException"><see cref="AssemblyName" /> is not a valid assembly name (fully or partially qualified).</exception>
	public AssemblyFacilityOverride ToOverride()
	{
		try
		{
			return new AssemblyFacilityOverride(AssemblyName, Identifier);
		}
		catch (Exception ex) when (ex is ArgumentException or FormatException)
		{
			throw new FormatException(string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(TextResources.FacilityOverrideElement_ToOverride_Invalid), AssemblyName), ex);
		}
	}
}
