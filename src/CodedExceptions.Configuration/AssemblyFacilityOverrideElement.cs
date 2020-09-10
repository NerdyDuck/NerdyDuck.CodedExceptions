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
using System.Configuration;
using System.Globalization;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Represents an element in the &lt;facilityOverrides&gt; configuration section.
	/// </summary>
	internal class AssemblyFacilityOverrideElement : ConfigurationElement
	{
		#region ConfigurationProperties
		private static readonly ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
		private static readonly ConfigurationProperty s_assemblyNameProp = new ConfigurationProperty(Globals.AssemblyNameKey, typeof(string), "", ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty s_identifierProp = new ConfigurationProperty(Globals.IdentifierKey, typeof(int), 0, ConfigurationPropertyOptions.IsRequired);
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the fully or partially qualified name of the assembly to override the facility id for.
		/// </summary>
		[ConfigurationProperty(Globals.AssemblyNameKey)]
		[StringValidator(InvalidCharacters = Globals.InvalidNameChars, MinLength = 1)]
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
		[ConfigurationProperty(Globals.IdentifierKey)]
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
		#endregion

		#region Constructors
		/// <summary>
		/// Static constructor.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Cannot add other static fields to collection inline.")]
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
		#endregion

		#region Public methods
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
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
			{
				throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.FacilityOverrideElement_ToOverride_Invalid, AssemblyName), ex);
			}
		}
		#endregion
	}
}
