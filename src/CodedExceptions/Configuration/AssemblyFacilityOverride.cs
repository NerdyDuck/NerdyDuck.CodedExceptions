// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions.Configuration;
/// <summary>
/// Represents an override for a facility identifier.
/// </summary>
#if NETFRAMEWORK
[Serializable]
#endif
[ComVisible(false)]
public sealed class AssemblyFacilityOverride : IEquatable<AssemblyFacilityOverride>
#if NETFRAMEWORK
	, ISerializable
#endif
{
	/// <summary>
	/// Gets the name of the assembly that the identifier override is configured for.
	/// </summary>
	/// <value>A <see cref="AssemblyIdentity"/> defining an assembly or a range of assemblies.</value>
	public AssemblyIdentity AssemblyName
	{
		get; private set;
	}

	/// <summary>
	/// Gets the overridden identifier value.
	/// </summary>
	/// <value>The new assembly facility identifier for the assembly specified in <see cref="AssemblyName"/></value>
	public int Identifier
	{
		get; private set;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyFacilityOverride"/> class with the specified assembly name and facility identifier.
	/// </summary>
	/// <param name="assemblyName">The name of the assembly that the identifier override is configured for.</param>
	/// <param name="identifier">The overridden identifier value.</param>
	/// <exception cref="ArgumentNullException"><paramref name="assemblyName"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="identifier"/> is less than 0 or greater than 2017.</exception>
	public AssemblyFacilityOverride(AssemblyIdentity assemblyName, int identifier)
	{
		AssertIdentifier(identifier);
		AssemblyName = assemblyName ?? throw new ArgumentNullException(nameof(assemblyName));
		Identifier = identifier;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyFacilityOverride"/> class with the specified assembly name and facility identifier.
	/// </summary>
	/// <param name="assemblyName">The name of the assembly that the identifier override is configured for.</param>
	/// <param name="identifier">The overridden identifier value.</param>
	/// <exception cref="ArgumentNullException"><paramref name="assemblyName"/> is null or empty.</exception>
	/// <exception cref="FormatException"><paramref name="assemblyName"/> is not a valid assembly name (full or partially qualified).</exception>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="identifier"/> is less than 0 or greater than 2017.</exception>
	public AssemblyFacilityOverride(string assemblyName, int identifier)
	{
		AssertIdentifier(identifier);
		AssemblyName = new AssemblyIdentity(assemblyName);
		Identifier = identifier;
	}

	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to the current <see cref="AssemblyFacilityOverride" />.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with the current <see cref="AssemblyFacilityOverride" />.</param>
	/// <returns><see langword="true" /> if the specified <see cref="object" /> is equal to the current <see cref="AssemblyFacilityOverride" />; otherwise, <see langword="false" />. </returns>
	public override bool Equals(object? obj) => obj != null && obj is AssemblyFacilityOverride afo && Equals(afo);

	/// <summary>
	/// Serves as the default hash function.
	/// </summary>
	/// <returns>A hash code for the current object.</returns>
	public override int GetHashCode() => AssemblyName.GetHashCode();

	/// <summary>
	/// Determines whether the specified <see cref="AssemblyFacilityOverride" /> is equal to the current instance.
	/// </summary>
	/// <param name="other">The <see cref="AssemblyFacilityOverride" /> to compare with the current instance.</param>
	/// <returns><see langword="true" /> if the specified <see cref="AssemblyFacilityOverride" /> is equal to the current instance; otherwise, <see langword="false" />. </returns>
	public bool Equals(AssemblyFacilityOverride? other) => other is not null && AssemblyName.Equals(other.AssemblyName) && Identifier == other.Identifier;

#if NETFRAMEWORK
	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyFacilityOverride"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The instance could not be deserialized correctly.</exception>
	private AssemblyFacilityOverride(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
		{
			throw new ArgumentNullException(nameof(info));
		}

		AssemblyName = (AssemblyIdentity)(info.GetValue(nameof(AssemblyName), typeof(AssemblyIdentity)) ?? throw new SerializationException(TextResources.Global_ctor_MissingAssemblyIdentifier));
		Identifier = info.GetInt32(nameof(Identifier));
	}

	/// <summary>
	/// Sets the <see cref="SerializationInfo"/> with information about the object.
	/// </summary>
	/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data.</param>
	/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
	void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
		{
			throw new ArgumentNullException(nameof(info));
		}
		info.AddValue(nameof(AssemblyName), AssemblyName);
		info.AddValue(nameof(Identifier), Identifier);
	}
#endif

	/// <summary>
	/// Checks if the identifier is within range (0 to 2047).
	/// </summary>
	/// <param name="identifier">The identifier to check.</param>
	private static void AssertIdentifier(int identifier)
	{
		if (identifier is < 0 or > 2047)
		{
			throw new ArgumentOutOfRangeException(nameof(identifier), TextResources.Global_FacilityId_OutOfRange);
		}
	}
}
