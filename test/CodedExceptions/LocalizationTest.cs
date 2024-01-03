// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// Contains test methods to test localization.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class LocalizationTest
{
	[TestMethod]
	public void Global_LocalizationGermanEnglish_Success()
	{
		CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
		CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
		CultureInfo germanCulture = new("de-DE");
		CultureInfo englishCulture = new("en-US");
		CultureInfo frenchCulture = new("fr-FR");
		Thread.CurrentThread.CurrentCulture = germanCulture;
		Thread.CurrentThread.CurrentUICulture = germanCulture;

		try
		{
			throw new NerdyDuck.CodedExceptions.IO.CodedFileExistsException();
		}
		catch (NerdyDuck.CodedExceptions.IO.CodedFileExistsException ex)
		{
			StringAssert.Contains(ex.Message, "Die angegebene Datei existiert bereits.");
		}
		finally
		{
			Thread.CurrentThread.CurrentCulture = currentCulture;
			Thread.CurrentThread.CurrentUICulture = currentUICulture;
		}

		Thread.CurrentThread.CurrentCulture = englishCulture;
		Thread.CurrentThread.CurrentUICulture = englishCulture;

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
			Thread.CurrentThread.CurrentCulture = currentCulture;
			Thread.CurrentThread.CurrentUICulture = currentUICulture;
		}

		Thread.CurrentThread.CurrentCulture = frenchCulture;
		Thread.CurrentThread.CurrentUICulture = frenchCulture;

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
			Thread.CurrentThread.CurrentCulture = currentCulture;
			Thread.CurrentThread.CurrentUICulture = currentUICulture;
		}
	}
}
