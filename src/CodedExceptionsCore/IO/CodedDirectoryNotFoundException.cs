#region Copyright
/*******************************************************************************
 * <copyright file="CodedDirectoryNotFoundException.cs" owner="Daniel Kopp">
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
 * <file name="CodedDirectoryNotFoundException.cs" date="2015-08-07">
 * The exception that is thrown when an attempt to access a directory that does
 * not exist on disk fails. This exception provides constructors to set custom
 * HResult values.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Runtime.InteropServices;

namespace NerdyDuck.CodedExceptions.IO
{
	/// <summary>
	/// The exception that is thrown when an attempt to access a directory that does not exist on disk fails.
	/// This exception provides constructors to set custom <see cref="Exception.HResult"/> values.
	/// </summary>
#if WINDOWS_DESKTOP
	[Serializable]
#endif
	[CodedException]
	[ComVisible(true)]
	public class CodedDirectoryNotFoundException
#if WINDOWS_DESKTOP || NETCORE
		: System.IO.DirectoryNotFoundException
#endif
#if WINDOWS_UWP
		: System.IO.IOException
#endif
	{
		#region Private fields
		private string mDirectoryName;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the name of the directory that cannot be found.
		/// </summary>
		/// <value>The name of the directory, or <see langword="null"/> if no directory name was passed to the constructor for this instance.</value>
		public string DirectoryName
		{
			get { return mDirectoryName; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with its HRESULT set to COR_E_DIRECTORYNOTFOUND, and its inner exception set to a null reference.
		/// </summary>
		/// <remarks>
		/// The constructor initializes the <see cref="Exception.Message"/> property of the new instance to a message that describes the error, such as "Could not find the specified directory." This message takes into account the current system culture.
		/// </remarks>
		public CodedDirectoryNotFoundException()
			: base(CreateMessage(null, null))
		{
			mDirectoryName = null;
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
			mDirectoryName = null;
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
			mDirectoryName = null;
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
			mDirectoryName = directoryName;
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
			mDirectoryName = directoryName;
			HResult = unchecked((int)0x80070003);
		}

#if WINDOWS_DESKTOP
		/// <summary>
		/// Initializes a new instance of the <see cref="CodedDirectoryNotFoundException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info"/> argument is <see langword="null"/>.</exception>
		/// <exception cref="System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		protected CodedDirectoryNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
			mDirectoryName = info.GetString(nameof(DirectoryName));
		}
#endif

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
			mDirectoryName = null;
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
			mDirectoryName = null;
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
			mDirectoryName = null;
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
			mDirectoryName = directoryName;
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
			mDirectoryName = directoryName;
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
			if (!string.IsNullOrEmpty(mDirectoryName))
			{
				CustomText = string.Format(Properties.Resources.Global_DirectoryName, mDirectoryName);
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
			info.AddValue(nameof(DirectoryName), mDirectoryName);
		}
#endif
		#endregion

		#region Private methods
		/// <summary>
		/// Creates the string stored in the Message property, with or without the directory name.
		/// </summary>
		/// <param name="message">The message provided by the constructor. May be null.</param>
		/// <param name="directoryName">The directory name. May be null.</param>
		/// <returns>Either message, if it is not null; or a string stating that the directory cannot be found, with the directory name, if it is not null.</returns>
		private static string CreateMessage(string message, string directoryName)
		{
			if (message != null)
			{
				return message;
			}
			if (directoryName == null)
			{
				return Properties.Resources.CodedDirectoryNotFoundException_Message;
			}
			return string.Format(Properties.Resources.CodedDirectoryNotFoundException_MessageDirectory, directoryName);
		}
		#endregion
	}
}
