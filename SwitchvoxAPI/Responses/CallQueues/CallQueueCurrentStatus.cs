using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SwitchvoxAPI.ListDeserializationLayers;

namespace SwitchvoxAPI
{
    [XmlRoot("call_queue")]
    public class CallQueueCurrentStatus
    {
        /// <summary>
        /// Account ID of the Call Queue.
        /// </summary>
        [XmlAttribute("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// Extension of the Call Queue.
        /// </summary>
        [XmlAttribute("extension")]
        public string Extension { get; set; }

        [XmlAttribute("call_queue_name")]
        public string Name { get; set; }

        [XmlAttribute("strategy")]
        public CallQueueStrategy Strategy;

        [XmlElement("queue_members")]
        protected CallQueueStatusMembers<CallQueueCurrentStatusMember> members { get; set; } //this will contain a list of things with attribute queue_member. should this class inherit from

        [XmlIgnore]
        public List<CallQueueCurrentStatusMember> Members => members.Items;
    }
}
