' Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
' This file Is licensed to you under the MIT license.
' See the LICENSE file in the project root for more information.

''' <summary>
''' Provides easy access to the assembly's facility id and base HRESULT code.
''' </summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("NerdyDuck.CodedExceptions", "2.0.0.0"), Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
Partial Friend Module HResult

	' Load the facility id lazy and thread-safe.
	Private ReadOnly _facilityId As Global.System.Lazy(Of Integer) = New Global.System.Lazy(Of Integer)(AddressOf InitFacilityId)

	Private ReadOnly _hResultBase As Global.System.Lazy(Of Integer) =
		New Global.System.Lazy(Of Integer)(Function() Global.NerdyDuck.CodedExceptions.HResultHelper.GetBaseHResult(_facilityId.Value))

	Private ReadOnly _hResultErrorBase As Global.System.Lazy(Of Integer) =
		New Global.System.Lazy(Of Integer)(Function() Global.NerdyDuck.CodedExceptions.HResultHelper.GetBaseHResultError(_facilityId.Value))

	''' <summary>
	''' Gets the facility identifier of the current assembly.
	''' </summary>
	''' <returns>The facility identifier, or 0, if no <see cref="AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</returns>
	''' <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend ReadOnly Property FacilityId() As Integer
		Get
			Return _facilityId.Value
		End Get
	End Property

	''' <summary>
	''' Gets the base HRESULT success value of the current assembly.
	''' </summary>
	''' <returns>The base HRESULT value, or 0x20000000, if no <see cref="AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</returns>
	''' <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend ReadOnly Property HResultBase() As Integer
		Get
			Return _hResultBase.Value
		End Get
	End Property

	''' <summary>
	''' Gets the base HRESULT error value of the current assembly.
	''' </summary>
	''' <returns>The base HRESULT value, or 0xa0000000, if no <see cref="AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</returns>
	''' <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend ReadOnly Property HResultErrorBase() As Integer
		Get
			Return _hResultErrorBase.Value
		End Get
	End Property

	''' <summary>
	''' Combines the specified error identifier with the base HRESULT value for this assembly.
	''' </summary>
	''' <param name="errorId">The error identifier to add to the base HRESULT value.</param>
	''' <returns>A custom HRESULT value, combined from <paramref name="errorId"/> And <see cref="HResultErrorBase"/>.</returns>
	''' <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend Function Create(errorId As Integer) As Integer
		Return _hResultErrorBase.Value Or errorId
	End Function

	''' <summary>
	''' Combines the specified error identifier, represented by an enumeration, with the base HRESULT value for this assembly.
	''' </summary>
	''' <param name="errorId">The error identifier to add to the base HRESULT value.</param>
	''' <returns>A custom HRESULT value, combined from <paramref name="errorId"/> And <see cref="HResultErrorBase"/>.</returns>
	''' <remarks><para>This method can only be used for enumerations based on <see cref="System.Int32"/>.</para>
	''' <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</para></remarks>
	''' <exception cref="NerdyDuck.CodedExceptions.CodedArgumentException"><paramref name="errorId"/> Is Not based on <see cref="System.Int32"/> Or Not a valid enumeration.</exception>
	Friend Function Create(errorId As System.Enum) As Integer
		Return _hResultErrorBase.Value Or Global.NerdyDuck.CodedExceptions.HResultHelper.EnumToInt32(errorId)
	End Function

	''' <summary>
	''' Combines the specified success identifier with the base HRESULT value for this assembly.
	''' </summary>
	''' <param name="successId">The success identifier to add to the base HRESULT value.</param>
	''' <returns>A custom HRESULT value, combined from <paramref name="successId"/> And <see cref="HResultBase"/>.</returns>
	''' <remarks>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend Function CreateSuccess(successId As Integer) As Integer
		Return _hResultBase.Value Or successId
	End Function

	''' <summary>
	''' Combines the specified success identifier, represented by an enumeration, with the base HRESULT value for this assembly.
	''' </summary>
	''' <param name="successId">The success identifier to add to the base HRESULT value.</param>
	''' <returns>A custom HRESULT value, combined from <paramref name="successId"/> And <see cref="HResultBase"/>.</returns>
	''' <remarks><para>This method can only be used for enumerations based on <see cref="System.Int32"/>.</para>
	''' <para>See the <a href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/0642cb2f-2075-4469-918c-4441e69c548a">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</para></remarks>
	''' <exception cref="NerdyDuck.CodedExceptions.CodedArgumentException"><paramref name="successId"/> Is not based on <see cref="System.Int32"/> or not a valid enumeration.</exception>
	Friend Function CreateSuccess(successId As System.Enum) As Integer
		Return _hResultBase.Value Or Global.NerdyDuck.CodedExceptions.HResultHelper.EnumToInt32(successId)
	End Function

	''' <summary>
	''' Load the facility id lazy and thread-safe.
	''' </summary>
	''' <returns>The facility identifier, or 0, if no <see cref="NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</returns>
	Private Function InitFacilityId()
		Dim Identifier As Integer = 0
		' Check for override in configuration
		If Global.NerdyDuck.CodedExceptions.Configuration.AssemblyFacilityOverrideCache.Global.TryGetOverride(GetType(HResult).Assembly, Identifier) Then
			Return Identifier
		End If
		' Try to get identifier from assembly attribute
		Return If(Global.NerdyDuck.CodedExceptions.AssemblyFacilityIdentifierAttribute.FromAssembly(GetType(HResult).Assembly)?.FacilityId, 0)
	End Function
End Module
