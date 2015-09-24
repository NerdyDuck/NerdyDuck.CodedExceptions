#region Copyright
/*******************************************************************************
 * <copyright file="LocalizationTest.cs"
 * owner="Daniel Kopp">
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
 * <file name="LocalizationTest.cs" date="2015-08-12">
 * Contains test methods to test the localization.
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
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace NerdyDuck.Tests.CodedExceptions
{
	/// <summary>
	/// Contains test methods to test localization.
	/// </summary>
#if WINDOWS_DESKTOP
	[ExcludeFromCodeCoverage]
#endif
	[TestClass]
	public class LocalizationTest
	{
#if WINDOWS_DESKTOP
		[TestMethod]
		public void TestLocalizationGermanEnglish()
		{
			CultureInfo CurrentCulture = Thread.CurrentThread.CurrentCulture;
			CultureInfo CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
			CultureInfo GermanCulture = new CultureInfo("de-DE");
			CultureInfo EnglishCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentCulture = GermanCulture;
			Thread.CurrentThread.CurrentUICulture = GermanCulture;

			try
			{
				throw new NerdyDuck.CodedExceptions.IO.CodedFileExistsException();
			}
			catch (NerdyDuck.CodedExceptions.IO.CodedFileExistsException ex)
			{
				StringAssert.Contains(ex.Message, "Die angegebene Datei existiert bereits.");
			}

			Thread.CurrentThread.CurrentCulture = EnglishCulture;
			Thread.CurrentThread.CurrentUICulture = EnglishCulture;

			try
			{
				throw new NerdyDuck.CodedExceptions.IO.CodedFileExistsException();
			}
			catch (NerdyDuck.CodedExceptions.IO.CodedFileExistsException ex)
			{
				StringAssert.Contains(ex.Message, "The specified file already exists.");
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = CurrentCulture;
				Thread.CurrentThread.CurrentUICulture = CurrentUICulture;
			}
		}
#endif

#if WINDOWS_UWP && DEBUG
		/// <summary>
		/// Switching language only works in debug.
		/// </summary>
		[TestMethod]
		public void TestLocalizationGermanEnglish()
		{
			string PrimaryOverride = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride;
			CultureInfo CurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
			CultureInfo CurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;

			CultureInfo GermanCulture = new CultureInfo("de-DE");
			CultureInfo EnglishCulture = new CultureInfo("en-US");

			Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = GermanCulture.Name;
			CultureInfo.DefaultThreadCurrentCulture = GermanCulture;
			CultureInfo.DefaultThreadCurrentUICulture = GermanCulture;

			try
			{
				throw new NerdyDuck.CodedExceptions.IO.CodedFileExistsException();
			}
			catch (NerdyDuck.CodedExceptions.IO.CodedFileExistsException ex)
			{
				StringAssert.Contains(ex.Message, "Die angegebene Datei existiert bereits.");
			}

			Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = EnglishCulture.Name;
			CultureInfo.DefaultThreadCurrentCulture = EnglishCulture;
			CultureInfo.DefaultThreadCurrentUICulture = EnglishCulture;

			try
			{
				throw new NerdyDuck.CodedExceptions.IO.CodedFileExistsException();
			}
			catch (NerdyDuck.CodedExceptions.IO.CodedFileExistsException ex)
			{
				StringAssert.Contains(ex.Message, "The specified file already exists.");
			}
			finally
			{
				Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = PrimaryOverride;
				CultureInfo.DefaultThreadCurrentCulture = CurrentCulture;
				CultureInfo.DefaultThreadCurrentUICulture = CurrentUICulture;
			}
		}
#endif
	}
}
