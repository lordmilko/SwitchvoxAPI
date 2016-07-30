using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    internal class ConvertExtensions
    {
        /// <summary>
        /// Convert a string that may be empty to an integer.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt32(string str)
        {
            return str == string.Empty ? 0 : Convert.ToInt32(str);
        }

        public static double ToDouble(string str)
        {
            return str == string.Empty ? 0 : Convert.ToDouble(str);
        }

        public static TimeSpan ToTimeSpan(string str)
        {
            return str == string.Empty ? TimeSpan.FromSeconds(0) : TimeSpan.FromSeconds(Convert.ToDouble(str));
        }
    }
}
