#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.CodedExceptions - Unit tests for the
 * NerdyDuck.CodedExceptions assembly
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
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions;

#pragma warning disable CA1707
namespace NerdyDuck.Tests.CodedExceptions
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
		/// Contains test methods to test the NerdyDuck.CodedExceptions.ExceptionExtensions class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class ExceptionExtensionsTests
		{
			#region GetErrorId
			[TestMethod]
			public void GetErrorId_Success()
			{
				try
				{
					throw new CodedException(Globals.CustomHResult);
				}
				catch (CodedException ex)
				{
					Assert.AreEqual(0x1234, ExceptionExtensions.GetErrorId(ex));
				}
			}

			[TestMethod]
			public void GetErrorId_ExceptionNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
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
					throw new CodedException(Globals.CustomHResult);
				}
				catch (CodedException ex)
				{
					Assert.AreEqual(2047, ExceptionExtensions.GetFacilityId(ex));
				}
			}

			[TestMethod]
			public void GetFacilityId_ExceptionNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
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
					throw new CodedException(Globals.CustomHResult);
				}
				catch (CodedException ex)
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
				catch (NotImplementedException ex)
				{
					Assert.IsFalse(ExceptionExtensions.IsCodedException(ex));
				}
			}

			[TestMethod]
			public void IsCodedException_ExceptionNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
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
					throw new CodedException(Globals.CustomHResult);
				}
				catch (CodedException ex)
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
				catch (NotImplementedException ex)
				{
					Assert.IsFalse(ExceptionExtensions.IsCustomHResult(ex));
				}
			}

			[TestMethod]
			public void IsCustomHResult_ExceptionNull_Throw()
			{
				Assert.ThrowsException<ArgumentNullException>(() =>
				{
					ExceptionExtensions.IsCustomHResult(null);
				});
			}
			#endregion
		}
	}
}
