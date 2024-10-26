// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace NerdyDuck.CodedExceptions;

/// <summary>
/// The exception that is thrown when one of the arguments provided to a method is not valid.
/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
/// </summary>
#if !NET5_0_OR_GREATER
[Serializable]
#endif
[CodedException]
public class CodedArgumentException : ArgumentException
{
	internal const int COR_E_ARGUMENT = unchecked((int)0x80070057);

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class.
	/// </summary>
	/// <remarks>This constructor initializes the Message property of the new instance to a system-supplied message that describes the error, such as "An invalid argument was specified." This message takes into account the current system culture.</remarks>
	public CodedArgumentException()
		: base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with a specified error message.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/>.</remarks>
	public CodedArgumentException(string? message)
		: base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</remarks>
	public CodedArgumentException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with a specified error message and the name of the parameter that causes this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="paramName">The name of the parameter that caused the current exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para></remarks>
	public CodedArgumentException(string? message, string? paramName)
		: base(message, paramName)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with a specified error message, the parameter name, and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="paramName">The name of the parameter that caused the current exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para></remarks>
	public CodedArgumentException(string? message, string? paramName, Exception? innerException)
		: base(message, paramName, innerException)
	{
	}

#if !NET5_0_OR_GREATER
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The exception could not be deserialized correctly.</exception>
	protected CodedArgumentException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
#endif

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with a specified HRESULT value and a system-supplied message that describes the error.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error,
	/// such as "An invalid argument was specified." This message takes into account the current system culture.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedArgumentException(int hresult)
		: base() => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with a specified HRESULT value and an error message.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/>.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedArgumentException(int hresult, string? message)
		: base(message) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with a specified HRESULT value, an error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedArgumentException(int hresult, string? message, Exception? innerException)
		: base(message, innerException) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with a specified HRESULT value, an error message and the name of the parameter that causes this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="paramName">The name of the parameter that caused the current exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedArgumentException(int hresult, string? message, string? paramName)
		: base(message, paramName) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentException"/> class with a specified HRESULT value, an error message, the parameter name, and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="paramName">The name of the parameter that caused the current exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedArgumentException(int hresult, string? message, string? paramName, Exception? innerException)
		: base(message, paramName, innerException) => HResult = hresult;

	/// <summary>
	/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
	/// </summary>
	/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace. </returns>
	public override string ToString() => HResultHelper.CreateToString(this, null);

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
	/// <summary>
	/// Throws an exception if <paramref name="argument"/> is <see langword="null"/> or empty.
	/// </summary>
	/// <param name="argument">The string argument to validate as non-<see langword="null"/> and non-empty.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	/// <exception cref="CodedArgumentNullException"><paramref name="argument"/> is <see langword="null"/>.</exception>
	/// <exception cref="CodedArgumentException"><paramref name="argument"/> is empty.</exception>
#if NET5_0_OR_GREATER
	public static new void ThrowIfNullOrEmpty(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = default) => ThrowNullOrEmptyException(argument, null, paramName);
#else
	public static new void ThrowIfNullOrEmpty(string? argument, string? paramName = default) => ThrowNullOrEmptyException(argument, null, paramName);
#endif

	/// <summary>
	/// Throws an exception if <paramref name="argument"/> is <see langword="null"/> or empty.
	/// </summary>
	/// <param name="argument">The string argument to validate as non-<see langword="null"/> and non-empty.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	/// <exception cref="CodedArgumentNullException"><paramref name="argument"/> is <see langword="null"/>.</exception>
	/// <exception cref="CodedArgumentException"><paramref name="argument"/> is empty.</exception>
#if NET5_0_OR_GREATER
	public static void ThrowIfNullOrEmpty(string? argument, int hresult, [CallerArgumentExpression(nameof(argument))] string? paramName = default) => ThrowNullOrEmptyException(argument, hresult, paramName);
#else
	public static void ThrowIfNullOrEmpty(string? argument, int hresult, string? paramName = default) => ThrowNullOrEmptyException(argument, hresult, paramName);
#endif

	/// <summary>
	/// Throws an exception if <paramref name="argument"/> is <see langword="null"/>, empty, or consists only of white-space characters.
	/// </summary>
	/// <param name="argument">The string argument to validate.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	/// <exception cref="CodedArgumentNullException"><paramref name="argument"/> is <see langword="null"/>.</exception>
	/// <exception cref="CodedArgumentException"><paramref name="argument"/> is empty or consists only of white-space characters.</exception>
#if NET5_0_OR_GREATER
	public static new void ThrowIfNullOrWhiteSpace(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = default) => ThrowNullOrWhiteSpaceException(argument, null, paramName);
#else
	public static new void ThrowIfNullOrWhiteSpace(string? argument, string? paramName = default) => ThrowNullOrWhiteSpaceException(argument, null, paramName);
#endif

	/// <summary>
	/// Throws an exception if <paramref name="argument"/> is <see langword="null"/>, empty, or consists only of white-space characters.
	/// </summary>
	/// <param name="argument">The string argument to validate.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	/// <exception cref="CodedArgumentNullException"><paramref name="argument"/> is <see langword="null"/>.</exception>
	/// <exception cref="CodedArgumentException"><paramref name="argument"/> is empty or consists only of white-space characters.</exception>
#if NET5_0_OR_GREATER
	public static void ThrowIfNullOrWhiteSpace(string? argument, int hresult, [CallerArgumentExpression(nameof(argument))] string? paramName = default) => ThrowNullOrWhiteSpaceException(argument, hresult, paramName);
#else
	public static void ThrowIfNullOrWhiteSpace(string? argument, int hresult, string? paramName = default) => ThrowNullOrWhiteSpaceException(argument, hresult, paramName);
#endif
#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required
#pragma warning restore IDE0079 // Remove unnecessary suppression

	private static void ThrowNullOrEmptyException(string? argument, int? hresult, string? paramName)
	{
		if (string.IsNullOrEmpty(argument))
		{
			if (hresult is null)
			{
				CodedArgumentNullException.ThrowIfNull(argument, paramName);
				throw new CodedArgumentException(SR.CodedArgumentException_EmptyMessage, paramName);
			}
			else
			{
				CodedArgumentNullException.ThrowIfNull(argument, hresult.Value, paramName);
				throw new CodedArgumentException(hresult.Value, SR.CodedArgumentException_EmptyMessage, paramName);
			}
		}
	}

	private static void ThrowNullOrWhiteSpaceException(string? argument, int? hresult, string? paramName)
	{
		if (string.IsNullOrWhiteSpace(argument))
		{
			if (hresult is null)
			{
				CodedArgumentNullException.ThrowIfNull(argument, paramName);
				throw new CodedArgumentException(SR.CodedArgumentException_WhitespaceMessage, paramName);
			}
			else
			{
				CodedArgumentNullException.ThrowIfNull(argument, hresult.Value, paramName);
				throw new CodedArgumentException(hresult.Value, SR.CodedArgumentException_WhitespaceMessage, paramName);
			}
		}
	}
}
