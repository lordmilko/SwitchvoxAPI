using System;
using System.Linq;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    internal static class ConvertExtensions
    {
        /// <summary>
        /// Convert a string that may be empty into a <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(string str)
        {
            return str == string.Empty ? TimeSpan.FromSeconds(0) : TimeSpan.FromSeconds(Convert.ToDouble(str));
        }

        public static object ToEnum<T>(string str, Type type) where T : XmlEnumAttribute
        {
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttributes(field, typeof(T)).Where(a => a.GetType() == typeof(T)).Cast<T>().FirstOrDefault();

                if (attribute != null)
                {
                    if (attribute.Name == str)
                        return field.GetValue(null);
                }
            }

            return Enum.Parse(type, str, true);
        }
    }
}
