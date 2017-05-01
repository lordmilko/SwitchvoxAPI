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
        public ExtensionGroup GetInfo(string extensionGroupId)
        {
            var response = client.Execute(new RequestMethod("switchvox.extensionGroups.getInfo", new XElement("extension_group_id", extensionGroupId)));

            return response.Deserialize<ExtensionGroup>();
        }
    }
}
