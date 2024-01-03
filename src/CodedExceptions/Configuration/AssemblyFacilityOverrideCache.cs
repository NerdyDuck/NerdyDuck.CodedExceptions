// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Manages an easily queryable list of overrides for assembly identifiers.
/// </summary>
[ComVisible(false)]
public sealed class AssemblyFacilityOverrideCache : IDisposable
{
	private static readonly Lazy<AssemblyFacilityOverrideCache> s_global = new(() => new AssemblyFacilityOverrideCache());
	private readonly List<AssemblyFacilityOverride> _facilityOverrides;
	private readonly ReaderWriterLockSlim _listLock;
	private int _isDisposed;

	/// <summary>
	/// Gets a global singleton instance of <see cref="AssemblyFacilityOverrideCache"/>.
	/// </summary>
	/// <value>A <see langword="static"/> instance of <see cref="AssemblyFacilityOverrideCache"/>.</value>
	/// <remarks>This instance is used by the <c>Errors</c> class that is automatically added to every project referencing the NerdyDuck.CodedExceptions NuGet package.</remarks>
	public static AssemblyFacilityOverrideCache Global => s_global.Value;

	/// <summary>
	/// Gets the number of entries in the cache.
	/// </summary>
	internal int Count => _facilityOverrides.Count;

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyFacilityOverrideCache"/> class.
	/// </summary>
	public AssemblyFacilityOverrideCache()
	{
		_facilityOverrides = [];
		_listLock = new ReaderWriterLockSlim();
		_isDisposed = 0;
	}

	/// <summary>
	/// Destructor.
	/// </summary>
	~AssemblyFacilityOverrideCache()
	{
		Dispose(false);
	}

	/// <summary>
	/// Attempts to find the facility identifier override with the best match for the specified assembly.
	/// </summary>
	/// <param name="assembly">The assembly to get a facility identifier override for.</param>
	/// <param name="identifier">When this method returns, contains the identifier for the specified assembly, if found. Otherwise the value is 0. This parameter is passed uninitialized.</param>
	/// <returns><see langword="true"/>, if a match for the specified <paramref name="assembly"/> was found; otherwise, <see langword="false"/>.</returns>
	/// <remarks>See <see cref="AssemblyIdentity.Match(Assembly)"/> for the algorithm to find the best match for the assembly.</remarks>
	/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
	public bool TryGetOverride(Assembly assembly, out int identifier)
	{
		identifier = 0;
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

		_listLock.EnterReadLock();
		try
		{
			identifier = ExtensionHelper.GetMaximumMatch(_facilityOverrides, assembly, (facilityOverride) => facilityOverride.AssemblyName)?.Identifier ?? 0;

		}
		finally
		{
			_listLock.ExitReadLock();
		}
		return identifier > 0;
	}

	/// <summary>
	/// Attempts to find the facility identifier override with the best match for the assembly containing the specified type.
	/// </summary>
	/// <param name="type">The type that is part of the assembly to get a facility identifier override for.</param>
	/// <param name="identifier">When this method returns, contains the identifier for the assembly, if found. Otherwise the value is 0. This parameter is passed uninitialized.</param>
	/// <returns><see langword="true"/>, if a match for the assembly of specified <paramref name="type"/> was found; otherwise, <see langword="false"/>.</returns>
	/// <remarks>See <see cref="AssemblyIdentity.Match(Assembly)"/> for the algorithm to find the best match for the assembly.</remarks>
	/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
	public bool TryGetOverride(Type type, out int identifier)
	{
#if NET7_0_OR_GREATER
		ObjectDisposedException.ThrowIf(_isDisposed == 1, this);
#else
		if (_isDisposed == 1)
		{
			throw new ObjectDisposedException(GetType().Name);
		}
#endif
		return type == null ? throw new ArgumentNullException(nameof(type)) : TryGetOverride(type.Assembly, out identifier);
	}

	/// <summary>
	/// Adds the specified assembly facility identifier override to the cache.
	/// </summary>
	/// <param name="facilityOverride">The override to add.</param>
	/// <remarks>If an override with the identical <see cref="AssemblyFacilityOverride.AssemblyName"/> already exists in the cache, it is replaced with the current override.</remarks>
	/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="facilityOverride"/> is <see langword="null"/>.</exception>
	public void Add(AssemblyFacilityOverride facilityOverride)
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
		if (facilityOverride == null)
		{
			throw new ArgumentNullException(nameof(facilityOverride));
		}
#else
		ArgumentNullException.ThrowIfNull(facilityOverride, nameof(facilityOverride));
#endif

		_listLock.EnterWriteLock();
		try
		{
			for (int i = 0; i < _facilityOverrides.Count; i++)
			{
				if (facilityOverride.AssemblyName.Equals(_facilityOverrides[i].AssemblyName))
				{
					_facilityOverrides[i] = facilityOverride;
					return;
				}
			}
			_facilityOverrides.Add(facilityOverride);
		}
		finally
		{
			_listLock.ExitWriteLock();
		}
	}

	/// <summary>
	/// Adds the specified assembly identity and facility override to the cache.
	/// </summary>
	/// <param name="assembly">The <see cref="AssemblyIdentity"/> to add an identifier override for.</param>
	/// <param name="identifier">The new facility identifier for the assembly.</param>
	/// <remarks>If an override with the identical <see cref="AssemblyFacilityOverride.AssemblyName"/> already exists in the cache, it is replaced with the current override.</remarks>
	/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
	/// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
	public void Add(AssemblyIdentity assembly, int identifier) => Add(new AssemblyFacilityOverride(assembly, identifier));

	/// <summary>
	/// Adds a list of <see cref="AssemblyFacilityOverride"/>s to the cache.
	/// </summary>
	/// <param name="facilityOverrides">A list of <see cref="AssemblyFacilityOverride"/>s. May be empty or <see langword="null"/>.</param>
	/// <remarks>If an override with the identical <see cref="AssemblyFacilityOverride.AssemblyName"/> already exists in the cache, it is replaced with the override in the specified list.</remarks>
	/// <exception cref="ObjectDisposedException">The current object is already disposed.</exception>
	public void AddRange(IEnumerable<AssemblyFacilityOverride> facilityOverrides)
	{
#if NET7_0_OR_GREATER
		ObjectDisposedException.ThrowIf(_isDisposed == 1, this);
#else
		if (_isDisposed == 1)
		{
			throw new ObjectDisposedException(GetType().Name);
		}
#endif
		if (facilityOverrides == null)
		{
			return;
		}

		_listLock.EnterWriteLock();
		try
		{
			bool alreadyExists;
			foreach (AssemblyFacilityOverride facilityOverride in facilityOverrides)
			{
				alreadyExists = false;
				for (int i = 0; i < _facilityOverrides.Count; i++)
				{
					if (facilityOverride.AssemblyName.Equals(_facilityOverrides[i].AssemblyName))
					{
						_facilityOverrides[i] = facilityOverride;
						alreadyExists = true;
						break;
					}
				}
				if (!alreadyExists)
				{
					_facilityOverrides.Add(facilityOverride);
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
			_facilityOverrides.Clear();
		}
		finally
		{
			_listLock.ExitWriteLock();
		}
	}

	/// <summary>
	/// Releases all resources used by the <see cref="AssemblyFacilityOverrideCache"/>.
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Releases the unmanaged resources used by the <see cref="AssemblyFacilityOverrideCache"/> and optionally releases the managed resources.
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
			_facilityOverrides?.Clear();
			_listLock?.Dispose();
		}
	}
}
