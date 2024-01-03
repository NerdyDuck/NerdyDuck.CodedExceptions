// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable

namespace $rootnamespace$
{
	/// <summary>
	/// Provides easy access to the assembly's debug mode status.
	/// </summary>
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("NerdyDuck.CodedExceptions", "2.0.0.0")]
	[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
	internal static partial class DebugMode
	{
		private static bool s_isInitialized = false;
		private static bool s_isEnabled = false;
		private static readonly object s_syncRoot = new();

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
				if (!s_isInitialized)
				{
					lock (s_syncRoot)
					{
						// Double check in locked section to call code only once
						if (!s_isInitialized)
						{
							s_isEnabled = global::NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.IsDebugModeEnabled(typeof(DebugMode).Assembly);
							s_isInitialized = true;
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
			s_isEnabled = global::NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.IsDebugModeEnabled(typeof(DebugMode).Assembly);
		}
	}
}
