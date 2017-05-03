using System;

namespace SwitchvoxAPI
{
    internal static class ConvertExtensions
    {
        /// <summary>
        /// Convert a string that may be empty to an <see cref="Int32"/> .
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns></returns>
        public static int ToInt32(string str)
        {
            return str == string.Empty ? 0 : Convert.ToInt32(str);
        }

        /// <summary>
        /// Convert a string that may be empty in a <see cref="Double"/>.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns></returns>
        public static double ToDouble(string str)
        {
            return str == string.Empty ? 0 : Convert.ToDouble(str);
        }

        /// <summary>
        /// Convert a string that may be empty into a <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(string str)
        {
            return str == string.Empty ? TimeSpan.FromSeconds(0) : TimeSpan.FromSeconds(Convert.ToDouble(str));
        }
    }
}
