using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SwitchvoxAPI;

namespace Switchvox.Extensions
{
    /// <summary>
    /// Fetch basic information about the extensions on a phone system.
    /// </summary>
    public class GetInfo : RequestMethod
    {
        private string tagGroup;
        private string tagInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.Extensions.GetInfo"/> class with a single Extension Account ID or Extension Number value.
        /// </summary>
        /// <param name="identifier">A <see cref="T:Switchvox.ExtensionIdentifier"/> value indicating whether Extension numbers or Account IDs will be used to get info for the extensions on your system</param>
        /// <param name="value">An Extension Account ID or Extension Number to get information for.</param>
        public GetInfo(ExtensionIdentifier identifier, string value) : this(identifier, new[] { value })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.Extensions.GetInfo"/> class with one or more Extension Account ID or Extension Number values.
        /// </summary>
        /// <param name="identifier">A <see cref="T:Switchvox.ExtensionIdentifier"/> value indicating whether Extension numbers or Account IDs will be used to get info for the extensions on your system</param>
        /// <param name="values">A list of Extension Account IDs or Extension Numbers to get information for</param>
        public GetInfo(ExtensionIdentifier identifier, string[] values) : base("switchvox.extensions.getInfo")
        {
            if (values.Length == 0)
                throw new ArgumentException("At least one Extension Account ID or Extension Number must be specified.");

            SetTagTypes(identifier);

            List<XElement> valuesList = values.Select(val => new XElement(tagInstance, val)).ToList();

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
                throw new NotImplementedException("No handler for the value " + identifier.ToString() + " has been implemented.");
        }
    }
}
