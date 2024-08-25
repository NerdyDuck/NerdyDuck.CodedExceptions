// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions;

#if NET8_0_OR_GREATER
internal class CompositeFormatCache
{
	public static readonly CompositeFormatCache Default = new();

	public CompositeFormatCache() { }

	private readonly System.Collections.Generic.Dictionary<string, System.Text.CompositeFormat> _cache = [];

#if NET9_0_OR_GREATER
	private readonly System.Threading.Lock _cacheLock = new();
#else
	private readonly object _cacheLock = new();
#endif

	public System.Text.CompositeFormat Get(string compositeText)
	{
		lock (_cacheLock)
		{
			if (_cache.TryGetValue(compositeText, out System.Text.CompositeFormat? compositeFormat))
			{
				return compositeFormat;
			}

			compositeFormat = System.Text.CompositeFormat.Parse(compositeText);
			_cache.Add(compositeText, compositeFormat);
			return compositeFormat;
		}
	}
}
#else
#pragma warning disable CA1822 // Required to keep interface symmetric to .NET 8 version
internal class CompositeFormatCache
{
	public static readonly CompositeFormatCache Default = new();

	public CompositeFormatCache() { }

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	public string Get(string compositeText) => compositeText;
}
#pragma warning restore CA1822 // Mark members as static
#endif
