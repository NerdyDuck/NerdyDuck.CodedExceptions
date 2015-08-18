﻿#region Copyright
/*******************************************************************************
 * <copyright file="CodedDirectoryNotFoundExceptionTest.cs" owner="Daniel Kopp">
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
 * <file name="CodedDirectoryNotFoundExceptionTest.cs" date="2015-08-18">
 * Contains test methods to test the
 * NerdyDuck.CodedExceptions.IO.CodedDirectoryNotFoundException class.
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
using NerdyDuck.CodedExceptions.IO;
using System;

namespace NerdyDuck.Tests.CodedExceptions.IO
{
	/// <summary>
	/// Contains test methods to test the NerdyDuck.CodedExceptions.IO.CodedDirectoryNotFoundException class.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class CodedDirectoryNotFoundExceptionTest
	{
		#region Constructors
		[TestMethod]
		public void Ctor_Void_Success()
		{
			try
			{
				throw new CodedDirectoryNotFoundException();
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.COR_E_DIRECTORYNOTFOUND, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.IsNull(ex.DirectoryName);
			}
		}

		[TestMethod]
		public void Ctor_String_Success()
		{
			try
			{
				throw new CodedDirectoryNotFoundException(Constants.TestMessage);
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.COR_E_DIRECTORYNOTFOUND, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.IsNull(ex.DirectoryName);
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
					throw new CodedDirectoryNotFoundException(Constants.TestMessage, ex);
				}
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.COR_E_DIRECTORYNOTFOUND, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.IsNull(ex.DirectoryName);
			}
		}

		[TestMethod]
		public void Ctor_StringString_Success()
		{
			try
			{
				throw new CodedDirectoryNotFoundException(Constants.TestMessage, Constants.DirectoryName);
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.COR_E_DIRECTORYNOTFOUND, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(Constants.DirectoryName, ex.DirectoryName);
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
					throw new CodedDirectoryNotFoundException(Constants.TestMessage, Constants.DirectoryName, ex);
				}
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.COR_E_DIRECTORYNOTFOUND, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(Constants.DirectoryName, ex.DirectoryName);
			}
		}

		[TestMethod]
		public void Ctor_Int32_Success()
		{
			try
			{
				throw new CodedDirectoryNotFoundException(Constants.CustomHResult);
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.IsNull(ex.DirectoryName);
			}
		}

		[TestMethod]
		public void Ctor_IntString_Success()
		{
			try
			{
				throw new CodedDirectoryNotFoundException(Constants.CustomHResult, Constants.TestMessage);
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.IsNull(ex.DirectoryName);
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
					throw new CodedDirectoryNotFoundException(Constants.CustomHResult, Constants.TestMessage, ex);
				}
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.IsNull(ex.DirectoryName);
			}
		}

		[TestMethod]
		public void Ctor_IntStringString_Success()
		{
			try
			{
				throw new CodedDirectoryNotFoundException(Constants.CustomHResult, Constants.TestMessage, Constants.DirectoryName);
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(Constants.DirectoryName, ex.DirectoryName);
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
					throw new CodedDirectoryNotFoundException(Constants.CustomHResult, Constants.TestMessage, Constants.DirectoryName, ex);
				}
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				Assert.AreEqual(Constants.CustomHResult, ex.HResult);
				Assert.IsNotNull(ex.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex.Message);
				Assert.AreEqual(Constants.DirectoryName, ex.DirectoryName);
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
					throw new CodedDirectoryNotFoundException(Constants.CustomHResult, Constants.TestMessage, Constants.DirectoryName, ex);
				}
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				System.IO.MemoryStream Buffer = SerializationHelper.Serialize(ex);
				CodedDirectoryNotFoundException ex2 = SerializationHelper.Deserialize<CodedDirectoryNotFoundException>(Buffer);

				Assert.AreEqual(Constants.CustomHResult, ex2.HResult);
				Assert.IsNotNull(ex2.InnerException);
				Assert.AreEqual(Constants.TestMessage, ex2.Message);
				Assert.AreEqual(Constants.DirectoryName, ex2.DirectoryName);
			}
		}
#endif
		#endregion

		#region ToString
		public void ToString_Success()
		{
			try
			{
				throw new CodedDirectoryNotFoundException(Constants.CustomHResult, Constants.TestMessage, Constants.DirectoryName);
			}
			catch (CodedDirectoryNotFoundException ex)
			{
				string str = HResultHelper.CreateToString(ex, null);
				StringAssert.StartsWith(str, string.Format("{0}: ({1}) {2}", typeof(CodedDirectoryNotFoundException).FullName, Constants.CustomHResultString, Constants.TestMessage));
				StringAssert.Contains(str, "ToString_Success");
				StringAssert.Contains(str, ex.DirectoryName);
			}
		}
		#endregion
	}
}
