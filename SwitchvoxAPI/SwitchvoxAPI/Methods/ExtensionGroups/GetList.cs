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
        public List<ExtensionGroupOverview> GetList(ExtensionGroupSortField sortField = ExtensionGroupSortField.Name, SortOrder sortOrder = SortOrder.Desc)
        {
            var xml = new[]
            {
                new XElement("sort_field", GetSortField(sortField)),
                new XElement("sort_order", sortOrder.ToString())
            };

            var response = client.Execute(new RequestMethod("switchvox.extensionGroups.getList", xml));

            return response.Deserialize<ListDeserializationLayers.ExtensionGroupOverviews>().Items;
        }

        private string GetSortField(ExtensionGroupSortField sortField)
        {
            var val = "";

            switch (sortField)
            {
                case ExtensionGroupSortField.Name:
                    val = "name";
                    break;

                case ExtensionGroupSortField.DateCreated:
                    val = "date_created";
                    break;

                case ExtensionGroupSortField.Id:
                    val = "id";
                    break;

                case ExtensionGroupSortField.MemberCount:
                    val = "member_count";
                    break;

                default:
                    throw new NotImplementedException("No handler for the value " + sortField.ToString() + " has been implemented.");
            }

            return val;
        }
    }
}
