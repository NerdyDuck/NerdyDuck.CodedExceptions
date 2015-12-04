#region Copyright
/*******************************************************************************
 * <copyright file="ExceptionExtensions.cs" owner="Daniel Kopp">
 * Copyright 2015 Daniel Kopp
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * </copyright>
 * <author name="Daniel Kopp" email="dak@nerdyduck.de" />
 * <assembly name="NerdyDuck.CodedExceptions">
 * Exceptions with custom HRESULTs for .NET
 * </assembly>
 * <file name="ExceptionExtensions.cs" date="2015-08-06">
 * Extends the System.Exception class with methods to easily handle
 * coded exceptions.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.ComponentModel;

namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// Extends the <see cref="Exception" /> class with methods to easily handle coded exceptions.
	/// </summary>
	/// <remarks>Most functionality is available via the static methods of <see cref="HResultHelper"/>.</remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ExceptionExtensions
	{
		#region GetErrorId
		/// <summary>
		/// Gets the error identifier part of a <see cref="Exception.HResult"/>.
		/// </summary>
		/// <param name="ex">The <see cref="Exception"/> to extract the error identifier from.</param>
		/// <returns>An error identifier.</returns>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		public static int GetErrorId(this Exception ex)
		{
			if (ex == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x08), nameof(ex));

			return HResultHelper.GetErrorId(ex.HResult);
		}
		#endregion

		#region GetFacilityId
		/// <summary>
		/// Gets the facility (= the assembly) identifier part of an <see cref="Exception.HResult"/>.
		/// </summary>
		/// <param name="ex">The <see cref="Exception"/> to extract the facility identifier from.</param>
		/// <returns>The facility identifier.</returns>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		public static int GetFacilityId(this Exception ex)
		{
			if (ex == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x09), nameof(ex));

			return HResultHelper.GetFacilityId(ex.HResult);
		}
		#endregion

		#region IsCodedException
		/// <summary>
		/// Checks if the type of the specified instance of <see cref="Exception"/>
		/// or a derived class has a <see cref="CodedExceptionAttribute"/>.
		/// </summary>
		/// <param name="ex">The <see cref="Exception"/> instance to check for a <see cref="CodedExceptionAttribute"/>.</param>
		/// <returns><see langword="true"/>, if the type has a <see cref="CodedExceptionAttribute"/>; otherwise, <see langword="false"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="ex"/> is <see langword="null"/>.</exception>
		/// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
		public static bool IsCodedException(this Exception ex)
		{
			if (ex == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x0a), nameof(ex));

			return CodedExceptionAttribute.IsCodedException(ex);
		}
		#endregion

		#region IsCustomHResult
		/// <summary>
		/// Checks if the <see cref="Exception.HResult"/> of the exception is a custom error value.
		/// </summary>
		/// <param name="ex">The exception to check.</param>
		/// <returns><see langword="true"/>, if <paramref name="ex"/> is a custom value; otherwise, <see langword="false"/>.</returns>
		public static bool IsCustomHResult(this Exception ex)
		{
			if (ex == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x0b), nameof(ex));

			return HResultHelper.IsCustomHResult(ex.HResult);
		}
		#endregion
	}
}
