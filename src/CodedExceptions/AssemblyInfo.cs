#region Copyright
/*******************************************************************************
 * NerdyDuck.CodedExceptions - Exceptions with custom HRESULTs to identify the 
 * origins of errors.
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

global using System;
global using System.Globalization;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using System.Runtime.Serialization;

using System.Runtime.CompilerServices;

[assembly: CLSCompliant(true)]
[assembly: ComVisible(true)]
[assembly: AssemblyTrademark("Covered by MIT License")]
[assembly: InternalsVisibleTo("NerdyDuck.Tests.CodedExceptions, PublicKey=00240000048000009400000006020000002400005253413100040000010001002d77b95f108da7cd87483c660f294773a523b16443c4cfca82a347913f75a017c6002a5abc6c1439a8c040ca7ec9061055c10b23bea0e25d4dd713edabec9913e2f066f5976d7f2335d8adfd0ff901b3a045f4839e920ca3e275278a3fc49ff5bb4eb602c5a940837e6bd0e948e942a61fc1485c977f78018bf544489fa906d6")]
[assembly: InternalsVisibleTo("NerdyDuck.Tests.CodedExceptions.Configuration, PublicKey=00240000048000009400000006020000002400005253413100040000010001002d77b95f108da7cd87483c660f294773a523b16443c4cfca82a347913f75a017c6002a5abc6c1439a8c040ca7ec9061055c10b23bea0e25d4dd713edabec9913e2f066f5976d7f2335d8adfd0ff901b3a045f4839e920ca3e275278a3fc49ff5bb4eb602c5a940837e6bd0e948e942a61fc1485c977f78018bf544489fa906d6")]
