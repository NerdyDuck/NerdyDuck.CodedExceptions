#region Copyright
/*******************************************************************************
 * <copyright file="CodedExceptionAttributeTest.cs" owner="Daniel Kopp">
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
 * <assembly name="NerdyDuck.Tests.CodedExceptions">
 * Unit tests for NerdyDuck.CodedExceptions assembly.
 * </assembly>
 * <file name="CodedExceptionAttributeTest.cs" date="2015-08-17">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.CodedExceptionAttribute class.
 * </file>
 ******************************************************************************/
#endregion

#if WINDOWS_UWP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif
#if WINDOWS_DESKTOP
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
#endif
using NerdyDuck.CodedExceptions;
using System;

namespace NerdyDuck.Tests.CodedExceptions
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedExceptionAttribute class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class CodedExceptionAttributeTest
	{
		#region Constructor
		[TestMethod]
		public void Ctor_Success()
		{
			CodedExceptionAttribute att = new CodedExceptionAttribute();
		}
		#endregion

		#region IsCodedException
		[TestMethod]
		public void IsCodedException_True()
		{
			Assert.IsTrue(CodedExceptionAttribute.IsCodedException(new CodedException()));
		}

		[TestMethod]
		public void IsCodedException_False()
		{
			Assert.IsFalse(CodedExceptionAttribute.IsCodedException(new Exception()));
		}

		[TestMethod]
		public void IsCodedException_ExceptionNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				CodedExceptionAttribute.IsCodedException(null);
			});
		}
		#endregion
	}
}
