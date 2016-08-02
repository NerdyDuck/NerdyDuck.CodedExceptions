#region Copyright
/*******************************************************************************
 * <copyright file="CodedArgumentExceptionTest.cs" owner="Daniel Kopp">
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
 * <file name="CodedArgumentExceptionTest.cs" date="2015-08-13">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.CodedArgumentException class.
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
	/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedArgumentException class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class CodedArgumentExceptionTest
	{
		#region Constructors
		[TestMethod]
		public void Ctor_Void_Success()
		{
			try
			{
				throw new CodedArgumentException();
			}
			catch (CodedArgumentException ex)
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
				throw new CodedArgumentException(Constants.TestMessage);
			}
			catch (CodedArgumentException ex)
			{
				Assert.AreEqual(Constants.COR_E_ARGUMENT, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.IsNull(ex.ParamName);
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
					throw new CodedArgumentException(Constants.TestMessage, ex);
				}
			}
			catch (CodedArgumentException ex)
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
				throw new CodedArgumentException(Constants.TestMessage, Constants.ParamName);
			}
			catch (CodedArgumentException ex)
			{
				Assert.AreEqual(Constants.COR_E_ARGUMENT, ex.HResult);
				Assert.IsNull(ex.InnerException);
				StringAssert.StartsWith(ex.Message, Constants.TestMessage);
				Assert.AreEqual(Constants.ParamName, ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_StringStringException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					throw new CodedArgumentException(Constants.TestMessage, Constants.ParamName, ex);
				}
			}
			catch (CodedArgumentException ex)
			{
				Assert.AreEqual(Constants.COR_E_ARGUMENT, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				StringAssert.StartsWith(ex.Message, Constants.TestMessage);
				Assert.AreEqual(Constants.ParamName, ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_Int32_Success()
		{
			try
			{
				throw new CodedArgumentException(Constants.CustomHResult);
			}
			catch (CodedArgumentException ex)
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
				throw new CodedArgumentException(Constants.CustomHResult, Constants.TestMessage);
			}
			catch (CodedArgumentException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.IsNull(ex.ParamName);
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
					throw new CodedArgumentException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (CodedArgumentException ex)
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
				throw new CodedArgumentException(Constants.CustomHResult, Constants.TestMessage, Constants.ParamName);
			}
			catch (CodedArgumentException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				StringAssert.StartsWith(ex.Message, Constants.TestMessage);
				Assert.AreEqual(Constants.ParamName, ex.ParamName);
			}
		}

		[TestMethod]
		public void Ctor_IntStringStringException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					throw new CodedArgumentException(Constants.CustomHResult, Constants.TestMessage, Constants.ParamName, ex);
				}
			}
			catch (CodedArgumentException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
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
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					throw new CodedArgumentException(Constants.CustomHResult, Constants.TestMessage, Constants.ParamName, ex);
				}
			}
			catch (CodedArgumentException ex)
			{
				System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
				CodedArgumentException ex2 = SerializationHelper.Deserialize<CodedArgumentException>(Buffer);

				Assert.AreEqual(Constants.CustomHResult, ex2.HResult);
				Assert.IsNotNull(ex2.InnerException);
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
				throw new CodedArgumentException(Constants.CustomHResult, Constants.TestMessage, Constants.ParamName);
			}
			catch (Exception ex)
			{
				string str = ex.ToString();
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedArgumentException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "ToString_Success");
				StringAssert.Contains(str, Constants.ParamName);
			}
		}
		#endregion
	}
}
