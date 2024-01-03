// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Manages an easily queryable list of assembly debug mode flags.
/// </summary>
[ComVisible(false)]
public sealed class AssemblyDebugModeCache : IDisposable
{
	private static readonly Lazy<AssemblyDebugModeCache> s_global = new(() => new AssemblyDebugModeCache());
	private readonly List<AssemblyDebugMode> _debugModes;
	private readonly ReaderWriterLockSlim _listLock;
	private int _isDisposed;
	private bool _canNotifyChange;
	private bool _hasChanges;

	/// <summary>
	/// Notifies listeners of dynamic changes, such as when an item is added and removed or the whole cache is cleared.
	/// </summary>
	public event EventHandler? CacheChanged;

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

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyDebugModeCache"/> class.
	/// </summary>
	public AssemblyDebugModeCache()
	{
		_debugModes = new List<AssemblyDebugMode>();
		_listLock = new ReaderWriterLockSlim();
		_isDisposed = 0;
		_canNotifyChange = true;
		_hasChanges = false;
	}

	/// <summary>
	/// Destructor.
	/// </summary>
	~AssemblyDebugModeCache()
	{
		Dispose(false);
	}

	/// <summary>
	/// Disables raising of the <see cref="CacheChanged" /> event when the cache is updated.
	/// </summary>
	public void BeginUpdate() => _canNotifyChange = false;

	/// <summary>
	/// Enables raising of the <see cref="CacheChanged" /> event when the cache is updated, and raises the event if any changes happened since the last time <see cref="BeginUpdate" /> was called.
	/// </summary>
	public void EndUpdate()
	{
		_canNotifyChange = true;
		if (_hasChanges)
		{
			_hasChanges = false;
			OnCollectionChanged();
		}
	}

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
#if NET7_0_OR_GREATER
		ObjectDisposedException.ThrowIf(_isDisposed == 1, this);
#else
		if (_isDisposed == 1)
		{
			throw new ObjectDisposedException(GetType().Name);
		}
#endif
#if NETFRAMEWORK
		if (assembly == null)
		{
			throw new ArgumentNullException(nameof(assembly));
		}
#else
		ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
#endif

		bool result = false;
		_listLock.EnterReadLock();
		try
		{
			result = ExtensionHelper.GetMaximumMatch(_debugModes, assembly, (debugMode) => debugMode.AssemblyName)?.IsEnabled ?? false;
		}
		finally
		{
			_listLock.ExitReadLock();
		}
		return result;
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
#if NET7_0_OR_GREATER
		ObjectDisposedException.ThrowIf(_isDisposed == 1, this);
#else
		if (_isDisposed == 1)
		{
			throw new ObjectDisposedException(GetType().Name);
		}
#endif
		return type == null ? throw new ArgumentNullException(nameof(type)) : IsDebugModeEnabled(type.Assembly);
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
#if NET7_0_OR_GREATER
		ObjectDisposedException.ThrowIf(_isDisposed == 1, this);
#else
		if (_isDisposed == 1)
		{
			throw new ObjectDisposedException(GetType().Name);
		}
#endif
#if NETFRAMEWORK
		if (debugMode == null)
		{
			throw new ArgumentNullException(nameof(debugMode));
		}
#else
		ArgumentNullException.ThrowIfNull(debugMode, nameof(debugMode));
#endif

		bool raiseEvent = false;
		_listLock.EnterWriteLock();
		try
		{
			for (int i = 0; i < _debugModes.Count; i++)
			{
				if (debugMode.AssemblyName.Equals(_debugModes[i].AssemblyName))
				{
					_debugModes[i] = debugMode;
					raiseEvent = true;
					return;
				}
			}
			_debugModes.Add(debugMode);
			raiseEvent = true;
		}
		finally
		{
			_listLock.ExitWriteLock();
		}

		if (raiseEvent)
		{
			OnCollectionChanged();
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

		bool raiseEvent = false;
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
						raiseEvent = true;
						break;
					}
				}
				if (!alreadyExists)
				{
					_debugModes.Add(debugMode);
					raiseEvent = true;
				}
			}
		}
		finally
		{
			_listLock.ExitWriteLock();
		}

		if (raiseEvent)
		{
			OnCollectionChanged();
		}
	}

	/// <summary>
	/// Clears the cache.
	/// </summary>
	/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
	public void Clear()
	{
#if NET7_0_OR_GREATER
		ObjectDisposedException.ThrowIf(_isDisposed == 1, this);
#else
		if (_isDisposed == 1)
		{
			throw new ObjectDisposedException(GetType().Name);
		}
#endif
		_listLock.EnterWriteLock();
		try
		{
			_debugModes.Clear();
		}
		finally
		{
			_listLock.ExitWriteLock();
		}
		OnCollectionChanged();
	}

	/// <summary>
	/// Removes the debug mode information for the assembly with the specified identity from the cache.
	/// </summary>
	/// <param name="identity">The identity of the assembly to remove from the cache.</param>
	/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="identity"/> is <see langword="null"/>.</exception>
	public void Remove(AssemblyIdentity identity)
	{
#if NET7_0_OR_GREATER
		ObjectDisposedException.ThrowIf(_isDisposed == 1, this);
#else
		if (_isDisposed == 1)
		{
			throw new ObjectDisposedException(GetType().Name);
		}
#endif
#if NETFRAMEWORK
		if (identity == null)
		{
			throw new ArgumentNullException(nameof(identity));
		}
#else
		ArgumentNullException.ThrowIfNull(identity, nameof(identity));
#endif

		bool raiseEvent = false;
		_listLock.EnterWriteLock();
		try
		{
			for (int i = 0; i < _debugModes.Count; i++)
			{
				if (identity.Equals(_debugModes[i].AssemblyName))
				{
					_debugModes.RemoveAt(i);
					raiseEvent = true;
					break;
				}
			}
		}
		finally
		{
			_listLock.ExitWriteLock();
		}

		if (raiseEvent)
		{
			OnCollectionChanged();
		}
	}

	/// <summary>
	/// Raises the <see cref="CacheChanged" />.
	/// </summary>
	private void OnCollectionChanged()
	{
		if (_canNotifyChange)
		{
			CacheChanged?.Invoke(this, EventArgs.Empty);
		}
		else
		{
			_hasChanges = true;
		}
	}

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
			_canNotifyChange = false;
			_debugModes?.Clear();
			_listLock?.Dispose();
		}
	}
}
