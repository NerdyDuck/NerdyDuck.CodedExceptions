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
''' Provides easy access to the assembly's debug mode status.
''' </summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("NerdyDuck.CodedExceptions", "2.0.0.0"), Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
Partial Friend Module DebugMode

	Private _isInitialized As Boolean = False
	Private _isEnabled As Boolean = False
	Private ReadOnly _syncRoot As Object = New Object

	''' <summary>
	''' Gets a value indicting if the assembly was set into debug mode via configuration.
	''' </summary>
	''' <returns><see langword="true"/>, if the assembly is running in debug mode; otherwise, <see langword="false"/>.</returns>
	''' <remarks>Use this value to determine if extra log output, additional checks etc. are required.</remarks>
	Friend ReadOnly Property IsEnabled() As Boolean
		Get
			' First check without lock to avoid locking as far as possible
			If Not _isInitialized Then
				SyncLock _syncRoot
					' Double check in locked section to call code only once
					If Not _isInitialized Then
						_isEnabled = Global.NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.IsDebugModeEnabled(GetType(DebugMode).Assembly)
						_isInitialized = True
						AddHandler Global.NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.CacheChanged, AddressOf (Global_CacheChanged())
					End If
				End SyncLock
			End If

			Return _isEnabled
		End Get
	End Property

	''' <summary>
	''' Reloads the value of <see cref="IsEnabled" /> from the default cache, after the cache has raised an event to notify a change in the cache.
	''' </summary>
	''' <param name="sender">The cache that raised the event.</param>
	''' <param name="e">The event arguments.</param>
	Private Sub Global_CacheChanged(sender As Object, e As Global.System.EventArgs)
		_isEnabled = Global.NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCache.Global.IsDebugModeEnabled(GetType(DebugMode).Assembly)
	End Sub
End Module
