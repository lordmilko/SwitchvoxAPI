using System;
using System.Linq;
using System.Xml.Serialization;
using SwitchvoxAPI.Exceptions;

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

        internal static object EnumToXml(this Enum element)
        {
            var attribute = element.GetEnumAttribute<XmlEnumAttribute>();

            if (attribute == null)
                return element.ToString();

            else
                return attribute.Name;
        }

        public static TAttribute GetEnumAttribute<TAttribute>(this Enum element, bool mandatory = false) where TAttribute : Attribute
        {
            var attributes = element.GetType().GetMember(element.ToString()).First().GetCustomAttributes(typeof(TAttribute), false);

            if (attributes.Any())
                return (TAttribute)attributes.First();

            if (!mandatory)
                return null;
            else
                throw new MissingAttributeException(element.GetType(), element.ToString(), typeof(TAttribute));
        }
    }
}
