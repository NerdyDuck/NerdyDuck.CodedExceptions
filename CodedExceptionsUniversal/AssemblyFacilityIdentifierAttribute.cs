#region Copyright
/*******************************************************************************
 * <copyright file="AssemblyFacilityIdentifierAttribute.cs" owner="Daniel Kopp">
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
 * <file name="AssemblyFacilityIdentifierAttribute.cs" date="2015-08-06">
 * When applied to an assembly, specifies the facility id of the assembly when
 * creating HResult values for coded exceptions.
 * </file>
 ******************************************************************************/
#endregion

using NerdyDuck.CodedExceptions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// When applied to an assembly, specifies the facility id of the assembly when creating
	/// <see cref="Exception.HResult"/> values for coded exceptions.
	/// </summary>
	/// <remarks>
	/// <para>Apply this attribute to an assembly if that assembly throws exceptions with custom <see cref="Exception.HResult"/> values,
	/// and you want to create values that adhere to the HRESULT format specified by Microsoft. See the
	/// <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for more information about
	/// the definition of HRESULT values.</para>
	/// <para>The <see cref="FacilityId"/> value of the attribute defines the lower bits of the higher two bytes of the HRESULT value.
	/// It can be used to discover the assembly that an exception is thrown by. The value can range between 0 and 2047, as 11 bits
	/// are available for the facility. The highest 5 bits are reserved, and should always be set to value 0xa, as that specifies a
	/// custom HRESULT value (values from Microsoft always start with 0x8). The lower two bytes of the HRESULT are used for specific
	/// errror numbers, so a maximum of 65,535 individual error numbers per assembly are possible using this schema.</para>
	/// </remarks>
	/// <example>
	/// <para>First decorate your assembly with the <see cref="AssemblyFacilityIdentifierAttribute"/>.</para>
	/// <code language="C#" title="Assembly.cs">
	/// using NerdyDuck.CodedExceptions;
	///
	/// [assembly: AssemblyFacilityIdentifier(0x0305)]
	/// </code>
	///
	/// <para>Then you can use the <see cref="AssemblyFacilityIdentifierAttribute"/> and <see cref="HResultHelper"/> classes to retrieve the
	/// facility id and build a HRESULT value.</para>
	///
	/// <code language="C#" title="Program.cs">
	/// using NerdyDuck.CodedExceptions;
	/// using System;
	/// using System.Reflection;
	///
	/// namespace Example
	/// {
	///     class Program
	///     {
	///         static void Main()
	///         {
	///             int facilityId = AssemblyFacilityIdentifierAttribute.FromAssembly(Assembly.GetExecutingAssembly()).FacilityId;
	///             // facilityId is 0x0305
	///	            int baseHResult = HResultHelper.GetBaseHResult(facilityId);
	///             // baseHResult is 0xa3050000
	///             int myHResult = baseHResult | 0x42;
	///             // myHResult is 0xa3050042;
	///         }
	///     }
	/// }
	/// </code>
	/// </example>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class AssemblyFacilityIdentifierAttribute : System.Attribute
	{
		#region Private fields
		private int mFacilityId;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the identifier for the facility (= the attributed assembly).
		/// </summary>
		/// <value>An integer ranging between 0 and 2047.</value>
		public int FacilityId
		{
			get { return mFacilityId; }
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyFacilityIdentifierAttribute"/> with the specified facility id.
		/// </summary>
		/// <param name="facilityId">The identifier for the facility (= the attributed assembly).</param>
		/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="facilityId"/> is less than 0 or greater than 2048.</exception>
		public AssemblyFacilityIdentifierAttribute(int facilityId)
		{
			if (facilityId < 0 || facilityId > 2047)
				throw new CodedArgumentOutOfRangeException(Errors.CreateHResult(0x02), "facilityId", Properties.Resources.AssemblyFacilityIdentifierAttribute_ctor_OutOfRange);
			mFacilityId = facilityId;
		}
		#endregion

		#region Public methods
		#region FromAssembly
		/// <summary>
		/// Gets an <see cref="AssemblyFacilityIdentifierAttribute"/> located in the specified assembly.
		/// </summary>
		/// <param name="assembly">The assembly to retrieve the <see cref="AssemblyFacilityIdentifierAttribute"/> from.</param>
		/// <returns>An <see cref="AssemblyFacilityIdentifierAttribute"/>, or <see langword="null"/>, if no matching attribute is found in the <paramref name="assembly"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
		public static AssemblyFacilityIdentifierAttribute FromAssembly(Assembly assembly)
		{
			if (assembly == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x03), "assembly");

			return assembly.GetCustomAttribute<AssemblyFacilityIdentifierAttribute>();
		}
		#endregion

		#region FromType
		/// <summary>
		/// Gets an <see cref="AssemblyFacilityIdentifierAttribute"/> located in the assembly that defines the specified type.
		/// </summary>
		/// <param name="type">The type that is defined by the assembly that is checked for the attribute.</param>
		/// <returns>An <see cref="AssemblyFacilityIdentifierAttribute"/>, or <see langword="null"/>, if no matching attribute is found.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
		public static AssemblyFacilityIdentifierAttribute FromType(Type type)
		{
			if (type == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x04), "type");

			return FromAssembly(type.GetTypeInfo().Assembly);
		}
		#endregion

		#region TryGetOverride
		/// <summary>
		/// Attempts to get a facility identifier override for the specified assembly from the application configuration file.
		/// </summary>
		/// <param name="assembly">The assembly to override the identifier for.</param>
		/// <param name="identifier">When the method returns, the argument contains the identifier override, if one was found; otherwise, 0.</param>
		/// <returns><see langword="true"/>, if an override was found; otherwise, <see langword="false"/>.</returns>
		/// <exception cref="CodedArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
		public static bool TryGetOverride(Assembly assembly, out int identifier)
		{
			if (assembly == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x05), "assembly");

			identifier = 0;
			bool ReturnValue = false;
			List<FacilityOverride> Overrides = null;

#if WINDOWS_DESKTOP
			CodedExceptionsSection Config =
				(CodedExceptionsSection)System.Configuration.ConfigurationManager.GetSection(CodedExceptionsSection.ConfigSectionName);

			if (Config == null)
			{
				return false;
			}

			Overrides = Config.GetFacilityOverrides();
#endif
#if WINDOWS_UWP
			Overrides = XmlFacilityOverrides.GetFacilityOverrides();
#endif

			if (Overrides != null)
			{
				AssemblyName CurrentName = assembly.GetName();
				foreach (FacilityOverride fo in Overrides)
				{
					if (fo.Equals(CurrentName))
					{
						identifier = fo.Identifier;
						ReturnValue = true;
						break;
					}
				}
			}

			return ReturnValue;
		}

		/// <summary>
		/// Attempts to get a facility identifier override for the assembly containing the specified type from the application configuration file.
		/// </summary>
		/// <param name="type">The type that resides in the assembly to override the identifier for.</param>
		/// <param name="identifier">When the method returns, the argument contains the identifier override, if one was found; otherwise, 0.</param>
		/// <returns><see langword="true"/>, if an override was found; otherwise, <see langword="false"/>.</returns>
		/// <exception cref="CodedArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
		/// <exception cref="CodedFormatException">The overrides section in the configuration file is invalid.</exception>
		public static bool TryGetOverride(Type type, out int identifier)
		{
			if (type == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x06), "type");

			return TryGetOverride(type.GetTypeInfo().Assembly, out identifier);
		}
		#endregion
		#endregion
	}
}
