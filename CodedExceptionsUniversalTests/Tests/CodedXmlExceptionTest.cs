#region Copyright
/*******************************************************************************
 * <copyright file="CodedXmlExceptionTest.cs" owner="Daniel Kopp">
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
 * <file name="CodedXmlExceptionTest.cs" date="2015-08-17">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.CodedXmlException class.
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
	/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedXmlException class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class CodedXmlExceptionTest
	{
		#region Constructors
		[TestMethod]
		public void Ctor_Void_Success()
		{
			try
			{
				throw new CodedXmlException();
			}
			catch (CodedXmlException ex)
			{
				Assert.AreEqual(Constants.XmlHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(0, ex.LineNumber);
				Assert.AreEqual(0, ex.LinePosition);
			}
		}

		[TestMethod]
		public void Ctor_String_Success()
		{
			try
			{
				throw new CodedXmlException(Constants.TestMessage);
			}
			catch (CodedXmlException ex)
			{
				Assert.AreEqual(Constants.XmlHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(0, ex.LineNumber);
				Assert.AreEqual(0, ex.LinePosition);
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
					throw new CodedXmlException(Constants.TestMessage, ex);
				}
			}
			catch (CodedXmlException ex)
			{
				Assert.AreEqual(Constants.XmlHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(0, ex.LineNumber);
				Assert.AreEqual(0, ex.LinePosition);
			}
		}

		[TestMethod]
		public void Ctor_StringExceptionInt32Int32_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					throw new CodedXmlException(Constants.TestMessage, ex, 2710, 42);
				}
			}
			catch (CodedXmlException ex)
			{
				Assert.AreEqual(Constants.XmlHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				StringAssert.StartsWith(ex.Message, Constants.TestMessage);
				Assert.AreEqual(2710, ex.LineNumber);
				Assert.AreEqual(42, ex.LinePosition);
			}
		}

		[TestMethod]
		public void Ctor_Int32_Success()
		{
			try
			{
				throw new CodedXmlException(Constants.CustomHResult);
			}
			catch (CodedXmlException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(0, ex.LineNumber);
				Assert.AreEqual(0, ex.LinePosition);
			}
		}

		[TestMethod]
		public void Ctor_IntString_Success()
		{
			try
			{
				throw new CodedXmlException(Constants.CustomHResult, Constants.TestMessage);
			}
			catch (CodedXmlException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(0, ex.LineNumber);
				Assert.AreEqual(0, ex.LinePosition);
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
					throw new CodedXmlException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (CodedXmlException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(0, ex.LineNumber);
				Assert.AreEqual(0, ex.LinePosition);
			}
		}

		[TestMethod]
		public void Ctor_IntStringExceptionInt32Int32_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					throw new CodedXmlException(Constants.CustomHResult, Constants.TestMessage, ex, 2710, 42);
				}
			}
			catch (CodedXmlException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				StringAssert.StartsWith(ex.Message, Constants.TestMessage);
				Assert.AreEqual(2710, ex.LineNumber);
				Assert.AreEqual(42, ex.LinePosition);
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
					throw new CodedXmlException(Constants.CustomHResult, Constants.TestMessage, ex, 2710, 42);
				}
			}
			catch (CodedXmlException ex)
			{
				System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
				CodedXmlException ex2 = SerializationHelper.Deserialize<CodedXmlException>(Buffer);

				Assert.AreEqual(Constants.CustomHResult, ex2.HResult);
				Assert.IsNotNull(ex2.InnerException);
				StringAssert.StartsWith(ex2.Message, Constants.TestMessage);
				Assert.AreEqual(2710, ex2.LineNumber);
				Assert.AreEqual(42, ex2.LinePosition);
			}
		}
#endif
		#endregion

		#region ToString
		public void ToString_Success()
		{
			try
			{
				throw new CodedXmlException(Constants.CustomHResult, Constants.TestMessage, null, 2710, 42);
			}
			catch (Exception ex)
			{
				string str = HResultHelper.CreateToString(ex, null);
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedXmlException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "ToString_Success");
				StringAssert.Contains(str, "2710");
				StringAssert.Contains(str, "42");
			}
		}
		#endregion
	}
}
