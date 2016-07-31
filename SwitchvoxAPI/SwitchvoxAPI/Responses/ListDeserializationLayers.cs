using System.Collections.Generic;
using System.Xml.Serialization;

namespace SwitchvoxAPI.ListDeserializationLayers
{
    [XmlRoot("extensions")]
    public class Extensions
    {
        [XmlElement("extension")]
        public List<Extension> Items { get; set; }
    }

    [XmlRoot("queue_members")]
    public class CallQueueStatusMembers
    {
        [XmlElement("queue_member")]
        public List<CallQueueUserStatusMember> Items { get; set; }
    }

    [XmlRoot("waiting_callers")]
    public class CallQueueStatusWaitingCallers
    {
        [XmlElement("waiting_caller")]
        public List<CallQueueStatusWaitingCall> Items { get; set; }
    }

    public class CallLogEvents
    {
        [XmlElement("event")]
        public List<CallLogEvent> Items { get; set; }
    }  

    [XmlRoot("current_calls")]
    public class CurrentCalls
    {
        [XmlElement("current_call")]
        public List<CurrentCall> Items { get; set; }
    }

    [XmlRoot("global_ivr_variables")]
    public class GlobalIVRVariables
    {
        [XmlElement("global_ivr_variable")]
        public List<GlobalIVRVariable> Items { get; set; }
    }
}
