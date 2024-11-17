// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace NerdyDuck.CodedExceptions;

/// <summary>
/// The exception that is thrown when the value of an argument is outside the allowable range of values as defined by the invoked method.
/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
/// </summary>
#if !NET5_0_OR_GREATER
[Serializable]
#endif
[CodedException]
public class CodedArgumentOutOfRangeException : ArgumentOutOfRangeException
{
	internal const int COR_E_ARGUMENTOUTOFRANGE = unchecked((int)0x80131502);

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class.
	/// </summary>
	/// <remarks>This constructor initializes the Message property of the new instance to a system-supplied message that describes the error, such as "Nonnegative number required." This message takes into account the current system culture.</remarks>
	public CodedArgumentOutOfRangeException()
		: base(null, SR.Arg_ArgumentOutOfRangeException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with the name of the parameter that causes this exception.
	/// </summary>
	/// <param name="paramName">The name of the parameter that caused the exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "Nonnegative number required." This message takes into account the current system culture.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using the <paramref name="paramName"/> parameter. The content of <paramref name="paramName"/> is intended to be understood by humans.</para></remarks>
	public CodedArgumentOutOfRangeException(string? paramName)
		: base(paramName, SR.Arg_ArgumentOutOfRangeException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</remarks>
	public CodedArgumentOutOfRangeException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with a specified error message and the name of the parameter that causes this exception.
	/// </summary>
	/// <param name="paramName">The name of the parameter that caused the current exception.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para></remarks>
	public CodedArgumentOutOfRangeException(string? paramName, string? message)
		: base(paramName, message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with the parameter name, the value of the argument, and a specified error message.
	/// </summary>
	/// <param name="paramName">The name of the parameter that caused the current exception.</param>
	/// <param name="actualValue">The value of the argument that causes this exception.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>The <paramref name="actualValue"/> parameter is not used within the .NET Framework class library. However, the ActualValue property is provided so that applications can use the available argument value.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para></remarks>
	public CodedArgumentOutOfRangeException(string? paramName, object? actualValue, string? message)
		: base(paramName, actualValue, message)
	{
	}

#if !NET5_0_OR_GREATER
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The exception could not be deserialized correctly.</exception>
	protected CodedArgumentOutOfRangeException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
#endif

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with a specified HRESULT value and a system-supplied message that describes the error.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error,
	/// such as "Nonnegative number required." This message takes into account the current system culture.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedArgumentOutOfRangeException(int hresult)
		: base(null, SR.Arg_ArgumentOutOfRangeException) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with a specified HRESULT value and the name of the parameter that causes this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter that caused the exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "Nonnegative number required." This message takes into account the current system culture.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using the <paramref name="paramName"/> parameter. The content of <paramref name="paramName"/> is intended to be understood by humans.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedArgumentOutOfRangeException(int hresult, string? paramName)
		: base(paramName, SR.Arg_ArgumentOutOfRangeException) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with a specified HRESULT value, an error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedArgumentOutOfRangeException(int hresult, string? message, Exception? innerException)
		: base(message, innerException) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with a specified HRESULT value, an error message and the name of the parameter that causes this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter that caused the current exception.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedArgumentOutOfRangeException(int hresult, string? paramName, string? message)
		: base(paramName, message) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedArgumentOutOfRangeException"/> class with a specified HRESULT value, the parameter name, the value of the argument, and a specified error message.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter that caused the current exception.</param>
	/// <param name="actualValue">The value of the argument that causes this exception.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>The <paramref name="actualValue"/> parameter is not used within the .NET Framework class library. However, the ActualValue property is provided so that applications can use the available argument value.</para>
	/// <para>This constructor initializes the <see cref="ArgumentException.ParamName"/> property of the new instance using <paramref name="paramName"/>. The content of <paramref name="paramName"/> is intended to be understood by humans.</para></remarks>
	public CodedArgumentOutOfRangeException(int hresult, string? paramName, object? actualValue, string? message)
		: base(paramName, actualValue, message) => HResult = hresult;

	/// <summary>
	/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
	/// </summary>
	/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace. </returns>
	public override string ToString() => HResultHelper.CreateToString(this, null);

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is equal to <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as not equal to <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is equal to <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static new void ThrowIfEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IEquatable<T>
#else
	public static new void ThrowIfEqual<T>(T value, T other, string? paramName = default) where T : IEquatable<T>
#endif
	{
		if (EqualityComparer<T>.Default.Equals(value, other))
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNotEqual), paramName, (object?)value ?? "null", (object?)other ?? "null"));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is equal to <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as not equal to <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is equal to <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static void ThrowIfEqual<T>(T value, T other, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IEquatable<T>
#else
	public static void ThrowIfEqual<T>(T value, T other, int hresult, string? paramName = default) where T : IEquatable<T>
#endif
	{
		if (EqualityComparer<T>.Default.Equals(value, other))
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNotEqual), paramName, (object?)value ?? "null", (object?)other ?? "null"));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as less or equal than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is greater than <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static new void ThrowIfGreaterThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static new void ThrowIfGreaterThan<T>(T value, T other, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(other) > 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeLessOrEqual), paramName, value, other));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as less or equal than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is greater than <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static void ThrowIfGreaterThan<T>(T value, T other, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static void ThrowIfGreaterThan<T>(T value, T other, int hresult, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(other) > 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeLessOrEqual), paramName, value, other));
		}
	}

	/// <summary>
	/// Throws an <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than or equal to <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as less than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is greater than or equal to <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static new void ThrowIfGreaterThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static new void ThrowIfGreaterThanOrEqual<T>(T value, T other, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(other) >= 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeLess), paramName, value, other));
		}
	}

	/// <summary>
	/// Throws an <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than or equal to <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as less than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is greater than or equal to <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static void ThrowIfGreaterThanOrEqual<T>(T value, T other, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static void ThrowIfGreaterThanOrEqual<T>(T value, T other, int hresult, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(other) >= 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeLess), paramName, value, other));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="minimum"/> or greater than <paramref name="maximum"/>.
	/// </summary>
	/// <param name="value">The argument to validate as greater than or equal to <paramref name="minimum"/> and less than or equal to <paramref name="maximum"/>.</param>
	/// <param name="minimum">The minimum value that is valid for <paramref name="value"/>.</param>
	/// <param name="maximum">The maximum value that is valid for <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is less than <paramref name="minimum"/> or greater than <paramref name="maximum"/>.</exception>
#if NET5_0_OR_GREATER
	public static new void ThrowIfLessOrGreaterThan<T>(T value, T minimum, T maximum, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static new void ThrowIfLessOrGreaterThan<T>(T value, T minimum, T maximum, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(minimum) < 0 || value.CompareTo(maximum) > 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustNotBeOutOfRange), paramName, value, minimum, maximum));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="minimum"/> or greater than <paramref name="maximum"/>.
	/// </summary>
	/// <param name="value">The argument to validate as greater than or equal to <paramref name="minimum"/> and less than or equal to <paramref name="maximum"/>.</param>
	/// <param name="minimum">The minimum value that is valid for <paramref name="value"/>.</param>
	/// <param name="maximum">The maximum value that is valid for <paramref name="value"/>.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is less than <paramref name="minimum"/> or greater than <paramref name="maximum"/>.</exception>
#if NET5_0_OR_GREATER
	public static void ThrowIfLessOrGreaterThan<T>(T value, T minimum, T maximum, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static void ThrowIfLessOrGreaterThan<T>(T value, T minimum, T maximum, int hresult, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(minimum) < 0 || value.CompareTo(maximum) > 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustNotBeOutOfRange), paramName, value, minimum, maximum));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as greater than or equal to <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is less than <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static new void ThrowIfLessThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static new void ThrowIfLessThan<T>(T value, T other, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(other) < 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeGreaterOrEqual), paramName, value, other));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as greater than or equal to <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is less than <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static void ThrowIfLessThan<T>(T value, T other, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static void ThrowIfLessThan<T>(T value, T other, int hresult, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(other) < 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeGreaterOrEqual), paramName, value, other));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is less than or equal to <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as greater than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is less than or equal to <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static new void ThrowIfLessThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static new void ThrowIfLessThanOrEqual<T>(T value, T other, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(other) <= 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeGreater), paramName, value, other));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is less than or equal to <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as greater than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is less than or equal to <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static void ThrowIfLessThanOrEqual<T>(T value, T other, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IComparable<T>
#else
	public static void ThrowIfLessThanOrEqual<T>(T value, T other, int hresult, string? paramName = default) where T : IComparable<T>
#endif
	{
		if (value.CompareTo(other) <= 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeGreater), paramName, value, other));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not equal to <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as equal to <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is not equal to <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static new void ThrowIfNotEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IEquatable<T>
#else
	public static new void ThrowIfNotEqual<T>(T value, T other, string? paramName = default) where T : IEquatable<T>
#endif
	{
		if (!EqualityComparer<T>.Default.Equals(value, other))
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeEqual), paramName, (object?)value ?? "null", (object?)other ?? "null"));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not equal to <paramref name="other"/>.
	/// </summary>
	/// <param name="value">The argument to validate as equal to <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is not equal to <paramref name="other"/>.</exception>
#if NET5_0_OR_GREATER
	public static void ThrowIfNotEqual<T>(T value, T other, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : IEquatable<T>
#else
	public static void ThrowIfNotEqual<T>(T value, T other, int hresult, string? paramName = default) where T : IEquatable<T>
#endif
	{
		if (!EqualityComparer<T>.Default.Equals(value, other))
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeEqual), paramName, (object?)value ?? "null", (object?)other ?? "null"));
		}
	}

#if NET7_0_OR_GREATER
	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static new void ThrowIfNegative<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : System.Numerics.INumberBase<T>
	{
		if (T.IsNegative(value))
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative<T>(T value, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : System.Numerics.INumberBase<T>
	{
		if (T.IsNegative(value))
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static new void ThrowIfNegativeOrZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : System.Numerics.INumberBase<T>
	{
		if (T.IsNegative(value) || T.IsZero(value))
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero<T>(T value, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : System.Numerics.INumberBase<T>
	{
		if (T.IsNegative(value) || T.IsZero(value))
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static new void ThrowIfNotMultiple<T>(T value, T multiple, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : System.Numerics.IBinaryInteger<T>
	{
		if ((value % multiple) != T.Zero)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNotMultiple<T>(T value, T multiple, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : System.Numerics.IBinaryInteger<T>
	{
		if ((value % multiple) != T.Zero)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static new void ThrowIfZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : System.Numerics.INumberBase<T>
	{
		if (T.IsZero(value))
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <typeparam name="T">The type of the objects to validate.</typeparam>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero<T>(T value, int hresult, [CallerArgumentExpression(nameof(value))] string? paramName = default) where T : System.Numerics.INumberBase<T>
	{
		if (T.IsZero(value))
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}
#else
	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfNegative(sbyte value, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfNegative(sbyte value, int hresult, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}
	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(short value, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(short value, int hresult, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(int value, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(int value, int hresult, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(long value, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(long value, int hresult, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(float value, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(float value, int hresult, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(double value, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNegative(double value, int hresult, string? paramName = default)
	{
		if (value < 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegative), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfNegativeOrZero(sbyte value, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfNegativeOrZero(sbyte value, int hresult, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(short value, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(short value, int hresult, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(int value, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(int value, int hresult, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(long value, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(long value, int hresult, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(float value, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(float value, int hresult, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(double value, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative or zero.</exception>
	public static void ThrowIfNegativeOrZero(double value, int hresult, string? paramName = default)
	{
		if (value <= 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonNegativeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static new void ThrowIfNotMultiple(byte value, byte multiple, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNotMultiple(byte value, byte multiple, int hresult, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static new void ThrowIfNotMultiple(short value, short multiple, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNotMultiple(short value, short multiple, int hresult, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static new void ThrowIfNotMultiple(int value, int multiple, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNotMultiple(int value, int multiple, int hresult, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static new void ThrowIfNotMultiple(long value, long multiple, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	public static void ThrowIfNotMultiple(long value, long multiple, int hresult, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static new void ThrowIfNotMultiple(sbyte value, sbyte multiple, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfNotMultiple(sbyte value, sbyte multiple, int hresult, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static new void ThrowIfNotMultiple(ushort value, ushort multiple, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfNotMultiple(ushort value, ushort multiple, int hresult, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static new void ThrowIfNotMultiple(uint value, uint multiple, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfNotMultiple(uint value, uint multiple, int hresult, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static new void ThrowIfNotMultiple(ulong value, ulong multiple, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is not a multiple of <paramref name="multiple"/>.
	/// </summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="multiple">The value that <paramref name="value"/> must be a multiple of.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfNotMultiple(ulong value, ulong multiple, int hresult, string? paramName = default)
	{
		if ((value % multiple) != 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeMultiple), paramName, value, multiple));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfZero(sbyte value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfZero(sbyte value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(short value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(short value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(int value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(int value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(long value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(long value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(float value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(float value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(double value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(double value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(byte value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(byte value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfZero(ushort value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfZero(ushort value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfZero(uint value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfZero(uint value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfZero(ulong value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	[CLSCompliant(false)]
	public static void ThrowIfZero(ulong value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(char value, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}

	/// <summary>
	/// Throws a <see cref="CodedArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.
	/// </summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	/// <exception cref="CodedArgumentOutOfRangeException"><paramref name="value"/> is zero.</exception>
	public static void ThrowIfZero(char value, int hresult, string? paramName = default)
	{
		if (value == 0)
		{
			throw new CodedArgumentOutOfRangeException(hresult, paramName, value, CompositeFormatCache.Default.Format(nameof(SR.ArgumentOutOfRange_MustBeNonZero), paramName, value));
		}
	}
#endif
#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required
#pragma warning restore IDE0079 // Remove unnecessary suppression
}
