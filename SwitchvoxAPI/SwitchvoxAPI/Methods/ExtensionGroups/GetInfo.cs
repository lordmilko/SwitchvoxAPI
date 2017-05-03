using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwitchvoxAPI
{
    public partial class ExtensionGroups
    {
        /// <summary>
        /// Retrieve extension group information and a list of extensions each group contains.
        /// </summary>
        /// <param name="extensionGroupId">The ID of the extension group to retrieve.</param>
        /// <returns></returns>
        public ExtensionGroup GetInfo(string extensionGroupId)
        {
            var response = client.Execute(new RequestMethod("switchvox.extensionGroups.getInfo", new XElement("extension_group_id", extensionGroupId)));

            return response.Deserialize<ExtensionGroup>();
        }
    }
}
