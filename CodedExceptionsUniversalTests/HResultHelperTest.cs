using NerdyDuck.CodedExceptions;
using System;
#if WINDOWS_UWP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#endif
#if WINDOWS_DESKTOP
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace NerdyDuck.Tests.CodedExceptions
{
	[TestClass]
	public class HResultHelperTest
	{
		private static int CustomHResult = unchecked((int)0xa7ff1234);
		private static int MicrosoftHResult = unchecked((int)0x87ff1234);

		[TestMethod]
		public void GetFacilityId_Success()
		{
			int i = HResultHelper.GetFacilityId(CustomHResult);
			Assert.AreEqual<int>(2047, i);
		}

		[TestMethod]
		public void GetErrorId_Success()
		{
			int i = HResultHelper.GetErrorId(CustomHResult);
			Assert.AreEqual<int>(0x1234, i);
		}

		[TestMethod]
		public void GetBaseHResult_Success()
		{
			int i = HResultHelper.GetBaseHResult(0x7ff);
			Assert.AreEqual<int>(unchecked((int)0xa7ff0000), i);
		}

		[TestMethod]
		public void IsCustomHResult_True()
		{
			Assert.IsTrue(HResultHelper.IsCustomHResult(CustomHResult));
		}

		[TestMethod]
		public void IsCustomHResult_False()
		{
			Assert.IsFalse(HResultHelper.IsCustomHResult(MicrosoftHResult));
		}

		[TestMethod]
		public void CreateToString_Default()
		{
			try
			{
				throw new CodedException(0x12345678, "[TestMessage]");
			}
			catch (Exception ex)
			{
				string str = HResultHelper.CreateToString(ex, null);
				StringAssert.StartsWith(str, "NerdyDuck.CodedExceptions.CodedException: (0x12345678) [TestMessage]");
				StringAssert.Contains(str, "CreateToString_Default");
			}
		}

		[TestMethod]
		public void CreateToString_Extended()
		{
			try
			{
				throw new CodedException(0x12345678, "[TestMessage]");
			}
			catch (Exception ex)
			{
				string str = HResultHelper.CreateToString(ex, "[ExtendedMessage]");
				StringAssert.StartsWith(str, "NerdyDuck.CodedExceptions.CodedException: (0x12345678) [TestMessage]");
				StringAssert.Contains(str, "CreateToString_Extended");
				StringAssert.Contains(str, Environment.NewLine + "[ExtendedMessage]");
			}
		}

		public void CreateToString_InnerException()
		{
			try
			{
				try
				{
					throw new InvalidOperationException();
				}
				catch (Exception ex)
				{
					throw new CodedException(0x12345678, "[TestMessage]", ex);
				}
			}
			catch (Exception ex)
			{
				string str = HResultHelper.CreateToString(ex, null);
				StringAssert.StartsWith(str, "NerdyDuck.CodedExceptions.CodedException: (0x12345678) [TestMessage]");
				StringAssert.Contains(str, "CreateToString_InnerException");
				StringAssert.Contains(str, "System.InvalidOperationException");
			}
		}

#if WINDOWS_DESKTOP
		[TestMethod]
		[ExpectedException(typeof(CodedArgumentNullException))]
		public void CreateToString_ExNull()
		{
			string str = HResultHelper.CreateToString(null, null);
		}
#endif

#if WINDOWS_UWP
		[TestMethod]
		public void CreateToString_ExNull()
		{
			Assert.ThrowsException<CodedArgumentNullException>(() =>
			{
				string str = HResultHelper.CreateToString(null, null);
			});
		}
#endif
	}
}
