// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
		_ = Assert.ThrowsException<FormatException>(() =>
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

#if !NETFRAMEWORK
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
