#region Copyright
/*******************************************************************************
 * <copyright file="CodedExceptionAttribute.cs" owner="Daniel Kopp">
 * Copyright 2015-2016 Daniel Kopp
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
 * <file name="CodedExceptionAttribute.cs" date="2015-08-05">
 * When applied to a class that inherits from Exception, specifies that the
 * HResult property contains a custom error code.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Reflection;

namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// When applied to a class that inherits from <see cref="Exception"/>,
	/// specifies that the <see cref="Exception.HResult"/> property contains a custom error code.
	/// </summary>
	/// <remarks>
	/// When creating a new exception that can contain custom <see cref="Exception.HResult"/> values,
	/// decorate the class with the <see cref="CodedExceptionAttribute"/>, so that other code knows
	/// that the <see cref="Exception.HResult"/> property contains a meaningful coded numerical value
	/// that identifies an application-specific exception.
	/// </remarks>
	/// <example>
	/// <code language="C#" title="Using the attribute">
	/// using NerdyDuck.CodedExceptions;
	///
	/// [CodedException]
	/// public class MyException : System.Exception
	/// {
	///     ...
	/// }
	/// </code>
	/// </example>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class CodedExceptionAttribute : System.Attribute
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedExceptionAttribute"/> class.
		/// </summary>
		public CodedExceptionAttribute()
			: base()
		{
		}
		#endregion

		#region Public static methods
		/// <summary>
		/// Checks if the type of the specified instance of <see cref="Exception"/>
		/// or a derived class is decorated with a <see cref="CodedExceptionAttribute"/>.
		/// </summary>
		/// <param name="ex">The <see cref="Exception"/> instance to check for a <see cref="CodedExceptionAttribute"/>.</param>
		/// <returns><see langword="true"/>, if the type has a <see cref="CodedExceptionAttribute"/>; otherwise, <see langword="false"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="ex"/> is <see langword="null"/>.</exception>
		/// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
		public static bool IsCodedException(Exception ex)
		{
			if (ex == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(0x01), nameof(ex));

			return (ex.GetType().GetTypeInfo().GetCustomAttribute<CodedExceptionAttribute>(true) != null);
		}
		#endregion
	}
}
