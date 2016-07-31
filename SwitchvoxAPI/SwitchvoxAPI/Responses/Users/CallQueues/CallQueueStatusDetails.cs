using System.Xml.Serialization;
using SwitchvoxAPI.ListDeserializationLayers;

namespace SwitchvoxAPI
{
    [XmlRoot("detailed_view")]
    public class CallQueueStatusDetails
    {
        [XmlElement("queue_members")]
        public CallQueueStatusMembers Members { get; set; }

        [XmlElement("waiting_callers")]
        public CallQueueStatusWaitingCallers CallsWaiting { get; set; }
    }
}
