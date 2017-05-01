using System.Collections.Generic;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    [XmlRoot("callQueue")]
    public class CallQueueTodaysStatus
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
        public CallQueueTodaysStatusStatus Status { get; set; }

        [XmlElement("overview")]
        public CallQueueTodaysStatusOverview Overview { get; set; }

        [XmlElement("detailed_view")]
        public CallQueueTodaysStatusDetails _RawDetails { get; set; }

        [XmlIgnore]
        public List<CallQueueTodaysStatusMember> Members => _RawDetails.Members.Items;

        [XmlIgnore]
        public List<CallQueueTodaysStatusWaitingCall> CallsWaiting => _RawDetails.CallsWaiting.Items;


    }
}
