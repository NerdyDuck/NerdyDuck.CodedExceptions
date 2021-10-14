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

namespace NerdyDuck.CodedExceptions.IO;

/// <summary>
/// The exception that is thrown when an I/O error occurs.
/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
/// </summary>
[Serializable]
[CodedException]
public class CodedIOException : System.IO.IOException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedIOException"/> class with its message string set to the empty string (""), its HRESULT set to COR_E_IO, and its inner exception set to a null reference.
	/// </summary>
	/// <remarks>
	/// The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "An I/O error occurred while performing the requested operation." This message takes into account the current system culture.
	/// </remarks>
	public CodedIOException()
		: base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedIOException"/> class with with its message string set to <paramref name="message"/>, its HRESULT set to COR_E_IO, and its inner exception set to <see langword="null"/>.
	/// </summary>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/>.</remarks>
	public CodedIOException(string? message)
		: base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedIOException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	public CodedIOException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedIOException"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The exception could not be deserialized correctly.</exception>
	protected CodedIOException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedIOException"/> class with its message string set to the empty string ("") and a specified HRESULT value.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "An I/O error occurred while performing the requested operation." This message takes into account the current system culture.</para>
	/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedIOException(int hresult)
		: base() => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedIOException"/> class with a specified HRESULT value and error message.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	public CodedIOException(int hresult, string? message)
		: base(message, hresult)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedIOException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	public CodedIOException(int hresult, string? message, Exception? innerException)
		: base(message, innerException) => HResult = hresult;

	/// <summary>
	/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
	/// </summary>
	/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.</returns>
	public override string ToString() => HResultHelper.CreateToString(this, null);
}
