#region Copyright
/*******************************************************************************
 * <copyright file="SerializationHelper.cs" owner="Daniel Kopp">
 * Copyright 2015 Daniel Kopp
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
 * <assembly name="NerdyDuck.Tests.CodedExceptions">
 * Unit tests for NerdyDuck.CodedExceptions assembly.
 * </assembly>
 * <file name="SerializationHelper.cs" date="2015-08-13">
 * Contains methods to serialize and deserialize objects using the
 * SerializableAttribute and/or ISerializable.
 * </file>
 ******************************************************************************/
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NerdyDuck.Tests.CodedExceptions
{
	/// <summary>
	/// Contains methods to serialize and deserialize objects using the SerializableAttribute and/or ISerializable.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static class SerializationHelper
	{
		/// <summary>
		/// Serializes an exception.
		/// </summary>
		/// <typeparam name="TException">The type of exception to serialize.</typeparam>
		/// <param name="ex">The exception to serialize.</param>
		/// <returns>A MemoryStream containing the serialized exception data.</returns>
		public static MemoryStream Serialize<TException>(TException ex) where TException : Exception
		{
			BinaryFormatter formatter = new BinaryFormatter();
			MemoryStream buffer = new MemoryStream();
			formatter.Serialize(buffer, ex);
			buffer.Seek(0, SeekOrigin.Begin);

			return buffer;
		}

		/// <summary>
		/// Deserializes an exception.
		/// </summary>
		/// <typeparam name="TException">The type of exception to deserialize.</typeparam>
		/// <param name="buffer">The MemoryStream containing the serialized exception data.</param>
		/// <returns>The deserialized exception.</returns>
		public static TException Deserialize<TException>(MemoryStream buffer) where TException : Exception
		{
			BinaryFormatter formatter = new BinaryFormatter();
			return (TException)formatter.Deserialize(buffer);
		}
	}
}
