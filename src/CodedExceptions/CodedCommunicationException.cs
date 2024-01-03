// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions;

/// <summary>
/// Represents a communication error in either the service or client application.
/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
/// </summary>
#if NETFRAMEWORK
[Serializable]
#endif
[CodedException]
[ComVisible(false)]
public class CodedCommunicationException : CodedException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedCommunicationException"/> class.
	/// </summary>
	public CodedCommunicationException()
		: base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedCommunicationException"/> class with a specified error message.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public CodedCommunicationException(string? message)
		: base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedCommunicationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	public CodedCommunicationException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}

#if NETFRAMEWORK
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedCommunicationException"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The exception could not be deserialized correctly.</exception>
	protected CodedCommunicationException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
#endif

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedCommunicationException"/> class with a specified HRESULT value.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <remarks>See the MSDN for more information about the definition of HRESULT values.</remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedCommunicationException(int hresult)
		: base(hresult)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedCommunicationException"/> class with a specified HRESULT value and error message.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The message that describes the error.</param>
	/// <remarks>See the MSDN for more information about the definition of HRESULT values.</remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedCommunicationException(int hresult, string? message)
		: base(hresult, message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedCommunicationException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks>See the MSDN for more information about the definition of HRESULT values.</remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedCommunicationException(int hresult, string? message, Exception? innerException)
		: base(hresult, message, innerException)
	{
	}
}
