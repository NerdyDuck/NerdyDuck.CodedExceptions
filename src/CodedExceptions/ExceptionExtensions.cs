// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;

namespace NerdyDuck.CodedExceptions;

/// <summary>
/// Extends the <see cref="Exception" /> class with methods to easily handle coded exceptions.
/// </summary>
/// <remarks>Most functionality is available via the static methods of <see cref="HResultHelper"/>.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ExceptionExtensions
{
	/// <summary>
	/// Gets the error identifier part of a <see cref="Exception.HResult"/>.
	/// </summary>
	/// <param name="ex">The <see cref="Exception"/> to extract the error identifier from.</param>
	/// <returns>An error identifier.</returns>
	/// <remarks>See the MSDN for more information about the definition of HRESULT values.</remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public static int GetErrorId(this Exception ex) => ex == null ? throw new ArgumentNullException(nameof(ex)) : HResultHelper.GetErrorId(ex.HResult);

	/// <summary>
	/// Gets the facility (= the assembly) identifier part of an <see cref="Exception.HResult"/>.
	/// </summary>
	/// <param name="ex">The <see cref="Exception"/> to extract the facility identifier from.</param>
	/// <returns>The facility identifier.</returns>
	/// <remarks>See the MSDN for more information about the definition of HRESULT values.</remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public static int GetFacilityId(this Exception ex) => ex == null ? throw new ArgumentNullException(nameof(ex)) : HResultHelper.GetFacilityId(ex.HResult);

	/// <summary>
	/// Checks if the type of the specified instance of <see cref="Exception"/>
	/// or a derived class has a <see cref="CodedExceptionAttribute"/>.
	/// </summary>
	/// <param name="ex">The <see cref="Exception"/> instance to check for a <see cref="CodedExceptionAttribute"/>.</param>
	/// <returns><see langword="true"/>, if the type has a <see cref="CodedExceptionAttribute"/>; otherwise, <see langword="false"/>.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="ex"/> is <see langword="null"/>.</exception>
	/// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
	public static bool IsCodedException(this Exception ex) => ex == null ? throw new ArgumentNullException(nameof(ex)) : CodedExceptionAttribute.IsCodedException(ex);

	/// <summary>
	/// Checks if the <see cref="Exception.HResult"/> of the exception is a custom error value.
	/// </summary>
	/// <param name="ex">The exception to check.</param>
	/// <returns><see langword="true"/>, if <paramref name="ex"/> is a custom value; otherwise, <see langword="false"/>.</returns>
	public static bool IsCustomHResult(this Exception ex) => ex == null ? throw new ArgumentNullException(nameof(ex)) : HResultHelper.IsCustomHResult(ex.HResult);
}
