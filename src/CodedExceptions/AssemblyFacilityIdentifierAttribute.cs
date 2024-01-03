// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions;

/// <summary>
/// When applied to an assembly, specifies the facility id of the assembly when creating
/// <see cref="Exception.HResult"/> values for coded exceptions.
/// </summary>
/// <remarks>
/// <para>Apply this attribute to an assembly if that assembly throws exceptions with custom <see cref="Exception.HResult"/> values,
/// and you want to create values that adhere to the HRESULT format specified by Microsoft. See the MSDN for more information about
/// the definition of HRESULT values.</para>
/// <para>The <see cref="FacilityId"/> value of the attribute defines the lower bits of the higher two bytes of the HRESULT value.
/// It can be used to discover the assembly that an exception is thrown by. The value can range between 0 and 2047, as 11 bits
/// are available for the facility. The highest 5 bits are reserved, and should always be set to value 0xa, as that specifies a
/// custom HRESULT value (values from Microsoft always start with 0x8). The lower two bytes of the HRESULT are used for specific
/// error numbers, so a maximum of 65,535 individual error numbers per assembly are possible using this schema.</para>
/// </remarks>
/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
/// <example>
/// <para>First decorate your assembly with the <see cref="AssemblyFacilityIdentifierAttribute"/>.</para>
/// <code source="..\..\doc\Examples\AssemblyFacilityIdentifierAttribute.cs" region="example1part1" language="C#" title="AssemblyInfo.cs" />
///
/// <para>Then you can use the <see cref="AssemblyFacilityIdentifierAttribute"/> and <see cref="HResultHelper"/> classes to retrieve the
/// facility id and build a HRESULT value.</para>
///
/// <code source="..\..\doc\Examples\AssemblyFacilityIdentifierAttribute.cs" region="example1part2" language="C#" title="Program.cs" />
/// </example>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
[ComVisible(false)]
public sealed class AssemblyFacilityIdentifierAttribute : System.Attribute
{
	/// <summary>
	/// Gets the identifier for the facility (= the attributed assembly).
	/// </summary>
	/// <value>An integer ranging between 0 and 2047.</value>
	public int FacilityId
	{
		get; private set;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyFacilityIdentifierAttribute"/> with the specified facility id.
	/// </summary>
	/// <param name="facilityId">The identifier for the facility (= the attributed assembly).</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="facilityId"/> is less than 0 or greater than 2048.</exception>
	public AssemblyFacilityIdentifierAttribute(int facilityId)
	{
		if (facilityId is < 0 or > 2047)
		{
			throw new ArgumentOutOfRangeException(nameof(facilityId), TextResources.Global_FacilityId_OutOfRange);
		}
		FacilityId = facilityId;
	}

	/// <summary>
	/// Gets an <see cref="AssemblyFacilityIdentifierAttribute"/> located in the specified assembly.
	/// </summary>
	/// <param name="assembly">The assembly to retrieve the <see cref="AssemblyFacilityIdentifierAttribute"/> from.</param>
	/// <returns>An <see cref="AssemblyFacilityIdentifierAttribute"/>, or <see langword="null"/>, if no matching attribute is found in the <paramref name="assembly"/>.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
	public static AssemblyFacilityIdentifierAttribute? FromAssembly(Assembly assembly) => assembly == null
			? throw new ArgumentNullException(nameof(assembly))
			: assembly.GetCustomAttribute<AssemblyFacilityIdentifierAttribute>();

	/// <summary>
	/// Gets an <see cref="AssemblyFacilityIdentifierAttribute"/> located in the assembly that defines the specified type.
	/// </summary>
	/// <param name="type">The type that is defined by the assembly that is checked for the attribute.</param>
	/// <returns>An <see cref="AssemblyFacilityIdentifierAttribute"/>, or <see langword="null"/>, if no matching attribute is found.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
	public static AssemblyFacilityIdentifierAttribute? FromType(Type type) => type == null ? throw new ArgumentNullException(nameof(type)) : FromAssembly(type.GetTypeInfo().Assembly);
}
