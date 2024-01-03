// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace NerdyDuck.CodedExceptions;

/// <summary>
/// When applied to a class that inherits from <see cref="Exception"/>,
/// specifies that the <see cref="Exception.HResult"/> property contains a custom error code.
/// </summary>
/// <remarks>
/// When creating a new exception that can contain custom <see cref="Exception.HResult"/> values,
/// decorate the class with the <see cref="CodedExceptionAttribute"/>, so that other code knows
/// that the <see cref="Exception.HResult"/> property contains a meaningful coded numerical value
/// that identifies an application-specific exception.
/// </remarks>
/// <example>
/// <code source="..\..\doc\Examples\CodedExceptionAttribute.cs" language="C#" title="Using the attribute" />
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
[ComVisible(false)]
public sealed class CodedExceptionAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CodedExceptionAttribute"/> class.
	/// </summary>
	public CodedExceptionAttribute()
		: base()
	{
	}

	/// <summary>
	/// Checks if the type of the specified instance of <see cref="Exception"/>
	/// or a derived class is decorated with a <see cref="CodedExceptionAttribute"/>.
	/// </summary>
	/// <param name="ex">The <see cref="Exception"/> instance to check for a <see cref="CodedExceptionAttribute"/>.</param>
	/// <returns><see langword="true"/>, if the type has a <see cref="CodedExceptionAttribute"/>; otherwise, <see langword="false"/>.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="ex"/> is <see langword="null"/>.</exception>
	/// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
	public static bool IsCodedException(Exception ex) => ex == null
			? throw new ArgumentNullException(nameof(ex))
			: ex.GetType().GetTypeInfo().GetCustomAttribute<CodedExceptionAttribute>(true) != null;
}
