// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

global using System;
global using System.Diagnostics.CodeAnalysis;
global using System.Globalization;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using NerdyDuck.CodedExceptions;
#if !NETFRAMEWORK
global using System.Buffers;
#endif

using System.Reflection;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]
[assembly: ComVisible(true)]
[assembly: AssemblyTrademark("Covered by MIT License")]
[assembly: AssemblyFacilityIdentifier(0x002a)]
