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

namespace $rootnamespace$
{
	/// <summary>
	/// Provides easy access to the assembly's debug mode status.
	/// </summary>
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("NerdyDuck.CodedExceptions", "2.0.0.0")]
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
	internal static class DebugMode
	{
		private static bool _isInitialized = false;
		private static bool _isEnabled = false;
		private static readonly object _syncRoot = new();

		/// <summary>
		/// Gets a value indicting if the assembly was set into debug mode via configuration.
		/// </summary>
		/// <value><see langword="true"/>, if the assembly is running in debug mode; otherwise, <see langword="false"/>.</value>
		/// <remarks>Use this value to determine if extra log output, additional checks etc. are required.</remarks>
		internal static bool IsEnabled
		{
			get
			{
				// First check without lock to avoid locking as far as possible
				if (!_isInitialized)
				{
					lock (_syncRoot)
					{
						// Double check in locked section to call code only once
						if (!_isInitialized)
						{
							_isEnabled = global::NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.IsDebugModeEnabled(typeof(DebugMode).Assembly);
							_isInitialized = true;
							global::NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.CacheChanged += Global_CacheChanged;
						}
					}
				}

				return _isEnabled;
			}
		}

		/// <summary>
		/// Reloads the value of <see cref="IsEnabled" /> from the default cache, after the cache has raised an event to notify a change in the cache.
		/// </summary>
		/// <param name="sender">The cache that raised the event.</param>
		/// <param name="e">The event arguments.</param>
		private static void Global_CacheChanged(object? sender, global::System.EventArgs e)
		{
			_isEnabled = global::NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.IsDebugModeEnabled(typeof(DebugMode).Assembly);
		}
	}
}
