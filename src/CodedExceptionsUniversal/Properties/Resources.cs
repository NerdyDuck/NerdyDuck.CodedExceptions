#region Copyright
/*******************************************************************************
 * <copyright file="Resources.cs" owner="Daniel Kopp">
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
 * <file name="Resources.cs" date="2016-04-12">
 * Helper class to access localized string resources.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Globalization;

namespace NerdyDuck.CodedExceptions.Properties
{
	/// <summary>
	/// Helper class to access localized string resources.
	/// </summary>
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Resources.tt", "1.0.0.0")]
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
	internal static class Resources
	{
		#region String resource properties
		/// <summary>
		/// Gets a localized string similar to "{0}{1}---&gt; (Inner exception #{2}) {3}&lt;---{1}".
		/// </summary>
		internal static string CodedAggregateException_ToString
		{
			get { return GetResource("CodedAggregateException_ToString"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Argument cannot be null or empty.".
		/// </summary>
		internal static string CodedArgumentNullOrEmptyException_Message
		{
			get { return GetResource("CodedArgumentNullOrEmptyException_Message"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Argument cannot be null or empty, or contains only white-space characters.".
		/// </summary>
		internal static string CodedArgumentNullOrWhiteSpaceException_Message
		{
			get { return GetResource("CodedArgumentNullOrWhiteSpaceException_Message"); }
		}

		/// <summary>
		/// Gets a localized string similar to "The data is invalid.".
		/// </summary>
		internal static string CodedDataException_Message
		{
			get { return GetResource("CodedDataException_Message"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Could not find the specified directory.".
		/// </summary>
		internal static string CodedDirectoryNotFoundException_Message
		{
			get { return GetResource("CodedDirectoryNotFoundException_Message"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Could not find directory '{0}'.".
		/// </summary>
		internal static string CodedDirectoryNotFoundException_MessageDirectory
		{
			get { return GetResource("CodedDirectoryNotFoundException_MessageDirectory"); }
		}

		/// <summary>
		/// Gets a localized string similar to "The specified file already exists.".
		/// </summary>
		internal static string CodedFileExistsException_Message
		{
			get { return GetResource("CodedFileExistsException_Message"); }
		}

		/// <summary>
		/// Gets a localized string similar to "File '{0}' already exists.".
		/// </summary>
		internal static string CodedFileExistsException_MessageFile
		{
			get { return GetResource("CodedFileExistsException_MessageFile"); }
		}

		/// <summary>
		/// Gets a localized string similar to "The specified file is invalid".
		/// </summary>
		internal static string CodedInvalidFileException_Message
		{
			get { return GetResource("CodedInvalidFileException_Message"); }
		}

		/// <summary>
		/// Gets a localized string similar to "File '{0}' is invalid.".
		/// </summary>
		internal static string CodedInvalidFileException_MessageFile
		{
			get { return GetResource("CodedInvalidFileException_MessageFile"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Invalid facility identifier override element in application configuration. Assembly name: '{0}'.".
		/// </summary>
		internal static string FacilityOverrideElement_ToOverride_Invalid
		{
			get { return GetResource("FacilityOverrideElement_ToOverride_Invalid"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Directory name: '{0}'".
		/// </summary>
		internal static string Global_DirectoryName
		{
			get { return GetResource("Global_DirectoryName"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Facility id must range between 0 and 2047.".
		/// </summary>
		internal static string Global_FacilityId_OutOfRange
		{
			get { return GetResource("Global_FacilityId_OutOfRange"); }
		}

		/// <summary>
		/// Gets a localized string similar to "File name: '{0}'".
		/// </summary>
		internal static string Global_FileName
		{
			get { return GetResource("Global_FileName"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Enumeration is not based on Int32 data type.".
		/// </summary>
		internal static string HResultHelper_GetEnumInt32Value_NotInt32
		{
			get { return GetResource("HResultHelper_GetEnumInt32Value_NotInt32"); }
		}

		/// <summary>
		/// Gets a localized string similar to "The Enum type should contain one and only one instance field.".
		/// </summary>
		internal static string HResultHelper_GetEnumUnderlyingType_EnumInvalid
		{
			get { return GetResource("HResultHelper_GetEnumUnderlyingType_EnumInvalid"); }
		}

		/// <summary>
		/// Gets a localized string similar to "The provided Type must be Enum.".
		/// </summary>
		internal static string HResultHelper_GetEnumUnderlyingType_MustBeEnum
		{
			get { return GetResource("HResultHelper_GetEnumUnderlyingType_MustBeEnum"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Element '{0}' has no attribute '{1}'.".
		/// </summary>
		internal static string XmlFacilityOverrides_GetFacilityOverrides_AttributeMissing
		{
			get { return GetResource("XmlFacilityOverrides_GetFacilityOverrides_AttributeMissing"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Facility identifier overrides file '{0}' is invalid.".
		/// </summary>
		internal static string XmlFacilityOverrides_GetFacilityOverrides_FileInvalid
		{
			get { return GetResource("XmlFacilityOverrides_GetFacilityOverrides_FileInvalid"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Cannot open facility identifier overrides file '{0}'.".
		/// </summary>
		internal static string XmlFacilityOverrides_GetFacilityOverrides_FileOpenFailed
		{
			get { return GetResource("XmlFacilityOverrides_GetFacilityOverrides_FileOpenFailed"); }
		}

		/// <summary>
		/// Gets a localized string similar to "Identifier override of assembly '{0}' is invalid.".
		/// </summary>
		internal static string XmlFacilityOverrides_GetFacilityOverrides_IdentifierInvalid
		{
			get { return GetResource("XmlFacilityOverrides_GetFacilityOverrides_IdentifierInvalid"); }
		}
		#endregion

#if WINDOWS_UWP
		#region Private fields
		private static Windows.ApplicationModel.Resources.Core.ResourceMap mResourceMap;
		private static Windows.ApplicationModel.Resources.Core.ResourceContext mContext;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the main resource map of the assembly.
		/// </summary>
		internal static Windows.ApplicationModel.Resources.Core.ResourceMap ResourceMap
		{
			get
			{
				if (object.ReferenceEquals(mResourceMap, null))
				{
					mResourceMap = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap;
				}

				return mResourceMap;
			}
		}

		/// <summary>
		/// Gets or sets the resource context to use when retrieving resources.
		/// </summary>
		internal static Windows.ApplicationModel.Resources.Core.ResourceContext Context
		{
			get { return mContext; }
			set { mContext = value; }
		}
		#endregion

		#region Methods
		/// <summary>
		/// Retrieves a string resource using the resource map.
		/// </summary>
		/// <param name="name">The name of the string resource.</param>
		/// <returns>A localized string.</returns>
		internal static string GetResource(string name)
		{
			Windows.ApplicationModel.Resources.Core.ResourceContext context = Context;
			if (context == null)
			{
				context = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse();
			}

			Windows.ApplicationModel.Resources.Core.ResourceCandidate resourceCandidate = ResourceMap.GetValue("NerdyDuck.CodedExceptions/Resources/" + name, context);

			if (resourceCandidate == null)
			{
				throw new ArgumentOutOfRangeException(nameof(name));
			}

			return resourceCandidate.ValueAsString;
		}

		/// <summary>
		/// Retrieves a string resource for the specified culture using the resource map.
		/// </summary>
		/// <param name="name">The name of the string resource.</param>
		/// <param name="culture">The culture to retrieve a matching string for. May be <see langword="null"/>.</param>
		/// <returns>A localized string.</returns>
		internal static string GetResource(string name, CultureInfo culture)
		{
			Windows.ApplicationModel.Resources.Core.ResourceContext context;
			if (culture == null || culture.IsNeutralCulture)
			{
				context = Context;
				if (context == null)
				{
					context = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse();
				}
			}
			else
			{
				context = new Windows.ApplicationModel.Resources.Core.ResourceContext();
				context.Languages = new string[] { culture.TwoLetterISOLanguageName };
			}

			Windows.ApplicationModel.Resources.Core.ResourceCandidate resourceCandidate = ResourceMap.GetValue("NerdyDuck.Logging/Resources/" + name, context);

			if (resourceCandidate == null)
			{
				throw new ArgumentOutOfRangeException(nameof(name));
			}

			return resourceCandidate.ValueAsString;
		}
		#endregion
#endif

#if WINDOWS_DESKTOP
		#region Private fields
		private static System.Resources.ResourceManager mResourceManager;
		private static System.Globalization.CultureInfo mResourceCulture;
		#endregion

		#region Properties
		/// <summary>
		/// Returns the cached ResourceManager instance used by this class.
		/// </summary>
		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(mResourceManager, null))
				{
					System.Resources.ResourceManager temp = new System.Resources.ResourceManager("NerdyDuck.CodedExceptions.Properties.Resources", typeof(Resources).Assembly);
					mResourceManager = temp;
				}
				return mResourceManager;
			}
		}

		/// <summary>
		/// Overrides the current thread's CurrentUICulture property for all resource lookups using this strongly typed resource class.
		/// </summary>
		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static System.Globalization.CultureInfo Culture
		{
			get { return mResourceCulture; }
			set { mResourceCulture = value; }
		}
		#endregion

		#region Methods
		/// <summary>
		/// Retrieves a string resource using the resource manager.
		/// </summary>
		/// <param name="name">The name of the string resource.</param>
		/// <returns>A localized string.</returns>
		internal static string GetResource(string name)
		{
			return ResourceManager.GetString(name, mResourceCulture);
		}

		/// <summary>
		/// Retrieves a string resource for the specified culture using the resource manager.
		/// </summary>
		/// <param name="name">The name of the string resource.</param>
		/// <param name="culture">The culture to retrieve a matching string for. May be <see langword="null"/>.</param>
		/// <returns>A localized string.</returns>
		internal static string GetResource(string name, CultureInfo culture)
		{
			return ResourceManager.GetString(name, culture);
		}
		#endregion
#endif
	}
}
