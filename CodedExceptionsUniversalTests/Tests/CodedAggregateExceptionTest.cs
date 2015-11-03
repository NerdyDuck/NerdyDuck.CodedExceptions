#region Copyright
/*******************************************************************************
 * <copyright file="CodedAggregateExceptionTest.cs" owner="Daniel Kopp">
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
 * <file name="CodedAggregateExceptionTest.cs" date="2015-08-13">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.CodedAggregateExceptionTest class.
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
using System.Collections.Generic;

namespace NerdyDuck.Tests.CodedExceptions
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.CodedAggregateExceptionTest class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class CodedAggregateExceptionTest
	{
		#region Constructors
		[TestMethod]
		public void Ctor_Void_Success()
		{
			try
			{
				throw new CodedAggregateException();
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
				Assert.AreEqual(0, ex.InnerExceptions.Count);
			}
		}

		[TestMethod]
		public void Ctor_IEnumerableException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					try
					{
						throw new NotSupportedException();
					}
					catch (Exception ex2)
					{
						List<Exception> exs = new List<Exception>() { ex, ex2 };
						throw new CodedAggregateException(exs);
					}
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
				Assert.AreEqual(2, ex.InnerExceptions.Count);
			}
		}

		[TestMethod]
		public void Ctor_ParamsException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					try
					{
						throw new NotSupportedException();
					}
					catch (Exception ex2)
					{
						throw new CodedAggregateException(ex, ex2);
					}
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
				Assert.AreEqual(2, ex.InnerExceptions.Count);
			}
		}

		[TestMethod]
		public void Ctor_String_Success()
		{
			try
			{
				throw new CodedAggregateException(Constants.TestMessage);
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
				Assert.AreEqual(0, ex.InnerExceptions.Count);
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
					throw new CodedAggregateException(Constants.TestMessage, ex);
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
				Assert.AreEqual(1, ex.InnerExceptions.Count);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
			}
		}

		[TestMethod]
		public void Ctor_StringIEnumerableException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					try
					{
						throw new NotSupportedException();
					}
					catch (Exception ex2)
					{
						List<Exception> exs = new List<Exception>() { ex, ex2 };
						throw new CodedAggregateException(Constants.TestMessage, exs);
					}
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
				Assert.AreEqual(2, ex.InnerExceptions.Count);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
			}
		}

		[TestMethod]
		public void Ctor_StringParamsException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					try
					{
						throw new NotSupportedException();
					}
					catch (Exception ex2)
					{
						throw new CodedAggregateException(Constants.TestMessage, ex, ex2);
					}
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.COR_E_EXCEPTION, ex.HResult);
				Assert.AreEqual(2, ex.InnerExceptions.Count);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
			}
		}

		[TestMethod]
		public void Ctor_Int32_Success()
		{
			try
			{
				throw new CodedAggregateException(Constants.CustomHResult);
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.AreEqual(0, ex.InnerExceptions.Count);
			}
		}

		[TestMethod]
		public void Ctor_Int32IEnumerableException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					try
					{
						throw new NotSupportedException();
					}
					catch (Exception ex2)
					{
						List<Exception> exs = new List<Exception>() { ex, ex2 };
						throw new CodedAggregateException(Constants.CustomHResult, exs);
					}
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.AreEqual(2, ex.InnerExceptions.Count);
			}
		}

		[TestMethod]
		public void Ctor_Int32ParamsException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					try
					{
						throw new NotSupportedException();
					}
					catch (Exception ex2)
					{
						throw new CodedAggregateException(Constants.CustomHResult, ex, ex2);
					}
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.AreEqual(2, ex.InnerExceptions.Count);
			}
		}

		[TestMethod]
		public void Ctor_IntString_Success()
		{
			try
			{
				throw new CodedAggregateException(Constants.CustomHResult, Constants.TestMessage);
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.AreEqual(0, ex.InnerExceptions.Count);
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
					throw new CodedAggregateException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.AreEqual(1, ex.InnerExceptions.Count);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
			}
		}

		[TestMethod]
		public void Ctor_Int32StringIEnumerableException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					try
					{
						throw new NotSupportedException();
					}
					catch (Exception ex2)
					{
						List<Exception> exs = new List<Exception>() { ex, ex2 };
						throw new CodedAggregateException(Constants.CustomHResult, Constants.TestMessage, exs);
					}
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.AreEqual(2, ex.InnerExceptions.Count);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
			}
		}

		[TestMethod]
		public void Ctor_Int32StringParamsException_Success()
		{
			try
			{
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					try
					{
						throw new NotSupportedException();
					}
					catch (Exception ex2)
					{
						throw new CodedAggregateException(Constants.CustomHResult, Constants.TestMessage, ex, ex2);
					}
				}
			}
			catch (CodedAggregateException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.AreEqual(2, ex.InnerExceptions.Count);
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
					throw new CodedAggregateException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (CodedAggregateException ex)
			{
				System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
				CodedAggregateException ex2 = SerializationHelper.Deserialize<CodedAggregateException>(Buffer);

				Assert.AreEqual(Constants.CustomHResult, ex2.HResult);
				Assert.AreEqual(1, ex2.InnerExceptions.Count);
				Assert.AreEqual(Constants.TestMessage, ex2.Message);
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
				try
				{
					throw new FormatException();
				}
				catch (Exception ex)
				{
					try
					{
						throw new NotSupportedException();
					}
					catch (Exception ex2)
					{
						throw new CodedAggregateException(Constants.CustomHResult, Constants.TestMessage, ex, ex2);
					}
				}
			}
			catch (Exception ex)
			{
				string str = ex.ToString();
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedAggregateException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "ToString_Success");
				StringAssert.Contains(str, typeof(FormatException).FullName);
				StringAssert.Contains(str, typeof(NotSupportedException).FullName);
			}
		}
		#endregion
	}
}
