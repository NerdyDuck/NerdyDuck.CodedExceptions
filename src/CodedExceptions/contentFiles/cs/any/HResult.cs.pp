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

namespace $rootnamespace$
{
	/// <summary>
	/// Provides easy access to the assembly's facility id and base HRESULT code.
	/// </summary>
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("NerdyDuck.CodedExceptions", "2.0.0.0")]
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
	internal static class HResult
	{
		// Load the facility id lazy and thread-safe.
		private static readonly global::System.Lazy<int> _facilityId = new global::System.Lazy<int>(() =>
		{
			// Check for override in configuration
			if (global::NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideCache.Global.TryGetOverride(typeof(HResult).Assembly, out int identifier))
			{
				return identifier;
			}

			// Try to get identifier from assembly attribute
			return global::NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute.FromAssembly(typeof(HResult).Assembly)?.FacilityId ?? 0;
		});

		private static readonly global::System.Lazy<int> _hResultBase = new global::System.Lazy<int>(() => global::NerdyDuck.CodedExceptions.HResultHelper.GetBaseHResult(_facilityId.Value));

		private static bool _isDebugModeInitialized = false;
		private static bool _isDebugModeEnabled = false;

		/// <summary>
		/// Gets the facility identifier of the current assembly.
		/// </summary>
		/// <value>The facility identifier, or 0, if no <see cref="NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</value>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		internal static int FacilityId => _facilityId.Value;

		/// <summary>
		/// Gets the base HRESULT value of the current assembly.
		/// </summary>
		/// <value>The base HRESULT value, or 0xa0000000, if no <see cref="NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</value>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		internal static int HResultBase => _hResultBase.Value;

		/// <summary>
		/// Gets a value indicting if the assembly was set into debug mode via configuration.
		/// </summary>
		/// <value><see langword="true"/>, if the assembly is running in debug mode; otherwise, <see langword="false"/>.</value>
		/// <remarks>Use this value to determine if extra log output, additional checks etc. are required.</remarks>
		internal static bool IsDebugModeEnabled
		{
			get
			{
				// First check without lock to avoid locking as far as possible
				if (!_isDebugModeInitialized)
				{
					lock (_hResultBase)
					{
						// Double check in locked section to call code only once
						if (!_isDebugModeInitialized)
						{
							_isDebugModeEnabled = global::NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.IsDebugModeEnabled(typeof(HResult).Assembly);
							_isDebugModeInitialized = true;
							global::NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.CacheChanged += Global_CacheChanged;
						}
					}
				}

				return _isDebugModeEnabled;
			}
		}

		/// <summary>
		/// Combines the specified error identifier with the base HRESULT value for this assembly.
		/// </summary>
		/// <param name="errorId">The error identifier to add to the base HRESULT value.</param>
		/// <returns>A custom HRESULT value, combined from <paramref name="errorId"/> and <see cref="HResultBase"/>.</returns>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		internal static int Create(int errorId) => _hResultBase.Value | errorId;

		/// <summary>
		/// Combines the specified error identifier, represented by an enumeration, with the base HRESULT value for this assembly.
		/// </summary>
		/// <param name="errorId">The error identifier to add to the base HRESULT value.</param>
		/// <returns>A custom HRESULT value, combined from <paramref name="errorId"/> and <see cref="HResultBase"/>.</returns>
		/// <remarks><para>This method can only be used for enumerations based on <see cref="System.Int32"/>.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		/// <exception cref="NerdyDuck.CodedExceptions.CodedArgumentException"><paramref name="errorId"/> is not based on <see cref="System.Int32"/> or not a valid enumeration.</exception>
		internal static int Create(global::System.Enum errorId) => _hResultBase.Value | global::NerdyDuck.CodedExceptions.HResultHelper.EnumToInt32(errorId);

		/// <summary>
		/// Reloads the value of <see cref="IsDebugModeEnabled" /> from the default cache, after the cache has raised an event to notify a change in the cache.
		/// </summary>
		/// <param name="sender">The cache that raised the event.</param>
		/// <param name="e">The event arguments.</param>
		private static void Global_CacheChanged(object? sender, global::System.EventArgs e)
		{
			_isDebugModeEnabled = global::NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.IsDebugModeEnabled(typeof(HResult).Assembly);
		}
	}
}
