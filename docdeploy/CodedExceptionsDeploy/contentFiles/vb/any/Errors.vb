#Region "Copyright"
'===============================================================================
' <copyright file="Errors.cs" owner="Daniel Kopp">
' Copyright 2015 Daniel Kopp
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may Not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law Or agreed to in writing, software
' distributed under the License Is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES Or CONDITIONS OF ANY KIND, either express Or implied.
' See the License for the specific language governing permissions And
' limitations under the License.
' </copyright>
' <author name="Daniel Kopp" email="dak@nerdyduck.de" />
' <file name="Errors.cs" date="2015-08-06">
' Provides easy access to the assembly's facility id and base HRESULT code.
' </file>
'===============================================================================
#End Region

Imports NerdyDuck.CodedExceptions

Friend Module Errors
#Region "Private fields"
	Private ReadOnly mFacilityId As Lazy(Of Integer) = New Lazy(Of Integer)(AddressOf InitFacilityId)
	Private ReadOnly mHResultBase As Lazy(Of Integer) = New Lazy(Of Integer)(AddressOf InitHResultBase)
#End Region

#Region "Properties"
	''' <summary>
	''' Gets the facility identifier of the current assembly.
	''' </summary>
	''' <returns>The facility identifier, or 0, if no <see cref="AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</returns>
	''' <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend ReadOnly Property FacilityId() As Integer
		Get
			Return mFacilityId.Value
		End Get
	End Property

	''' <summary>
	''' Gets the base HRESULT value of the current assembly.
	''' </summary>
	''' <returns>The base HRESULT value, or 0xa0000000, if no <see cref="AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</returns>
	''' <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend ReadOnly Property HResultBase() As Integer
		Get
			Return mHResultBase.Value
		End Get
	End Property
#End Region

#Region "Public methods"
	''' <summary>
	''' Combines the specified error identifier with the base HRESULT value for this assembly.
	''' </summary>
	''' <param name="errorId">The error identifier to add to the base HRESULT value.</param>
	''' <returns>A custom HRESULT value, combined from <paramref name="errorId"/> and <see cref="HResultBase"/>.</returns>
	''' <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend Function CreateHResult(errorId As Integer) As Integer
		Return mHResultBase.Value Or errorId
	End Function

	''' <summary>
	''' Combines the specified error identifier, represented by an enumeration, with the base HRESULT value for this assembly.
	''' </summary>
	''' <param name="errorId">The error identifier to add to the base HRESULT value.</param>
	''' <returns>A custom HRESULT value, combined from <paramref name="errorId"/> and <see cref="HResultBase"/>.</returns>
	''' <remarks><para>This method can only be used for enumerations based on <see cref="System.Int32"/>.</para>
	''' <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</para></remarks>
	''' <exception cref="NerdyDuck.CodedExceptions.CodedArgumentException"><paramref name="errorId"/> is not based on <see cref="System.Int32"/> or not a valid enumeration.</exception>
	Friend Function CreateHResult(errorId As System.Enum) As Integer
		Return mHResultBase.Value Or HResultHelper.GetEnumUnderlyingInt32Value(errorId)
	End Function
#End Region

#Region "Private methods"
	''' <summary>
	''' Load the facility id lazy and thread-safe.
	''' </summary>
	''' <returns>The facility identifier, or 0, if no <see cref="AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</returns>
	Private Function InitFacilityId()
		Dim Identifier As Integer = 0

		' Check for override in app.config file
		If AssemblyFacilityIdentifierAttribute.TryGetOverride(GetType(Errors), Identifier) = False Then
			' Try to get identifier from assembly attribute
			Dim IdentifierAttribute As AssemblyFacilityIdentifierAttribute = AssemblyFacilityIdentifierAttribute.FromType(GetType(Errors))
			If Not IdentifierAttribute Is Nothing Then
				Identifier = IdentifierAttribute.FacilityId
			End If
		End If
		Return Identifier
	End Function

	''' <summary>
	''' Load the HRESULT base value lazy and thread-safe.
	''' </summary>
	''' <returns>The base HRESULT value, or 0xa0000000, if no <see cref="AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</returns>
	Private Function InitHResultBase()
		Return HResultHelper.GetBaseHResult(FacilityId)
	End Function
#End Region
End Module

