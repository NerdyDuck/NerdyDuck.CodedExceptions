#region Copyright
/*******************************************************************************
 * <copyright file="CodedSerializationExceptionTest.cs" owner="Daniel Kopp">
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
 * <file name="CodedSerializationExceptionTest.cs" date="2015-08-12">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.CodedSerializationException class.
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
	/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedSerializationException class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class CodedSerializationExceptionTest
	{
		#region Constructors
		[TestMethod]
		public void Ctor_Void_Success()
		{
			try
			{
				throw new CodedSerializationException();
			}
			catch (CodedSerializationException ex)
			{
#if WINDOWS_UWP
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
#endif
#if WINDOWS_DESKTOP
				Assert.AreEqual(Constants.COR_E_SERIALIZATION, ex.HResult);
#endif
				Assert.IsNull(ex.InnerException);
			}
		}

		[TestMethod]
		public void Ctor_String_Success()
		{
			try
			{
				throw new CodedSerializationException(Constants.TestMessage);
			}
			catch (CodedSerializationException ex)
			{
#if WINDOWS_UWP
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
#endif
#if WINDOWS_DESKTOP
				Assert.AreEqual(Constants.COR_E_SERIALIZATION, ex.HResult);
#endif
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
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
					throw new CodedSerializationException(Constants.TestMessage, ex);
				}
			}
			catch (CodedSerializationException ex)
			{
#if WINDOWS_UWP
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
#endif
#if WINDOWS_DESKTOP
				Assert.AreEqual(Constants.COR_E_SERIALIZATION, ex.HResult);
#endif
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
			}
		}

		[TestMethod]
		public void Ctor_Int32_Success()
		{
			try
			{
				throw new CodedSerializationException(Constants.CustomHResult);
			}
			catch (CodedSerializationException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
			}
		}

		[TestMethod]
		public void Ctor_IntString_Success()
		{
			try
			{
				throw new CodedSerializationException(Constants.CustomHResult, Constants.TestMessage);
			}
			catch (CodedSerializationException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
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
					throw new CodedSerializationException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (CodedSerializationException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
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
					throw new CodedSerializationException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (CodedSerializationException ex)
			{
				System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
				CodedSerializationException ex2 = SerializationHelper.Deserialize<CodedSerializationException>(Buffer);

				Assert.AreEqual(Constants.CustomHResult, ex2.HResult);
				Assert.IsNotNull(ex2.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex2.Message);
			}
		}
#endif
#endregion

#region ToString
		public void ToString_Success()
		{
			try
			{
				throw new CodedSerializationException(Constants.CustomHResult, Constants.TestMessage);
			}
			catch (Exception ex)
			{
				string str = HResultHelper.CreateToString(ex, null);
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedSerializationException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "ToString_Success");
			}
		}
#endregion
	}
}
