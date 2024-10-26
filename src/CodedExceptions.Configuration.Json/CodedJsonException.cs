// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if !NET5_0_OR_GREATER
using System.Runtime.Serialization;
#endif

#pragma warning disable IDE0130 // Namespace does not match folder structure
// Necessary to bring exception into the same namespace as other exceptions from NerdyDuck.CodedExceptions package.
// Put here so NerdyDuck.CodedExceptions package does not need a reference to System.Text.Json package in net472 and netstandard2.0 just for this one exception.
namespace NerdyDuck.CodedExceptions;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Defines a custom exception object that is thrown when invalid JSON text is encountered, when the defined maximum depth is passed,
/// or the JSON text is not compatible with the type of a property on an object. This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
/// </summary>
#if !NET5_0_OR_GREATER
[Serializable]
#endif
[CodedException]
public class CodedJsonException : JsonException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/> class.
	/// </summary>
	public CodedJsonException()
		: base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/> class with a specified error message.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	public CodedJsonException(string? message)
		: base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	public CodedJsonException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/> class with a specified error message, path and line and byte position of the error.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="path">The path where the invalid JSON was encountered.</param>
	/// <param name="lineNumber">The line number at which the invalid JSON was encountered (starting at 0) when deserializing.</param>
	/// <param name="bytePositionInLine">The byte count within the current line where the invalid JSON was encountered (starting at 0).</param>
	/// <remarks>
	/// Note that the <paramref name="bytePositionInLine"/> counts the number of bytes (i.e. UTF-8 code units) and not characters or scalars.
	/// </remarks>
	public CodedJsonException(string? message, string? path, long? lineNumber, long? bytePositionInLine)
		: base(message, path, lineNumber, bytePositionInLine)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/> class with a specified error message, path, line and byte position of the error, and a reference to the inner exception that is the cause of this exception..
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="path">The path where the invalid JSON was encountered.</param>
	/// <param name="lineNumber">The line number at which the invalid JSON was encountered (starting at 0) when deserializing.</param>
	/// <param name="bytePositionInLine">The byte count within the current line where the invalid JSON was encountered (starting at 0).</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	/// <remarks>
	/// Note that the <paramref name="bytePositionInLine"/> counts the number of bytes (i.e. UTF-8 code units) and not characters or scalars.
	/// </remarks>
	public CodedJsonException(string? message, string? path, long? lineNumber, long? bytePositionInLine, Exception innerException)
		: base(message, path, lineNumber, bytePositionInLine, innerException)
	{
	}

#if !NET5_0_OR_GREATER
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The exception could not be deserialized correctly.</exception>
	protected CodedJsonException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
#endif

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/> class with a specified HRESULT value and a system-supplied message that describes the error.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	public CodedJsonException(int hresult)
		: base() => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/> class with a specified HRESULT value and error message.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	public CodedJsonException(int hresult, string? message)
		: base(message) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	public CodedJsonException(int hresult, string? message, Exception? innerException)
		: base(message, innerException) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/>  class with a specified HRESULT value, error message, path and line and byte position of the error.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="path">The path where the invalid JSON was encountered.</param>
	/// <param name="lineNumber">The line number at which the invalid JSON was encountered (starting at 0) when deserializing.</param>
	/// <param name="bytePositionInLine">The byte count within the current line where the invalid JSON was encountered (starting at 0).</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <remarks>
	/// Note that the <paramref name="bytePositionInLine"/> counts the number of bytes (i.e. UTF-8 code units) and not characters or scalars.
	/// </remarks>
	public CodedJsonException(int hresult, string? message, string? path, long? lineNumber, long? bytePositionInLine)
		: base(message, path, lineNumber, bytePositionInLine) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedJsonException"/>  class with a specified HRESULT value, error message, path, line and byte position of the error, and a reference to the inner exception that is the cause of this exception..
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="path">The path where the invalid JSON was encountered.</param>
	/// <param name="lineNumber">The line number at which the invalid JSON was encountered (starting at 0) when deserializing.</param>
	/// <param name="bytePositionInLine">The byte count within the current line where the invalid JSON was encountered (starting at 0).</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <remarks>
	/// Note that the <paramref name="bytePositionInLine"/> counts the number of bytes (i.e. UTF-8 code units) and not characters or scalars.
	/// </remarks>
	public CodedJsonException(int hresult, string? message, string? path, long? lineNumber, long? bytePositionInLine, Exception innerException)
		: base(message, path, lineNumber, bytePositionInLine, innerException) => HResult = hresult;

	/// <summary>
	/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
	/// </summary>
	/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace. </returns>
	public override string ToString() => HResultHelper.CreateToString(this, null);
}
