using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Encapsulates a response from a <see cref="SwitchvoxClient"/>.
    /// </summary>
    public class SwitchvoxResponse
    {
        /// <summary>
        /// The XML returned from the phone system.
        /// </summary>
        public XDocument Xml { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchvoxResponse"/> class.
        /// </summary>
        /// <param name="xml">The XML to be contained in the response.</param>
        public SwitchvoxResponse(XDocument xml)
        {
            Xml = xml;
        }

        /// <summary>
        /// Retrieve all elements with a specified tag name from the <see cref="SwitchvoxResponse"/>.
        /// </summary>
        /// <param name="name">The name of the XML tag</param>
        /// <returns>An enumeration of <see cref="XElement"/>s with the specified name</returns>
        public IEnumerable<XElement> GetElements(string name)
        {
            return Xml.Descendants(name);
        }
        
        /// <summary>
        /// Retrieve a single attribute of a specified XML tag name
        /// </summary>
        /// <param name="element">The name of the XML tag</param>
        /// <param name="attribute">The name of the attribute</param>
        /// <returns>The value of the attribute</returns>
        public string GetAttribute(string element, string attribute)
        {
            var attrib = Xml.Descendants(element).Single().Attribute(attribute).Value;

            return attrib;
        }

        /// <summary>
        /// Retrieve all attributes of a specified XML tag name
        /// </summary>
        /// <param name="element">The name of the XML tag</param>
        /// <param name="attribute">The name of the XML attribute</param>
        /// <returns>A string array containing the value of specified attribute of each specified element</returns>
        public string[] GetAttributes(string element, string attribute)
        {
            var attribs = Xml.Descendants(element).Select(e => e.Attribute(attribute).Value);

            return attribs.ToArray();
        }

        internal T Deserialize<T>() where T : new()
        {
            var root = Xml.Descendants(typeof(T).GetXmlRoot()).Single();

            var deserializer = new XmlSerializer(typeof(T));

            var stream = GetStream(root);

            try
            {
                var result = deserializer.Deserialize(stream);
                return (T) result;
            }
            catch(InvalidOperationException ex)
            {
                Exception ex1 = null;

                try
                {
                    ex1 = GetInvalidXml(root, ex, typeof(T));
                }
                catch
                {
                }

                if (ex1 != null)
                    throw ex1;
                else
                    throw;
            }
        }

        private Exception GetInvalidXml(XElement root, InvalidOperationException ex, Type type)
        {
            var stream = GetStream(root);

            var xmlReader = (XmlReader)new XmlTextReader(stream)
            {
                WhitespaceHandling = WhitespaceHandling.Significant,
                Normalization = true,
                XmlResolver = null
            };

            var regex = new Regex("(.+\\()(.+)(, )(.+)(\\).+)");
            var line = Convert.ToInt32(regex.Replace(ex.Message, "$2"));
            var position = Convert.ToInt32(regex.Replace(ex.Message, "$4"));

            while (xmlReader.Read())
            {
                IXmlLineInfo xmlLineInfo = (IXmlLineInfo)xmlReader;

                if (xmlLineInfo.LineNumber == line)
                {
                    var xml = xmlReader.ReadOuterXml();

                    var prevSpace = xml.LastIndexOf(' ', position) + 1;
                    var nextSpace = xml.IndexOf(' ', position);

                    var length = nextSpace - prevSpace;

                    var str = xml.Substring(prevSpace, length);
                    
                    return new XmlDeserializationException(type, str, ex);
                }
            }

            return null;
        }

        private Stream GetStream(XElement root)
        {
            var stream = new MemoryStream();
            root.Save(stream);
            stream.Position = 0;

            return stream;
        }
    }
}
