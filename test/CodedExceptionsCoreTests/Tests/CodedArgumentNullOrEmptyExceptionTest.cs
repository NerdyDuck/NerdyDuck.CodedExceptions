#region Copyright
/*******************************************************************************
 * <copyright file="CodedArgumentNullOrEmptyExceptionTest.cs" owner="Daniel Kopp">
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
 * <file name="CodedArgumentNullOrEmptyExceptionTest.cs" date="2015-10-23">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.CodedArgumentNullOrEmptyException class.
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
	/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedArgumentNullOrEmptyException class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class CodedArgumentNullOrEmptyExceptionTest
	{
		#region Constructors
		[TestMethod]
		public void Ctor_Void_Success()
		{
			try
			{
				throw new CodedArgumentNullOrEmptyException();
			}
			catch (CodedArgumentNullOrEmptyException ex)
			{
				Assert.AreEqual(Constants.COR_E_ARGUMENT, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.IsNull(ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_String_Success()
		{
			try
			{
				throw new CodedArgumentNullOrEmptyException(Constants.ParamName);
			}
			catch (CodedArgumentNullOrEmptyException ex)
			{
				Assert.AreEqual(Constants.COR_E_ARGUMENT, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.ParamName, ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_StringException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					throw new CodedArgumentNullOrEmptyException(Constants.TestMessage, ex);
				}
			}
			catch (CodedArgumentNullOrEmptyException ex)
			{
				Assert.AreEqual(Constants.COR_E_ARGUMENT, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.IsNull(ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_StringString_Success()
		{
			try
			{
				throw new CodedArgumentNullOrEmptyException(Constants.ParamName, Constants.TestMessage);
			}
			catch (CodedArgumentNullOrEmptyException ex)
			{
				Assert.AreEqual(Constants.COR_E_ARGUMENT, ex.HResult);
				Assert.IsNull(ex.InnerException);
				StringAssert.StartsWith(ex.Message, Constants.TestMessage);
				Assert.AreEqual(Constants.ParamName, ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_Int32_Success()
		{
			try
			{
				throw new CodedArgumentNullOrEmptyException(Constants.CustomHResult);
			}
			catch (CodedArgumentNullOrEmptyException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.IsNull(ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_IntString_Success()
		{
			try
			{
				throw new CodedArgumentNullOrEmptyException(Constants.CustomHResult, Constants.ParamName);
			}
			catch (CodedArgumentNullOrEmptyException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.ParamName, ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_IntStringException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					throw new CodedArgumentNullOrEmptyException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (CodedArgumentNullOrEmptyException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.IsNull(ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_IntStringString_Success()
		{
			try
			{
				throw new CodedArgumentNullOrEmptyException(Constants.CustomHResult, Constants.ParamName, Constants.TestMessage);
			}
			catch (CodedArgumentNullOrEmptyException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				StringAssert.StartsWith(ex.Message, Constants.TestMessage);
				Assert.AreEqual(Constants.ParamName, ex.ParamName);
			}
		}

#if WINDOWS_DESKTOP
		[TestMethod]
		public void Ctor_SerializationInfo_Success()
		{
			try
			{
				throw new CodedArgumentNullOrEmptyException(Constants.CustomHResult, Constants.ParamName, Constants.TestMessage);
			}
			catch (CodedArgumentNullOrEmptyException ex)
			{
				System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
				CodedArgumentNullOrEmptyException ex2 = SerializationHelper.Deserialize<CodedArgumentNullOrEmptyException>(Buffer);

				Assert.AreEqual(Constants.CustomHResult, ex2.HResult);
				StringAssert.StartsWith(ex2.Message, Constants.TestMessage);
				Assert.AreEqual(Constants.ParamName, ex2.ParamName);
			}
		}
#endif
		#endregion

		#region ToString
		[TestMethod]
		public void ToString_Success()
		{
			try
			{
				throw new CodedArgumentNullOrEmptyException(Constants.CustomHResult, Constants.ParamName, Constants.TestMessage);
			}
			catch (Exception ex)
			{
				string str = ex.ToString();
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedArgumentNullOrEmptyException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "ToString_Success");
				StringAssert.Contains(str, Constants.ParamName);
			}
		}
		#endregion
	}
}
