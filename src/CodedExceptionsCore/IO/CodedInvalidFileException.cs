﻿#region Copyright
/*******************************************************************************
 * <copyright file="CodedInvalidFileException.cs" owner="Daniel Kopp">
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
 * <file name="CodedInvalidFileException.cs" date="2015-08-10">
 * The exception that is thrown when a file content is in an invalid format.
 * This exception provides constructors to set custom HResult values.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Runtime.InteropServices;

namespace NerdyDuck.CodedExceptions.IO
{
	/// <summary>
	/// The exception that is thrown when a file content is in an invalid format.
	/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
	/// </summary>
#if WINDOWS_DESKTOP
	[Serializable]
#endif
	[CodedException]
	[ComVisible(true)]
	public class CodedInvalidFileException : System.IO.IOException
	{
		#region Private fields
		private string mFileName;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the name of the invalid file.
		/// </summary>
		/// <value>The name of the file, or <see langword="null"/> if no file name was passed to the constructor for this instance.</value>
		public string FileName
		{
			get { return mFileName; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with its HRESULT set to COR_E_IO, and its inner exception set to a null reference.
		/// </summary>
		/// <remarks>
		/// The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a message that describes the error, such as "The specified file is invalid." This message takes into account the current system culture.
		/// </remarks>
		public CodedInvalidFileException()
			: base(CreateMessage(null, null))
		{
			mFileName = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with with its message string set to <paramref name="message"/>, its HRESULT set to COR_E_IO, and its inner exception set to <see langword="null"/>.
		/// </summary>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <remarks>This constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/>.</remarks>
		public CodedInvalidFileException(string message)
			: base(CreateMessage(message, null))
		{
			mFileName = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		public CodedInvalidFileException(string message, Exception innerException)
			: base(CreateMessage(message, null), innerException)
		{
			mFileName = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with with its message string set to <paramref name="message"/>, specifying the name of the invalid file, its HRESULT set to COR_E_IO.
		/// </summary>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="fileName">The full name of the invalid file.</param>
		/// <remarks>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="FileName"/> property using <paramref name="fileName"/>.</remarks>
		public CodedInvalidFileException(string message, string fileName)
			: base(CreateMessage(message, fileName))
		{
			mFileName = fileName;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with its message string set to <paramref name="message"/>, specifying the name of the invalid file,
		/// its HRESULT set to COR_E_IO, and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="fileName">The full name of the invalid file.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="FileName"/> property using <paramref name="fileName"/>.</remarks>
		public CodedInvalidFileException(string message, string fileName, Exception innerException)
			: base(CreateMessage(message, fileName), innerException)
		{
			mFileName = fileName;
		}

#if WINDOWS_DESKTOP
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		protected CodedInvalidFileException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
			mFileName = info.GetString(nameof(FileName));
		}
#endif

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with the specified HRESULT value.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a system-supplied message that describes the error, such as "The specified file is invalid." This message takes into account the current system culture.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedInvalidFileException(int hresult)
			: base(CreateMessage(null, null))
		{
			mFileName = null;
			HResult = hresult;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with a specified HRESULT value and error message.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		public CodedInvalidFileException(int hresult, string message)
			: base(CreateMessage(message, null), hresult)
		{
			mFileName = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with a specified HRESULT value, error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		public CodedInvalidFileException(int hresult, string message, Exception innerException)
			: base(CreateMessage(message, null), innerException)
		{
			mFileName = null;
			HResult = hresult;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with with a specified HRESULT value, its message string set to <paramref name="message"/> and the name of the invalid file.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="fileName">The full name of the invalid file.</param>
		/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="FileName"/> property using <paramref name="fileName"/>.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedInvalidFileException(int hresult, string message, string fileName)
			: base(CreateMessage(message, fileName))
		{
			mFileName = fileName;
			HResult = hresult;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodedInvalidFileException"/> class with a specified HRESULT value, its message string set to <paramref name="message"/>, the name of the invalid file,
		/// and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="hresult">The HRESULT that describes the error.</param>
		/// <param name="message">A description of the error. The content of <paramref name="message"/> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="fileName">The full name of the invalid file.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
		/// <remarks><para>The constructor initializes the <see cref="Exception.Message"/> property of the new instance using <paramref name="message"/> and the <see cref="FileName"/> property using <paramref name="fileName"/>.</para>
		/// <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</para></remarks>
		public CodedInvalidFileException(int hresult, string message, string fileName, Exception innerException)
			: base(CreateMessage(message, fileName), innerException)
		{
			mFileName = fileName;
			HResult = hresult;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Returns the fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace.
		/// </summary>
		/// <returns>The fully qualified name of this exception, the <see cref="Exception.HResult"/> and possibly the error message, the name of the inner exception, and the stack trace. </returns>
		public override string ToString()
		{
			string CustomText = null;
			if (!string.IsNullOrEmpty(mFileName))
			{
				CustomText = string.Format(Properties.Resources.Global_FileName, mFileName);
			}

			return HResultHelper.CreateToString(this, CustomText);
		}

#if WINDOWS_DESKTOP
		/// <summary>
		/// Sets the <see cref="System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(nameof(FileName), mFileName);
		}
#endif
		#endregion

		#region Private methods
		/// <summary>
		/// Creates the string stored in the Message property, with or without the file name.
		/// </summary>
		/// <param name="message">The message provided by the constructor. May be null.</param>
		/// <param name="fileName">The file name. May be null.</param>
		/// <returns>Either message, if it is not null; or a string stating that the file already exists, with the file name, if it is not null.</returns>
		private static string CreateMessage(string message, string fileName)
		{
			if (message != null)
			{
				return message;
			}
			if (fileName == null)
			{
				return Properties.Resources.CodedInvalidFileException_Message;
			}
			return string.Format(Properties.Resources.CodedInvalidFileException_MessageFile, fileName);
		}
		#endregion
	}
}
