using System.Collections.Generic;
using System.Xml.Linq;

namespace SwitchvoxAPI
{
    public partial class GlobalVariables
    {
        /// <summary>
        /// Get a list of Global IVR Variables in the system and their current values.
        /// </summary>
        public List<GlobalIVRVariable> GetList()
        {            
            return GetList((XElement)null);
        }

        /// <summary>
        /// Get a single Global IVR Variable by its ID Number.
        /// </summary>
        /// <param name="id">The Global IVR Variable ID of the Global IVR Variable to retrieve.</param>
        public List<GlobalIVRVariable> GetList(int id)
        {
            return GetList(new XElement("global_ivr_variable_id", id));
        }

        /// <summary>
        /// Get a single Global IVR Variable by its name.
        /// </summary>
        /// <param name="name">The Global IVR Varaible Name of the Global IVR Variable to retrieve.</param>
        public List<GlobalIVRVariable> GetList(string name)
        {
            return GetList(new XElement("global_ivr_variable_name", name));
        }

        private List<GlobalIVRVariable> GetList(XElement xml)
        {
            var response = client.Execute(new RequestMethod("switchvox.ivr.globalVariables.getList", xml));

            return response.Deserialize<ListDeserializationLayers.GlobalIVRVariables>().Items;
        }
    }
}
