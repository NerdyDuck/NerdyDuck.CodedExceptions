#region Copyright
/*******************************************************************************
 * <copyright file="ErrorCodes.cs" owner="Daniel Kopp">
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
 * <file name="ErrorCodes.cs" date="2016-04-12">
 * Error codes for the NerdyDuck.CodedExceptions assembly.
 * </file>
 ******************************************************************************/
#endregion


namespace NerdyDuck.CodedExceptions
{
	/// <summary>
	/// Error codes for the NerdyDuck.CodedExceptions assembly.
	/// </summary>
	internal enum ErrorCodes
	{
		/// <summary>
		/// 0x01; HResultHelper.GetEnumUnderlyingType; enumType is null.
		/// </summary>
		HResultHelper_GetEnumUnderlyingType_ArgNull = 0x01,
		/// <summary>
		/// 0x02; HResultHelper.GetEnumUnderlyingType; enumType must be an enumeration.
		/// </summary>
		HResultHelper_GetEnumUnderlyingType_MustBeEnum,
		/// <summary>
		/// 0x03; HResultHelper.GetEnumUnderlyingType; enumType is not a valid enumeration.
		/// </summary>
		HResultHelper_GetEnumUnderlyingType_EnumInvalid,
		/// <summary>
		/// 0x04; HResultHelper.CreateToString; ex is null.
		/// </summary>
		HResultHelper_CreateToString_ExNull,
		/// <summary>
		/// 0x05; HResultHelper.GetEnumInt32Value; enumValue is not based on Int32.
		/// </summary>
		HResultHelper_GetEnumInt32Value_NotInt32,
		/// <summary>
		/// 0x06; AssemblyFacilityIdentifierAttribute.ctor; facilityId is less than 0 or greater than 2047.
		/// </summary>
		AssemblyFacilityIdentifierAttribute_ctor_OutOfRangeRange,
		/// <summary>
		/// 0x07; AssemblyFacilityIdentifierAttribut.FromAssembly; assembly is null.
		/// </summary>
		AssemblyFacilityIdentifierAttribute_FromAssembly_AssemblyNull,
		/// <summary>
		/// 0x08; AssemblyFacilityIdentifierAttribute.FromAssembly; type is null.
		/// </summary>
		AssemblyFacilityIdentifierAttribute_FromAssembly_TypeNull,
		/// <summary>
		/// 0x09; AssemblyFacilityIdentifierAttribute.TryGetOverride; assembly is null.
		/// </summary>
		AssemblyFacilityIdentifierAttribute_TryGetOverride_AssemblyNull,
		/// <summary>
		/// 0x0a; AssemblyFacilityIdentifierAttribute.TryGetOverride; type is null.
		/// </summary>
		AssemblyFacilityIdentifierAttribute_TryGetOverride_TypeNull,
		/// <summary>
		/// 0x0b; CodedExceptionAttribute.IsCodedException; ex is null.
		/// </summary>
		CodedExceptionAttribute_IsCodedException_ArgNull,
		/// <summary>
		/// 0x0c; ExceptionExtensions.GetErrorId; ex is null.
		/// </summary>
		ExceptionExtensions_GetErrorId_ArgNull,
		/// <summary>
		/// 0x0d; ExceptionExtensions.GetErrorIdGetFacilityId; ex is null.
		/// </summary>
		ExceptionExtensions_GetFacilityId_ArgNull,
		/// <summary>
		/// 0x0e; ExceptionExtensions.IsCodedException; ex is null.
		/// </summary>
		ExceptionExtensions_IsCodedException_ArgNull,
		/// <summary>
		/// 0x0f; ExceptionExtensions.IsCustomHResult; ex is null.
		/// </summary>
		ExceptionExtensions_IsCustomHResult_ArgNull,
		/// <summary>
		/// 0x10; FacilityOverride.ctor; assemblyName is null.
		/// </summary>
		FacilityOverride_ctor_AssemblyNull,
		/// <summary>
		/// 0x11; FacilityOverride.ctor; assemblyName is null.
		/// </summary>
		FacilityOverride_ctor_AssemblyStringNull,
		/// <summary>
		/// 0x12; FacilityOverride.ctor; facilityId is less than 0 or greater than 2047.
		/// </summary>
		FacilityOverride_ctor_FacilityOutOfRange,
#if WINDOWS_DESKTOP
		/// <summary>
		/// 0x13; FacilityOverrideElement.ToOverride; assemblyName is invalid.
		/// </summary>
		FacilityOverrideElement_ToOverride_Invalid,
#endif
	}
}
