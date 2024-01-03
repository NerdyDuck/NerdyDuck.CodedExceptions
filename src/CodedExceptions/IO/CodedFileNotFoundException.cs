// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Ignore Spelling: hresult

namespace NerdyDuck.CodedExceptions.IO;
/// <summary>
/// The exception that is thrown when an attempt to access a file that does not exist on disk fails.
/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
/// </summary>
#if NETFRAMEWORK
[Serializable]
#endif
[CodedException]
public class CodedFileNotFoundException : System.IO.FileNotFoundException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with its HRESULT set to COR_E_FILENOTFOUND, and its inner exception set to a null reference.
	/// </summary>
	/// <remarks>
	/// The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "Could not find the specified file." This message takes into account the current system culture.
	/// </remarks>
	public CodedFileNotFoundException()
		: base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with with its message string set to <paramref name="message"/>, its HRESULT set to COR_E_FILENOTFOUND, and its inner exception set to <see langword="null"/>.
	/// </summary>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/>.</remarks>
	public CodedFileNotFoundException(string? message)
		: base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	public CodedFileNotFoundException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with with its message string set to <paramref name="message"/>, specifying the file name that cannot be found, its HRESULT set to COR_E_FILENOTFOUND.
	/// </summary>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="fileName">The full name of the file with the invalid image.</param>
	/// <remarks>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="System.IO.FileNotFoundException.FileName"/> property using <paramref name="fileName"/>.</remarks>
	public CodedFileNotFoundException(string? message, string? fileName)
		: base(message, fileName)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with its message string set to <paramref name="message"/>, specifying the file name that cannot be found,
	/// its HRESULT set to COR_E_FILENOTFOUND, and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="fileName">The full name of the file with the invalid image.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="System.IO.FileNotFoundException.FileName"/> property using <paramref name="fileName"/>.</remarks>
	public CodedFileNotFoundException(string? message, string? fileName, Exception? innerException)
		: base(message, fileName, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with its message string set to the empty string ("") and a specified HRESULT value.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "An I/O error occurred while performing the requested operation." This message takes into account the current system culture.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedFileNotFoundException(int hresult)
		: base() => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with a specified HRESULT value and error message.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <remarks>See the MSDN for more information about the definition of HRESULT values.</remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedFileNotFoundException(int hresult, string? message)
		: base(message) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	///<remarks>See the MSDN for more information about the definition of HRESULT values.</remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedFileNotFoundException(int hresult, string? message, Exception? innerException)
		: base(message, innerException) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with with a specified HRESULT value, its message string set to <paramref name="message"/>, specifying the file name that cannot be found, its HRESULT set to COR_E_FILENOTFOUND.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="fileName">The full name of the file with the invalid image.</param>
	/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="System.IO.FileNotFoundException.FileName"/> property using <paramref name="fileName"/>.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedFileNotFoundException(int hresult, string? message, string? fileName)
		: base(message, fileName) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with a specified HRESULT value, its message string set to <paramref name="message"/>, specifying the file name that cannot be found,
	/// its HRESULT set to COR_E_FILENOTFOUND, and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="fileName">The full name of the file with the invalid image.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="System.IO.FileNotFoundException.FileName"/> property using <paramref name="fileName"/>.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedFileNotFoundException(int hresult, string? message, string? fileName, Exception? innerException)
		: base(message, fileName, innerException) => HResult = hresult;

#if NETFRAMEWORK
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedFileNotFoundException"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The exception could not be deserialized correctly.</exception>
	protected CodedFileNotFoundException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
#endif

	/// <summary>
	/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
	/// </summary>
	/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace. </returns>
	public override string ToString()
	{
		string? customText = null;
		if (!string.IsNullOrEmpty(FileName))
		{
			customText = string.Format(CultureInfo.CurrentCulture, CompositeFormatCache.Default.Get(TextResources.Global_FileName), FileName);
		}

		return HResultHelper.CreateToString(this, customText);
	}
}
