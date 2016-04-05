#region Copyright
/*******************************************************************************
 * <copyright file="FacilityOverrideElement.cs" owner="Daniel Kopp">
 * Copyright 2015-2016 Daniel Kopp
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
 * <file name="FacilityOverrideElement.cs" date="2015-08-10">
 * Represents an element in the &lt;facilityOverrides&gt; configuration section.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Represents an element in the &lt;facilityOverrides&gt; configuration section.
	/// </summary>
	/// <remarks>This class is only available for the desktop platform.</remarks>
	internal class FacilityOverrideElement : ConfigurationElement
	{
		#region Constants
		private const string AssemblyNameKey = "assemblyName";
		private const string IdentifierKey = "identifier";
		internal const string InvalidNameChars = "~!@#$%^&*()[]{}/'\"|\\";
		#endregion

		#region ConfigurationProperties
		private static readonly ConfigurationPropertyCollection mProperties = new ConfigurationPropertyCollection();
		private static readonly ConfigurationProperty AssemblyNameProp = new ConfigurationProperty(AssemblyNameKey, typeof(string), "", ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty IdentifierProp = new ConfigurationProperty(IdentifierKey, typeof(int), 0, ConfigurationPropertyOptions.IsRequired);
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the fully or partially qualified name of the assembly to override the facility id for.
		/// </summary>
		[ConfigurationProperty(AssemblyNameKey)]
		[StringValidator(InvalidCharacters = InvalidNameChars, MinLength = 1)]
		public string AssemblyName
		{
			get { return (string)base[AssemblyNameProp]; }
			set { base[AssemblyNameProp] = value; }
		}

		/// <summary>
		/// Gets a collection of configuration properties.
		/// </summary>
		protected override ConfigurationPropertyCollection Properties
		{
			get { return mProperties; }
		}

		/// <summary>
		/// Gets or sets the overriding value for the facility id.
		/// </summary>
		[ConfigurationProperty(IdentifierKey)]
		[IntegerValidator(MinValue = 0, MaxValue = 2047)]
		public int Identifier
		{
			get { return (int)base[IdentifierProp]; }
			set { base[IdentifierProp] = value; }
		}

		/// <summary>
		/// Gets a value indicating if the element can be modified.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required to override behaviour.")]
		public new bool IsReadOnly
		{
			get { return false; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Static constructor.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Cannot add other static fields to collection inline.")]
		static FacilityOverrideElement()
		{
			mProperties.Add(AssemblyNameProp);
			mProperties.Add(IdentifierProp);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FacilityOverrideElement" /> class.
		/// </summary>
		public FacilityOverrideElement()
			: base()
		{
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Creates a <see cref="FacilityOverride" /> object from the current element.
		/// </summary>
		/// <returns>A <see cref="FacilityOverride" /> reflecting data of the current element.</returns>
		/// <exception cref="CodedFormatException"><see cref="AssemblyName" /> is not a valid assembly name (fully or partially qualified).</exception>
		public FacilityOverride ToOverride()
		{
			try
			{
				return new FacilityOverride(AssemblyName, Identifier);
			}
			catch (System.Threading.ThreadAbortException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new CodedFormatException(Errors.CreateHResult(0x0c), string.Format(CodedExceptions.Properties.Resources.FacilityOverrideElement_ToOverride_Invalid, AssemblyName), ex);
			}
		}
		#endregion
	}
}
