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

#nullable enable
using System;
using System.Globalization;
using System.Text;

namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// Helper class to create and examine custom <see cref="Exception.HResult"/> values.
	/// </summary>
	/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</remarks>
	public static class HResultHelper
	{
		#region Constants
		/// <summary>
		/// The base value for all custom HRESULT values, to unambiguously distinguish the exceptions from Microsoft error codes.
		/// </summary>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		public const int HResultBase = unchecked((int)0xa0000000);

		/// <summary>
		/// A bit mask to filter the id of the facility (= the assembly) that threw the exception.
		/// </summary>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		/// <example>int FacilityId = (ex.HResult &amp; HResultHelper.FacilityIdMask) >> HResultHelper.FacilityIdShift;</example>
		public const int FacilityIdMask = 0x07ff0000;

		/// <summary>
		/// The number of bits to shift an <see cref="Exception.HResult"/> to the left to get the facility (= the assembly) id on the lowest bit.
		/// </summary>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		/// <example>int FacilityId = (ex.HResult &amp; HResultHelper.FacilityIdMask) >> HResultHelper.FacilityIdShift;</example>
		public const int FacilityIdShift = 16;

		/// <summary>
		/// A bit mask to filter the error id of the <see cref="Exception.HResult"/>.
		/// </summary>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		/// <example>int ErrorId = ex.HResult &amp; HResultHelper.ErrorIdMask;</example>
		public const int ErrorIdMask = 0x0000ffff;

		/// <summary>
		/// A bit mask to check if the <see cref="Exception.HResult"/> is a custom value.
		/// </summary>
		/// <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
		/// more information about the definition of HRESULT values.</remarks>
		/// <example>bool IsCustom = (ex.HResult &amp; HResultHelper.CustomHResultMask) > 0;</example>
		public const int CustomHResultMask = 0x20000000;

		/// <summary>
		/// Exception message format that includes the HResult
		/// </summary>
		internal const string ExceptionBaseFormat = "{0}: (0x{1:x8}) {2}";

		/// <summary>
		/// Format to concatenate an exception's ToString output with the output of one or more calls to ToString of other exceptions.
		/// </summary>
		internal const string ExceptionConcatFormat = " ---> ";

		/// <summary>
		/// Error code of E_Fail.
		/// </summary>
		internal const int E_FAIL = unchecked((int)0x80004005);

		// Maximum capacity for StringBuilder instances cached in _cachedInstance.
		private const int MaximumCapacity = 360;
		#endregion

		#region Private fields
		[ThreadStatic]
		private static StringBuilder? s_cachedInstance;
		#endregion

		#region Public methods
		#region GetFacilityId
		/// <summary>
		/// Gets the facility (= the assembly) identifier part of an <see cref="Exception.HResult"/>.
		/// </summary>
		/// <param name="hresult">The <see cref="Exception.HResult"/> to extract the facility identifier from.</param>
		/// <returns>The facility identifier.</returns>
		public static int GetFacilityId(int hresult) => (hresult & FacilityIdMask) >> FacilityIdShift;
		#endregion

		#region GetErrorId
		/// <summary>
		/// Gets the error identifier part of an <see cref="Exception.HResult"/>.
		/// </summary>
		/// <param name="hresult">The <see cref="Exception.HResult"/> to extract the error identifier from.</param>
		/// <returns>An error identifier.</returns>
		public static int GetErrorId(int hresult) => hresult & ErrorIdMask;
		#endregion

		#region GetBaseHResult
		/// <summary>
		/// Gets the HRESULT base value for the specified facility identifier.
		/// </summary>
		/// <param name="facilityId">A facility identifier.</param>
		/// <returns>A HRESULT base value, starting with 0xa0nnnnnn.</returns>
		public static int GetBaseHResult(int facilityId) => HResultBase | (facilityId << FacilityIdShift);
		#endregion

		#region IsCustomHResult
		/// <summary>
		/// Checks if the specified HRESULT is a custom error value.
		/// </summary>
		/// <param name="hresult">The <see cref="Exception.HResult"/> to check.</param>
		/// <returns><see langword="true"/>, if <paramref name="hresult"/> is a custom value; otherwise, <see langword="false"/>.</returns>
		public static bool IsCustomHResult(int hresult) => (hresult & CustomHResultMask) > 0;
		#endregion

		#region GetEnumInt32Value
		/// <summary>
		/// Returns the integer value of an enumeration value.
		/// </summary>
		/// <param name="enumValue">The enumeration value to get the integer value of.</param>
		/// <returns>An integer that represents <paramref name="enumValue"/>.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031", Justification = "Only enumerations should be used to avoid confusion.", MessageId = "enumValue")]
		public static int EnumToInt32(Enum enumValue) => ((IConvertible)enumValue ?? throw new ArgumentNullException(nameof(enumValue))).ToInt32(null);
		#endregion

		#region CreateToString
		/// <summary>
		/// Creates the default ToString implementation for CodedExceptions, with the optional addition of custom text.
		/// </summary>
		/// <param name="ex">The exception to create the ToString return value for.</param>
		/// <param name="customText">A custom text (e.g. parameter values) that is inserted between the exception message and the stack trace.</param>
		/// <remarks>Unlike the default implementation, this implementation inserts the HResult between class name and message.</remarks>
		/// <returns>A string representing the specified exception.</returns>
		/// <exception cref="ArgumentNullException">ex is null.</exception>
		public static string CreateToString(Exception ex, string? customText)
		{
			if (ex == null)
			{
				throw new ArgumentNullException(nameof(ex));
			}

			StringBuilder sb = AcquireStringBuilder();
			sb.AppendFormat(CultureInfo.CurrentCulture, ExceptionBaseFormat, ex.GetType().FullName, ex.HResult, ex.Message);
			if (!string.IsNullOrEmpty(customText))
			{
				sb.Append(Environment.NewLine);
				sb.Append(customText);
			}
			if (ex.InnerException != null)
			{
				sb.Append(ExceptionConcatFormat);
				sb.Append(ex.InnerException.ToString());
			}
			if (ex.StackTrace != null)
			{
				sb.Append(Environment.NewLine);
				sb.Append(ex.StackTrace);
			}

			return GetStringAndRelease(sb);
		}
		#endregion

		#region HexStringToByteArray
		/// <summary>
		/// Converts a string of hexadecimal values into a byte array.
		/// </summary>
		/// <param name="value">The string containing the hexadecimal value.</param>
		/// <returns>An array of bytes, or <see langword="null"/>, if <paramref name="value"/> is <see langword="null"/> or empty.</returns>
		/// <remarks>The <paramref name="value"/> may be prefixed by 0x; the method is case-insensitive.</remarks>
		/// <exception cref="CodedFormatException"><paramref name="value"/> contains at least one character that is not a valid hexadecimal digit.</exception>
		public static byte[]? HexStringToByteArray(string? value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			// HACK: disabling warning until string.IsNullOrEmpty is recognized as a null check
#pragma warning disable CS8602
			int valueLength = (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) ? value.Length - 2 : value.Length;
#pragma warning restore CS8602
			byte[] returnValue = new byte[(valueLength + 1) >> 1];
			int lastCell = returnValue.Length - 1;
			int lastChar = value.Length - 1;

			for (int i = 0; i < valueLength; i++)
			{
				returnValue[lastCell - (i >> 1)] |= (byte)(HexToInt(value[lastChar - i]) << ((i & 1) << 2));
			}

			return returnValue;
		}
		#endregion
		#endregion

		#region Internal methods
		/// <summary>
		/// Acquires a cached instance of a <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="capacity">The expected capacity the returned <see cref="StringBuilder"/> will require.</param>
		/// <returns>A cached instance of <see cref="StringBuilder"/>, if <paramref name="capacity"/> is less than 360; otherwise, a new instance.</returns>
		internal static StringBuilder AcquireStringBuilder(int capacity = 16)
		{
			if (capacity < MaximumCapacity)
			{
				StringBuilder? cachedInstance = s_cachedInstance;
				if ((cachedInstance != null) && (capacity <= cachedInstance.Capacity))
				{
					s_cachedInstance = null;
					cachedInstance.Clear();
					return cachedInstance;
				}
			}
			return new StringBuilder(capacity);
		}

		/// <summary>
		/// Gets the string in the specified <see cref="StringBuilder"/>, and releases it to the cache.
		/// </summary>
		/// <param name="sb">The <see cref="StringBuilder"/> to release.</param>
		/// <returns>The string that <paramref name="sb"/> contained.</returns>
		internal static string GetStringAndRelease(StringBuilder sb)
		{
			Release(sb);
			return sb.ToString();
		}

		/// <summary>
		/// Releases the specified <see cref="StringBuilder"/> to the cache.
		/// </summary>
		/// <param name="sb">The <see cref="StringBuilder"/> to release.</param>
		internal static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= MaximumCapacity)
			{
				s_cachedInstance = sb;
			}
		}
		#endregion

		#region Private methods
		private static int HexToInt(char value)
		{
			switch (value)
			{
				case '0':
					return 0;
				case '1':
					return 1;
				case '2':
					return 2;
				case '3':
					return 3;
				case '4':
					return 4;
				case '5':
					return 5;
				case '6':
					return 6;
				case '7':
					return 7;
				case '8':
					return 8;
				case '9':
					return 9;
				case 'a':
				case 'A':
					return 10;
				case 'b':
				case 'B':
					return 11;
				case 'c':
				case 'C':
					return 12;
				case 'd':
				case 'D':
					return 13;
				case 'e':
				case 'E':
					return 14;
				case 'f':
				case 'F':
					return 15;
				default:
					throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.HResultHelper_HexToInt_InvalidChar, value));
			}
		}
		#endregion
	}
}
