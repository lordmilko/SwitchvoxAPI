using System.Xml.Serialization;
using SwitchvoxAPI.ListDeserializationLayers;

namespace SwitchvoxAPI
{
    [XmlRoot("detailed_view")]
    public class CallQueueTodaysStatusDetails
    {
        [XmlElement("queue_members")]
        public CallQueueStatusMembers<CallQueueTodaysStatusMember> Members { get; set; }

        [XmlElement("waiting_callers")]
        public CallQueueTodaysStatusWaitingCallers CallsWaiting { get; set; }
    }
}
