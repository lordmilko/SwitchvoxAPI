using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SwitchvoxAPI.Methods
{
    public partial class Extensions
    {
        public List<ExtensionInfo> Search(string minExtension = null, string maxExtension = null, DateTime? minCreationDate = null,
            DateTime? maxCreationDate = null, int itemsPerPage = 50, int pageNumber = 1,
            params ExtensionType[] extensionTypes
        )
        {
            var valuesList = new List<XElement>();

            if (minExtension != null)
                valuesList.Add(new XElement("min_extension", minExtension));

            if (maxExtension != null)
                valuesList.Add(new XElement("max_extension", maxExtension));

            if (extensionTypes != null && extensionTypes.Length > 0)
                valuesList.Add(new XElement("extension_types", extensionTypes.ToList().Select(t => new XElement("extension_type", t.EnumToXml()))));

            if (minCreationDate != null)
                valuesList.Add(new XElement("min_create_date", minCreationDate.Value.ToString("YYYY-MM-DD hh:mm:ss")));

            if(maxCreationDate != null)
                valuesList.Add(new XElement("max_create_date", maxCreationDate.Value.ToString("YYYY-MM-DD hh:mm:ss")));

            if (itemsPerPage != 50)
                valuesList.Add(new XElement("items_per_page", itemsPerPage));

            if (pageNumber != 1)
                valuesList.Add(new XElement("page_number", pageNumber));

            var response = client.Execute<ListDeserializationLayers.Extensions>(new RequestMethod("switchvox.extensions.search", valuesList));
            return response.Items;
        }
    }
}
