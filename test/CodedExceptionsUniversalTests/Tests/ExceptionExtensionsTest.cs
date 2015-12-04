#region Copyright
/*******************************************************************************
 * <copyright file="ExceptionExtensionsTest.cs" owner="Daniel Kopp">
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
 * <file name="ExceptionExtensionsTest.cs" date="2015-08-12">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.ExceptionExtensions class.
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
	/// Contains test methods to test the NerdyDuck.CodedExceptions.ExceptionExtensions class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class ExceptionExtensionsTest
	{

		#region GetErrorId
		[TestMethod]
		public void GetErrorId_Success()
		{
			try
			{
				throw new CodedException(Constants.CustomHResult);
			}
			catch (Exception ex)
			{
				Assert.AreEqual(0x1234, ExceptionExtensions.GetErrorId(ex));
			}
		}

		[TestMethod]
		public void GetErrorId_ExceptionNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				ExceptionExtensions.GetErrorId(null);
			});
		}
		#endregion

		#region GetFacilityId
		[TestMethod]
		public void GetFacilityId_Success()
		{
			try
			{
				throw new CodedException(Constants.CustomHResult);
			}
			catch (Exception ex)
			{
				Assert.AreEqual(2047, ExceptionExtensions.GetFacilityId(ex));
			}
		}

		[TestMethod]
		public void GetFacilityId_ExceptionNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				ExceptionExtensions.GetFacilityId(null);
			});
		}
		#endregion

		#region IsCodedException
		[TestMethod]
		public void IsCodedException_True()
		{
			try
			{
				throw new CodedException(Constants.CustomHResult);
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ExceptionExtensions.IsCodedException(ex));
			}
		}

		[TestMethod]
		public void IsCodedException_False()
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Assert.IsFalse(ExceptionExtensions.IsCodedException(ex));
			}
		}

		[TestMethod]
		public void IsCodedException_ExceptionNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				ExceptionExtensions.IsCodedException(null);
			});
		}
		#endregion

		#region IsCustomHResult
		[TestMethod]
		public void IsCustomHResult_True()
		{
			try
			{
				throw new CodedException(Constants.CustomHResult);
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ExceptionExtensions.IsCustomHResult(ex));
			}
		}

		[TestMethod]
		public void IsCustomHResult_False()
		{
			try
			{
				throw new NotImplementedException();
			}
			catch (Exception ex)
			{
				Assert.IsFalse(ExceptionExtensions.IsCustomHResult(ex));
			}
		}

		[TestMethod]
		public void IsCustomHResult_ExceptionNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				ExceptionExtensions.IsCustomHResult(null);
			});
		}
		#endregion
	}
}
