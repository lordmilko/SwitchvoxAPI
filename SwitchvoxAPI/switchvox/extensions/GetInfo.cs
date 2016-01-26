using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using SwitchvoxAPI;

namespace SwitchvoxAPI
{
    public partial class Extensions
    {
        private string tagGroup;
        private string tagInstance;

        public List<Extension> GetInfo(ExtensionIdentifier identifier, params string[] values)
        {
            if (values.Length == 0)
                throw new ArgumentException("At least one Extension Account ID or Extension Number must be specified.");

            SetTagTypes(identifier);

            List<XElement> valuesList = values.Select(val => new XElement(tagInstance, val)).ToList();

            var xml = new XElement(tagGroup, valuesList);

            var response = request.Execute(new Switchvox.RequestMethod("switchvox.extensions.getInfo", xml));

            return response.Deserialize("extensions").Extensions; ;
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
                throw new NotImplementedException("No handler for the value " + identifier.ToString() + " has been implemented.");
        }
    }
}
