using System.Collections.Generic;
using System.Xml.Linq;

namespace Switchvox.IVR.GlobalVariables
{
    /// <summary>
    /// Update the value of a Global IVR Variable
    /// </summary>
    public class Update : RequestMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.IVR.GlobalVariables.Update"/> class.
        /// </summary>
        /// <param name="id">The Global IVR Variable ID of the Global IVR Variable you wish to update</param>
        /// <param name="value">The value you wish to assign to the Global IVR Variable</param>
        public Update(int id, string value) : base("switchvox.ivr.globalVariables.update")
        {
            var xml = new List<XElement>
            {
                new XElement("global_ivr_variable_id", id),
                new XElement("global_ivr_variable_value", value)
            };

            SetXml(xml);
        }
    }
}
