// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.Tests.CodedExceptions;

/// <summary>
/// A simple enumeration with integer as base type.
/// </summary>
public enum Int32Enumeration
{
	One = 1,
	Two,
	Three
}

/// <summary>
/// A simple enumeration with byte as base type
/// </summary>
public enum ByteEnumeration : byte
{
	One = 1,
	Two,
	Three
}

/// <summary>
/// A simple enumeration with unsigned integer as base type
/// </summary>
[CLSCompliant(false)]
public enum UInt32Enumeration : uint
{
	One = 1,
	Two,
	Three
}
