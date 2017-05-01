using System;
using System.Linq;
using System.Xml.Serialization;

namespace SwitchvoxAPI
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

            throw new Exception($"Cannot deserialize type {type}. The type is missing an XmlRootAttribute");
        }
    }
}
