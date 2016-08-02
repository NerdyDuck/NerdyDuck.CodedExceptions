#region Copyright
/*******************************************************************************
 * <copyright file="HResultHelper.cs" owner="Daniel Kopp">
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
 * <file name="HResultHelper.cs" date="2015-08-06">
 * Helper class to create and examine custom HResult values.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Reflection;

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
		internal const string ExceptionConcatFormat = "{0} ---> {1}";

		internal const int E_FAIL = unchecked((int)0x80004005);
		#endregion

		#region Public methods
		#region GetFacilityId
		/// <summary>
		/// Gets the facility (= the assembly) identifier part of an <see cref="Exception.HResult"/>.
		/// </summary>
		/// <param name="hresult">The <see cref="Exception.HResult"/> to extract the facility identifier from.</param>
		/// <returns>The facility identifier.</returns>
		public static int GetFacilityId(int hresult)
		{
			return (hresult & FacilityIdMask) >> FacilityIdShift;
		}
		#endregion

		#region GetErrorId
		/// <summary>
		/// Gets the error identifier part of an <see cref="Exception.HResult"/>.
		/// </summary>
		/// <param name="hresult">The <see cref="Exception.HResult"/> to extract the error identifier from.</param>
		/// <returns>An error identifier.</returns>
		public static int GetErrorId(int hresult)
		{
			return hresult & ErrorIdMask;
		}
		#endregion

		#region GetBaseHResult
		/// <summary>
		/// Gets the HRESULT base value for the specified facility identifier.
		/// </summary>
		/// <param name="facilityId">A facility identifier.</param>
		/// <returns>A HRESULT base value, starting with 0xa0nnnnnn.</returns>
		public static int GetBaseHResult(int facilityId)
		{
			return HResultBase | (facilityId << FacilityIdShift);
		}
		#endregion

		#region IsCustomHResult
		/// <summary>
		/// Checks if the specified HRESULT is a custom error value.
		/// </summary>
		/// <param name="hresult">The <see cref="Exception.HResult"/> to check.</param>
		/// <returns><see langword="true"/>, if <paramref name="hresult"/> is a custom value; otherwise, <see langword="false"/>.</returns>
		public static bool IsCustomHResult(int hresult)
		{
			return (hresult & CustomHResultMask) > 0;
		}
		#endregion

		#region GetEnumInt32Value
		/// <summary>
		/// Returns the integer value of an enumeration value.
		/// </summary>
		/// <param name="enumValue">The enumeration value to get the integer value of.</param>
		/// <returns>An integer that represents <paramref name="enumValue"/>.</returns>
		/// <remarks>This method only works for enumerations based on <see cref="System.Int32"/>.</remarks>
		/// <exception cref="CodedArgumentException"><paramref name="enumValue"/> is not based on <see cref="System.Int32"/> or not a valid enumeration.</exception>
		public static int GetEnumInt32Value(Enum enumValue)
		{
			if (GetEnumUnderlyingType(enumValue.GetType()) != typeof(int))
			{
				throw new CodedArgumentException(Errors.CreateHResult(ErrorCodes.HResultHelper_GetEnumInt32Value_NotInt32), Properties.Resources.HResultHelper_GetEnumInt32Value_NotInt32, nameof(enumValue));
			}

			return (int)((object)enumValue);
		}
		#endregion

		#region GetEnumUnderlyingType
		/// <summary>
		/// Returns the underlying type of the current enumeration type.
		/// </summary>
		/// <param name="enumType">The enumeration type to get the underlying type of.</param>
		/// <returns>The underlying type of the current enumeration.</returns>
		/// <exception cref="CodedArgumentNullException"><paramref name="enumType"/> is <see langword="null"/>.</exception>
		/// <exception cref="CodedArgumentException"><paramref name="enumType"/> is not an enumeration or the enumeration type is not valid, because it contains more than one instance field.</exception>
		/// <remarks>This method is a reimplementation of the GetEnumUnderlyingType method of the <see cref="Type"/> class, which does not exist on the Universal Windows platform.</remarks>
		public static Type GetEnumUnderlyingType(Type enumType)
		{
			if (enumType == null)
			{
				throw new CodedArgumentNullException(Errors.CreateHResult(ErrorCodes.HResultHelper_GetEnumUnderlyingType_ArgNull), nameof(enumType));
			}
			TypeInfo enumTypeInfo = enumType.GetTypeInfo();
			if (!enumTypeInfo.IsEnum)
			{
				throw new CodedArgumentException(Errors.CreateHResult(ErrorCodes.HResultHelper_GetEnumUnderlyingType_MustBeEnum), Properties.Resources.HResultHelper_GetEnumUnderlyingType_MustBeEnum, nameof(enumType));
			}
#if NETCORE
			FieldInfo[] fields = enumTypeInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
#else
			FieldInfo[] fields = enumType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
#endif
			if ((fields == null) || (fields.Length != 1))
			{
				throw new CodedArgumentException(Errors.CreateHResult(ErrorCodes.HResultHelper_GetEnumUnderlyingType_EnumInvalid), Properties.Resources.HResultHelper_GetEnumUnderlyingType_EnumInvalid, nameof(enumType));
			}
			return fields[0].FieldType;
		}
		#endregion

		#region CreateToString
		/// <summary>
		/// Creates the default ToString implementation for CodedExceptions, with the optional addition of custom text.
		/// </summary>
		/// <param name="ex">The exception to create the ToString return value for.</param>
		/// <param name="customText">A custom text (e.g. parameter values) that is inserted between the exception message and the stack trace.</param>
		/// <remarks>Unlike the default implementation, this implementation inserts the HResult between class name and message.</remarks>
		/// <returns>A string representing the specified exception.</returns>
		/// <exception cref="CodedArgumentNullException">ex is null.</exception>
		public static string CreateToString(Exception ex, string customText)
		{
			if (ex == null)
				throw new CodedArgumentNullException(Errors.CreateHResult(ErrorCodes.HResultHelper_CreateToString_ExNull), nameof(ex));

			string ReturnValue = string.Format(ExceptionBaseFormat, ex.GetType().FullName, ex.HResult, ex.Message);
			if (!string.IsNullOrEmpty(customText))
			{
				ReturnValue = ReturnValue + Environment.NewLine + customText;
			}
			if (ex.InnerException != null)
			{
				ReturnValue = string.Format(ExceptionConcatFormat, ReturnValue, ex.InnerException.ToString());
			}
			if (ex.StackTrace != null)
			{
				ReturnValue = ReturnValue + Environment.NewLine + ex.StackTrace;
			}

			return ReturnValue;
		}
		#endregion
		#endregion
	}
}
