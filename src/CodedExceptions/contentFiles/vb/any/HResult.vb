#Region "Copyright"
'******************************************************************************
' NerdyDuck.CodedExceptions - Exceptions with custom HRESULTs to identify the 
' origins of errors.
' 
' The MIT License (MIT)
'
' Copyright (c) Daniel Kopp, dak@nerdyduck.de
'
' All rights reserved.
'
' Permission Is hereby granted, free of charge, to any person obtaining a copy
' of this software And associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
' copies of the Software, And to permit persons to whom the Software Is
' furnished to do so, subject to the following conditions:
'
' The above copyright notice And this permission notice shall be included in
' all copies Or substantial portions of the Software.
'
' THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS Or
' IMPLIED, INCLUDING BUT Not LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS Or COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES Or OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT Or OTHERWISE, ARISING FROM,
' OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
' SOFTWARE.
'******************************************************************************
#End Region

''' <summary>
''' Provides easy access to the assembly's facility id and base HRESULT code.
''' </summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("NerdyDuck.CodedExceptions", "2.0.0.0"), Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
Partial Friend Module HResult

	' Load the facility id lazy and thread-safe.
	Private ReadOnly _facilityId As Global.System.Lazy(Of Integer) = New Global.System.Lazy(Of Integer)(AddressOf InitFacilityId)

	Private ReadOnly _hResultBase As Global.System.Lazy(Of Integer) =
		New Global.System.Lazy(Of Integer)(Function() Global.NerdyDuck.CodedExceptions.HResultHelper.GetBaseHResult(_facilityId.Value))

	''' <summary>
	''' Gets the facility identifier of the current assembly.
	''' </summary>
	''' <returns>The facility identifier, or 0, if no <see cref="AssemblyFacilityIdentifierAttribute"/> was found on the current assembly.</returns>
	''' <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend ReadOnly Property FacilityId() As Integer
		Get
			Return _facilityId.Value
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
			Return _hResultBase.Value
		End Get
	End Property


	''' <summary>
	''' Combines the specified error identifier with the base HRESULT value for this assembly.
	''' </summary>
	''' <param name="errorId">The error identifier to add to the base HRESULT value.</param>
	''' <returns>A custom HRESULT value, combined from <paramref name="errorId"/> And <see cref="HResultBase"/>.</returns>
	''' <remarks>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</remarks>
	Friend Function Create(errorId As Integer) As Integer
		Return _hResultBase.Value Or errorId
	End Function

	''' <summary>
	''' Combines the specified error identifier, represented by an enumeration, with the base HRESULT value for this assembly.
	''' </summary>
	''' <param name="errorId">The error identifier to add to the base HRESULT value.</param>
	''' <returns>A custom HRESULT value, combined from <paramref name="errorId"/> And <see cref="HResultBase"/>.</returns>
	''' <remarks><para>This method can only be used for enumerations based on <see cref="System.Int32"/>.</para>
	''' <para>See the <a href="http://msdn.microsoft.com/en-us/library/cc231198.aspx">HRESULT definition at MSDN</a> for
	''' more information about the definition of HRESULT values.</para></remarks>
	''' <exception cref="NerdyDuck.CodedExceptions.CodedArgumentException"><paramref name="errorId"/> Is Not based on <see cref="System.Int32"/> Or Not a valid enumeration.</exception>
	Friend Function Create(errorId As System.Enum) As Integer
		Return _hResultBase.Value Or Global.NerdyDuck.CodedExceptions.HResultHelper.EnumToInt32(errorId)
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
