#region Copyright
/*******************************************************************************
 * <copyright file="HResultHelperTest.cs" owner="Daniel Kopp">
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
 * <file name="HResultHelperTest.cs" date="2015-08-12">
 * Contains test methods to test the NerdyDuck.CodedExceptions.HResultHelper
 * class.
 * </file>
 ******************************************************************************/
#endregion

#if WINDOWS_UWP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif
#if WINDOWS_DESKTOP || NETCORE
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
#endif
using NerdyDuck.CodedExceptions;
using System;

namespace NerdyDuck.Tests.CodedExceptions
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.HResultHelper class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class HResultHelperTest
	{

		#region GetFacilityId
		[TestMethod]
		public void GetFacilityId_Success()
		{
			int i = HResultHelper.GetFacilityId(Constants.CustomHResult);
			Assert.AreEqual(2047, i);
		}
		#endregion

		#region GetErrorId
		[TestMethod]
		public void GetErrorId_Success()
		{
			int i = HResultHelper.GetErrorId(Constants.CustomHResult);
			Assert.AreEqual(0x1234, i);
		}
		#endregion

		#region GetBaseHResult
		[TestMethod]
		public void GetBaseHResult_Success()
		{
			int i = HResultHelper.GetBaseHResult(0x7ff);
			Assert.AreEqual(unchecked((int)0xa7ff0000), i);
		}
		#endregion

		#region IsCustomHResult
		[TestMethod]
		public void IsCustomHResult_True()
		{
			Assert.IsTrue(HResultHelper.IsCustomHResult(Constants.CustomHResult));
		}

		[TestMethod]
		public void IsCustomHResult_False()
		{
			Assert.IsFalse(HResultHelper.IsCustomHResult(Constants.MicrosoftHResult));
		}
		#endregion

		#region CreateToString
		[TestMethod]
		public void CreateToString_SimpleException_Success()
		{
			try
			{
				throw new CodedException(Constants.CustomHResult, Constants.TestMessage);
			}
			catch (Exception ex)
			{
				string str = HResultHelper.CreateToString(ex, null);
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "CreateToString_SimpleException_Success");
			}
		}

		[TestMethod]
		public void CreateToString_ExtendedMessage_Success()
		{
			try
			{
				throw new CodedException(Constants.CustomHResult, Constants.TestMessage);
			}
			catch (Exception ex)
			{
				string str = HResultHelper.CreateToString(ex, "[ExtendedMessage]");
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "CreateToString_ExtendedMessage_Success");
				StringAssert.Contains(str, Environment.NewLine + "[ExtendedMessage]");
			}
		}

		public void CreateToString_InnerException_Success()
		{
			try
			{
				try
				{
					throw new InvalidOperationException();
				}
				catch (Exception ex)
				{
					throw new CodedException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (Exception ex)
			{
				string str = HResultHelper.CreateToString(ex, null);
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "CreateToString_InnerException_Success");
				StringAssert.Contains(str, "System.InvalidOperationException");
			}
		}

		[TestMethod]
		public void CreateToString_ExceptionNull_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				string str = HResultHelper.CreateToString(null, null);
			});
		}
		#endregion

		#region GetEnumInt32Value
		[TestMethod]
		public void GetEnumInt32Value_Success()
		{
			int i = HResultHelper.GetEnumInt32Value(Int32Enum.One);
			Assert.AreEqual(1, i);
		}

		[TestMethod]
		public void GetEnumInt32Value_NotInt32_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentException>(() =>
			{
				int i = HResultHelper.GetEnumInt32Value(ByteEnum.One);
			});
		}
		#endregion

		#region GetEnumUnderlyingType
		[TestMethod]
		public void GetEnumUnderlyingType_Success()
		{
			Type t = HResultHelper.GetEnumUnderlyingType(typeof(Int32Enum));
			Assert.AreEqual(typeof(int), t);
		}

		[TestMethod]
		public void GetEnumUnderlyingType_Null_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentNullException>(() =>
			{
				Type t = HResultHelper.GetEnumUnderlyingType(null);
			});
		}

		[TestMethod]
		public void GetEnumUnderlyingType_NotEnum_Throw()
		{
			CustomAssert.ThrowsException<CodedArgumentException>(() =>
			{
				Type t = HResultHelper.GetEnumUnderlyingType(typeof(System.EventArgs));
			});
		}
		#endregion
	}
}
