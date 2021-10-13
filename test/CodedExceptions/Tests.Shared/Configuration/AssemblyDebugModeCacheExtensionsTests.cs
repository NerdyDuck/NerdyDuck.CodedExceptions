#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.CodedExceptions.Configuration - Unit tests for the
 * NerdyDuck.CodedExceptions.Configuration assembly
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

using System.IO;
using System.Xml;
using NerdyDuck.CodedExceptions.Configuration;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains test methods to test the NerdyDuck.CodedExceptions.Configuration.AssemblyDebugModeCacheExtensions class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestClass]
public class AssemblyDebugModeCacheExtensionsTests
{

	[TestMethod]
	public void LoadXml_Void_Success()
	{
		using AssemblyDebugModeCache cache = new();
		_ = cache.LoadXml();
		Assert.AreEqual(7, cache.Count);
	}

	[TestMethod]
	public void LoadXml_String_Success()
	{
		using AssemblyDebugModeCache cache = new();
		_ = cache.LoadXml(@"TestFiles\AssemblyDebugModes.xml");
		Assert.AreEqual(7, cache.Count);
	}

	[TestMethod]
	public void LoadXml_StringEmpty_Throw()
	{
		_ = Assert.ThrowsException<ArgumentException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.LoadXml(string.Empty);
		  });
	}

	[TestMethod]
	public void LoadXml_StringInvalid_Throw()
	{
		_ = Assert.ThrowsException<IOException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.LoadXml("NoFileHere.xml");
		  });
	}

	[TestMethod]
	public void LoadXml_Stream_Success()
	{
		using AssemblyDebugModeCache cache = new();
		using (FileStream stream = new(@"TestFiles\AssemblyDebugModes.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			_ = cache.LoadXml(stream);
		}
		Assert.AreEqual(7, cache.Count);
	}

	[TestMethod]
	public void LoadXml_StreamNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.LoadXml((Stream)null);
		  });
	}

	[TestMethod]
	public void LoadXml_StreamNoRead_Throw()
	{
		_ = Assert.ThrowsException<ArgumentException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.LoadXml(new NoReadStream());
		  });
	}

	[TestMethod]
	public void LoadXml_TextReader_Success()
	{
		using AssemblyDebugModeCache cache = new();
		using (FileStream stream = new(@"TestFiles\AssemblyDebugModes.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
		using (TextReader reader = new StreamReader(stream))
		{
			_ = cache.LoadXml(reader);
		}
		Assert.AreEqual(7, cache.Count);
	}

	[TestMethod]
	public void LoadXml_TextReaderNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.LoadXml((TextReader)null);
		  });
	}

	[TestMethod]
	public void ParseXml_String_Success()
	{
		using AssemblyDebugModeCache cache = new();
		string xml = File.ReadAllText(@"TestFiles\AssemblyDebugModes.xml");
		_ = cache.ParseXml(xml);
		Assert.AreEqual(7, cache.Count);
	}

	[TestMethod]
	public void FromXml_XmlReader_Success()
	{
		using AssemblyDebugModeCache cache = new();
		using (FileStream stream = new(@"TestFiles\AssemblyDebugModes.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
		using (XmlReader reader = XmlReader.Create(stream))
		{
			_ = cache.FromXml(reader);
		}
		Assert.AreEqual(7, cache.Count);
	}

	[TestMethod]
	public void FromXml_XmlReaderNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentNullException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.FromXml((XmlReader)null);
		  });
	}

	[TestMethod]
	public void ParseXml_StringNull_Throw()
	{
		_ = Assert.ThrowsException<ArgumentException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.ParseXml(null);
		  });
	}

	[TestMethod]
	public void LoadXml_InvAssemblyName_Throw()
	{
		_ = Assert.ThrowsException<FormatException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.LoadXml(@"TestFiles\AssemblyDebugModesInvAssemblyName.xml");
		  });
	}

	[TestMethod]
	public void LoadXml_InvIsEnabled_Throw()
	{
		_ = Assert.ThrowsException<XmlException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.LoadXml(@"TestFiles\AssemblyDebugModesInvIsEnabled.xml");
		  });
	}

	[TestMethod]
	public void LoadXml_MissingAssemblyName_Throw()
	{
		_ = Assert.ThrowsException<XmlException>(() =>
		  {
			  using AssemblyDebugModeCache cache = new();
			  _ = cache.LoadXml(@"TestFiles\AssemblyDebugModesMissingAssemblyName.xml");
		  });
	}

	[TestMethod]
	public void LoadXml_NoAssemblyName_Success()
	{
		using AssemblyDebugModeCache cache = new();
		_ = cache.LoadXml(@"TestFiles\AssemblyDebugModesNoAssemblyName.xml");
		Assert.AreEqual(1, cache.Count);
	}

#if NET50
	[TestMethod]
	public void LoadXml_ReadOnlySequence_Success()
	{
		using AssemblyDebugModeCache cache = new();
		ReadOnlySequence<byte> buffer = new(File.ReadAllBytes(@"TestFiles\AssemblyDebugModes.xml"));
		_ = cache.LoadXml(buffer);
		Assert.AreEqual(7, cache.Count);
	}

	[TestMethod]
	public void LoadXml_ReadOnlyMemory_Success()
	{
		using AssemblyDebugModeCache cache = new();
		ReadOnlyMemory<byte> buffer = new(File.ReadAllBytes(@"TestFiles\AssemblyDebugModes.xml"));
		_ = cache.LoadXml(buffer);
		Assert.AreEqual(7, cache.Count);
	}
#endif
}
