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
		/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedProtocolViolationException class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class CodedProtocolViolationExceptionTests
		{
			[TestMethod]
			public void Ctor_Void_Success()
			{
				try
				{
					throw new CodedProtocolViolationException();
				}
				catch (CodedProtocolViolationException ex)
				{
					Assert.AreEqual(Globals.COR_E_INVALIDOPERATION, ex.HResult);
					Assert.IsNull(ex.InnerException);
				}
			}

			[TestMethod]
			public void Ctor_String_Success()
			{
				try
				{
					throw new CodedProtocolViolationException(Globals.TestMessage);
				}
				catch (CodedProtocolViolationException ex)
				{
					Assert.AreEqual(Globals.COR_E_INVALIDOPERATION, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
				}
			}

			[TestMethod]
			public void Ctor_Int32_Success()
			{
				try
				{
					throw new CodedProtocolViolationException(Globals.CustomHResult);
				}
				catch (CodedProtocolViolationException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNull(ex.InnerException);
				}
			}

			[TestMethod]
			public void Ctor_IntString_Success()
			{
				try
				{
					throw new CodedProtocolViolationException(Globals.CustomHResult, Globals.TestMessage);
				}
				catch (CodedProtocolViolationException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
				}
			}

			[TestMethod]
			public void Ctor_SerializationInfo_Success()
			{
				try
				{
					throw new CodedProtocolViolationException(Globals.CustomHResult, Globals.TestMessage);
				}
				catch (CodedProtocolViolationException ex)
				{
					using System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
					CodedProtocolViolationException ex2 = SerializationHelper.Deserialize<CodedProtocolViolationException>(Buffer);

					Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
					Assert.IsNull(ex2.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex2.Message);
				}
			}

			[TestMethod]
			public void ToString_Success()
			{
				try
				{
					throw new CodedProtocolViolationException(Globals.CustomHResult, Globals.TestMessage);
				}
				catch (CodedProtocolViolationException ex)
				{
					string str = ex.ToString();
					StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedProtocolViolationException).FullName, Globals.CustomHResultString, Globals.TestMessage));
					StringAssert.Contains(str, nameof(ToString_Success));
				}
			}
		}
	}
}
