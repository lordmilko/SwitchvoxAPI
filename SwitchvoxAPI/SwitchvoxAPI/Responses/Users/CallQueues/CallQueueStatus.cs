using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallQueueStatus
    {
        [XmlAttribute("account_id")]
        public string AccountId { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("extension")]
        public string Extension { get; set; }

        [XmlElement("my_status")]
        public CallQueueStatusStatus Status { get; set; }

        [XmlElement("overview")]
        public CallQueueStatusOverview Overview { get; set; }

        [XmlElement("detailed_view")]
        public CallQueueStatusDetails _RawDetails { get; set; }

        [XmlIgnore]
        public List<CallQueueStatusMember> Members => _RawDetails.Members.CallQueueStatusMembers;

        [XmlIgnore]
        public List<CallQueueStatusWaitingCall> CallsWaiting => _RawDetails.CallsWaiting.CallQueueStatusWaitingCallers;


    }
}
