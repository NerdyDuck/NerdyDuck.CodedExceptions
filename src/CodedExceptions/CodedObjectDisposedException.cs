// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions;

/// <summary>
/// The exception that is thrown when an operation is performed on a disposed object.
/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
/// </summary>
#if !NET
[Serializable]
#endif
[CodedException]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "Base class does not have ctor()")]
public class CodedObjectDisposedException : ObjectDisposedException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedObjectDisposedException"/> class with a string containing the name of the disposed object.
	/// </summary>
	/// <param name="objectName">A string containing the name of the disposed object.</param>
	/// <remarks>The <see cref="Exception.Message"/> property is initialized to a system-supplied message that describes the error and includes the <paramref name="objectName"/> parameter. This message takes into account the current system culture. If <paramref name="objectName"/> is <see langword="null"/>, the Message property contains only an error message.</remarks>
	public CodedObjectDisposedException(string? objectName)
		: base(objectName, SR.ObjectDisposed_Generic)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedObjectDisposedException"/> class with the specified object name and message.
	/// </summary>
	/// <param name="objectName">A string containing the name of the disposed object.</param>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</remarks>
	public CodedObjectDisposedException(string? objectName, string? message)
		: base(objectName, message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedObjectDisposedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</remarks>
	public CodedObjectDisposedException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}

#if !NET
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedObjectDisposedException"/> class with serialized data.
	/// </summary>
	/// <param name="info">The object that holds the serialized object data.</param>
	/// <param name="context">The contextual information about the source or destination.</param>
	/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
	/// <exception cref="SerializationException">The exception could not be deserialized correctly.</exception>
	protected CodedObjectDisposedException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
#endif

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedObjectDisposedException"/> class with a specified HRESULT value and a string containing the name of the disposed object..
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="objectName">A string containing the name of the disposed object.</param>
	/// <remarks><para>The <see cref="Exception.Message"/> property is initialized to a system-supplied message that describes the error and includes the <paramref name="objectName"/> parameter. This message takes into account the current system culture. If <paramref name="objectName"/> is <see langword="null"/>, the Message property contains only an error message.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedObjectDisposedException(int hresult, string? objectName)
		: base(objectName, SR.ObjectDisposed_Generic) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedObjectDisposedException"/> class with a specified HRESULT value, object name and message.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="objectName">A string containing the name of the disposed object.</param>
	/// <param name="message">The message that describes the error.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedObjectDisposedException(int hresult, string? objectName, string? message)
		: base(objectName, message) => HResult = hresult;

	/// <summary>
	/// Initializes a new instance of the <see cref="CodedObjectDisposedException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
	/// </summary>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
	/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
	/// <para>See the MSDN for more information about the definition of HRESULT values.</para></remarks>
	/// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</seealso>
	public CodedObjectDisposedException(int hresult, string? message, Exception? innerException)
		: base(message, innerException) => HResult = hresult;

	/// <summary>
	/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
	/// </summary>
	/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.</returns>
	public override string ToString() => HResultHelper.CreateToString(this, null);

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
	/// <summary>
	/// Throws an <see cref="CodedObjectDisposedException"/> if <paramref name="isDisposed"/> is <see langword="true"/>.
	/// </summary>
	/// <param name="isDisposed">The value to check if the object was already disposed.</param>
	/// <param name="type">The type of the disposed object.</param>
	/// <exception cref="CodedObjectDisposedException"><paramref name="isDisposed"/> is <see langword="true"/>.</exception>
	public static new void ThrowIf(bool isDisposed, Type type)
	{
		if (isDisposed)
		{
			throw new CodedObjectDisposedException(type?.Name);
		}
	}

	/// <summary>
	/// Throws an <see cref="CodedObjectDisposedException"/> if <paramref name="isDisposed"/> is <see langword="true"/>.
	/// </summary>
	/// <param name="isDisposed">The value to check if the object was already disposed.</param>
	/// <param name="instance">The disposed object.</param>
	/// <exception cref="CodedObjectDisposedException"><paramref name="isDisposed"/> is <see langword="true"/>.</exception>
	public static new void ThrowIf(bool isDisposed, object instance)
	{
		if (isDisposed)
		{
			throw new CodedObjectDisposedException(instance?.GetType().Name);
		}
	}
#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required
#pragma warning restore IDE0079 // Remove unnecessary suppression

	/// <summary>
	/// Throws an <see cref="CodedObjectDisposedException"/> if <paramref name="isDisposed"/> is <see langword="true"/>.
	/// </summary>
	/// <param name="isDisposed">The value to check if the object was already disposed.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="type">The type of the disposed object.</param>
	/// <exception cref="CodedObjectDisposedException"><paramref name="isDisposed"/> is <see langword="true"/>.</exception>
	public static void ThrowIf(bool isDisposed, int hresult, Type type)
	{
		if (isDisposed)
		{
			throw new CodedObjectDisposedException(hresult, type?.Name);
		}
	}

	/// <summary>
	/// Throws an <see cref="CodedObjectDisposedException"/> if <paramref name="isDisposed"/> is <see langword="true"/>.
	/// </summary>
	/// <param name="isDisposed">The value to check if the object was already disposed.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="instance">The disposed object.</param>
	/// <exception cref="CodedObjectDisposedException"><paramref name="isDisposed"/> is <see langword="true"/>.</exception>
	public static void ThrowIf(bool isDisposed, int hresult, object instance)
	{
		if (isDisposed)
		{
			throw new CodedObjectDisposedException(hresult, instance?.GetType().Name);
		}
	}

	/// <summary>
	/// Throws an <see cref="CodedObjectDisposedException"/> if <paramref name="isDisposed"/> is not 0.
	/// </summary>
	/// <param name="isDisposed">The value to check if the object was already disposed.</param>
	/// <param name="type">The type of the disposed object.</param>
	/// <exception cref="CodedObjectDisposedException"><paramref name="isDisposed"/> is not 0.</exception>
	public static void ThrowIf(int isDisposed, Type type)
	{
		if (isDisposed != 0)
		{
			throw new CodedObjectDisposedException(type?.Name);
		}
	}

	/// <summary>
	/// Throws an <see cref="CodedObjectDisposedException"/> if <paramref name="isDisposed"/> is not 0.
	/// </summary>
	/// <param name="isDisposed">The value to check if the object was already disposed.</param>
	/// <param name="instance">The disposed object.</param>
	/// <exception cref="CodedObjectDisposedException"><paramref name="isDisposed"/> is not 0.</exception>
	public static void ThrowIf(int isDisposed, object instance)
	{
		if (isDisposed != 0)
		{
			throw new CodedObjectDisposedException(instance?.GetType().Name);
		}
	}

	/// <summary>
	/// Throws an <see cref="CodedObjectDisposedException"/> if <paramref name="isDisposed"/> is not 0.
	/// </summary>
	/// <param name="isDisposed">The value to check if the object was already disposed.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="type">The type of the disposed object.</param>
	/// <exception cref="CodedObjectDisposedException"><paramref name="isDisposed"/> is not 0.</exception>
	public static void ThrowIf(int isDisposed, int hresult, Type type)
	{
		if (isDisposed != 0)
		{
			throw new CodedObjectDisposedException(hresult, type?.Name);
		}
	}

	/// <summary>
	/// Throws an <see cref="CodedObjectDisposedException"/> if <paramref name="isDisposed"/> is not 0.
	/// </summary>
	/// <param name="isDisposed">The value to check if the object was already disposed.</param>
	/// <param name="hresult">The HRESULT that describes the error.</param>
	/// <param name="instance">The disposed object.</param>
	/// <exception cref="CodedObjectDisposedException"><paramref name="isDisposed"/> is not 0.</exception>
	public static void ThrowIf(int isDisposed, int hresult, object instance)
	{
		if (isDisposed != 0)
		{
			throw new CodedObjectDisposedException(hresult, instance?.GetType().Name);
		}
	}
}
