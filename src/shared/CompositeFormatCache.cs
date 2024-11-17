// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

#pragma warning disable IDE0005 // Reqquired to use it in all projects
using NerdyDuck.CodedExceptions.Configuration;
#pragma warning restore IDE0005

namespace NerdyDuck.CodedExceptions;

#if NET8_0_OR_GREATER
[global::System.CodeDom.Compiler.GeneratedCodeAttribute("NerdyDuck.CodedExceptions", "2.0.0.0")]
[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
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

	public string Format<TArg0>(string resourceName, TArg0 arg0)
	{
		System.Text.CompositeFormat compositeFormat = GetFormat(resourceName);
		return string.Format(System.Globalization.CultureInfo.CurrentCulture, compositeFormat, arg0);
	}

	public string Format<TArg0, TArg1>(string resourceName, TArg0 arg0, TArg1 arg1)
	{
		System.Text.CompositeFormat compositeFormat = GetFormat(resourceName);
		return string.Format(System.Globalization.CultureInfo.CurrentCulture, compositeFormat, arg0, arg1);
	}

	public string Format<TArg0, TArg1, TArg2>(string resourceName, TArg0 arg0, TArg1 arg1, TArg2 arg2)
	{
		System.Text.CompositeFormat compositeFormat = GetFormat(resourceName);
		return string.Format(System.Globalization.CultureInfo.CurrentCulture, compositeFormat, arg0, arg1, arg2);
	}

	public string Format(string resourceName, params object?[] args)
	{
		System.Text.CompositeFormat compositeFormat = GetFormat(resourceName);
		return string.Format(System.Globalization.CultureInfo.CurrentCulture, compositeFormat, args);
	}

	public System.Text.CompositeFormat Get(string resourceText)
	{
		lock (_cacheLock)
		{
			if (_cache.TryGetValue(resourceText, out System.Text.CompositeFormat? compositeFormat))
			{
				return compositeFormat;
			}

			compositeFormat = System.Text.CompositeFormat.Parse(resourceText);
			_cache.Add(resourceText, compositeFormat);
			return compositeFormat;
		}
	}

	private System.Text.CompositeFormat GetFormat(string resourceName)
	{
		string? compositeText = SR.ResourceManager.GetString(resourceName, System.Globalization.CultureInfo.CurrentCulture);
		ArgumentNullException.ThrowIfNull(compositeText, resourceName);

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
[global::System.CodeDom.Compiler.GeneratedCodeAttribute("NerdyDuck.CodedExceptions", "2.0.0.0")]
[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
internal class CompositeFormatCache
{
	public static readonly CompositeFormatCache Default = new();

	public CompositeFormatCache() { }

	public string Format<TArg0>(string resourceName, TArg0 arg0) => string.Format(System.Globalization.CultureInfo.CurrentCulture, GetFormat(resourceName) ?? throw new ArgumentNullException(nameof(resourceName)), arg0);

	public string Format<TArg0, TArg1>(string resourceName, TArg0 arg0, TArg1 arg1) => string.Format(System.Globalization.CultureInfo.CurrentCulture, GetFormat(resourceName) ?? throw new ArgumentNullException(nameof(resourceName)), arg0, arg1);

	public string Format<TArg0, TArg1, TArg2>(string resourceName, TArg0 arg0, TArg1 arg1, TArg2 arg2) => string.Format(System.Globalization.CultureInfo.CurrentCulture, GetFormat(resourceName) ?? throw new ArgumentNullException(nameof(resourceName)), arg0, arg1, arg2);

	public string Format(string resourceName, params object?[] args) => string.Format(System.Globalization.CultureInfo.CurrentCulture, GetFormat(resourceName) ?? throw new ArgumentNullException(nameof(resourceName)), args);

	public string Get(string resourceText) => resourceText;

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private string? GetFormat(string resourceName) => SR.ResourceManager.GetString(resourceName, System.Globalization.CultureInfo.CurrentCulture);
}
#pragma warning restore CA1822 // Mark members as static
#endif
