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

using System.Text;

namespace NerdyDuck.CodedExceptions;

/// <summary>
/// Helper class to create and examine custom <see cref="Exception.HResult"/> values.
/// </summary>
/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
/// more information about the definition of HRESULT values.</remarks>
public static class HResultHelper
{
	/// <summary>
	/// The base value for all custom HRESULT values, to unambiguously distinguish the exceptions from Microsoft error codes.
	/// </summary>
	/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</remarks>
	public const int HResultBase = unchecked((int)0xa0000000);

	/// <summary>
	/// A bit mask to filter the id of the facility (= the assembly) that threw the exception.
	/// </summary>
	/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</remarks>
	/// <example>int FacilityId = (ex.HResult &amp; HResultHelper.FacilityIdMask) >> HResultHelper.FacilityIdShift;</example>
	public const int FacilityIdMask = 0x07ff0000;

	/// <summary>
	/// The number of bits to shift an <see cref="Exception.HResult"/> to the left to get the facility (= the assembly) id on the lowest bit.
	/// </summary>
	/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</remarks>
	/// <example>int FacilityId = (ex.HResult &amp; HResultHelper.FacilityIdMask) >> HResultHelper.FacilityIdShift;</example>
	public const int FacilityIdShift = 16;

	/// <summary>
	/// A bit mask to filter the error id of the <see cref="Exception.HResult"/>.
	/// </summary>
	/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	/// more information about the definition of HRESULT values.</remarks>
	/// <example>int ErrorId = ex.HResult &amp; HResultHelper.ErrorIdMask;</example>
	public const int ErrorIdMask = 0x0000ffff;

	/// <summary>
	/// A bit mask to check if the <see cref="Exception.HResult"/> is a custom value.
	/// </summary>
	/// <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
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

	[ThreadStatic]
	private static StringBuilder? s_cachedInstance;

	/// <summary>
	/// Gets the facility (= the assembly) identifier part of an <see cref="Exception.HResult"/>.
	/// </summary>
	/// <param name="hresult">The <see cref="Exception.HResult"/> to extract the facility identifier from.</param>
	/// <returns>The facility identifier.</returns>
	public static int GetFacilityId(int hresult) => (hresult & FacilityIdMask) >> FacilityIdShift;

	/// <summary>
	/// Gets the error identifier part of an <see cref="Exception.HResult"/>.
	/// </summary>
	/// <param name="hresult">The <see cref="Exception.HResult"/> to extract the error identifier from.</param>
	/// <returns>An error identifier.</returns>
	public static int GetErrorId(int hresult) => hresult & ErrorIdMask;

	/// <summary>
	/// Gets the HRESULT base value for the specified facility identifier.
	/// </summary>
	/// <param name="facilityId">A facility identifier.</param>
	/// <returns>A HRESULT base value, starting with 0xa0nnnnnn.</returns>
	public static int GetBaseHResult(int facilityId) => HResultBase | (facilityId << FacilityIdShift);

	/// <summary>
	/// Checks if the specified HRESULT is a custom error value.
	/// </summary>
	/// <param name="hresult">The <see cref="Exception.HResult"/> to check.</param>
	/// <returns><see langword="true"/>, if <paramref name="hresult"/> is a custom value; otherwise, <see langword="false"/>.</returns>
	public static bool IsCustomHResult(int hresult) => (hresult & CustomHResultMask) > 0;

	/// <summary>
	/// Returns the integer value of an enumeration value.
	/// </summary>
	/// <param name="enumValue">The enumeration value to get the integer value of.</param>
	/// <returns>An integer that represents <paramref name="enumValue"/>.</returns>
	public static int EnumToInt32(Enum enumValue) => ((IConvertible)enumValue ?? throw new ArgumentNullException(nameof(enumValue))).ToInt32(null);

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
		_ = sb.AppendFormat(CultureInfo.CurrentCulture, ExceptionBaseFormat, ex.GetType().FullName, ex.HResult, ex.Message);
		if (!string.IsNullOrEmpty(customText))
		{
			_ = sb.Append(Environment.NewLine).Append(customText);
		}
		if (ex.InnerException != null)
		{
			_ = sb.Append(ExceptionConcatFormat).Append(ex.InnerException.ToString());
		}
		if (ex.StackTrace != null)
		{
			_ = sb.Append(Environment.NewLine).Append(ex.StackTrace);
		}

		return GetStringAndRelease(sb);
	}

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
#pragma warning disable IDE0079
#pragma warning disable CS8602 // Dereference of a possibly null reference.
		int valueLength = value.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? value.Length - 2 : value.Length;
#pragma warning restore CS8602, IDE0079 // Dereference of a possibly null reference.
		byte[] returnValue = new byte[(valueLength + 1) >> 1];
		int lastCell = returnValue.Length - 1;
		int lastChar = value.Length - 1;

		for (int i = 0; i < valueLength; i++)
		{
			returnValue[lastCell - (i >> 1)] |= (byte)(HexToInt(value[lastChar - i]) << ((i & 1) << 2));
		}

		return returnValue;
	}

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
				_ = cachedInstance.Clear();
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

	/// <summary>
	/// Converts a hexadecimal digit character into a decimal value.
	/// </summary>
	/// <param name="value">The value to convert.</param>
	/// <returns>A value between 0 and 15</returns>
	/// <exception cref="FormatException"><paramref name="value"/> is not a hexadecimal digit.</exception>
	private static int HexToInt(char value) => value switch
	{
		'0' => 0,
		'1' => 1,
		'2' => 2,
		'3' => 3,
		'4' => 4,
		'5' => 5,
		'6' => 6,
		'7' => 7,
		'8' => 8,
		'9' => 9,
		'a' or 'A' => 10,
		'b' or 'B' => 11,
		'c' or 'C' => 12,
		'd' or 'D' => 13,
		'e' or 'E' => 14,
		'f' or 'F' => 15,
		_ => throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.HResultHelper_HexToInt_InvalidChar, value)),
	};
}
