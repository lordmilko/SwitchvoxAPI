using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

using SwitchvoxAPI;

namespace Switchvox.Extensions
{
    /// <summary>
    /// Fetch basic information about the extensions on a PBX.
    /// </summary>
    public class GetInfo : RequestMethod
    {
        private string tagGroup;
        private string tagInstance;

        /// <summary>
        /// Initializes a new instance of the Switchvox.Extensions.GetInfo class.
        /// </summary>
        /// <param name="identifier">A SwitchvoxAPI.ExtensionIdentifier value indicating whether Extension numbers or Account IDs will be used to get info for the extensions on your system</param>
        /// <param name="values">A list of Extension Account IDs or Extension Numbers to get information for</param>
        public GetInfo(ExtensionIdentifier identifier, string[] values) : base("switchvox.extensions.getInfo")
        {
            //what happens if values has no entries?
            SetTagTypes(identifier);

            List<XElement> valuesList = new List<XElement>();

            foreach (var val in values)
            {
                valuesList.Add(new XElement(tagInstance, val));
            }

            var xml = new XElement(tagGroup, valuesList);

            SetXml(xml);
        }

        private void SetTagTypes(ExtensionIdentifier identifier)
        {
            if (identifier == ExtensionIdentifier.AccountID)
            {
                tagGroup = "account_ids";
                tagInstance = "account_id";
            }
            else if (identifier == ExtensionIdentifier.Extension)
            {
                tagGroup = "extensions";
                tagInstance = "extension";
            }
            else
                throw new NotImplementedException();
        }
    }
}
