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

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		/// Contains test methods to test localization.
		/// </summary>
		[ExcludeFromCodeCoverage]
		[TestClass]
		public class LocalizationTest
		{
			[TestMethod]
			public void Global_LocalizationGermanEnglish_Success()
			{
				CultureInfo CurrentCulture = Thread.CurrentThread.CurrentCulture;
				CultureInfo CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
				CultureInfo GermanCulture = new("de-DE");
				CultureInfo EnglishCulture = new("en-US");
				CultureInfo FrenchCulture = new("fr-FR");
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
				finally
				{
					Thread.CurrentThread.CurrentCulture = CurrentCulture;
					Thread.CurrentThread.CurrentUICulture = CurrentUICulture;
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

				Thread.CurrentThread.CurrentCulture = FrenchCulture;
				Thread.CurrentThread.CurrentUICulture = FrenchCulture;

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
		}
	}
}
