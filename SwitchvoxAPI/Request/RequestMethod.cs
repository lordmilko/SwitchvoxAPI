﻿using System.Text;
using System.Xml.Linq;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Serves as the base class for all Switchvox API XML requests.
    /// </summary>
    public class RequestMethod
    {
        /// <summary>
        /// The XML to be sent to the phone system.
        /// </summary>
        public XDocument Xml { get; private set; }

        /// <summary>
        /// The name of this Switchvox Request Method.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMethod"/> class.
        /// </summary>
        /// <param name="method">The name of the Switchvox API Method this class implements. For a list of valid methods please see http://developers.digium.com/switchvox/wiki/index.php/WebService_methods </param>
        /// <param name="xml">The XML to use for the request.</param>
        public RequestMethod(string method, object xml)
        {
            Name = method;
            SetXml(xml);
        }

        /// <summary>
        /// Convert the XML of this API Request to its equivalent byte representation.
        /// </summary>
        /// <returns>The byte representation of this XML API Request.</returns>
        public byte[] ToBytes()
        {
            byte[] bytes = Encoding.ASCII.GetBytes(Xml.ToString());

            return bytes;
        }

        /// <summary>
        /// Set the XML of this RequestMethod.
        /// </summary>
        /// <param name="xml">The XML to use. If this Switchvox API Method does not include any parameters, this value can be null.</param>
        private void SetXml(object xml)
        {
            Xml = new XDocument(
                new XElement("request",
                    new XAttribute("method", Name),
                    new XElement("parameters", xml)
                )
            );
        }
    }
}
