using System.Xml.Linq;

namespace SwitchvoxAPI
{
    public partial class GlobalVariables
    {
        /// <summary>
        /// Remove a Global IVR Variable from the system
        /// </summary>
        /// <param name="id">The Global IVR Variable ID of the Global IVR Variable you wish to remove</param>
        public void Remove(int id)
        {
            var xml = new XElement("global_ivr_variable_id", id);

            var response = client.Execute(new RequestMethod("switchvox.ivr.globalVariables.remove", xml));
        }
    }
}
