// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Represents a collection of facility identifier override configurations.
/// </summary>
internal class AssemblyFacilityOverrideCollection : ConfigurationElementCollection
{
	/// <summary>
	/// Gets or sets a <see cref="AssemblyFacilityOverrideElement"/> at the specified <paramref name="index"/>.
	/// </summary>
	/// <param name="index">The index in the collection to get or set the <see cref="AssemblyFacilityOverrideElement"/>.</param>
	/// <returns>The <see cref="AssemblyFacilityOverrideElement"/> at the specified <paramref name="index"/>.</returns>
	public AssemblyFacilityOverrideElement this[int index]
	{
		get => (AssemblyFacilityOverrideElement)BaseGet(index);
		set
		{
			if (BaseGet(index) != null)
			{
				BaseRemoveAt(index);
			}

			BaseAdd(index, value);
		}
	}

	/// <summary>
	/// Gets the <see cref="AssemblyFacilityOverrideElement"/> with the specified assembly name.
	/// </summary>
	/// <param name="assemblyName">The assembly name of the <see cref="AssemblyFacilityOverrideElement"/>.</param>
	/// <returns>The <see cref="AssemblyFacilityOverrideElement"/> with the specified assembly name, if found; otherwise, <see langword="null"/>.</returns>
	public new AssemblyFacilityOverrideElement this[string assemblyName] => (AssemblyFacilityOverrideElement)BaseGet(assemblyName);

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyFacilityOverrideCollection"/> class.
	/// </summary>
	public AssemblyFacilityOverrideCollection()
		: base()
	{
	}

	/// <summary>
	/// Adds the specified <see cref="AssemblyFacilityOverrideElement"/> to the collection.
	/// </summary>
	/// <param name="element">The <see cref="AssemblyFacilityOverrideElement"/> to add.</param>
	public void Add(AssemblyFacilityOverrideElement element) => BaseAdd(element);

	/// <summary>
	/// Clears all elements from the collection.
	/// </summary>
	public void Clear() => BaseClear();

	/// <summary>
	/// Gets the index of the specified <see cref="AssemblyFacilityOverrideElement"/> in the collection.
	/// </summary>
	/// <param name="element">The <see cref="AssemblyFacilityOverrideElement"/> for the specified index location.</param>
	/// <returns>The index of the specified <see cref="AssemblyFacilityOverrideElement"/>, if found; otherwise, -1;</returns>
	public int IndexOf(AssemblyFacilityOverrideElement element) => BaseIndexOf(element);

	/// <summary>
	/// Removes the specified <see cref="AssemblyFacilityOverrideElement"/> from the collection.
	/// </summary>
	/// <param name="element">The <see cref="AssemblyFacilityOverrideElement"/> to remove.</param>
	public void Remove(AssemblyFacilityOverrideElement element)
	{
		if (BaseIndexOf(element) != -1)
		{
			BaseRemove(element.AssemblyName);
		}
	}

	/// <summary>
	/// Removes the <see cref="AssemblyFacilityOverrideElement"/> with the specified <paramref name="assemblyName"/> from the collection.
	/// </summary>
	/// <param name="assemblyName">The <see cref="AssemblyFacilityOverrideElement.AssemblyName"/> of the <see cref="AssemblyFacilityOverrideElement"/> to remove.</param>
	public void Remove(string assemblyName) => BaseRemove(assemblyName);

	/// <summary>
	/// Removes the <see cref="AssemblyFacilityOverrideElement"/> at the specified <paramref name="index"/> from the collection.
	/// </summary>
	/// <param name="index">The index of the <see cref="AssemblyFacilityOverrideElement"/> to remove from the collection.</param>
	public void RemoveAt(int index) => BaseRemoveAt(index);

	/// <summary>
	/// Creates a new <see cref="AssemblyFacilityOverrideElement"/>.
	/// </summary>
	/// <returns>A <see cref="AssemblyFacilityOverrideElement"/>.</returns>
	protected override ConfigurationElement CreateNewElement() => new AssemblyFacilityOverrideElement();

	/// <summary>
	/// Creates a new <see cref="AssemblyFacilityOverrideElement"/> with the specified assembly name.
	/// </summary>
	/// <param name="elementName">The assembly name of the <see cref="AssemblyFacilityOverrideElement"/> to create.</param>
	/// <returns>A <see cref="AssemblyFacilityOverrideElement"/>.</returns>
	protected override ConfigurationElement CreateNewElement(string elementName) => new AssemblyFacilityOverrideElement() { AssemblyName = elementName };

	/// <summary>
	/// Gets the element key for the specified <see cref="AssemblyFacilityOverrideElement"/>.
	/// </summary>
	/// <param name="element">The <see cref="AssemblyFacilityOverrideElement"/> to return the key for.</param>
	/// <returns>A string that acts as a key for the specified <see cref="AssemblyFacilityOverrideElement"/>.</returns>
	protected override object GetElementKey(ConfigurationElement element) => ((AssemblyFacilityOverrideElement)element).AssemblyName;
}
