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
/// The exception that is thrown when an attempt to access a directory that does not exist on disk fails.
/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
/// </summary>
[Serializable]
[CodedException]
public class CodedDirectoryNotFoundException : System.IO.DirectoryNotFoundException
{
	/// <summary>
	/// Gets the name of the directory that cannot be found.
	/// </summary>
	/// <value>The name of the directory, or <see langword="null"/> if no directory name was passed to the constructor for this instance.</value>
	public string? DirectoryName
	{
		get; private set;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with its HRESULT set to COR_E_DIRECTORYNOTFOUND, and its inner exception set to a null reference.
	/// </summary>
	/// <remarks>
	/// The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a message that describes the error, such as "Could not find the specified directory." This message takes into account the current system culture.
	/// </remarks>
	public CodedDirectoryNotFoundException()
		: base(CreateMessage(null, null))
	{
		DirectoryName = null;
		HResult = unchecked((int)0x80070003);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with with its message string set to <paramref name="message"/>, its HRESULT set to COR_E_DIRECTORYNOTFOUND, and its inner exception set to <see langword="null"/>.
	/// </summary>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/>.</remarks>
	public CodedDirectoryNotFoundException(string message)
		: base(CreateMessage(message, null))
	{
		DirectoryName = null;
		HResult = unchecked((int)0x80070003);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	public CodedDirectoryNotFoundException(string message, Exception innerException)
		: base(CreateMessage(message, null), innerException)
	{
		DirectoryName = null;
		HResult = unchecked((int)0x80070003);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with with its message string set to <paramref name="message"/>, specifying the directory name that cannot be found, its HRESULT set to COR_E_DIRECTORYNOTFOUND.
	/// </summary>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="directoryName">The full name of the directory that cannot be found.</param>
	/// <remarks>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="DirectoryName"/> property using <paramref name="directoryName"/>.</remarks>
	public CodedDirectoryNotFoundException(string message, string directoryName)
		: base(CreateMessage(message, directoryName))
	{
		DirectoryName = directoryName;
		HResult = unchecked((int)0x80070003);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with its message string set to <paramref name="message"/>, specifying the directory name that cannot be found,
	/// its HRESULT set to COR_E_DIRECTORYNOTFOUND, and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="directoryName">The full name of the directory that cannot be found.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="DirectoryName" /> property using <paramref name="directoryName"/>.</remarks>
	public CodedDirectoryNotFoundException(string message, string directoryName, Exception innerException)
		: base(CreateMessage(message, directoryName), innerException)
	{
		DirectoryName = directoryName;
		HResult = unchecked((int)0x80070003);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The exception could not be deserialized correctly.</exception>
	protected CodedDirectoryNotFoundException(SerializationInfo info, StreamingContext context)
		: base(info, context) => DirectoryName = info.GetString(nameof(DirectoryName));

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with the specified HRESULT value.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "Could not find the specified directory." This message takes into account the current system culture.</para>
	/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedDirectoryNotFoundException(int hresult)
		: base(CreateMessage(null, null))
	{
		DirectoryName = null;
		HResult = hresult;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with a specified HRESULT value and error message.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</remarks>
	public CodedDirectoryNotFoundException(int hresult, string message)
		: base(CreateMessage(message, null))
	{
		DirectoryName = null;
		HResult = hresult;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	///<remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</remarks>
	public CodedDirectoryNotFoundException(int hresult, string message, Exception innerException)
		: base(CreateMessage(message, null), innerException)
	{
		DirectoryName = null;
		HResult = hresult;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with with a specified HRESULT value, its message string set to <paramref name="message"/>, specifying the directory name that cannot be found.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="directoryName">The full name of the directory that cannot be found.</param>
	/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="DirectoryName"/> property using <paramref name="directoryName"/>.</para>
	/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedDirectoryNotFoundException(int hresult, string message, string directoryName)
		: base(CreateMessage(message, directoryName))
	{
		DirectoryName = directoryName;
		HResult = hresult;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with a specified HRESULT value, its message string set to <paramref name="message"/>, specifying the directory that cannot be found,
	/// and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
	/// <param name="directoryName">The full name of the directory that cannot be found.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="DirectoryName"/> property using <paramref name="directoryName"/>.</para>
	/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</para></remarks>
	public CodedDirectoryNotFoundException(int hresult, string message, string directoryName, Exception innerException)
		: base(CreateMessage(message, directoryName), innerException)
	{
		DirectoryName = directoryName;
		HResult = hresult;
	}

	/// <summary>
	/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
	/// </summary>
	/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace. </returns>
	public override string ToString()
	{
		string? CustomText = null;
		if (!string.IsNullOrEmpty(DirectoryName))
		{
			CustomText = string.Format(CultureInfo.CurrentCulture, TextResources.Global_DirectoryName, DirectoryName);
		}

		return HResultHelper.CreateToString(this, CustomText);
	}

	/// <summary>
	/// Sets the <see cref="SerializationInfo"/> with information about the exception.
	/// </summary>
	/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue(nameof(DirectoryName), DirectoryName);
	}

	/// <summary>
	/// Creates the string stored in the Message property, with or without the directory name.
	/// </summary>
	/// <param name="message">The message provided by the constructor. May be null.</param>
	/// <param name="directoryName">The directory name. May be null.</param>
	/// <returns>Either message, if it is not null; or a string stating that the directory cannot be found, with the directory name, if it is not null.</returns>
	private static string CreateMessage(string? message, string? directoryName) => message ?? (directoryName == null
			? TextResources.CodedDirectoryNotFoundException_Message
			: string.Format(CultureInfo.CurrentCulture, TextResources.CodedDirectoryNotFoundException_MessageDirectory, directoryName));
}
