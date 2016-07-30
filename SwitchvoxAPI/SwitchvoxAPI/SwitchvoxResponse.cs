using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using SwitchvoxAPI.SwitchvoxAPI;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Encapsulates a response from a <see cref="T:SwitchvoxAPI.SwitchvoxClient"/>.
    /// </summary>
    public class SwitchvoxResponse
    {
        /// <summary>
        /// The XML returned from the phone system.
        /// </summary>
        public XDocument Xml { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.SwitchvoxResponse"/> class.
        /// </summary>
        /// <param name="xml">The XML to be contained in the response.</param>
        public SwitchvoxResponse(XDocument xml)
        {
            Xml = xml;
        }

        /// <summary>
        /// Retrieve all elements with a specified tag name from the <see cref="T:SwitchvoxAPI.SwitchvoxResponse"/>.
        /// </summary>
        /// <param name="name">The name of the XML tag</param>
        /// <returns>An enumeration of <see cref="T:System.Xml.Linq.XElement"/>s with the specified name</returns>
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

            var result = deserializer.Deserialize(stream);

            return (T)result;
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
