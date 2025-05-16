// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

global using NerdyDuck.CodedExceptions.Configuration;

using System.Reflection;
using System.Runtime.InteropServices;
using NerdyDuck.CodedExceptions;

[assembly: Parallelize(Scope = ExecutionScope.ClassLevel)]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(true)]
[assembly: AssemblyTrademark("Covered by MIT License")]
[assembly: AssemblyFacilityIdentifier(0x002a)]
