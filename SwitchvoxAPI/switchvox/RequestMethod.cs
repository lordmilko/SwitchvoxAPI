using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace Switchvox
{
    /// <summary>
    /// Serves as the base class for all Switchvox API XML requests
    /// </summary>
    public abstract class RequestMethod
    {
        XDocument xml;

        private readonly string method;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.RequestMethod"/> class.
        /// </summary>
        /// <param name="method">The name of the Switchvox API Method this class implements. For a list of valid methods please see http://developers.digium.com/switchvox/wiki/index.php/WebService_methods </param>
        protected RequestMethod(string method)
        {
            this.method = method;
        }

        /// <summary>
        /// Convert the Xml this API Request to its equivalent byte representation
        /// </summary>
        /// <returns>The byte representation of this Xml API Request</returns>
        public byte[] ToBytes()
        {
            byte[] bytes = Encoding.ASCII.GetBytes(xml.ToString());

            return bytes;
        }

        /// <summary>
        /// Set the Xml attribute of this RequestMethod
        /// </summary>
        /// <param name="xml">The XML to use. If this Switchvox API Method does not include any parameters, this value can be null"/></param>
        protected void SetXml(object xml)
        {
            this.xml = new XDocument(
                new XElement("request",
                    new XAttribute("method", method),
                    new XElement("parameters", xml)
                )
            );
        }
    }
}
