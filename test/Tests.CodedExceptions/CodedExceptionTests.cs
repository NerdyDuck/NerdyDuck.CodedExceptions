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
#pragma warning disable CA1303
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
		/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedException class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class CodedExceptionTests
		{
			#region Constructors
			[TestMethod]
			public void Ctor_Void_Success()
			{
				try
				{
					throw new CodedException();
				}
				catch (CodedException ex)
				{
					Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
					Assert.IsNull(ex.InnerException);
					StringAssert.Contains(ex.Message, typeof(CodedException).FullName);
				}
			}

			[TestMethod]
			public void Ctor_String_Success()
			{
				try
				{
					throw new CodedException(Globals.TestMessage);
				}
				catch (CodedException ex)
				{
					Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
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
						throw new CodedException(Globals.TestMessage, ex);
					}
				}
				catch (CodedException ex)
				{
					Assert.AreEqual(Globals.COR_E_EXCEPTION, ex.HResult);
					Assert.IsNotNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
				}
			}

			[TestMethod]
			public void Ctor_Int32_Success()
			{
				try
				{
					throw new CodedException(Globals.CustomHResult);
				}
				catch (CodedException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNull(ex.InnerException);
					StringAssert.Contains(ex.Message, typeof(CodedException).FullName);
				}
			}

			[TestMethod]
			public void Ctor_IntString_Success()
			{
				try
				{
					throw new CodedException(Globals.CustomHResult, Globals.TestMessage);
				}
				catch (CodedException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
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
						throw new CodedException(Globals.CustomHResult, Globals.TestMessage, ex);
					}
				}
				catch (CodedException ex)
				{
					Assert.AreEqual(Globals.CustomHResult, ex.HResult);
					Assert.IsNotNull(ex.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex.Message);
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
						throw new CodedException(Globals.CustomHResult, Globals.TestMessage, ex);
					}
				}
				catch (CodedException ex)
				{
					using System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
					CodedException ex2 = SerializationHelper.Deserialize<CodedException>(Buffer);

					Assert.AreEqual(Globals.CustomHResult, ex2.HResult);
					Assert.IsNotNull(ex2.InnerException);
					Assert.AreEqual(Globals.TestMessage, ex2.Message);
				}
			}
			#endregion

			#region ToString
			[TestMethod]
			public void ToString_Success()
			{
				try
				{
					throw new CodedException(Globals.CustomHResult, Globals.TestMessage);
				}
				catch (CodedException ex)
				{
					string str = ex.ToString();
					StringAssert.StartsWith(str, string.Format(CultureInfo.InvariantCulture, Globals.DefaultToStringFormat, typeof(CodedException).FullName, Globals.CustomHResultString, Globals.TestMessage));
					StringAssert.Contains(str, nameof(ToString_Success));
				}
			}
			#endregion
		}
	}
}
