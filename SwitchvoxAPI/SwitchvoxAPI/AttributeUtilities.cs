using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI.SwitchvoxAPI
{
    internal static class AttributeUtilities
    {
        public static string GetXmlRoot(this Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(XmlRootAttribute), false);

            if (attributes.Length > 0)
            {
                return ((XmlRootAttribute) attributes.First()).ElementName;
            }

            //todo: create an exception for this
            throw new NotImplementedException("missing an xmlroot");
        }
    }
}
