﻿#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.CodedExceptions.Configuration - Unit tests for the
 * NerdyDuck.CodedExceptions.Configuration assembly
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

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions.Configuration;

#pragma warning disable CA1707
namespace NerdyDuck.Tests.CodedExceptions.Configuration
{
#if NETCORE21
	namespace NetCore21
#elif NETCORE31
	namespace NetCore31
#elif NET48
	namespace Net48
#elif NET50
	namespace Net50
#endif
	{

		/// <summary>
		/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeElement class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class AssemblyDebugModeElementTests
		{
			#region IsReadOnly
			[TestMethod]
			public void IsReadOnly_Success()
			{
				AssemblyDebugModeElement debugModeElement = new AssemblyDebugModeElement();
				Assert.IsFalse(debugModeElement.IsReadOnly);
			}
			#endregion

			#region ToOverride
			[TestMethod]
			public void ToOverride_Success()
			{
				AssemblyDebugModeElement debugModeElement = new AssemblyDebugModeElement
				{
					AssemblyName = Globals.ThisAssemblyNameString
				};

				AssemblyDebugMode debugMode = debugModeElement.ToAssemblyDebugMode();
				Assert.AreEqual(Globals.ThisAssemblyNameString, debugMode.AssemblyName.ToString());
				Assert.IsTrue(debugMode.IsEnabled);
			}

			[TestMethod]
			public void ToOverride_IdentifierInvalid_Throw()
			{
				Assert.ThrowsException<FormatException>(() =>
				{
					AssemblyDebugModeElement debugModeElement = new AssemblyDebugModeElement
					{
						AssemblyName = "TestAssembly, Version=1.0.xxx",
						IsEnabled = true
					};

					debugModeElement.ToAssemblyDebugMode();
				});
			}
			#endregion
		}
	}
}
