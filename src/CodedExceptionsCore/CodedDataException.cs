#region Copyright
/*******************************************************************************
 * <copyright file="CodedDataException.cs" owner="Daniel Kopp">
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
 * <file name="CodedDataException.cs" date="2015-08-06">
 * Represents the exception that is thrown when errors are generated using
 * ADO.NET components. This exception provides constructors to set custom
 * HResult values.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Runtime.InteropServices;

namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// Represents the exception that is thrown when errors are generated using ADO.NET components.
	/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
	/// </summary>
#if WINDOWS_DESKTOP
	[Serializable]
#endif
	[CodedException]
	public class CodedDataException
#if WINDOWS_DESKTOP
		: System.Data.DataException
#endif
#if WINDOWS_UWP || NETCORE
		: System.Exception
#endif
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedDataException"/> class with a default message, and its inner exception set to a null reference.
		/// </summary>
		/// <remarks>
		/// The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a message that describes the error, such as "Could not find the specified directory." This message takes into account the current system culture.
		/// </remarks>
		public CodedDataException()
			: base(Properties.Resources.CodedDataException_Message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedDataException"/> class with with its message string set to <paramref name="message"/> and its inner exception set to <see langword="null"/>.
		/// </summary>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <remarks>This constructor initializes the Message property of the new instance using <paramref name="message"/>.</remarks>
		public CodedDataException(string message)
			: base(message ?? Properties.Resources.CodedDataException_Message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedDataException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		public CodedDataException(string message, Exception innerException)
			: base(message ?? Properties.Resources.CodedDataException_Message, innerException)
		{
		}

#if WINDOWS_DESKTOP
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedDataException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		protected CodedDataException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
#endif

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedDataException"/> class with the specified HRESULT value.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "Could not find the specified directory." This message takes into account the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedDataException(int hresult)
			: base(Properties.Resources.CodedDataException_Message)
		{
			HResult = hresult;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedDataException"/> class with a specified HRESULT value and error message.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		public CodedDataException(int hresult, string message)
			: base(message ?? Properties.Resources.CodedDataException_Message)
		{
			HResult = hresult;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedDataException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		public CodedDataException(int hresult, string message, Exception innerException)
			: base(message ?? Properties.Resources.CodedDataException_Message, innerException)
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
