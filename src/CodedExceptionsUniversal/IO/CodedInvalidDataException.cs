#region Copyright
/*******************************************************************************
 * <copyright file="CodedInvalidDataException.cs" owner="Daniel Kopp">
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
 * <file name="CodedInvalidDataException.cs" date="2015-08-10">
 * The exception that is thrown when a data stream is in an invalid format.
 * This exception provides constructors to set custom HResult values.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Runtime.InteropServices;

namespace NerdyDuck.CodedExceptions.IO
{
	/// <summary>
	/// The exception that is thrown when a data stream is in an invalid format.
	/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
	/// </summary>
	/// <remarks>The class is not derived from <see cref="System.IO.InvalidDataException"/>, because it is sealed.</remarks>
#if WINDOWS_DESKTOP
	[Serializable]
#endif
	[CodedException]
	[ComVisible(true)]
	public class CodedInvalidDataException : CodedIOException
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidDataException"/> class.
		/// </summary>
		/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "An invalid argument was specified." This message takes into account the current system culture.</remarks>
		public CodedInvalidDataException()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidDataException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</remarks>
		public CodedInvalidDataException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidDataException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</remarks>
		public CodedInvalidDataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

#if WINDOWS_DESKTOP
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidDataException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		protected CodedInvalidDataException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
#endif

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidDataException"/> class with a specified HRESULT value.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <remarks><para>This constructor initializes the Message property of the new instance to a system-supplied message that describes the error, such as "An invalid argument was specified." This message takes into account the current system culture.</para>
		/// <para>See <externalLink><linkUri>http://msdn.microsoft.com/en-us/library/cc231198.aspx</linkUri></externalLink> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedInvalidDataException(int hresult)
			: base(hresult)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidDataException"/> class with a specified HRESULT value and error message.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">The message that describes the error.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
		/// <para>See <externalLink><linkUri>http://msdn.microsoft.com/en-us/library/cc231198.aspx</linkUri></externalLink> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedInvalidDataException(int hresult, string message)
			: base(hresult, message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidDataException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks><para>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter. The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
		/// <para>See <externalLink><linkUri>http://msdn.microsoft.com/en-us/library/cc231198.aspx</linkUri></externalLink> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedInvalidDataException(int hresult, string message, Exception innerException)
			: base(hresult, message, innerException)
		{
		}
		#endregion
	}
}
