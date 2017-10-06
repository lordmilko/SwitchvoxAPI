using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SwitchvoxAPI.Deserialization
{
    class XmlSerializer
    {
        private Type outerType;

        public XmlSerializer(Type type)
        {
            outerType = type;
        }

        public object Deserialize(XElement doc)
        {
            return Deserialize(outerType, doc);
        }

        private object Deserialize(Type type, XElement elm)
        {
            var obj = Activator.CreateInstance(type);

            var mappings = GetMappings(type);

            foreach (var mapping in mappings)
            {
                try
                {
                    switch (mapping.AttributeType)
                    {
                        case XmlAttributeType.Element:
                            ProcessXmlElement(obj, mapping, elm);
                            break;
                        case XmlAttributeType.Attribute:
                            ProcessXmlAttribute(obj, mapping, elm);
                            break;
                        case XmlAttributeType.Text:
                            ProcessXmlText(obj, mapping, elm);
                            break;
                        // ProcessXmlEnum(obj, mapping, elm);
                        //break;
                        default:
                            throw new NotSupportedException(); //todo: add an appropriate failure message
                    }
                }
                catch (Exception)
                {
                    //throw new Exception("An error occurred while trying to deserialize property " + mapping.Property.Name);
                    throw;
                }

            }

            return obj;
        }

        private List<XmlMapping> GetMappings(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            var mappings = new List<XmlMapping>();

            foreach (var prop in properties)
            {
                if (FindXmlAttribute<XmlElementAttribute>(prop, mappings, type, a => a.ElementName, XmlAttributeType.Element))
                    continue;
                if (FindXmlAttribute<XmlAttributeAttribute>(prop, mappings, type, a => a.AttributeName, XmlAttributeType.Attribute))
                    continue;
                if (FindXmlAttribute<XmlTextAttribute>(prop, mappings, type, a => null, XmlAttributeType.Text))
                    continue;
            }

            return mappings;
        }

        private bool FindXmlAttribute<TAttribute>(PropertyInfo property, List<XmlMapping> mappings, Type type, Func<TAttribute, string> name, XmlAttributeType enumType) where TAttribute : Attribute
        {
            var attributes = property.GetCustomAttributes(typeof(TAttribute)) as IEnumerable<TAttribute>;

            if (attributes != null)
            {
                var list = attributes.ToList();

                if (list.Any())
                {
                    mappings.Add(new XmlMapping(type, property, list.Select(name).ToArray(), enumType));
                }
                else
                    return false;
                return true;
            }

            return false;
        }

        private void ProcessXmlElement(object obj, XmlMapping mapping, XElement elm)
        {
            var type = mapping.Property.PropertyType;

            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
            {
                ProcessEnumerableXmlElement(obj, mapping, elm);
            }
            else
            {
                if (mapping.Property.PropertyType.GetCustomAttribute<XmlRootAttribute>() != null)
                {
                    //var property = Activator.CreateInstance(mapping.Property.PropertyType);
                    var elms = mapping.AttributeValue.Select(a => elm.Elements(a)).First(x => x != null).FirstOrDefault();

                    var result = elms != null ? Deserialize(mapping.Property.PropertyType, elms) : null;

                    mapping.Property.SetValue(obj, result);

                    //we now want to deserialize whats inside me, and then somehow set this on me once we're done. its so confusing!
                    //1. create an instance of this type. 2. deserialize me?. 3. set me?
                }
                else
                    ProcessSingleXmlElement(obj, mapping, elm);
            }
        }

        private void ProcessEnumerableXmlElement(object obj, XmlMapping mapping, XElement elm)
        {
            var type = mapping.Property.PropertyType;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                ProcessListEnumerableXmlElement(obj, mapping, elm);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private void ProcessListEnumerableXmlElement(object obj, XmlMapping mapping, XElement elm)
        {
            var type = mapping.Property.PropertyType;
            var underlyingType = type.GetGenericArguments().First();

            var list = Activator.CreateInstance(type);

            var elms = mapping.AttributeValue.Select(a => elm.Elements(a)).First(x => x != null).ToList();

            foreach (var e in elms)
            {
                ((IList)list).Add(Deserialize(underlyingType, e));
            }

            mapping.Property.SetValue(obj, list);
        }

        private void ProcessSingleXmlElement(object obj, XmlMapping mapping, XElement elm)
        {
            var type = mapping.Property.PropertyType;
            var value = mapping.GetSingleXElementAttributeValue(elm);

            try
            {
                ProcessXmlText(obj, mapping, value);
                //value = NullifyMissingValue(value);

                //var finalValue = value == null ? null : GetValue(type, value.Value);

                //if (type.IsValueType && Nullable.GetUnderlyingType(type) == null && finalValue == null)
                //    throw new XmlDeserializationException($"An error occurred while attempting to deserialize XML element '{mapping.AttributeValue.First()}' to property '{mapping.Property.Name}': cannot assign 'null' to value type '{type.Name}'."); //value types cant be null

                //mapping.Property.SetValue(obj, finalValue);
            }
            catch (Exception ex) when (!(ex is XmlDeserializationException))
            {
                throw new XmlDeserializationException(mapping.Property.PropertyType, value?.ToString() ?? "null", ex);
            }
        }

        private XElement NullifyMissingValue(XElement value)
        {
            if (string.IsNullOrEmpty(value?.ToString()) || string.IsNullOrEmpty(value.Value))
            {
                return null;
            }

            return value;
        }

        private XAttribute NullifyMissingValue(XAttribute value)
        {
            if (string.IsNullOrEmpty(value?.ToString()) || string.IsNullOrEmpty(value.Value))
            {
                return null;
            }

            return value;
        }

        //todo: profile the performance of my method vs the .net method

        private void ProcessXmlAttribute(object obj, XmlMapping mapping, XElement elm)
        {
            var value = mapping.GetSingleXAttributeAttributeValue(elm);

            value = NullifyMissingValue(value);
            var finalValue = value == null ? null : GetValue(mapping.Property.PropertyType, value.Value);

            mapping.Property.SetValue(obj, finalValue);
        }

        private void ProcessXmlText(object obj, XmlMapping mapping, XElement elm)
        {
            var type = mapping.Property.PropertyType;

            elm = NullifyMissingValue(elm);

            var finalValue = elm == null ? null : GetValue(type, elm.Value);

            if (type.IsValueType && Nullable.GetUnderlyingType(type) == null && finalValue == null)
                throw new XmlDeserializationException($"An error occurred while attempting to deserialize XML element '{mapping.AttributeValue.First()}' to property '{mapping.Property.Name}': cannot assign 'null' to value type '{type.Name}'."); //value types cant be null

            mapping.Property.SetValue(obj, finalValue);
        }

        private object GetValue(Type type, object value)
        {
            if (type.IsPrimitive)
                return GetPrimitiveValue(type, value);
            else if (Nullable.GetUnderlyingType(type) != null) //if we're nullable, id say call the getvalue method again on the underlying type
            {
                var t = Nullable.GetUnderlyingType(type);
                return GetValue(t, value);
            }
            else if (type == typeof(string))
            {
                return value.ToString();
            }
            else if (type.IsEnum)
            {
                return ConvertExtensions.ToEnum<XmlEnumAttribute>(value.ToString(), type);
            }
            else if (type == typeof(DateTime))
            {
                return DateTime.Parse(value.ToString());
            }
            else if (type == typeof(TimeSpan))
            {
                return ConvertExtensions.ToTimeSpan(value.ToString());
            }
            return null;
        }

        private object GetPrimitiveValue(Type type, object value)
        {
            var str = value.ToString();

            switch (Type.GetTypeCode(type))
            {
                //case TypeCode.String:
                //    o = reader.Read_string();
                //    break;
                case TypeCode.Int32:
                    return XmlConvert.ToInt32(str);
                case TypeCode.Boolean:
                    return XmlConvert.ToBoolean(str.ToLower());
                case TypeCode.Int16:
                    return XmlConvert.ToInt16(str);
                case TypeCode.Int64:
                    return XmlConvert.ToInt64(str);
                case TypeCode.Single:
                    return XmlConvert.ToSingle(str);
                case TypeCode.Double:
                    return XmlConvert.ToDouble(str);
                case TypeCode.Decimal:
                    return XmlConvert.ToDecimal(str);
                case TypeCode.Char:
                    return XmlConvert.ToChar(str);
                case TypeCode.Byte:
                    return XmlConvert.ToByte(str);
                case TypeCode.SByte:
                    return XmlConvert.ToSByte(str);
                case TypeCode.UInt16:
                    return XmlConvert.ToUInt16(str);
                case TypeCode.UInt32:
                    return XmlConvert.ToUInt32(str);
                case TypeCode.UInt64:
                    return XmlConvert.ToUInt64(str);
            }

            throw new NotSupportedException(); //TODO - say the type is not deserializable
        }
    }
}
