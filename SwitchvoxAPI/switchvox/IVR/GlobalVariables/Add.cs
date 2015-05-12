using System.Collections.Generic;
using System.Xml.Linq;

namespace Switchvox.IVR.GlobalVariables
{
    /// <summary>
    /// Add a Global IVR Variable to the phone system
    /// </summary>
    public class Add : RequestMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.IVR.GlobalVariables.Add"/> class.
        /// </summary>
        /// <param name="name">The name of the Global IVR Variable to add. If a Global IVR Variable with this name already exists, this method will return the ID of the existing variable.</param>
        /// <param name="value">The initial value to assign to the Global IVR Variable. If a Global IVR Variable with the name specified in the name parameter already exists, this method set the variable to this value and return its ID.</param>
        public Add(string name, string value = null) : base("switchvox.ivr.globalVariables.add")
        {
            var xml = new List<XElement>
            {
                new XElement("global_ivr_variable_name", name)
            };

            if (value != null)
                xml.Add(new XElement("global_ivr_variable_value", value));

            SetXml(xml);
        }
    }
}
