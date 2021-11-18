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

using System.Net.Sockets;

namespace NerdyDuck.CodedExceptions;

/// <summary>
/// The exception that is thrown when a socket error occurs.
/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
/// </summary>
/// <remarks>This exception is not derived from <see cref="SocketException"/>, because that class provides only a minimum of constructors.</remarks>
[Serializable]
[CodedException]
public class CodedSocketException : CodedException
{
	/// <summary>
	/// Gets the error code that is associated with this exception.
	/// </summary>
	/// <value>One of the <see cref="SocketError"/> enumeration values.</value>
	public SocketError SocketErrorCode
	{
		get; private set;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class.
	/// </summary>
	/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <see cref="SocketError.SocketError"/>, and the <see cref="Exception.Message"/> property with a message that describes the error, such as "A socket error occurred". For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
	public CodedSocketException()
		: base(TextResources.CodedSocketException_Message)
	{
		HResult = HResultHelper.E_FAIL;
		SocketErrorCode = SocketError.SocketError;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with the specified error code.
	/// </summary>
	/// <param name="errorCode">The error code that indicates the error that occurred.</param>
	/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <paramref name="errorCode"/>, and the <see cref="Exception.Message"/> property with a message that describes the error, such as "A socket error occurred". For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
	public CodedSocketException(SocketError errorCode)
		: base(TextResources.CodedSocketException_Message)
	{
		HResult = HResultHelper.E_FAIL;
		SocketErrorCode = errorCode;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified error message.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <see cref="SocketError.SocketError"/>, and the <see cref="Exception.Message"/> property using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
	public CodedSocketException(string? message)
		: base(message)
	{
		HResult = HResultHelper.E_FAIL;
		SocketErrorCode = SocketError.SocketError;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified error message.
	/// </summary>
	/// <param name="errorCode">The error code that indicates the error that occurred.</param>
	/// <param name="message">The message that describes the error.</param>
	/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <paramref name="errorCode"/>, and the <see cref="Exception.Message"/> property using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
	public CodedSocketException(SocketError errorCode, string? message)
		: base(message)
	{
		HResult = HResultHelper.E_FAIL;
		SocketErrorCode = errorCode;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <see cref="SocketError.SocketError"/>, and the <see cref="Exception.Message"/> property using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
	public CodedSocketException(string? message, Exception innerException)
		: base(message, innerException)
	{
		HResult = HResultHelper.E_FAIL;
		SocketErrorCode = SocketError.SocketError;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="errorCode">The error code that indicates the error that occurred.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks>This constructor initializes the <see cref="SocketErrorCode"/> property of the new instance with <paramref name="errorCode"/>, and the <see cref="Exception.Message"/> property using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. For more information about socket error codes, see the Windows Sockets version 2 API error code documentation (https://msdn.microsoft.com/library/windows/desktop/ms740668.aspx).</remarks>
	public CodedSocketException(SocketError errorCode, string? message, Exception? innerException)
		: base(message, innerException)
	{
		HResult = HResultHelper.E_FAIL;
		SocketErrorCode = errorCode;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The exception could not be deserialized correctly.</exception>
	protected CodedSocketException(SerializationInfo info, StreamingContext context)
		: base(info, context) => SocketErrorCode = (SocketError)info.GetInt32(nameof(SocketErrorCode));

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <remarks><para>This constructor initializes the Message property of the new instance to a system-supplied message that describes the error, such as "A socket error occurred". This message takes into account the current system culture.</para>
	/// <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedSocketException(int hresult)
		: base(TextResources.CodedSocketException_Message)
	{
		HResult = hresult;
		SocketErrorCode = SocketError.SocketError;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value and socket error code.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="errorCode">The error code that indicates the error that occurred.</param>
	/// <remarks><para>This constructor initializes the Message property of the new instance to a system-supplied message that describes the error, such as "A socket error occurred". This message takes into account the current system culture.</para>
	/// <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedSocketException(int hresult, SocketError errorCode)
		: base(TextResources.CodedSocketException_Message)
	{
		HResult = hresult;
		SocketErrorCode = errorCode;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value and error message.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The message that describes the error.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedSocketException(int hresult, string? message)
		: base(message)
	{
		HResult = hresult;
		SocketErrorCode = SocketError.SocketError;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value, socket error code and error message.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="errorCode">The error code that indicates the error that occurred.</param>
	/// <param name="message">The message that describes the error.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedSocketException(int hresult, SocketError errorCode, string? message)
		: base(message)
	{
		HResult = hresult;
		SocketErrorCode = errorCode;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedSocketException(int hresult, string? message, Exception? innerException)
		: base(message, innerException)
	{
		HResult = hresult;
		SocketErrorCode = SocketError.SocketError;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedSocketException"/> class with a specified HRESULT value, socket error code, error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="errorCode">The error code that indicates the error that occurred.</param>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedSocketException(int hresult, SocketError errorCode, string? message, Exception? innerException)
		: base(message, innerException)
	{
		HResult = hresult;
		SocketErrorCode = errorCode;
	}

	/// <summary>
	/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
	/// </summary>
	/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.</returns>
	public override string ToString() => HResultHelper.CreateToString(this, string.Format(CultureInfo.CurrentCulture, TextResources.CodedSocketException_ToString_SocketErrorCode, SocketErrorCode));

	/// <summary>
	/// Sets the <see cref="SerializationInfo"/> with information about the exception.
	/// </summary>
	/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue(nameof(SocketErrorCode), SocketErrorCode);
	}
}
