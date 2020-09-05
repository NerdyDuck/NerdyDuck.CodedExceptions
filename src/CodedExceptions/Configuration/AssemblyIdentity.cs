#region Copyright
/*******************************************************************************
 * NerdyDuck.CodedExceptions - Exceptions with custom HRESULTs to identify the 
 * origins of errors.
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

#nullable enable
using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Specifies the identity of an assembly.
	/// </summary>
	/// <remarks>This class can contain a fully qualified assembly name, or parts of it.</remarks>
	[Serializable]
	[ComVisible(false)]
	public sealed class AssemblyIdentity : IEquatable<AssemblyIdentity>, ISerializable
	{
		#region Constants
		private const string AssemblyIdentityRegex = "^(?<name>[^,]*)(, Version=(?<version>[^,]*))?(, Culture=(?<culture>[^,]*))?(, PublicKeyToken=(?<pkt>[^,]*))?(, Type=(?<type>[^,]*))?";
		private const string PublicKeyTokenName = "PublicKeyToken";
		private const string NeutralLanguage = "neutral";

		/// <summary>
		/// The highest possible value returned by <see cref="Match(Assembly)"/>, meaning that name, version, culture and public key token are a match.
		/// </summary>
		public const int MaximumMatchValue = 15;
		#endregion

		#region Private fields
		private byte[]? _publicKeyToken;
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the culture of the assembly. May be <see langword="null"/>.
		/// </summary>
		/// <remarks>If the value is empty, the culture is neutral; if the value is <see langword="null"/>, the culture is not respected when comparing to an <see cref="Assembly"/>.</remarks>
		public string? Culture
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the name of the assembly. May be <see langword="null"/>.
		/// </summary>
		/// <remarks>If the value is <see langword="null"/>, the name is not respected when comparing to an <see cref="Assembly"/>.</remarks>
		public string Name
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the version of the assembly. May be <see langword="null"/>.
		/// </summary>
		/// <remarks>If the value is <see langword="null"/>, the version is not respected when comparing to an <see cref="Assembly"/>.</remarks>
		public Version? Version
		{
			get; set;
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyIdentity"/> class.
		/// </summary>
		/// <remarks>All properties are set to <see langword="null"/>. If no property is set, it matches to all <see cref="Assembly"/>s when comparing.</remarks>
		public AssemblyIdentity()
		{
			Culture = null;
			Name = string.Empty;
			_publicKeyToken = null;
			Version = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyIdentity"/> class with the specified <see cref="AssemblyIdentity"/>.
		/// </summary>
		/// <param name="identity">The <see cref="AssemblyIdentity"/> to copy. May be <see langword="null"/>.</param>
		/// <remarks>All properties are copied from the argument.</remarks>
		public AssemblyIdentity(AssemblyIdentity? identity)
			: this()
		{
			if (identity != null)
			{
				Culture = identity.Culture;
				Name = identity.Name;
				_publicKeyToken = (byte[]?)identity._publicKeyToken?.Clone();
				Version = (Version?)identity.Version?.Clone();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyIdentity"/> class with the specified <see cref="Assembly"/>.
		/// </summary>
		/// <param name="assembly">The assembly to create an identity from.</param>
		/// <param name="assemblyNameElements">A combination of <see cref="AssemblyNameElements"/> values indicating which properties of the <see cref="AssemblyName"/> to copy.</param>
		/// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
		public AssemblyIdentity(Assembly assembly, AssemblyNameElements assemblyNameElements)
			: this()
		{
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			AssemblyName assemblyName = assembly.GetName() ?? throw new ArgumentException(TextResources.AssemblyIdentity_ctor_NoAssemblyName, nameof(assembly));
			if (assemblyNameElements.HasFlag(AssemblyNameElements.Culture))
			{
				Culture = string.IsNullOrEmpty(assemblyName.CultureName) ? NeutralLanguage : assemblyName.CultureName;
			}
			if (assemblyNameElements.HasFlag(AssemblyNameElements.Name))
			{
				Name = assemblyName.Name ?? throw new ArgumentException(TextResources.AssemblyIdentity_ctor_NoAssemblyName, nameof(assembly));
			}
			if (assemblyNameElements.HasFlag(AssemblyNameElements.PublicKeyToken))
			{
				_publicKeyToken = assemblyName.GetPublicKeyToken();
			}
			if (assemblyNameElements.HasFlag(AssemblyNameElements.Version))
			{
				Version = (Version?)assemblyName.Version?.Clone();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyIdentity"/> class with the specified assembly name.
		/// </summary>
		/// <param name="assemblyName">The assembly name to parse.</param>
		/// <exception cref="FormatException">The version or public key token part of the assembly name are invalid.</exception>
		public AssemblyIdentity(string? assemblyName)
		{
			if (!string.IsNullOrEmpty(assemblyName))
			{
				Match match = new Regex(AssemblyIdentityRegex).Match(assemblyName);
				Name = match.Result("${name}");
				string temp = match.Result("${version}");
				if (!string.IsNullOrEmpty(temp))
				{
					if (!Version.TryParse(temp, out Version? version))
					{
						throw new FormatException(TextResources.AssemblyIdentifier_ctor_VersionInvalid);
					}
					Version = version;
				}
				temp = match.Result("${pkt}");
				if (!string.IsNullOrEmpty(temp))
				{
					try
					{
						_publicKeyToken = HResultHelper.HexStringToByteArray(temp);
					}
					catch (FormatException ex)
					{
						throw new FormatException(TextResources.AssemblyIdentity_ctor_KeyTokenInvalid, ex);
					}
				}
				temp = match.Result("${culture}");
				if (!string.IsNullOrEmpty(temp))
				{
					Culture = temp;
				}
			}
			else
			{
				Name = string.Empty;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyIdentity"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="SerializationException">The instance could not be deserialized correctly.</exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "context")] // HACK: suppress CA1801 until serialization constructors are handled correctly
		private AssemblyIdentity(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException(nameof(info));
			}

			Culture = info.GetString(nameof(Culture));
			Name = info.GetString(nameof(Name)) ?? throw new SerializationException(TextResources.AssemblyIdentity_ctor_NoAssemblyName);
			_publicKeyToken = (byte[]?)info.GetValue(PublicKeyTokenName, typeof(byte[]));
			Version = (Version?)info.GetValue(nameof(Version), typeof(Version));
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is an instance of <see cref="AssemblyIdentity"/> and equals the value of this instance, or <paramref name="obj"/> is an instance of <see cref="Assembly"/> and matches the values of this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object? obj) => (obj is AssemblyIdentity ai && Equals(ai)) || (obj is Assembly a && Equals(a));

		/// <summary>
		/// Returns a value indicating whether this instance is equal to the value of the specified <see cref="AssemblyIdentity"/> instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns><see langword="true"/> if the <paramref name="other"/> parameter equals the value of this instance; otherwise, <see langword="false"/>.</returns>
		public bool Equals(AssemblyIdentity? other)
		{
			if (other is null)
			{
				return false;
			}

			if (other.Name == null)
			{
				if (Name != null)
				{
					return false;
				}
			}
			else
			{
				if (Name == null)
				{
					return false;
				}
				else
				{
					if (string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase) != 0)
					{
						return false;
					}
				}
			}

			if (other.Version == null)
			{
				if (Version != null)
				{
					return false;
				}
			}
			else
			{
				if (Version == null)
				{
					return false;
				}
				else
				{
					if (!Version.Equals(other.Version))
					{
						return false;
					}
				}
			}

			if (other.Culture == null)
			{
				if (Culture != null)
				{
					return false;
				}
			}
			else
			{
				if (Culture == null)
				{
					return false;
				}
				else
				{
					if (string.Compare(Culture, other.Culture, StringComparison.OrdinalIgnoreCase) != 0)
					{
						return false;
					}
				}
			}

			if (other._publicKeyToken == null)
			{
				if (_publicKeyToken != null)
				{
					return false;
				}
			}
			else
			{
				if (_publicKeyToken == null)
				{
					return false;
				}
				else
				{
					if (!CompareKeyTokens(other._publicKeyToken))
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to the value of the specified <see cref="Assembly"/> instance.
		/// </summary>
		/// <param name="assembly">The <see cref="Assembly"/> to compare to this instance.</param>
		/// <returns><see langword="true"/>, if all non-<see langword="null"/> properties of this instance match the specified <see cref="Assembly"/>; otherwise, <see langword="false"/>.</returns>
		/// <remarks>This call is equivalent to <see cref="IsMatch(Assembly)"/>.</remarks>
		public bool Equals(Assembly assembly) => IsMatch(assembly);

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode() =>
#if NETSTD20
			ToString().GetHashCode();
#else
			ToString().GetHashCode(StringComparison.Ordinal);
#endif

		/// <summary>
		/// Gets the public key token of the assembly, which is the last 8 bytes of the SHA-1 hash of the public key under which the assembly is signed.
		/// </summary>
		/// <returns>An array of bytes containing the public key token, or <see langword="null"/>, if no public key was set.</returns>
		public byte[]? GetPublicKeyToken() => (byte[]?)_publicKeyToken?.Clone();

		/// <summary>
		/// Returns a value indicating whether the properties of this instance match the specified <see cref="Assembly"/>.
		/// </summary>
		/// <param name="other">The <see cref="Assembly"/> to check.</param>
		/// <returns><see langword="true"/>, if all non-<see langword="null"/> properties of this instance match the specified <see cref="Assembly"/>; otherwise, <see langword="false"/>.</returns>
		public bool IsMatch(Assembly other)
		{
			if (other == null)
			{
				return false;
			}

			AssemblyName assemblyName = other.GetName();

			if (Name != null && string.Compare(Name, assemblyName.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			
			if (Version != null && Version != assemblyName.Version)
			{
				return false;
			}

			if (Culture != null)
			{
				if (string.IsNullOrEmpty(assemblyName.CultureName))
				{
					if (Culture != NeutralLanguage)
					{
						return false;
					}
				}
				else
				{
					if (string.Compare(assemblyName.CultureName, Culture, StringComparison.OrdinalIgnoreCase) != 0)
					{
						return false;
					}
				}
			}

			if (_publicKeyToken != null)
			{
				byte[]? pkt = assemblyName.GetPublicKeyToken();
				if (pkt == null || pkt.Length == 0)
				{
					return false;
				}
				if (!CompareKeyTokens(pkt))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Compares the specified <see cref="Assembly"/> to the current <see cref="AssemblyIdentity"/>, and returns an integer value
		/// that specifies if the <paramref name="assembly"/> matches the identity, and how strong that match is.
		/// </summary>
		/// <param name="assembly">The <see cref="Assembly"/> to compare to.</param>
		/// <returns>A signed number indicating the relative matching strength of the <paramref name="assembly"/> to the identity.
		/// <list type="table">
		/// <listheader><term>Value</term><term>Description</term></listheader>
		/// <item><term>Less than zero</term><term>The <paramref name="assembly"/> does not match the identity, or <paramref name="assembly"/> is <see langword="null"/>.</term></item>
		/// <item><term>Zero</term><term>The current <see cref="AssemblyIdentity"/> is a "catch-all" where all property values are set to <see langword="null"/>, so every <see cref="Assembly"/> matches.</term></item>
		/// <item><term>More than zero</term><term>The <paramref name="assembly"/> matches the identity. The higher the value, the stronger the match.</term></item>
		/// </list></returns>
		/// <returns>This table details the specific values of the return value.
		/// <list type="table">
		/// <listheader><term>Value</term><term>Description</term></listheader>
		/// <item><term>-4</term><term>The <paramref name="assembly"/>'s public key does not match the identity.</term></item>
		/// <item><term>-3</term><term>The <paramref name="assembly"/> culture does not match the identity.</term></item>
		/// <item><term>-2</term><term>The <paramref name="assembly"/> version does not match the identity.</term></item>
		/// <item><term>-1</term><term>The <paramref name="assembly"/> name does not match the identity, or <paramref name="assembly"/> is <see langword="null"/>.</term></item>
		/// <item><term>0</term><term>The current <see cref="AssemblyIdentity"/> is a "catch-all" where all property values are set to <see langword="null"/>, so every <see cref="Assembly"/> matches.</term></item>
		/// <item><term>1</term><term>The <paramref name="assembly"/>'s public key matches the identity.</term></item>
		/// <item><term>2</term><term>The <paramref name="assembly"/> culture matches the identity.</term></item>
		/// <item><term>4</term><term>The <paramref name="assembly"/> version matches the identity.</term></item>
		/// <item><term>8</term><term>The <paramref name="assembly"/> name matches the identity.</term></item>
		/// </list>
		/// <para>Positive values are combined if multiple properties match, to detect the strongest match when comparing multiple <see cref="AssemblyIdentity"/>s
		/// to an <see cref="Assembly"/>, while a single mismatch results in the negative return value according to the mismatched property. The different properties of the <see cref="AssemblyIdentity"/>
		/// have different weights, so that a match via e.g. the assembly name will always be higher ranked than the match via the assembly version. Only
		/// properties of the <see cref="AssemblyIdentity"/> that are not <see langword="null"/> are compared to the assembly.</para></returns>
		public int Match(Assembly assembly)
		{
			if (assembly == null)
			{
				return -1;
			}

			int returnValue = 0;
			bool noCheck = true;
			AssemblyName assemblyName = assembly.GetName();

			if (Name != null)
			{
				noCheck = false;
				if (string.Compare(Name, assemblyName.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					// Assembly name has the strongest factor, as it is the most important part of the identity. Overrides all other matches by version, language or public key.
					returnValue += 8;
				}
				else
				{
					return -1;
				}
			}

			if (Version != null)
			{
				noCheck = false;
				if (Version == assemblyName.Version)
				{
					returnValue += 4;
				}
				else
				{
					return -2;
				}
			}

			if (Culture != null)
			{
				noCheck = false;
				if (string.IsNullOrEmpty(assemblyName.CultureName))
				{
					if (Culture == NeutralLanguage)
					{
						returnValue++;
					}
					else
					{
						return -3;
					}
				}
				else
				{
					if (string.Compare(assemblyName.CultureName, Culture, StringComparison.OrdinalIgnoreCase) == 0)
					{
						returnValue++;
					}
					else
					{
						return -3;
					}
				}
			}

			if (_publicKeyToken != null)
			{
				noCheck = false;
				byte[]? pkt = assemblyName.GetPublicKeyToken();
				if (pkt == null || pkt.Length == 0)
				{
					return -4;
				}
				if (CompareKeyTokens(pkt))
				{
					returnValue += 2;
				}
				else
				{
					return -4;
				}
			}

			if (noCheck)
			{
				// No comparison was done because all properties of AssemblyInfo are null, meaning it is a "catch-all".
				return 0;
			}

			return returnValue;
		}

		/// <summary>
		/// Sets the public key token of the assembly, which is the last 8 bytes of the SHA-1 hash of the public key under which the assembly is signed.
		/// </summary>
		/// <param name="publicKeyToken">An array of bytes containing the public key token. May be <see langword="null"/>.</param>
		public void SetPublicKeyToken(byte[] publicKeyToken) => _publicKeyToken = publicKeyToken;

		/// <summary>
		/// Returns the full name of the assembly identity.
		/// </summary>
		/// <returns>The full name of the assembly identity, containing all properties that are not <see langword="null"/>.</returns>
		public override string ToString()
		{
			StringBuilder sb = HResultHelper.AcquireStringBuilder();
			if (!string.IsNullOrEmpty(Name))
			{
				sb.Append(Name);
			}
			if (Version != null)
			{
				sb.AppendFormat(CultureInfo.InvariantCulture, ", Version={0}", Version.ToString(4));
			}
			if (Culture != null)
			{
				sb.AppendFormat(CultureInfo.InvariantCulture, ", Culture={0}", string.IsNullOrEmpty(Culture) ? NeutralLanguage : Culture);
			}
			if (_publicKeyToken != null)
			{
				sb.Append(", PublicKeyToken=");
				foreach (byte b in _publicKeyToken)
				{
					sb.AppendFormat(CultureInfo.InvariantCulture, "{0:x2}", b);
				}
			}
			return HResultHelper.GetStringAndRelease(sb);
		}
#endregion

#region Operators
		/// <summary>
		/// Determines whether two instances of <see cref="AssemblyIdentity"/> are equal.
		/// </summary>
		/// <param name="identity1">The first object to compare.</param>
		/// <param name="identity2">The second object to compare.</param>
		/// <returns><see langword="true"/>, if <paramref name="identity1"/> and <paramref name="identity2"/> represent the same byte array; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(AssemblyIdentity? identity1, AssemblyIdentity? identity2)
		{
			if (identity1 is null)
			{
				return identity2 is null;
			}
			return identity1.Equals(identity2);
		}

		/// <summary>
		/// Determines whether two specified instances of <see cref="AssemblyIdentity"/> are not equal.
		/// </summary>
		/// <param name="identity1">The first object to compare.</param>
		/// <param name="identity2">The second object to compare.</param>
		/// <returns><see langword="true"/>, if <paramref name="identity1"/> and <paramref name="identity2"/> do not represent the same byte array; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(AssemblyIdentity? identity1, AssemblyIdentity? identity2)
		{
			if (identity1 is null)
			{
				return identity2 is object;
			}
			return !identity1.Equals(identity2);
		}
#endregion

#region ISerializable implementation
		/// <summary>
		/// Sets the <see cref="SerializationInfo"/> with information about the exception.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the object being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException(nameof(info));
			}
			info.AddValue(nameof(Culture), Culture);
			info.AddValue(nameof(Name), Name);
			info.AddValue(PublicKeyTokenName, _publicKeyToken);
			info.AddValue(nameof(Version), Version);
		}
#endregion

#region Private methods
		/// <summary>
		/// Compares the specified byte array to PublicKeyToken.
		/// </summary>
		/// <param name="other">The array to compare.</param>
		/// <returns>true, if both arrays have the same length and content; otherwise, false.</returns>
		private bool CompareKeyTokens(byte[]? other)
		{
			if (other == null)
			{
				return _publicKeyToken == null;
			}
			else
			{
				if (_publicKeyToken == null)
				{
					return false;
				}
				if (_publicKeyToken.Length != other.Length)
				{
					return false;
				}
				for (int i = 0; i < _publicKeyToken.Length; i++)
				{
					if (_publicKeyToken[i] != other[i])
					{
						return false;
					}
				}
			}
			return true;
		}
#endregion

#region Enumerations
		/// <summary>
		/// Specifies the elements of a fully-qualified assembly name that are to be respected when creating an <see cref="AssemblyIdentity"/>.
		/// </summary>
		[Flags]
		public enum AssemblyNameElements
		{
			/// <summary>
			/// No fields are included. Do not use.
			/// </summary>
			None = 0,

			/// <summary>
			/// Assembly name is included.
			/// </summary>
			Name = 1,

			/// <summary>
			/// Assembly version is included.
			/// </summary>
			Version = 2,

			/// <summary>
			/// Assembly culture is included.
			/// </summary>
			Culture = 4,

			/// <summary>
			/// Public key token is included.
			/// </summary>
			PublicKeyToken = 8,

			/// <summary>
			/// The default setting includes assembly name, version, culture and the public key token.
			/// </summary>
			All = Name | Version | Culture | PublicKeyToken,

			/// <summary>
			/// The assembly name, culture and public key token are included, but not the version.
			/// </summary>
			NoVersion = Name | Culture | PublicKeyToken
		}
#endregion
	}
}
