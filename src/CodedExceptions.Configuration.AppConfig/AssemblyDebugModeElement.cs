// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Represents an element in the &lt;debugModes&gt; configuration section.
/// </summary>
internal class AssemblyDebugModeElement : ConfigurationElement
{
	private static readonly ConfigurationPropertyCollection s_properties = [];
	private static readonly ConfigurationProperty s_assemblyNameProp = new(GlobalStrings.AssemblyNameKey, typeof(string), "", ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
	private static readonly ConfigurationProperty s_isEnabledProp = new(GlobalStrings.IsEnabledKey, typeof(bool), true, ConfigurationPropertyOptions.None);

	/// <summary>
	/// Gets or sets the fully or partially qualified name of the assembly to set the debug mode id for.
	/// </summary>
	[ConfigurationProperty(GlobalStrings.AssemblyNameKey)]
	[StringValidator(InvalidCharacters = GlobalStrings.InvalidNameChars, MinLength = 1)]
	public string AssemblyName
	{
		get => (string)base[s_assemblyNameProp];
		set => base[s_assemblyNameProp] = value;
	}

	/// <summary>
	/// Gets or sets a value indicating if the debug mode is set for the assembly specified in <see cref="AssemblyName"/>.
	/// </summary>
	[ConfigurationProperty(GlobalStrings.IsEnabledKey)]
	public bool IsEnabled
	{
		get => (bool)base[s_isEnabledProp];
		set => base[s_isEnabledProp] = value;
	}

	/// <summary>
	/// Gets a collection of configuration properties.
	/// </summary>
	protected override ConfigurationPropertyCollection Properties => s_properties;

	/// <summary>
	/// Gets a value indicating if the element can be modified.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required to override behavior.")]
	public new bool IsReadOnly => false;

	/// <summary>
	/// Static constructor.
	/// </summary>
	static AssemblyDebugModeElement()
	{
		s_properties.Add(s_assemblyNameProp);
		s_properties.Add(s_isEnabledProp);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyDebugModeElement" /> class.
	/// </summary>
	public AssemblyDebugModeElement()
		: base()
	{
	}

	/// <summary>
	/// Creates a <see cref="AssemblyDebugMode" /> object from the current element.
	/// </summary>
	/// <returns>A <see cref="AssemblyDebugMode" /> reflecting data of the current element.</returns>
	/// <exception cref="FormatException"><see cref="AssemblyName" /> is not a valid assembly name (fully or partially qualified).</exception>
	public AssemblyDebugMode ToAssemblyDebugMode()
	{
		try
		{
			return new AssemblyDebugMode(AssemblyName, IsEnabled);
		}
		catch (Exception ex) when (ex is ArgumentException or FormatException)
		{
			throw new FormatException(string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(TextResources.DebugModeElement_ToAssemblyIdentity_Invalid), AssemblyName), ex);
		}
	}
}
