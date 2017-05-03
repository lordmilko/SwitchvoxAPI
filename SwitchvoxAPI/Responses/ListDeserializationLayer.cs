using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SwitchvoxAPI;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Intermediate layer used for deserializing lists of elements.
    /// </summary>
    public class ListDeserializationLayer
    {
        [XmlElement("extension")]
        public List<Extension> Extensions { get; set; }

        [XmlElement("queue_member")]
        public List<CallQueueUserStatusMember> CallQueueStatusMembers { get; set; }

        [XmlElement("waiting_caller")]
        public List<CallQueueStatusWaitingCall> CallQueueStatusWaitingCallers { get; set; }

        [XmlElement("event")]
        public List<CallLogEvent> CallLogEvents { get; set; }

        [XmlElement("current_call")]
        public List<CurrentCall> CurrentCalls { get; set; }
    }
}
