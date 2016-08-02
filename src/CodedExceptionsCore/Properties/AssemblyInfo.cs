#region Copyright
/*******************************************************************************
 * <copyright file="AssemblyInfo.cs" owner="Daniel Kopp">
 * Copyright 2015-2016 Daniel Kopp
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
 * <assembly name="NerdyDuck.CodedExceptions">
 * Exceptions with custom HRESULTs for .NET
 * </assembly>
 * <file name="AssemblyInfo.cs" date="2015-08-05">
 * Contains assembly-level properties.
 * </file>
 ******************************************************************************/
#endregion

using NerdyDuck.CodedExceptions;
using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General information
[assembly: AssemblyTitle("NerdyDuck.CodedExceptions")]
[assembly: AssemblyDescription("Exceptions with custom HRESULT values for .NET")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("NerdyDuck")]
[assembly: AssemblyProduct("NerdyDuck Core Libraries")]
[assembly: AssemblyCopyright("Copyright © Daniel Kopp 2015-2016")]
[assembly: AssemblyTrademark("Covered by Apache License 2.0")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: InternalsVisibleTo("NerdyDuck.Tests.CodedExceptions, PublicKey=0024000004800000940000000602000000240000525341310004000001000100632a3bae6d8036ee67ec223e6e09f81ca8eb957234344e93f6971143b07bd31213d9eab99cfbdad16575056b367399bd7456f7a295b030b56aa40a2bc3d546c91c7d32de2a079b1b1c6307f8f52cac99751cfc7df6ca5c5722c0293888d604e70dad647a7654e3affb8b44daed2dd69a5e2565a91cb95e15e249c413efa900c2")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: AssemblyFacilityIdentifier(0x0001)]
#if WINDOWS_UWP
[assembly: AssemblyMetadata("TargetPlatform", "UAP")]
#endif
#if WINDOWS_DESKTOP
[assembly: AssemblyMetadata("TargetPlatform", "AnyCPU")]
#endif
#if NETCORE
[assembly: AssemblyMetadata("TargetPlatform", "NetCore")]
#endif
// Version information
[assembly: AssemblyVersion("1.3.0.0")]
[assembly: AssemblyFileVersion("1.3.0.0")]
[assembly: AssemblyInformationalVersion("1.3.0")]
