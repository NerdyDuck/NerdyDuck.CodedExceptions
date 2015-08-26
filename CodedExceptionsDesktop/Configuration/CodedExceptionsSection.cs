#region Copyright
/*******************************************************************************
 * <copyright file="CodedExceptionsSection.cs" owner="Daniel Kopp">
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
 * <file name="CodedExceptionsSection.cs" date="2015-08-10">
 * Represents the application configuration file section that can modify the
 * handling of coded exceptions.
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
	/// Represents the application configuration file section that can modify the handling of coded exceptions.
	/// </summary>
	internal class CodedExceptionsSection : ConfigurationSection
	{
		#region Constants
		private const string FacilityOverridesKey = "facilityOverrides";
		internal const string ConfigSectionName = "nerdyDuck/codedExceptions";
		#endregion

		#region ConfigurationProperties
		private static readonly ConfigurationPropertyCollection mProperties = new ConfigurationPropertyCollection();
		private static readonly ConfigurationProperty FacilityOverridesProp = new ConfigurationProperty(FacilityOverridesKey, typeof(FacilityOverrideCollection), new FacilityOverrideCollection());
		#endregion

		#region Properties
		/// <summary>
		/// Gets a collection of facility identifier override configurations.
		/// </summary>
		[ConfigurationProperty(FacilityOverridesKey, IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(FacilityOverrideElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
		public FacilityOverrideCollection FacilityOverrides
		{
			get { return (FacilityOverrideCollection)base[FacilityOverridesProp]; }
		}

		/// <summary>
		/// Gets a value indicating if the element can be modified.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required to override behaviour.")]
		public new bool IsReadOnly
		{
			get { return false; }
		}

		/// <summary>
		/// Gets a collection of configuration properties.
		/// </summary>
		protected override ConfigurationPropertyCollection Properties
		{
			get { return mProperties; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Static constructor.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Cannot add other static fields inlin.e")]
		static CodedExceptionsSection()
		{
			mProperties.Add(FacilityOverridesProp);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedExceptionsSection" /> class.
		/// </summary>
		public CodedExceptionsSection()
			: base()
		{
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Converts the <see cref="CodedExceptionsSection.FacilityOverrides"/> collection into a list of <see cref="FacilityOverride"/> instances.
		/// </summary>
		/// <returns>A list of <see cref="FacilityOverride"/> instances, that can be easily compared to assemblies.</returns>
		public List<FacilityOverride> CreateFacilityOverrides()
		{
			if (FacilityOverrides != null && FacilityOverrides.Count != 0)
			{
				List<FacilityOverride> ReturnValue = new List<FacilityOverride>();
				foreach (FacilityOverrideElement element in FacilityOverrides)
					ReturnValue.Add(element.ToOverride());

				return ReturnValue;
			}

			return null;
		}
		#endregion

		#region Static methods
		/// <summary>
		/// Gets a list of <see cref="FacilityOverride"/>s from the default section in the application configuration file.
		/// </summary>
		/// <returns>A list of <see cref="FacilityOverride"/>s, or <see langword="null"/>, if the default section was not found in the application configuration file, or not configuration file exists.</returns>
		internal static List<FacilityOverride> GetFacilityOverrides()
		{
			return GetFacilityOverrides(ConfigSectionName);
		}

		/// <summary>
		/// Gets a list of <see cref="FacilityOverride"/>s from the specified section in the application configuration file.
		/// </summary>
		/// <param name="sectionName">The name of the configuration section containing the override list.</param>
		/// <returns>A list of <see cref="FacilityOverride"/>s, or <see langword="null"/>, if the default section was not found in the application configuration file, or not configuration file exists.</returns>
		internal static List<FacilityOverride> GetFacilityOverrides(string sectionName)
		{
			CodedExceptionsSection Config =
				(CodedExceptionsSection)ConfigurationManager.GetSection(sectionName);

			if (Config == null)
			{
				return null;
			}

			return Config.CreateFacilityOverrides();
		}
		#endregion
	}
}
