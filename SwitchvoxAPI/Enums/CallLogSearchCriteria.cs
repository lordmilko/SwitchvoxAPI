using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SwitchvoxAPI.Attributes;

namespace SwitchvoxAPI
{
    public enum CallLogSearchCriteria
    {
        /// <summary>
        /// Perform a search using one or more Account IDs
        /// </summary>
        [XmlEnum("account_id")]
        [XmlEnumGroup("account_ids")]
        AccountId,

        /// <summary>
        /// Perform a search using one or more Channel Group IDs
        /// </summary>
        [XmlEnum("channel_group_id")]
        [XmlEnumGroup("channel_group_ids")]
        ChannelGroupId,

        /// <summary>
        /// Perform a search using one or more IAX Provider IDs
        /// </summary>
        [XmlEnum("iax_provider_id")]
        [XmlEnumGroup("iax_provider_ids")]
        IAXProviderId,

        /// <summary>
        /// Perform a search using one or more SIP Provider IDs
        /// </summary>
        [XmlEnum("sip_provider_id")]
        [XmlEnumGroup("sip_provider_ids")]
        SIPProviderId,

        /// <summary>
        /// Perform a search using a single Caller ID Name
        /// </summary>
        [XmlEnum("caller_id_name")]
        CallerIdName,

        /// <summary>
        /// Perform a search using a single Caller ID Number
        /// </summary>
        [XmlEnum("caller_id_number")]
        CallerIdNumber,

        /// <summary>
        /// Perform a search using a single Incoming DID
        /// </summary>
        [XmlEnum("incoming_did")]
        IncomingDID
    }
}
