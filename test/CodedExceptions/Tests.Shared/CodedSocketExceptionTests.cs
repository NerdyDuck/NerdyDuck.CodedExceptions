﻿#region Copyright
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
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions;

namespace NerdyDuck.Tests.CodedExceptions
{
#if NET60
	namespace Net60
#elif NET50
	namespace Net50
#elif NETCORE31
	namespace NetCore31
#elif NET48
	namespace Net48
#endif
	{

		/// <summary>
		/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedSocketException class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class CodedSocketExceptionTests
		{
			[TestMethod]
			public void Ctor_Void_Success()
			{
				try
				{
					throw new CodedSocketException();
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.E_FAIL, ex.HResult);
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
					Assert.AreEqual(Globals.E_FAIL, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
				}
			}

			[TestMethod]
			public void Ctor_String_Success()
			{
				try
				{
					throw new CodedSocketException(Globals.TestMessage);
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.E_FAIL, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
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
						throw new CodedSocketException(Globals.TestMessage, ex);
					}
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.E_FAIL, ex.HResult);
					Assert.IsNotNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
					Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
				}
			}

			[TestMethod]
			public void Ctor_SocketErrorString_Success()
			{
				try
				{
					throw new CodedSocketException(SocketError.VersionNotSupported, Globals.TestMessage);
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.E_FAIL, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
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
						throw new CodedSocketException(SocketError.VersionNotSupported, Globals.TestMessage, ex);
					}
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.E_FAIL, ex.HResult);
					Assert.IsNotNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
					Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
				}
			}

			[TestMethod]
			public void Ctor_Int32_Success()
			{
				try
				{
					throw new CodedSocketException(Globals.CustomHResult);
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
				}
			}

			[TestMethod]
			public void Ctor_Int32SocketError_Success()
			{
				try
				{
					throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported);
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
				}
			}

			[TestMethod]
			public void Ctor_IntString_Success()
			{
				try
				{
					throw new CodedSocketException(Globals.CustomHResult, Globals.TestMessage);
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
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
						throw new CodedSocketException(Globals.CustomHResult, Globals.TestMessage, ex);
					}
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNotNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
					Assert.AreEqual(SocketError.SocketError, ex.SocketErrorCode);
				}
			}

			[TestMethod]
			public void Ctor_IntSocketErrorString_Success()
			{
				try
				{
					throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported, Globals.TestMessage);
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
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
						throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported, Globals.TestMessage, ex);
					}
				}
				catch (CodedSocketException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNotNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
					Assert.AreEqual(SocketError.VersionNotSupported, ex.SocketErrorCode);
				}
			}

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
						throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported, Globals.TestMessage, ex);
					}
				}
				catch (CodedSocketException ex)
				{
					using System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
					CodedSocketException ex2 = SerializationHelper.Deserialize<CodedSocketException>(Buffer);

					Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
					Assert.IsNotNull(ex2.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex2.Message);
					Assert.AreEqual(SocketError.VersionNotSupported, ex2.SocketErrorCode);
				}
			}

			[TestMethod]
			public void ToString_Success()
			{
				try
				{
					throw new CodedSocketException(Globals.CustomHResult, SocketError.VersionNotSupported, Globals.TestMessage);
				}
				catch (CodedSocketException ex)
				{
					string str = ex.ToString();
					StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedSocketException).FullName, Globals.CustomHResultString, Globals.TestMessage));
					StringAssert.Contains(str, nameof(ToString_Success));
					StringAssert.Contains(str, ((int)SocketError.VersionNotSupported).ToString(CultureInfo.InvariantCulture));
				}
			}
		}
	}
}