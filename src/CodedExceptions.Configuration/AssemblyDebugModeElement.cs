#region Copyright
/*******************************************************************************
 * NerdyDuck.CodedExceptions.Configuration - Configures facility identifier
 * overrides and debug mode flags implemented in NerdyDuck.CodedExceptions.
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

using System;
using System.Configuration;
using System.Globalization;

namespace NerdyDuck.CodedExceptions.Configuration
{
	/// <summary>
	/// Represents an element in the &lt;debugModes&gt; configuration section.
	/// </summary>
	internal class AssemblyDebugModeElement : ConfigurationElement
	{
		#region ConfigurationProperties
		private static readonly ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
		private static readonly ConfigurationProperty s_assemblyNameProp = new ConfigurationProperty(Globals.AssemblyNameKey, typeof(string), "", ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty s_isEnabledProp = new ConfigurationProperty(Globals.IsEnabledKey, typeof(bool), true, ConfigurationPropertyOptions.None);
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the fully or partially qualified name of the assembly to set the debug mode id for.
		/// </summary>
		[ConfigurationProperty(Globals.AssemblyNameKey)]
		[StringValidator(InvalidCharacters = Globals.InvalidNameChars, MinLength = 1)]
		public string AssemblyName
		{
			get => (string)base[s_assemblyNameProp];
			set => base[s_assemblyNameProp] = value;
		}

		/// <summary>
		/// Gets or sets a value indicating if the debug mode is set for the assembly specified in <see cref="AssemblyName"/>.
		/// </summary>
		[ConfigurationProperty(Globals.IsEnabledKey)]
		public bool IsEnabled
		{
			get => (bool)base[s_isEnabledProp];
			set => base[s_isEnabledProp] = value;
		}

		/// <summary>
		/// Gets a collection of configuration properties.
		/// </summary>
		protected override ConfigurationPropertyCollection Properties => s_properties;

		/// <summary>
		/// Gets a value indicating if the element can be modified.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required to override behavior.")]
		public new bool IsReadOnly => false;
		#endregion

		#region Constructors
		/// <summary>
		/// Static constructor.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Cannot add other static fields to collection inline.")]
		static AssemblyDebugModeElement()
		{
			s_properties.Add(s_assemblyNameProp);
			s_properties.Add(s_isEnabledProp);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyDebugModeElement" /> class.
		/// </summary>
		public AssemblyDebugModeElement()
			: base()
		{
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Creates a <see cref="AssemblyDebugMode" /> object from the current element.
		/// </summary>
		/// <returns>A <see cref="AssemblyDebugMode" /> reflecting data of the current element.</returns>
		/// <exception cref="FormatException"><see cref="AssemblyName" /> is not a valid assembly name (fully or partially qualified).</exception>
		public AssemblyDebugMode ToAssemblyDebugMode()
		{
			try
			{
				return new AssemblyDebugMode(AssemblyName, IsEnabled);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
			{
				throw new FormatException(string.Format(CultureInfo.CurrentCulture, TextResources.DebugModeElement_ToAssemblyIdentity_Invalid, AssemblyName), ex);
			}
		}
		#endregion
	}
}
