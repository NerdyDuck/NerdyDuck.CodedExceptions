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

using System;
using System.Reflection;
using System.Runtime.InteropServices;

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
	[ComVisible(false)]
	public sealed class CodedExceptionAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedExceptionAttribute"/> class.
		/// </summary>
		public CodedExceptionAttribute()
			: base()
		{
		}

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
			{
				throw new ArgumentNullException(nameof(ex));
			}

			return (ex.GetType().GetTypeInfo().GetCustomAttribute<CodedExceptionAttribute>(true) != null);
		}
	}
}
