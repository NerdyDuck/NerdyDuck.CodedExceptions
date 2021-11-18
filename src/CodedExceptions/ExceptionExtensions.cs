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
	/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</remarks>
	public static int GetErrorId(this Exception ex) => ex == null ? throw new ArgumentNullException(nameof(ex)) : HResultHelper.GetErrorId(ex.HResult);

	/// <summary>
	/// Gets the facility (= the assembly) identifier part of an <see cref="Exception.HResult"/>.
	/// </summary>
	/// <param name="ex">The <see cref="Exception"/> to extract the facility identifier from.</param>
	/// <returns>The facility identifier.</returns>
	/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</remarks>
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
