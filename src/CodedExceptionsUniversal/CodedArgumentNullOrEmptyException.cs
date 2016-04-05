﻿#region Copyright
/*******************************************************************************
 * <copyright file="CodedArgumentNullOrEmptyException.cs" owner="Daniel Kopp">
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
 * <file name="CodedArgumentNullOrEmptyException.cs" date="2015-10-23">
 * The exception that is thrown when one of the string arguments provided to a
 * method is null or empty. This exception provides constructors to set custom
 * HResult values.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Runtime.InteropServices;

namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// The exception that is thrown when one of the string arguments provided to a method is <see langword="null"/> (<b>Nothing</b> in Visual Basic) or empty. This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
	/// </summary>
	/// <remarks>This is the companion exception for <see cref="string.IsNullOrEmpty(string)"/>.</remarks>
#if WINDOWS_DESKTOP
	[Serializable]
#endif
	[CodedException]
	[ComVisible(true)]
	public class CodedArgumentNullOrEmptyException : ArgumentException
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedArgumentNullOrEmptyException"/> class.
		/// </summary>
		/// <remarks>This constructor initializes the Message property of the new instance to a system-supplied message that describes the error, such as "Argument cannot be null oe empty." This message takes into account the current system culture.</remarks>
		public CodedArgumentNullOrEmptyException()
			: base(Properties.Resources.CodedArgumentNullOrEmptyException_Message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedArgumentNullOrEmptyException"/> class with the name of the parameter that causes this exception.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "Argument cannot be null oe empty." This message takes into account the current system culture.</para>
		/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using the <paramref name="paramName"/> parameter. The content of <paramref name="paramName"/> is intended to be understood by humans.</para></remarks>
		public CodedArgumentNullOrEmptyException(string paramName)
			: base(Properties.Resources.CodedArgumentNullOrEmptyException_Message, paramName)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedArgumentNullOrEmptyException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</remarks>
		public CodedArgumentNullOrEmptyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedArgumentNullOrEmptyException"/> class with a specified error message and the name of the parameter that causes this exception.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused the current exception.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
		/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para></remarks>
		public CodedArgumentNullOrEmptyException(string paramName, string message)
			: base(message, paramName)
		{
		}

#if WINDOWS_DESKTOP
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedArgumentNullOrEmptyException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		protected CodedArgumentNullOrEmptyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
#endif

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedArgumentNullOrEmptyException"/> class with a specified HRESULT value and a system-supplied message that describes the error.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error,
		/// such as "Argument cannot be null." This message takes into account the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedArgumentNullOrEmptyException(int hresult)
			: base(Properties.Resources.CodedArgumentNullOrEmptyException_Message)
		{
			HResult = hresult;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedArgumentNullOrEmptyException"/> class with a specified HRESULT value and the name of the parameter that causes this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "Argument cannot be null oe empty." This message takes into account the current system culture.</para>
		/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using the <paramref name="paramName"/> parameter. The content of <paramref name="paramName"/> is intended to be understood by humans.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedArgumentNullOrEmptyException(int hresult, string paramName)
			: base(Properties.Resources.CodedArgumentNullOrEmptyException_Message, paramName)
		{
			HResult = hresult;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedArgumentNullOrEmptyException"/> class with a specified HRESULT value, an error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedArgumentNullOrEmptyException(int hresult, string message, Exception innerException)
			: base(message, innerException)
		{
			HResult = hresult;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedArgumentNullOrEmptyException"/> class with a specified HRESULT value, an error message and the name of the parameter that causes this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="paramName">The name of the parameter that caused the current exception.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
		/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedArgumentNullOrEmptyException(int hresult, string paramName, string message)
			: base(message, paramName)
		{
			HResult = hresult;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
		/// </summary>
		/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace. </returns>
		public override string ToString()
		{
			return HResultHelper.CreateToString(this, null);
		}
		#endregion
	}
}
