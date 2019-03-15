﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SwitchvoxAPI.Methods
{
    public partial class Extensions
    {
        private string tagGroup;
        private string tagInstance;

        /// <summary>
        /// Fetch basic information about the extensions on a phone system.
        /// </summary>
        /// <param name="identifier">A <see cref="ExtensionIdentifier"/> value indicating whether Extension numbers or Account IDs will be used to get info for the extensions on your system</param>
        /// <param name="values">A list of Extension Account IDs or Extension Numbers to get information for.</param>
        /// <returns>All extensions that match the specified search criteria.</returns>
        public List<ExtensionInfo> GetInfo(ExtensionIdentifier identifier, params string[] values)
        {
            if (values.Length == 0)
                throw new ArgumentException("At least one Extension Account ID or Extension Number must be specified.");

            SetTagTypes(identifier);

            List<XElement> valuesList = values.Select(val => new XElement(tagInstance, val)).ToList();

            var xml = new XElement(tagGroup, valuesList);

            var extensions = client.Execute<ListDeserializationLayers.Extensions>(new RequestMethod("switchvox.extensions.getInfo", xml)).Items;

            if (extensions.Count == 0)
                throw new SwitchvoxRequestException("No results for the given Account ID or Extension could be found.");

            return extensions;
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
                throw new NotImplementedException($"No handler for the value '{identifier}' has been implemented.");
        }
    }
}
