#region Copyright
/*******************************************************************************
 * <copyright file="CodedSocketException.cs" owner="Daniel Kopp">
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
 * <file name="CodedSocketException.cs" date="2016-07-29">
 * The exception that is thrown when a socket error occurs. This exception
 * provides constructors to set custom HResult values.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
#if WINDOWS_DESKTOP
using System.Runtime.Serialization;
#endif

namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// The exception that is thrown when a socket error occurs.
	/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
	/// </summary>
	/// <remarks>This exception is not derived from <see cref="SocketException"/>, because that class provides only a minimum of constructors.</remarks>
#if WINDOWS_DESKTOP
	[Serializable]
#endif
	[CodedException]
	[ComVisible(true)]
	public class CodedSocketException : CodedException
	{
		#region Private fields
		private SocketError mSocketErrorCode;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the error code that is associated with this exception.
		/// </summary>
		/// <value>One of the <see cref="SocketError"/> enumeration values.</value>
		public SocketError SocketErrorCode
		{
			get { return mSocketErrorCode; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class.
		/// </summary>
		/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <see cref="SocketError.SocketError"/>, and the <see cref="Exception.Message"/> property with a message that describes the error, such as "A socket error occurred". For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
		public CodedSocketException()
			: base(Properties.Resources.CodedSocketException_Message)
		{
			HResult = HResultHelper.E_FAIL;
			mSocketErrorCode = SocketError.SocketError;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with the specified error code.
		/// </summary>
		/// <param name="errorCode">The error code that indicates the error that occurred.</param>
		/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <paramref name="errorCode"/>, and the <see cref="Exception.Message"/> property with a message that describes the error, such as "A socket error occurred". For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
		public CodedSocketException(SocketError errorCode)
			: base(Properties.Resources.CodedSocketException_Message)
		{
			HResult = HResultHelper.E_FAIL;
			mSocketErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <see cref="SocketError.SocketError"/>, and the <see cref="Exception.Message"/> property using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
		public CodedSocketException(string message)
			: base(message)
		{
			HResult = HResultHelper.E_FAIL;
			mSocketErrorCode = SocketError.SocketError;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified error message.
		/// </summary>
		/// <param name="errorCode">The error code that indicates the error that occurred.</param>
		/// <param name="message">The message that describes the error.</param>
		/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <paramref name="errorCode"/>, and the <see cref="Exception.Message"/> property using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
		public CodedSocketException(SocketError errorCode, string message)
			: base(message)
		{
			HResult = HResultHelper.E_FAIL;
			mSocketErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <see cref="SocketError.SocketError"/>, and the <see cref="Exception.Message"/> property using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
		public CodedSocketException(string message, Exception innerException)
			: base(message, innerException)
		{
			HResult = HResultHelper.E_FAIL;
			mSocketErrorCode = SocketError.SocketError;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="errorCode">The error code that indicates the error that occurred.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <paramref name="errorCode"/>, and the <see cref="Exception.Message"/> property using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
		public CodedSocketException(SocketError errorCode, string message, Exception innerException)
			: base(message, innerException)
		{
			HResult = HResultHelper.E_FAIL;
			mSocketErrorCode = errorCode;
		}

#if WINDOWS_DESKTOP
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		protected CodedSocketException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
			mSocketErrorCode = (SocketError)info.GetInt32(nameof(SocketErrorCode));
		}
#endif

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <remarks><para>This constructor initializes the Message property of the new instance to a system-supplied message that describes the error, such as "A socket error occurred". This message takes into account the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedSocketException(int hresult)
			: base(Properties.Resources.CodedSocketException_Message)
		{
			HResult = hresult;
			mSocketErrorCode = SocketError.SocketError;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value and socket error code.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="errorCode">The error code that indicates the error that occurred.</param>
		/// <remarks><para>This constructor initializes the Message property of the new instance to a system-supplied message that describes the error, such as "A socket error occurred". This message takes into account the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedSocketException(int hresult, SocketError errorCode)
			: base(Properties.Resources.CodedSocketException_Message)
		{
			HResult = hresult;
			mSocketErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value and error message.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">The message that describes the error.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedSocketException(int hresult, string message)
			: base(message)
		{
			HResult = hresult;
			mSocketErrorCode = SocketError.SocketError;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value, socket error code and error message.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="errorCode">The error code that indicates the error that occurred.</param>
		/// <param name="message">The message that describes the error.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedSocketException(int hresult, SocketError errorCode, string message)
			: base(message)
		{
			HResult = hresult;
			mSocketErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedSocketException(int hresult, string message, Exception innerException)
			: base(message, innerException)
		{
			HResult = hresult;
			mSocketErrorCode = SocketError.SocketError;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value, socket error code, error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="errorCode">The error code that indicates the error that occurred.</param>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedSocketException(int hresult, SocketError errorCode, string message, Exception innerException)
			: base(message, innerException)
		{
			HResult = hresult;
			mSocketErrorCode = errorCode;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
		/// </summary>
		/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.</returns>
		public override string ToString()
		{
			return HResultHelper.CreateToString(this, string.Format(Properties.Resources.CodedSocketException_ToString_SocketErrorCode, mSocketErrorCode));
		}

#if WINDOWS_DESKTOP
		/// <summary>
		/// Sets the <see cref="System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(nameof(SocketErrorCode), mSocketErrorCode);
		}
#endif
		#endregion
	}
}
