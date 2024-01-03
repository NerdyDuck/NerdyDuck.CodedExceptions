// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace $rootnamespace$
{
	/// <summary>
	/// Provides easy access to the assembly's facility id and base HRESULT code.
	/// </summary>
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("NerdyDuck.CodedExceptions", "2.0.0.0")]
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
	internal static partial class HResult
	{
		// Load the facility id lazy and thread-safe.
		private static readonly global::System.Lazy<int> s_facilityId = new global::System.Lazy<int>(() =>
		{
			// Check for override in configuration
			if (global::NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideCache.Global.TryGetOverride(typeof(HResult).Assembly, out int identifier))
			{
				return identifier;
			}

			// Try to get identifier from assembly attribute
			return global::NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute.FromAssembly(typeof(HResult).Assembly)?.FacilityId ?? 0;
		});

		private static readonly global::System.Lazy<int> s_hResultBase = new global::System.Lazy<int>(() => global::NerdyDuck.CodedExceptions.HResultHelper.GetBaseHResult(s_facilityId.Value));
		private static readonly global::System.Lazy<int> s_hResultErrorBase = new global::System.Lazy<int>(() => global::NerdyDuck.CodedExceptions.HResultHelper.GetBaseHResultError(s_facilityId.Value));

		/// <summary>
		/// Gets the facility identifier of the current assembly.
		/// </summary>
		/// <value>The facility identifier, or 0, if no <see cref="NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</value>
		/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		internal static int FacilityId => s_facilityId.Value;

		/// <summary>
		/// Gets the base HRESULT success value of the current assembly.
		/// </summary>
		/// <value>The base HRESULT value, or 0x20000000, if no <see cref="NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</value>
		/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		internal static int HResultBase => s_hResultBase.Value;

		/// <summary>
		/// Gets the base HRESULT error value of the current assembly.
		/// </summary>
		/// <value>The base HRESULT value, or 0xa0000000, if no <see cref="NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</value>
		/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		internal static int HResultErrorBase => s_hResultErrorBase.Value;

		/// <summary>
		/// Combines the specified error identifier with the base HRESULT value for this assembly.
		/// </summary>
		/// <param name="errorId">The error identifier to add to the base HRESULT value.</param>
		/// <returns>A custom HRESULT value, combined from <paramref name="errorId"/> and <see cref="HResultBase"/>.</returns>
		/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		internal static int Create(int errorId) => s_hResultErrorBase.Value | errorId;

		/// <summary>
		/// Combines the specified error identifier, represented by an enumeration, with the base HRESULT value for this assembly.
		/// </summary>
		/// <param name="errorId">The error identifier to add to the base HRESULT value.</param>
		/// <returns>A custom HRESULT value, combined from <paramref name="errorId"/> and <see cref="HResultBase"/>.</returns>
		/// <remarks><para>This method can only be used for enumerations based on <see cref="System.Int32"/>.</para>
		/// <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		/// <exception cref="NerdyDuck.CodedExceptions.CodedArgumentException"><paramref name="errorId"/> is not based on <see cref="System.Int32"/> or not a valid enumeration.</exception>
		internal static int Create(global::System.Enum errorId) => s_hResultErrorBase.Value | global::NerdyDuck.CodedExceptions.HResultHelper.EnumToInt32(errorId);

		/// <summary>
		/// Combines the specified error identifier with the base HRESULT value for this assembly.
		/// </summary>
		/// <param name="successId">The error identifier to add to the base HRESULT value.</param>
		/// <returns>A custom HRESULT value, combined from <paramref name="successId"/> and <see cref="HResultBase"/>.</returns>
		/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		internal static int CreateSuccess(int successId) => s_hResultBase.Value | successId;

		/// <summary>
		/// Combines the specified success identifier, represented by an enumeration, with the base HRESULT value for this assembly.
		/// </summary>
		/// <param name="successId">The success identifier to add to the base HRESULT value.</param>
		/// <returns>A custom HRESULT value, combined from <paramref name="successId"/> and <see cref="HResultBase"/>.</returns>
		/// <remarks><para>This method can only be used for enumerations based on <see cref="System.Int32"/>.</para>
		/// <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		/// <exception cref="NerdyDuck.CodedExceptions.CodedArgumentException"><paramref name="successId"/> is not based on <see cref="System.Int32"/> or not a valid enumeration.</exception>
		internal static int CreateSuccess(global::System.Enum successId) => s_hResultBase.Value | global::NerdyDuck.CodedExceptions.HResultHelper.EnumToInt32(successId);
	}
}
