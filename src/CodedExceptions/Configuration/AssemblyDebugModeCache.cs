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

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Manages an easily queryable list of assembly debug mode flags.
	/// </summary>
	[ComVisible(false)]
	public sealed class AssemblyDebugModeCache : IDisposable
	{
		#region Private fields
		private static readonly Lazy<AssemblyDebugModeCache> s_global = new Lazy<AssemblyDebugModeCache>(() => new AssemblyDebugModeCache());
		private List<AssemblyDebugMode> _debugModes;
		private ReaderWriterLockSlim _listLock;
		private int _isDisposed;
		#endregion

		#region Properties
		/// <summary>
		/// Gets a global singleton instance of <see cref="AssemblyDebugModeCache"/>.
		/// </summary>
		/// 
		/// <value>A <see langword="static"/> instance of <see cref="AssemblyDebugModeCache"/>.</value>
		/// <remarks>This instance is used by the <c>Errors</c> class that is automatically added to every project referencing the NerdyDuck.CodedExceptions NuGet package.</remarks>
		public static AssemblyDebugModeCache Global => s_global.Value;

		/// <summary>
		/// Gets the number of entries in the cache.
		/// </summary>
		internal int Count => _debugModes.Count;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyDebugModeCache"/> class.
		/// </summary>
		public AssemblyDebugModeCache()
		{
			_debugModes = new List<AssemblyDebugMode>();
			_listLock = new ReaderWriterLockSlim();
			_isDisposed = 0;
		}
		#endregion

		#region Destructor
		/// <summary>
		/// Destructor.
		/// </summary>
		~AssemblyDebugModeCache()
		{
			Dispose(false);
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Attempts to find the debug mode setting with the best match for the specified assembly.
		/// </summary>
		/// <param name="assembly">The assembly to get a facility identifier override for.</param>
		/// <returns><see langword="true"/>, if a match for the specified <paramref name="assembly"/> was found, and <see cref="AssemblyDebugMode.IsEnabled"/> is <see langword="true"/>; otherwise, <see langword="false"/>.</returns>
		/// <remarks>See <see cref="AssemblyIdentity.Match(Assembly)"/> for the algorithm to find the best match for the assembly.</remarks>
		/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
		public bool IsDebugModeEnabled(Assembly assembly)
		{
			AssertDisposed();
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			int match, highestMatch = -1;
			_listLock.EnterReadLock();
			bool returnValue = false;
			try
			{
				foreach (AssemblyDebugMode debugMode in _debugModes)
				{
					if ((match = debugMode.AssemblyName.Match(assembly)) > 0 && match > highestMatch)
					{
						highestMatch = match;
						returnValue = debugMode.IsEnabled;
						if (match == AssemblyIdentity.MaximumMatchValue)
						{
							break; // Can't get any better.
						}
					}
				}
			}
			finally
			{
				_listLock.ExitReadLock();
			}
			return returnValue;
		}

		/// <summary>
		/// Attempts to find the debug mode setting with the best match for the assembly containing the specified type.
		/// </summary>
		/// <param name="type">The type that is part of the assembly to get the debug mode setting for.</param>
		/// <returns><see langword="true"/>, if a match for the specified type's assembly was found, and <see cref="AssemblyDebugMode.IsEnabled"/> is <see langword="true"/>; otherwise, <see langword="false"/>.</returns>
		/// <remarks>See <see cref="AssemblyIdentity.Match(Assembly)"/> for the algorithm to find the best match for the assembly.</remarks>
		/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
		public bool IsDebugModeEnabled(Type type)
		{
			AssertDisposed();
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			return IsDebugModeEnabled(type.Assembly);
		}

		/// <summary>
		/// Adds the specified assembly debug mode setting to the cache.
		/// </summary>
		/// <param name="debugMode">The debug mode setting to add.</param>
		/// <remarks>If an override with the identical <see cref="AssemblyDebugMode.AssemblyName"/> already exists in the cache, it is replaced with the current object.</remarks>
		/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="debugMode"/> is <see langword="null"/>.</exception>
		public void Add(AssemblyDebugMode debugMode)
		{
			AssertDisposed();
			if (debugMode == null)
			{
				throw new ArgumentNullException(nameof(debugMode));
			}

			_listLock.EnterWriteLock();
			try
			{
				for (int i = 0; i < _debugModes.Count; i++)
				{
					if (debugMode.AssemblyName.Equals(_debugModes[i].AssemblyName))
					{
						_debugModes[i] = debugMode;
						return;
					}
				}
				_debugModes.Add(debugMode);
			}
			finally
			{
				_listLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Adds the specified assembly identity and debug mode setting to the cache.
		/// </summary>
		/// <param name="assembly">The <see cref="AssemblyIdentity"/> to add a debug mode setting for.</param>
		/// <param name="isEnabled">A value indicating if the debug mode is activated for the assembly.</param>
		/// <remarks>If an override with the identical <see cref="AssemblyDebugMode.AssemblyName"/> already exists in the cache, it is replaced with the current setting.</remarks>
		/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
		public void Add(AssemblyIdentity assembly, bool isEnabled) => Add(new AssemblyDebugMode(assembly, isEnabled));

		/// <summary>
		/// Adds a list of <see cref="AssemblyDebugMode"/>s to the cache.
		/// </summary>
		/// <param name="debugModes">A list of <see cref="AssemblyDebugMode"/>s. May be empty or <see langword="null"/>.</param>
		/// <remarks>If an override with the identical <see cref="AssemblyDebugMode.AssemblyName"/> already exists in the cache, it is replaced with the override in the specified list.</remarks>
		/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
		public void AddRange(IEnumerable<AssemblyDebugMode> debugModes)
		{
			if (debugModes == null)
			{
				return;
			}

			_listLock.EnterWriteLock();
			try
			{
				bool alreadyExists;
				foreach (AssemblyDebugMode debugMode in debugModes)
				{
					alreadyExists = false;
					for (int i = 0; i < _debugModes.Count; i++)
					{
						if (debugMode.AssemblyName.Equals(_debugModes[i].AssemblyName))
						{
							_debugModes[i] = debugMode;
							alreadyExists = true;
							break;
						}
					}
					if (!alreadyExists)
					{
						_debugModes.Add(debugMode);
					}
				}
			}
			finally
			{
				_listLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Clears the cache.
		/// </summary>
		/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
		public void Clear()
		{
			AssertDisposed();
			_listLock.EnterWriteLock();
			try
			{
				_debugModes.Clear();
			}
			finally
			{
				_listLock.ExitWriteLock();
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Checks if the object has already been disposed.
		/// </summary>
		/// <exception cref="ObjectDisposedException">The object is already disposed.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void AssertDisposed()
		{
			if (_isDisposed == 1)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}
		#endregion

		#region IDisposed implementation
		/// <summary>
		/// Releases all resources used by the <see cref="AssemblyDebugModeCache"/>.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="AssemblyDebugModeCache"/> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
		private void Dispose(bool disposing)
		{
			if (Interlocked.Exchange(ref _isDisposed, 1) == 1)
			{
				return;
			}

			if (disposing)
			{
				_debugModes?.Clear();
				_listLock?.Dispose();
			}
		}
		#endregion
	}
}
