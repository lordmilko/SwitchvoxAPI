using System.Collections.Generic;
using System.Xml.Linq;

namespace SwitchvoxAPI.Methods
{
    public partial class GlobalVariables
    {
        /// <summary>
        /// Add a Global IVR Variable to the phone system
        /// </summary>
        /// <param name="name">The name of the Global IVR Variable to add. If a Global IVR Variable with this name already exists, this method will return the ID of the existing variable.</param>
        /// <param name="value">The initial value to assign to the Global IVR Variable. If a Global IVR Variable with the name specified in the name parameter already exists, this method set the variable to this value and return its ID.</param>
        public void Add(string name, string value = null)
        {
            var xml = new List<XElement>
            {
                new XElement("global_ivr_variable_name", name)
            };

            if (value != null)
                xml.Add(new XElement("global_ivr_variable_value", value));

            var response = client.Execute(new RequestMethod("switchvox.ivr.globalVariables.add", xml));
        }
    }
}
