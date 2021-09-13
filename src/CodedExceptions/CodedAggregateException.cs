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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// Represents one or more errors that occur during application execution.
	/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
	/// </summary>
	[Serializable]
	[CodedException]
	[DebuggerDisplay("Count = {InnerExceptionsCount}")]
	[ComVisible(false)]
	public class CodedAggregateException : AggregateException
	{
		/// <summary>
		/// Gets the count of inner exceptions for display in the debugger.
		/// </summary>
		private int InnerExceptionsCount => InnerExceptions.Count;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a system-supplied message that describes the error.
		/// </summary>
		/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error,
		/// such as "One or more errors occurred." This message takes into account the current system culture.</remarks>
		public CodedAggregateException()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with references to the inner exceptions that are the cause of this exception.
		/// </summary>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is <see langword="null"/>.</exception>
		public CodedAggregateException(IEnumerable<Exception> innerExceptions)
			: base(innerExceptions)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with references to the inner exceptions that are the cause of this exception.
		/// </summary>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is <see langword="null"/>.</exception>
		public CodedAggregateException(params Exception[] innerExceptions)
			: base(innerExceptions)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified message that describes the error.
		/// </summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		public CodedAggregateException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="innerException"/> argument is <see langword="null"/>.</exception>
		public CodedAggregateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		protected CodedAggregateException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified error message and references to the inner exceptions that are the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is <see langword="null"/>.</exception>
		public CodedAggregateException(string message, IEnumerable<Exception> innerExceptions)
			: base(message, innerExceptions)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified error message and references to the inner exceptions that are the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is <see langword="null"/>.</exception>
		public CodedAggregateException(string message, params Exception[] innerExceptions)
			: base(message, innerExceptions)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified HRESULT value and a system-supplied message that describes the error.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error,
		/// such as "One or more errors occurred." This message takes into account the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedAggregateException(int hresult)
			: base() => HResult = hresult;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified HRESULT value and references to the inner exceptions that are the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		/// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is <see langword="null"/>.</exception>
		public CodedAggregateException(int hresult, IEnumerable<Exception> innerExceptions)
			: base(innerExceptions) => HResult = hresult;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified HRESULT value and references to the inner exceptions that are the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		/// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is <see langword="null"/>.</exception>
		public CodedAggregateException(int hresult, params Exception[] innerExceptions)
			: base(innerExceptions) => HResult = hresult;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified HRESULT value and a message that describes the error.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		public CodedAggregateException(int hresult, string message)
			: base(message) => HResult = hresult;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified HRESULT value, an error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		/// <exception cref="ArgumentNullException">The <paramref name="innerException"/> argument is <see langword="null"/>.</exception>
		public CodedAggregateException(int hresult, string message, Exception innerException)
			: base(message, innerException) => HResult = hresult;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified HRESULT value, an error message and references to the inner exceptions that are the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		/// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is <see langword="null"/>.</exception>
		public CodedAggregateException(int hresult, string message, IEnumerable<Exception> innerExceptions)
			: base(message, innerExceptions) => HResult = hresult;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedAggregateException"/> class with a specified HRESULT value, an error message and references to the inner exceptions that are the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		/// <exception cref="ArgumentNullException">The <paramref name="innerExceptions"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">An element of <paramref name="innerExceptions"/> is <see langword="null"/>.</exception>
		public CodedAggregateException(int hresult, string message, params Exception[] innerExceptions)
			: base(message, innerExceptions) => HResult = hresult;

		/// <summary>
		/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
		/// </summary>
		/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace. </returns>
		public override string ToString()
		{
			string CustomText = string.Empty;
			for (int i = 0; i < base.InnerExceptions.Count; i++)
			{
				CustomText = string.Format(System.Globalization.CultureInfo.InvariantCulture, TextResources.CodedAggregateException_ToString,
					CustomText, Environment.NewLine, i, InnerExceptions[i].ToString());
			}
			return HResultHelper.CreateToString(this, CustomText);
		}
	}
}
