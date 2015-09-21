#region Copyright
/*******************************************************************************
 * <copyright file="Program.cs" owner="Daniel Kopp">
 * Copyright 2015 Daniel Kopp
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
 * <assembly name="ExampleCsharpDesktop">
 * Example for NerdyDuck.CodedExceptions - Desktop version
 * </assembly>
 * <file name="Program.cs" date="2015-09-21">
 * Main program class.
 * </file>
 ******************************************************************************/
#endregion

using NerdyDuck.CodedExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCsharpDesktop
{
	/// <summary>
	/// Main program class.
	/// </summary>
	class Program
	{
		/// <summary>
		/// Entry point of executable.
		/// </summary>
		/// <param name="args">The command line arguments.</param>
		static void Main(string[] args)
		{
			int hour = DateTime.Now.Hour; // Set your own value to trigger a different exception
			try
			{
				if (hour > 18)
				{
					// An exception with custom HRESULT. The facility identifier in AssemblyInfo.cs (0x42) is combined with the error code (0x11) and the default bits for HRESULTS (0xa0))
					throw new CodedTimeoutException(Errors.CreateHResult(0x11), "Why are you still working?");
				}
				else
				{
					// An exception with standard HRESULT.
					throw new InvalidOperationException("Get to work!");
				}
			}
			catch (Exception ex)
			{
				if (ex.IsCodedException()) // Extension method for Exception class, just add NerdyDuck.CodedExceptions to your 'using' directives.
				{
					Console.WriteLine("This exception has a custom HRESULT value.");
					Console.WriteLine("Facility identifier: 0x{0:x}", ex.GetFacilityId());
					Console.WriteLine("Error code:          0x{0:x}", ex.GetErrorId());
				}
				else
				{
					Console.WriteLine("This is a standard exception.");
				}
				Console.WriteLine("\r\n-----------------------------\r\n");
				Console.WriteLine("ToString() output:\r\n");

				Console.WriteLine(ex.ToString());
			}

			Console.WriteLine("\r\nHit enter to return.");
			Console.ReadLine();
		}
	}
}
