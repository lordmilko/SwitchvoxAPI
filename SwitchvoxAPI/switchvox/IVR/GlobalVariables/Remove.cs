using System.Xml.Linq;

namespace Switchvox.IVR.GlobalVariables
{
    /// <summary>
    /// Remove a Global IVR Variable from the system
    /// </summary>
    public class Remove : RequestMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.IVR.GlobalVariables.Remove"/> class.
        /// </summary>
        /// <param name="id">The Global IVR Variable ID of the Global IVR Variable you wish to remove</param>
        public Remove(int id) : base("switchvox.ivr.globalVariables.remove")
        {
            SetXml(new XElement("global_ivr_variable_id", id));
        }
    }
}
