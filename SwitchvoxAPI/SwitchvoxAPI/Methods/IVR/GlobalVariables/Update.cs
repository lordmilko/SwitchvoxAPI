using System.Collections.Generic;
using System.Xml.Linq;

namespace SwitchvoxAPI.Methods
{
    public partial class GlobalVariables
    {
        /// <summary>
        /// Update the value of a Global IVR Variable
        /// </summary>
        /// <param name="id">The Global IVR Variable ID of the Global IVR Variable you wish to update</param>
        /// <param name="value">The value you wish to assign to the Global IVR Variable</param>
        public void Update(int id, string value)
        {
            var xml = new List<XElement>
            {
                new XElement("global_ivr_variable_id", id),
                new XElement("global_ivr_variable_value", value)
            };

            var response = client.Execute(new RequestMethod("switchvox.ivr.globalVariables.update", xml));
        }
    }
}
