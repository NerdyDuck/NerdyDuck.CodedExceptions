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

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Represents a collection of assembly debug mode configurations.
/// </summary>
internal class AssemblyDebugModeCollection : ConfigurationElementCollection
{
	/// <summary>
	/// Gets or sets a <see cref="AssemblyDebugModeElement"/> at the specified <paramref name="index"/>.
	/// </summary>
	/// <param name="index">The index in the collection to get or set the <see cref="AssemblyDebugModeElement"/>.</param>
	/// <returns>The <see cref="AssemblyDebugModeElement"/> at the specified <paramref name="index"/>.</returns>
	public AssemblyDebugModeElement this[int index]
	{
		get => (AssemblyDebugModeElement)BaseGet(index);
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
	/// Gets the <see cref="AssemblyDebugModeElement"/> with the specified assembly name.
	/// </summary>
	/// <param name="assemblyName">The assembly name of the <see cref="AssemblyDebugModeElement"/>.</param>
	/// <returns>The <see cref="AssemblyDebugModeElement"/> with the specified assembly name, if found; otherwise, <see langword="null"/>.</returns>
	public new AssemblyDebugModeElement this[string assemblyName] => (AssemblyDebugModeElement)BaseGet(assemblyName);

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyDebugModeCollection"/> class.
	/// </summary>
	public AssemblyDebugModeCollection()
		: base()
	{
	}

	/// <summary>
	/// Adds the specified <see cref="AssemblyDebugModeElement"/> to the collection.
	/// </summary>
	/// <param name="element">The <see cref="AssemblyDebugModeElement"/> to add.</param>
	public void Add(AssemblyDebugModeElement element) => BaseAdd(element);

	/// <summary>
	/// Clears all elements from the collection.
	/// </summary>
	public void Clear() => BaseClear();

	/// <summary>
	/// Gets the index of the specified <see cref="AssemblyDebugModeElement"/> in the collection.
	/// </summary>
	/// <param name="element">The <see cref="AssemblyDebugModeElement"/> for the specified index location.</param>
	/// <returns>The index of the specified <see cref="AssemblyDebugModeElement"/>, if found; otherwise, -1;</returns>
	public int IndexOf(AssemblyDebugModeElement element) => BaseIndexOf(element);

	/// <summary>
	/// Removes the specified <see cref="AssemblyDebugModeElement"/> from the collection.
	/// </summary>
	/// <param name="element">The <see cref="AssemblyDebugModeElement"/> to remove.</param>
	public void Remove(AssemblyDebugModeElement element)
	{
		if (BaseIndexOf(element) != -1)
		{
			BaseRemove(element.AssemblyName);
		}
	}

	/// <summary>
	/// Removes the <see cref="AssemblyDebugModeElement"/> with the specified <paramref name="assemblyName"/> from the collection.
	/// </summary>
	/// <param name="assemblyName">The <see cref="AssemblyDebugModeElement.AssemblyName"/> of the <see cref="AssemblyDebugModeElement"/> to remove.</param>
	public void Remove(string assemblyName) => BaseRemove(assemblyName);

	/// <summary>
	/// Removes the <see cref="AssemblyDebugModeElement"/> at the specified <paramref name="index"/> from the collection.
	/// </summary>
	/// <param name="index">The index of the <see cref="AssemblyDebugModeElement"/> to remove from the collection.</param>
	public void RemoveAt(int index) => BaseRemoveAt(index);

	/// <summary>
	/// Creates a new <see cref="AssemblyDebugModeElement"/>.
	/// </summary>
	/// <returns>A <see cref="AssemblyDebugModeElement"/>.</returns>
	protected override ConfigurationElement CreateNewElement() => new AssemblyDebugModeElement();

	/// <summary>
	/// Creates a new <see cref="AssemblyDebugModeElement"/> with the specified assembly name.
	/// </summary>
	/// <param name="elementName">The assembly name of the <see cref="AssemblyDebugModeElement"/> to create.</param>
	/// <returns>A <see cref="AssemblyDebugModeElement"/>.</returns>
	protected override ConfigurationElement CreateNewElement(string elementName) => new AssemblyDebugModeElement() { AssemblyName = elementName };

	/// <summary>
	/// Gets the element key for the specified <see cref="AssemblyDebugModeElement"/>.
	/// </summary>
	/// <param name="element">The <see cref="AssemblyDebugModeElement"/> to return the key for.</param>
	/// <returns>A string that acts as a key for the specified <see cref="AssemblyDebugModeElement"/>.</returns>
	protected override object GetElementKey(ConfigurationElement element) => ((AssemblyDebugModeElement)element).AssemblyName;
}
