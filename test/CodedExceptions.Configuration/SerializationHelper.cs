// Copyright (c) Daniel Kopp, dak@nerdyduck.de. All rights reserved.
// This file is licensed to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NerdyDuck.Tests.CodedExceptions.Configuration;

/// <summary>
/// Contains methods to serialize and deserialize objects using the SerializableAttribute and/or ISerializable.
/// </summary>
[ExcludeFromCodeCoverage]
public static class SerializationHelper
{
#pragma warning disable IDE0079 // that next suppression is not unnecessary!
#if NETFRAMEWORK
#pragma warning disable SYSLIB0011 // the only simple way for serialization is still binary
	/// <summary>
	/// Serializes an object.
	/// </summary>
	/// <typeparam name="T">The type of class to serialize.</typeparam>
	/// <param name="ex">The object to serialize.</param>
	/// <returns>A MemoryStream containing the serialized object data.</returns>
	public static MemoryStream Serialize<T>(T ex) where T : class
	{
		BinaryFormatter formatter = new();
		MemoryStream buffer = new();
		formatter.Serialize(buffer, ex);
		_ = buffer.Seek(0, SeekOrigin.Begin);

		return buffer;
	}

	/// <summary>
	/// Deserializes an object.
	/// </summary>
	/// <typeparam name="T">The type of class to deserialize.</typeparam>
	/// <param name="buffer">The MemoryStream containing the serialized object data.</param>
	/// <returns>The deserialized object.</returns>
	public static T Deserialize<T>(MemoryStream buffer) where T : class
	{
		BinaryFormatter formatter = new();
		return (T)formatter.Deserialize(buffer);
	}
#pragma warning restore SYSLIB0011
#endif

	public static void InvokeSerializationConstructorWithNullContext(Type type)
	{
#pragma warning disable CA1510 // Use ArgumentNullException throw helper
		if (type == null)
		{
			throw new ArgumentNullException(nameof(type));
		}
#pragma warning restore CA1510 // Use ArgumentNullException throw helper

		ConstructorInfo ci = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.HasThis, [typeof(SerializationInfo), typeof(StreamingContext)], null);
		try
		{
			_ = ci.Invoke([null, new StreamingContext()]);
		}
		catch (TargetInvocationException ex)
		{
			throw ex.InnerException;
		}
	}
#pragma warning restore IDE0079
}
