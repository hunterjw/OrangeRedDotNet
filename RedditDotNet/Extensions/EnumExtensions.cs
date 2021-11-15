using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RedditDotNet.Extensions
{
	/// <summary>
	/// Extensions for enums
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Get the description string for an enum value with a Description attribute
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="value">Enum value</param>
		/// <returns>Description string</returns>
		public static string ToDescriptionString<T>(this T value) where T : Enum
		{
			DescriptionAttribute[] attributes = (DescriptionAttribute[])value
				.GetType()
				.GetField(value.ToString())
				.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes.Length > 0 ? attributes[0].Description :
				string.Empty;
		}

		/// <summary>
		/// Get an enum value for a given string by looking at the values of Description attribute on the enum values
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="value">Description string</param>
		/// <returns>Enum value, default enum value if there is no match</returns>
		public static T ToEnumFromDescriptionString<T>(this string value) where T : Enum
		{
			string valueUpper = value.ToUpper();
			T[] enumValues = (T[])Enum.GetValues(typeof(T));
			Dictionary<string, T> mapping = enumValues.ToDictionary(_ => _.ToDescriptionString().ToUpper());
			if (mapping.ContainsKey(valueUpper))
			{
				return mapping[valueUpper];
			}
			return default;
		}
	}
}
