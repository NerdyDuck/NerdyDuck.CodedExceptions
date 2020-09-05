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
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// When applied to an assembly, specifies the facility id of the assembly when creating
	/// <see cref="Exception.HResult"/> values for coded exceptions.
	/// </summary>
	/// <remarks>
	/// <para>Apply this attribute to an assembly if that assembly throws exceptions with custom <see cref="Exception.HResult"/> values,
	/// and you want to create values that adhere to the HRESULT format specified by Microsoft. See the
	/// <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for more information about
	/// the definition of HRESULT values.</para>
	/// <para>The <see cref="FacilityId"/> value of the attribute defines the lower bits of the higher two bytes of the HRESULT value.
	/// It can be used to discover the assembly that an exception is thrown by. The value can range between 0 and 2047, as 11 bits
	/// are available for the facility. The highest 5 bits are reserved, and should always be set to value 0xa, as that specifies a
	/// custom HRESULT value (values from Microsoft always start with 0x8). The lower two bytes of the HRESULT are used for specific
	/// error numbers, so a maximum of 65,535 individual error numbers per assembly are possible using this schema.</para>
	/// </remarks>
	/// <example>
	/// <para>First decorate your assembly with the <see cref="AssemblyFacilityIdentifierAttribute"/>.</para>
	/// <code language="C#" title="Assembly.cs">
	/// using NerdyDuck.CodedExceptions;
	///
	/// [assembly: AssemblyFacilityIdentifier(0x0305)]
	/// </code>
	///
	/// <para>Then you can use the <see cref="AssemblyFacilityIdentifierAttribute"/> and <see cref="HResultHelper"/> classes to retrieve the
	/// facility id and build a HRESULT value.</para>
	///
	/// <code language="C#" title="Program.cs">
	/// using NerdyDuck.CodedExceptions;
	/// using System;
	/// using System.Reflection;
	///
	/// namespace Example
	/// {
	///     class Program
	///     {
	///         static void Main()
	///         {
	///             int facilityId = AssemblyFacilityIdentifierAttribute.FromAssembly(Assembly.GetExecutingAssembly()).FacilityId;
	///             // facilityId is 0x0305
	///             int baseHResult = HResultHelper.GetBaseHResult(facilityId);
	///             // baseHResult is 0xa3050000
	///             int myHResult = baseHResult | 0x42;
	///             // myHResult is 0xa3050042;
	///         }
	///     }
	/// }
	/// </code>
	/// </example>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(false)]
	public sealed class AssemblyFacilityIdentifierAttribute : System.Attribute
	{
		#region Properties
		/// <summary>
		/// Gets the identifier for the facility (= the attributed assembly).
		/// </summary>
		/// <value>An integer ranging between 0 and 2047.</value>
		public int FacilityId
		{
			get; private set;
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyFacilityIdentifierAttribute"/> with the specified facility id.
		/// </summary>
		/// <param name="facilityId">The identifier for the facility (= the attributed assembly).</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="facilityId"/> is less than 0 or greater than 2048.</exception>
		public AssemblyFacilityIdentifierAttribute(int facilityId)
		{
			if (facilityId < 0 || facilityId > 2047)
			{
				throw new ArgumentOutOfRangeException(nameof(facilityId), TextResources.Global_FacilityId_OutOfRange);
			}
			FacilityId = facilityId;
		}
		#endregion

		#region Public methods
		#region FromAssembly
		/// <summary>
		/// Gets an <see cref="AssemblyFacilityIdentifierAttribute"/> located in the specified assembly.
		/// </summary>
		/// <param name="assembly">The assembly to retrieve the <see cref="AssemblyFacilityIdentifierAttribute"/> from.</param>
		/// <returns>An <see cref="AssemblyFacilityIdentifierAttribute"/>, or <see langword="null"/>, if no matching attribute is found in the <paramref name="assembly"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
		public static AssemblyFacilityIdentifierAttribute? FromAssembly(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			return assembly.GetCustomAttribute<AssemblyFacilityIdentifierAttribute>();
		}
		#endregion

		#region FromType
		/// <summary>
		/// Gets an <see cref="AssemblyFacilityIdentifierAttribute"/> located in the assembly that defines the specified type.
		/// </summary>
		/// <param name="type">The type that is defined by the assembly that is checked for the attribute.</param>
		/// <returns>An <see cref="AssemblyFacilityIdentifierAttribute"/>, or <see langword="null"/>, if no matching attribute is found.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
		public static AssemblyFacilityIdentifierAttribute? FromType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}

			return FromAssembly(type.GetTypeInfo().Assembly);
		}
		#endregion
		#endregion
	}
}
