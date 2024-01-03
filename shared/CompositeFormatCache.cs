// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdyDuck.CodedExceptions;
internal class CompositeFormatCache
{
	public static CompositeFormatCache Default = new();

	public CompositeFormatCache() { }

#if NET8_0_OR_GREATER
	private readonly Dictionary<string, CompositeFormat> _cache = [];
	private readonly object _cacheLock = new();

	public CompositeFormat Get(string compositeText)
	{
		lock (_cacheLock)
		{
			if (_cache.TryGetValue(compositeText, out CompositeFormat? compositeFormat))
			{
				return compositeFormat;
			}
			compositeFormat = CompositeFormat.Parse(compositeText);
			_cache.Add(compositeText, compositeFormat);
			return compositeFormat;
		}
	}
#else
#pragma warning disable CA1822 // Required to keep interface symmetric to .NET 8 version
	public string Get(string compositeText) => compositeText;
#pragma warning restore CA1822 // Mark members as static
#endif
}
