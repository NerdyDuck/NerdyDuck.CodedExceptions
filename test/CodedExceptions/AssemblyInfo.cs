// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

global using System.Globalization;
global using NerdyDuck.CodedExceptions;
#if NET || NETSTANDARD2_1
global using System.Buffers;
#endif

using System.Reflection;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]
[assembly: ComVisible(true)]
[assembly: AssemblyTrademark("Covered by MIT License")]
[assembly: AssemblyFacilityIdentifier(0x002a)]
