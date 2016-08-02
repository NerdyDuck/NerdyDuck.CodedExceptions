#region Copyright
/*******************************************************************************
 * <copyright file="XmlFacilityOverrides.cs" owner="Daniel Kopp">
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
 * <file name="XmlFacilityOverrides.cs" date="2015-08-10">
 * Provides methods to read overrides from an XML file for facility identifiers
 * from AssemblyFacilityIdentiferAttributes.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Provides methods to read overrides from an XML file for facility identifiers from <see cref="CodedExceptions.AssemblyFacilityIdentifierAttribute"/>s.
	/// </summary>
	internal static class XmlFacilityOverrides
	{
		#region Constants
		private const string DefaultFileName = "FacilityIdentifierOverrides.xml";
		private const string RootNodeName = "facilityIdentifierOverrides";
		private const string OverrideNodeName = "override";
		private const string AssemblyNameKey = "assemblyName";
		private const string IdentifierKey = "identifier";
		#endregion

		#region Methods
		/// <summary>
		/// Gets a list of <see cref="FacilityOverride"/>s from the default overrides file (FacilityIdentifierOverrides.xml), if it exists.
		/// </summary>
		/// <returns>A list of <see cref="FacilityOverride"/> objects, or <see langword="null"/>, if the file does not exists.</returns>
		internal static List<FacilityOverride> GetFacilityOverrides()
		{
			return GetFacilityOverrides(DefaultFileName);
		}

		/// <summary>
		/// Gets a list of <see cref="FacilityOverride"/>s from the specified file, if it exists.
		/// </summary>
		/// <param name="fileName">The name of the file to read.</param>
		/// <returns>A list of <see cref="FacilityOverride"/> objects, or <see langword="null"/>, if the file does not exists.</returns>
		internal static List<FacilityOverride> GetFacilityOverrides(string fileName)
		{
			if (string.IsNullOrWhiteSpace(fileName))
				throw new ArgumentNullException(nameof(fileName));

			if (!File.Exists(fileName))
			{
				return null;
			}

			List<FacilityOverride> ReturnValue = null;

			try
			{
				using (FileStream SourceStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					XmlReaderSettings settings = new XmlReaderSettings();
					settings.IgnoreComments = true;
					settings.IgnoreWhitespace = true;

					XmlReader Reader = XmlReader.Create(SourceStream, settings);
					Reader.ReadStartElement(RootNodeName);
					ReturnValue = new List<FacilityOverride>();
					string AssemblyString, IdentifierString;
					int Identifier;
					while (!(Reader.Name == RootNodeName && Reader.NodeType == XmlNodeType.EndElement))
					{
						if (Reader.Name == OverrideNodeName && Reader.NodeType == XmlNodeType.Element)
						{
							AssemblyString = Reader.GetAttribute(AssemblyNameKey);
							if (string.IsNullOrWhiteSpace(AssemblyString))
								throw new XmlException(string.Format(Properties.Resources.XmlFacilityOverrides_GetFacilityOverrides_AttributeMissing, OverrideNodeName, AssemblyNameKey));
							IdentifierString = Reader.GetAttribute(IdentifierKey);
							if (string.IsNullOrWhiteSpace(IdentifierString))
								throw new XmlException(string.Format(Properties.Resources.XmlFacilityOverrides_GetFacilityOverrides_AttributeMissing, OverrideNodeName, IdentifierKey));
							try
							{
								Identifier = XmlConvert.ToInt32(IdentifierString);
							}
							catch (FormatException ex)
							{
								throw new XmlException(string.Format(Properties.Resources.XmlFacilityOverrides_GetFacilityOverrides_IdentifierInvalid, AssemblyString), ex);
							}
							ReturnValue.Add(new FacilityOverride(AssemblyString, Identifier));
							Reader.Skip();
						}
						else
						{
							if (!Reader.Read())
							{
								break;
							}
						}
					}
				}
			}
			catch (IOException ex)
			{
				throw new IOException(string.Format(Properties.Resources.XmlFacilityOverrides_GetFacilityOverrides_FileOpenFailed, fileName), ex);
			}
			catch (System.Security.SecurityException ex)
			{
				throw new IOException(string.Format(Properties.Resources.XmlFacilityOverrides_GetFacilityOverrides_FileOpenFailed, fileName), ex);
			}
			catch (UnauthorizedAccessException ex)
			{
				throw new IOException(string.Format(Properties.Resources.XmlFacilityOverrides_GetFacilityOverrides_FileOpenFailed, fileName), ex);
			}
			catch (XmlException ex)
			{
				throw new IOException(string.Format(Properties.Resources.XmlFacilityOverrides_GetFacilityOverrides_FileInvalid, fileName), ex);
			}
			catch (FormatException ex)
			{
				throw new IOException(string.Format(Properties.Resources.XmlFacilityOverrides_GetFacilityOverrides_FileInvalid, fileName), ex);
			}

			return ReturnValue;
		}
		#endregion
	}
}
