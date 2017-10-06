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
        /// Retrieve a list of extension groups on the system.
        /// </summary>
        /// <returns></returns>
        public List<ExtensionGroupOverview> GetList()
        {
            var response = client.Execute<ListDeserializationLayers.ExtensionGroupOverviews>(new RequestMethod("switchvox.extensionGroups.getList", null));

            return response.Items;
        }
    }
}
