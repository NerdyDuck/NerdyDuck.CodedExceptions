#region Copyright
/*******************************************************************************
 * <copyright file="FacilityOverrideCollection.cs" owner="Daniel Kopp">
 * Copyright 2015 Daniel Kopp
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * </copyright>
 * <author name="Daniel Kopp" email="dak@nerdyduck.de" />
 * <assembly name="NerdyDuck.CodedExceptions">
 * Exceptions with custom HRESULTs for .NET
 * </assembly>
 * <file name="FacilityOverrideCollection.cs" date="2015-08-10">
 * Represents a collection of facility identifier override configurations.
 * </file>
 ******************************************************************************/
#endregion

using System.Configuration;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Represents a collection of facility identifier override configurations.
	/// </summary>
	/// <remarks>This class is only available for the desktop platform.</remarks>
	internal class FacilityOverrideCollection : ConfigurationElementCollection
	{
		#region Properties
		/// <summary>
		/// Gets or sets a <see cref="FacilityOverrideElement"/> at the specified <paramref name="index"/>.
		/// </summary>
		/// <param name="index">The index in the collection to get or set the <see cref="FacilityOverrideElement"/>.</param>
		/// <returns>The <see cref="FacilityOverrideElement"/> at the specified <paramref name="index"/>.</returns>
		public FacilityOverrideElement this[int index]
		{
			get { return (FacilityOverrideElement)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
					BaseRemoveAt(index);
				BaseAdd(index, value);
			}
		}

		/// <summary>
		/// Gets the <see cref="FacilityOverrideElement"/> with the specified assembly name.
		/// </summary>
		/// <param name="assemblyName">The assembly name of the <see cref="FacilityOverrideElement"/>.</param>
		/// <returns>The <see cref="FacilityOverrideElement"/> with the specified assembly name, if found; otherwise, <see langword="null"/>.</returns>
		public new FacilityOverrideElement this[string assemblyName]
		{
			get { return (FacilityOverrideElement)BaseGet(assemblyName); }
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="FacilityOverrideCollection"/> class.
		/// </summary>
		public FacilityOverrideCollection()
			: base()
		{
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Adds the specified <see cref="FacilityOverrideElement"/> to the collection.
		/// </summary>
		/// <param name="element">The <see cref="FacilityOverrideElement"/> to add.</param>
		public void Add(FacilityOverrideElement element)
		{
			BaseAdd(element);
		}

		/// <summary>
		/// Clears all elements from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}

		/// <summary>
		/// Gets the index of the specified <see cref="FacilityOverrideElement"/> in the collection.
		/// </summary>
		/// <param name="element">The <see cref="FacilityOverrideElement"/> for the specified index location.</param>
		/// <returns>The index of the specified <see cref="FacilityOverrideElement"/>, if found; otherwise, -1;</returns>
		public int IndexOf(FacilityOverrideElement element)
		{
			return BaseIndexOf(element);
		}

		/// <summary>
		/// Removes the specified <see cref="FacilityOverrideElement"/> from the collection.
		/// </summary>
		/// <param name="element">The <see cref="FacilityOverrideElement"/> to remove.</param>
		public void Remove(FacilityOverrideElement element)
		{
			if (BaseIndexOf(element) != -1)
				BaseRemove(element.AssemblyName);
		}

		/// <summary>
		/// Removes the <see cref="FacilityOverrideElement"/> with the specified <paramref name="assemblyName"/> from the collection.
		/// </summary>
		/// <param name="assemblyName">The <see cref="FacilityOverrideElement.AssemblyName"/> of the <see cref="FacilityOverrideElement"/> to remove.</param>
		public void Remove(string assemblyName)
		{
			BaseRemove(assemblyName);
		}

		/// <summary>
		/// Removes the <see cref="FacilityOverrideElement"/> at the specified <paramref name="index"/> from the collection.
		/// </summary>
		/// <param name="index">The index of the <see cref="FacilityOverrideElement"/> to remove from the collection.</param>
		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}
		#endregion

		#region Protected methods
		/// <summary>
		/// Creates a new <see cref="FacilityOverrideElement"/>.
		/// </summary>
		/// <returns>A <see cref="FacilityOverrideElement"/>.</returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new FacilityOverrideElement();
		}

		/// <summary>
		/// Creates a new <see cref="FacilityOverrideElement"/> with the specified assembly name.
		/// </summary>
		/// <param name="elementName">The assembly name of the <see cref="FacilityOverrideElement"/> to create.</param>
		/// <returns>A <see cref="FacilityOverrideElement"/>.</returns>
		protected override ConfigurationElement CreateNewElement(string elementName)
		{
			return new FacilityOverrideElement() { AssemblyName = elementName };
		}

		/// <summary>
		/// Gets the element key for the specified <see cref="FacilityOverrideElement"/>.
		/// </summary>
		/// <param name="element">The <see cref="FacilityOverrideElement"/> to return the key for.</param>
		/// <returns>A string that acts as a key for the specified <see cref="FacilityOverrideElement"/>.</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FacilityOverrideElement)element).AssemblyName;
		}
		#endregion
	}
}
