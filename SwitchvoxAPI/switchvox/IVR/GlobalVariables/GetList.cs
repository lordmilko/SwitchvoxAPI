using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace Switchvox.IVR.GlobalVariables
{
    /// <summary>
    /// Get a list of Global IVR Variables in the system and their current values, or a single Global IVR Variable and its current value.
    /// </summary>
    public class GetList : RequestMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.IVR.GlobalVariables"/> class, where all Global IVR Variables and their associated values will be retrieved.
        /// </summary>
        public GetList() : base("switchvox.ivr.globalVariables.getList")
        {
            SetXml(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.IVR.GlobalVariables"/> class, where a single Global IVR Variable will be retrieved (identified by its Global IVR Variable ID)
        /// </summary>
        /// <param name="globalIVRVariableId">The Global IVR Variable ID of the Global IVR Variable to retrieve.</param>
        public GetList(int globalIVRVariableId) : base("switchvox.ivr.globalVariables.getList")
        {
            SetXml(new XElement("global_ivr_variable_id", globalIVRVariableId));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.IVR.GlobalVariables"/> class, where a single Global IVR Variable will be retrieved (identified by its Global IVR Variable Name)
        /// </summary>
        /// <param name="globalIVRVariableName">The Global IVR Varaible Name of the Global IVR Variable to retrieve.</param>
        public GetList(string globalIVRVariableName) : base("switchvox.ivr.globalVariables.getList")
        {
            SetXml(new XElement("global_ivr_variable_name", globalIVRVariableName));
        }
    }
}
