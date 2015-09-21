#Region "Copyright"
'*******************************************************************************
' <copyright file="Module1.vb" owner="Daniel Kopp">
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
' <author name = "Daniel Kopp" email="dak@nerdyduck.de" />
' <assembly name="ExampleCsharpDesktop">
' Example for NerdyDuck.CodedExceptions - Desktop version
' </assembly>
' <file name="Module1.vb" date="2015-09-21">
' Main program module.
' </file>
'******************************************************************************/
#End Region

Imports NerdyDuck.CodedExceptions
''' <summary>
''' Main program module.
''' </summary>
Module Module1
	''' <summary>
	''' Entry point of executable.
	''' </summary>
	Sub Main()
		Dim Hour As Int32 = DateTime.Now.Hour ' Set your own value to trigger a different exception
		Try
			If Hour < 18 Then
				' An exception with custom HRESULT. The facility identifier in AssemblyInfo.cs (0x42) Is combined with the error code (0x11) And the default bits for HRESULTS (0xa0))
				Throw New CodedTimeoutException(Errors.CreateHResult(&H11), "Why are you still working?")
			Else
				' An exception with standard HRESULT.
				Throw New InvalidOperationException("Get to work!")
			End If
		Catch ex As Exception
			If ex.IsCodedException() Then ' Extension Then method For Exception Class, just add NerdyDuck.CodedExceptions To your 'using' directives.
				Console.WriteLine("This exception has a custom HRESULT value.")
				Console.WriteLine("Facility identifier: 0x{0:x}", ex.GetFacilityId())
				Console.WriteLine("Error code:          0x{0:x}", ex.GetErrorId())
			Else
				Console.WriteLine("This is a standard exception.")
			End If
			Console.WriteLine()
			Console.WriteLine("-----------------------------")
			Console.WriteLine()
			Console.WriteLine("ToString() output:")
			Console.WriteLine()

			Console.WriteLine(ex.ToString())
		End Try

		Console.WriteLine()
		Console.WriteLine("Hit enter to return.")
		Console.ReadLine()
	End Sub

End Module
