// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Global test values.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Globals
{
	public static readonly Assembly ThisAssembly = typeof(Globals).Assembly;
	public static readonly AssemblyName ThisAssemblyName = ThisAssembly.GetName();
	public static readonly string ThisAssemblyNameString = ThisAssembly.FullName;
	public static readonly Assembly OtherAssembly = typeof(TestClassAttribute).Assembly;
}
