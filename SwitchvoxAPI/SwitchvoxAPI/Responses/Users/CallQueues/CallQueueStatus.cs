using System.Collections.Generic;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    [XmlRoot("callQueue")]
    public class CallQueueStatus
    {
        /// <summary>
        /// Account ID of the Call Queue.
        /// </summary>
        [XmlAttribute("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// Name of the Call Queue.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Extension of the Call Queue.
        /// </summary>
        [XmlAttribute("extension")]
        public string Extension { get; set; }

        [XmlElement("my_status")]
        public CallQueueStatusStatus Status { get; set; }

        [XmlElement("overview")]
        public CallQueueStatusOverview Overview { get; set; }

        [XmlElement("detailed_view")]
        public CallQueueStatusDetails _RawDetails { get; set; }

        [XmlIgnore]
        public List<CallQueueUserStatusMember> Members => _RawDetails.Members.Items;

        [XmlIgnore]
        public List<CallQueueStatusWaitingCall> CallsWaiting => _RawDetails.CallsWaiting.Items;


    }
}
