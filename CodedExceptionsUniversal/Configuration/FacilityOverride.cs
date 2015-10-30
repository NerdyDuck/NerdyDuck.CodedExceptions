#region Copyright
/*******************************************************************************
 * <copyright file="FacilityOverride.cs" owner="Daniel Kopp">
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
 * <file name="FacilityOverride.cs" date="2015-08-10">
 * Represents an override for a facility identifier.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Reflection;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Represents an override for a facility identifier.
	/// </summary>
	internal class FacilityOverride : IEquatable<AssemblyName>
	{
		#region Private fields
		private AssemblyName mAssemblyName;
		private int mIdentifier;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the name of the assembly that the identifier override is configured for.
		/// </summary>
		public AssemblyName AssemblyName
		{
			get { return mAssemblyName; }
		}

		/// <summary>
		/// Gets the overriden identifier value.
		/// </summary>
		public int Identifier
		{
			get { return mIdentifier; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="FacilityOverride"/> class with the specified assembly name and facility identifier.
		/// </summary>
		/// <param name="assemblyName">The name of the assembly that the identifier override is configured for.</param>
		/// <param name="identifier">The overriden identifier value.</param>
		/// <exception cref="CodedArgumentNullException"><paramref name="assemblyName"/> is null.</exception>
		public FacilityOverride(AssemblyName assemblyName, int identifier)
		{
			if (assemblyName == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x0c), nameof(assemblyName));
			if (identifier < 0 || identifier > 2047)
				throw new CodedArgumentOutOfRangeException(Errors.CreateHResult(0x0f), nameof(identifier), Properties.Resources.Global_FacilityId_OutOfRange);
			mAssemblyName = assemblyName;
			mIdentifier = identifier;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FacilityOverride"/> class with the specified assembly name and facility identifier.
		/// </summary>
		/// <param name="assemblyName">The name of the assembly that the identifier override is configured for.</param>
		/// <param name="identifier">The overriden identifier value.</param>
		/// <exception cref="CodedArgumentNullException"><paramref name="assemblyName"/> is null or empty.</exception>
		/// <exception cref="System.IO.FileLoadException"><paramref name="assemblyName"/> is not a valid assembly name (full or partially qualified).</exception>
		public FacilityOverride(string assemblyName, int identifier)
		{
			if (string.IsNullOrWhiteSpace(assemblyName))
				throw new CodedArgumentNullException(Errors.CreateHResult(0x0d), nameof(assemblyName));
			if (identifier < 0 || identifier > 2047)
				throw new CodedArgumentOutOfRangeException(Errors.CreateHResult(0x0f), nameof(identifier), Properties.Resources.Global_FacilityId_OutOfRange);
			mAssemblyName = new AssemblyName(assemblyName);
			mIdentifier = identifier;
		}
		#endregion

		#region Overrides
		/// <summary>
		/// Determines whether the specified <see cref="Object" /> is equal to the current <see cref="FacilityOverride" />.
		/// </summary>
		/// <param name="obj">The <see cref="Object" /> to compare with the current <see cref="FacilityOverride" />.</param>
		/// <returns><see langword="true" /> if the specified <see cref="Object" /> is equal to the current <see cref="FacilityOverride" />; otherwise, <see langword="false" />. </returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (obj is AssemblyName)
				return Equals((AssemblyName)obj);
			if (obj is FacilityOverride)
				return base.Equals(obj);
			return false;
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return mAssemblyName.GetHashCode();
		}
		#endregion

		#region IEquatable implementation
		/// <summary>
		/// Determines whether the specified <see cref="AssemblyName" /> is equal to the current <see cref="FacilityOverride.AssemblyName" />.
		/// </summary>
		/// <param name="other">The <see cref="AssemblyName" /> to compare with the current <see cref="FacilityOverride.AssemblyName" />.</param>
		/// <returns><see langword="true" /> if the specified <see cref="AssemblyName" /> is equal to the current <see cref="FacilityOverride.AssemblyName" />; otherwise, <see langword="false" />. </returns>
		public bool Equals(AssemblyName other)
		{
			if (other == null)
				return false;

			// For convenience, not case sensitive (never distinguish files by casing, anyways)
			if (string.Compare(mAssemblyName.Name, other.Name, true) != 0)
				return false;

			// Version, culture and public key token MUST match, IF they are defined in the override.
			if (mAssemblyName.Version != null)
			{
				if (other.Version == null)
					return false;
				if (mAssemblyName.Version != other.Version)
					return false;
			}

			if (!string.IsNullOrWhiteSpace(mAssemblyName.CultureName))
			{
				if (string.IsNullOrWhiteSpace(other.CultureName))
					return false;
				if (string.Compare(mAssemblyName.CultureName, other.CultureName, true) != 0)
					return false;
			}

			byte[] ThisToken = mAssemblyName.GetPublicKeyToken();
			if (ThisToken != null && ThisToken.Length != 0)
			{
				byte[] OtherToken = other.GetPublicKeyToken();
				if (OtherToken == null || OtherToken.Length == 0 || ThisToken.Length != OtherToken.Length)
					return false;

				for (int i = 0; i < ThisToken.Length; i++)
				{
					if (ThisToken[i] != OtherToken[i])
						return false;
				}
			}

			return true;
		}
		#endregion
	}
}
