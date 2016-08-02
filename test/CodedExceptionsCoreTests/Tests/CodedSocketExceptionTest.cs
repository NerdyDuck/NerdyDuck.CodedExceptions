#region Copyright
/*******************************************************************************
 * <copyright file="CodedSocketExceptionTest.cs" owner="Daniel Kopp">
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
 * <file name="CodedSocketExceptionTest.cs" date="2016-08-02">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.IO.CodedSocketException class.
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
using NerdyDuck.CodedExceptions.IO;
using System;
using System.Net.Sockets;

namespace NerdyDuck.Tests.CodedExceptions
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.IO.CodedSocketException class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class CodedSocketExceptionTest
	{
		#region Constructors
		[TestMethod]
		public void Ctor_Void_Success()
		{
			try
			{
				throw new CodedSocketException();
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.E_FAIL, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
			}
		}

		[TestMethod]
		public void Ctor_SocketError_Success()
		{
			try
			{
				throw new CodedSocketException(SocketError.VersionNotSupported);
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.E_FAIL, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
			}
		}

		[TestMethod]
		public void Ctor_String_Success()
		{
			try
			{
				throw new CodedSocketException(Constants.TestMessage);
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.E_FAIL, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
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
					throw new CodedSocketException(Constants.TestMessage, ex);
				}
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.E_FAIL, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
			}
		}

		[TestMethod]
		public void Ctor_SocketErrorString_Success()
		{
			try
			{
				throw new CodedSocketException(SocketError.VersionNotSupported, Constants.TestMessage);
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.E_FAIL, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
			}
		}

		[TestMethod]
		public void Ctor_SocketErrorStringException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					throw new CodedSocketException(SocketError.VersionNotSupported, Constants.TestMessage, ex);
				}
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.E_FAIL, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
			}
		}

		[TestMethod]
		public void Ctor_Int32_Success()
		{
			try
			{
				throw new CodedSocketException(Constants.CustomHResult);
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
			}
		}

		[TestMethod]
		public void Ctor_Int32SocketError_Success()
		{
			try
			{
				throw new CodedSocketException(Constants.CustomHResult, SocketError.VersionNotSupported);
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
			}
		}

		[TestMethod]
		public void Ctor_IntString_Success()
		{
			try
			{
				throw new CodedSocketException(Constants.CustomHResult, Constants.TestMessage);
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
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
					throw new CodedSocketException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
			}
		}

		[TestMethod]
		public void Ctor_IntSocketErrorString_Success()
		{
			try
			{
				throw new CodedSocketException(Constants.CustomHResult, SocketError.VersionNotSupported, Constants.TestMessage);
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
			}
		}

		[TestMethod]
		public void Ctor_IntSocketErrorStringException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					throw new CodedSocketException(Constants.CustomHResult, SocketError.VersionNotSupported, Constants.TestMessage, ex);
				}
			}
			catch (CodedSocketException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
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
					throw new CodedSocketException(Constants.CustomHResult, SocketError.VersionNotSupported, Constants.TestMessage, ex);
				}
			}
			catch (CodedSocketException ex)
			{
				System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
				CodedSocketException ex2 = SerializationHelper.Deserialize<CodedSocketException>(Buffer);

				Assert.AreEqual(Constants.CustomHResult, ex2.HResult);
				Assert.IsNotNull(ex2.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex2.Message);
				Assert.AreEqual(SocketError.VersionNotSupported, ex2.SocketErrorCode);
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
				throw new CodedSocketException(Constants.CustomHResult, SocketError.VersionNotSupported, Constants.TestMessage);
			}
			catch (CodedSocketException ex)
			{
				string str = ex.ToString();
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedSocketException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "ToString_Success");
				StringAssert.Contains(str, "10092");
			}
		}
		#endregion
	}
}
