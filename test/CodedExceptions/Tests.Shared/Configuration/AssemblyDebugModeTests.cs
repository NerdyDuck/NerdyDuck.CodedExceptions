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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions.Configuration
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
		/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyDebugMode class.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class AssemblyDebugModeTests
		{
			private static readonly AssemblyIdentity s_thisAssemblyIdentity = new(Globals.ThisAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);
			private static readonly AssemblyIdentity s_otherAssemblyIdentity = new(Globals.OtherAssembly, AssemblyIdentity.AssemblyNameElements.NoVersion);

			[TestMethod]
			public void Ctor_AssemblyIdentityBool_Success()
			{
				AssemblyDebugMode assemblyDebugMode = new(s_thisAssemblyIdentity, true);
				Assert.AreEqual(s_thisAssemblyIdentity, assemblyDebugMode.AssemblyName);
				Assert.IsTrue(assemblyDebugMode.IsEnabled);

				assemblyDebugMode = new AssemblyDebugMode(s_thisAssemblyIdentity, false);
				Assert.AreEqual(s_thisAssemblyIdentity, assemblyDebugMode.AssemblyName);
				Assert.IsFalse(assemblyDebugMode.IsEnabled);
			}

			[TestMethod]
			public void Ctor_AssemblyIdentityNullBool_Throw()
			{
				_ = Assert.ThrowsException<ArgumentNullException>(() => new AssemblyDebugMode((AssemblyIdentity)null, true));
			}

			[TestMethod]
			public void Ctor_StringBool_Success()
			{
				AssemblyDebugMode assemblyDebugMode = new(s_thisAssemblyIdentity.ToString(), true);
				Assert.AreEqual(s_thisAssemblyIdentity, assemblyDebugMode.AssemblyName);
				Assert.IsTrue(assemblyDebugMode.IsEnabled);
			}

			[TestMethod]
			public void Ctor_SerializationInfo_Success()
			{
				AssemblyDebugMode assemblyDebugMode1 = new(s_thisAssemblyIdentity, true);
				using System.IO.MemoryStream Buffer = SerializationHelper.Serialize(assemblyDebugMode1);
				AssemblyDebugMode assemblyDebugMode2 = SerializationHelper.Deserialize<AssemblyDebugMode>(Buffer);

				Assert.AreEqual(s_thisAssemblyIdentity, assemblyDebugMode2.AssemblyName);
				Assert.IsTrue(assemblyDebugMode2.IsEnabled);
			}

			[TestMethod]
			public void Ctor_SerializationInfoNull_Throw()
			{
				_ = Assert.ThrowsException<ArgumentNullException>(() => SerializationHelper.InvokeSerializationConstructorWithNullContext(typeof(AssemblyDebugMode)));
			}

			[TestMethod]
			public void GetHashCode_Success()
			{
				AssemblyDebugMode assemblyDebugMode = new(s_thisAssemblyIdentity, true);
				int i = assemblyDebugMode.GetHashCode();
				Assert.AreNotEqual(0, i);
			}

			[TestMethod]
			public void Equals_Various_Success()
			{
				AssemblyDebugMode assemblyDebugMode1a = new(s_thisAssemblyIdentity, true);
				AssemblyDebugMode assemblyDebugMode1b = new(s_thisAssemblyIdentity, true);
				AssemblyDebugMode assemblyDebugMode1c = new(s_thisAssemblyIdentity, false);
				AssemblyDebugMode assemblyDebugMode2 = new(s_otherAssemblyIdentity, true);


				Assert.IsFalse(assemblyDebugMode1a.Equals((AssemblyDebugMode)null), "1a=null");
				Assert.IsFalse(assemblyDebugMode1a.Equals((object)null), "1a=objnull");
				Assert.IsFalse(assemblyDebugMode1a.Equals(new object()), "1a=obj");
				Assert.IsTrue(assemblyDebugMode1a.Equals((object)assemblyDebugMode1a), "1a=obj1a");

				Assert.IsTrue(assemblyDebugMode1a.Equals(assemblyDebugMode1a), "1a=1a");
				Assert.IsTrue(assemblyDebugMode1a.Equals(assemblyDebugMode1b), "1a=1b");
				Assert.IsFalse(assemblyDebugMode1a.Equals(assemblyDebugMode1c), "1a!=1c");
				Assert.IsFalse(assemblyDebugMode1a.Equals(assemblyDebugMode2), "1a!=2");
			}

			[TestMethod]
			public void GetObjectData_SerializationInfoNull_Throw()
			{
				_ = Assert.ThrowsException<ArgumentNullException>(() =>
				  {
					  System.Runtime.Serialization.ISerializable assemblyDebugMode = new AssemblyDebugMode(s_thisAssemblyIdentity, true);
					  assemblyDebugMode.GetObjectData(null, new System.Runtime.Serialization.StreamingContext());
				  });
			}
		}
	}
}
