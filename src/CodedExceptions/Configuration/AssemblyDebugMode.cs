// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;

/// <summary>
/// Represents a debug mode setting for an assembly.
/// </summary>
#if NETFRAMEWORK
[Serializable]
#endif
[ComVisible(false)]
public sealed class AssemblyDebugMode : IEquatable<AssemblyDebugMode>
#if NETFRAMEWORK
	, ISerializable
#endif
{
	/// <summary>
	/// Gets the name of the assembly that the debug mode is set for.
	/// </summary>
	/// <value>A <see cref="AssemblyIdentity"/> defining an assembly or a range of assemblies.</value>
	public AssemblyIdentity AssemblyName
	{
		get; private set;
	}

	/// <summary>
	/// Gets a value indicating if debug mode is enabled for the assembly defined in <see cref="AssemblyName"/>.
	/// </summary>
	/// <value><see langword="true"/>, if the debug mode is enabled for the assembly; otherwise, <see langword="false"/>.</value>
	public bool IsEnabled
	{
		get; private set;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyDebugMode"/> class with the specified assembly name and debug mode setting.
	/// </summary>
	/// <param name="assemblyName">The name of the assembly that the debug mode is configured for.</param>
	/// <param name="isEnabled">A value indicating if debug mode is enabled for the assembly defined in <paramref name="assemblyName"/>.</param>
	/// <exception cref="ArgumentNullException"><paramref name="assemblyName"/> is <see langword="null"/>.</exception>
	public AssemblyDebugMode(AssemblyIdentity assemblyName, bool isEnabled)
	{
		AssemblyName = assemblyName ?? throw new ArgumentNullException(nameof(assemblyName));
		IsEnabled = isEnabled;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyDebugMode"/> class with the specified assembly name and debug mode setting.
	/// </summary>
	/// <param name="assemblyName">The name of the assembly that the debug mode is configured for.</param>
	/// <param name="isEnabled">A value indicating if debug mode is enabled for the assembly defined in <paramref name="assemblyName"/>.</param>
	/// <exception cref="ArgumentNullException"><paramref name="assemblyName"/> is <see langword="null"/>.</exception>
	/// <exception cref="FormatException"><paramref name="assemblyName"/> is not a valid assembly name (full or partially qualified).</exception>
	public AssemblyDebugMode(string assemblyName, bool isEnabled)
	{
		AssemblyName = new AssemblyIdentity(assemblyName);
		IsEnabled = isEnabled;
	}

	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to the current <see cref="AssemblyDebugMode" />.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with the current <see cref="AssemblyDebugMode" />.</param>
	/// <returns><see langword="true" /> if the specified <see cref="object" /> is equal to the current <see cref="AssemblyDebugMode" />; otherwise, <see langword="false" />. </returns>
	public override bool Equals(object? obj) => obj != null && obj is AssemblyDebugMode adm && Equals(adm);

	/// <summary>
	/// Serves as the default hash function.
	/// </summary>
	/// <returns>A hash code for the current object.</returns>
	public override int GetHashCode() => AssemblyName.GetHashCode() ^ IsEnabled.GetHashCode();

	/// <summary>
	/// Determines whether the specified <see cref="AssemblyDebugMode" /> is equal to the current instance.
	/// </summary>
	/// <param name="other">The <see cref="AssemblyDebugMode" /> to compare with the current instance.</param>
	/// <returns><see langword="true" /> if the specified <see cref="AssemblyDebugMode" /> is equal to the current instance; otherwise, <see langword="false" />. </returns>
	public bool Equals(AssemblyDebugMode? other) => other is not null && AssemblyName.Equals(other.AssemblyName) && IsEnabled == other.IsEnabled;

#if NETFRAMEWORK
	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyDebugMode"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The instance could not be deserialized correctly.</exception>
	private AssemblyDebugMode(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
		{
			throw new ArgumentNullException(nameof(info));
		}
		AssemblyName = (AssemblyIdentity)(info.GetValue(nameof(AssemblyName), typeof(AssemblyIdentity)) ?? throw new SerializationException(TextResources.Global_ctor_MissingAssemblyIdentifier));
		IsEnabled = info.GetBoolean(nameof(IsEnabled));
	}

	/// <summary>
	/// Sets the <see cref="SerializationInfo"/> with information about the exception.
	/// </summary>
	/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the object being thrown.</param>
	/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException"><paramref name="info"/> is <see langword="null" />.</exception>
	void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
		{
			throw new ArgumentNullException(nameof(info));
		}
		info.AddValue(nameof(AssemblyName), AssemblyName);
		info.AddValue(nameof(IsEnabled), IsEnabled);
	}
#endif
}
