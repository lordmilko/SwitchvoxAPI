using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    [XmlRoot("detailed_view")]
    public class CallQueueStatusDetails
    {
        [XmlElement("queue_members")]
        public ListDeserializationLayer Members { get; set; }

        [XmlElement("waiting_callers")]
        public ListDeserializationLayer CallsWaiting { get; set; }
    }
}
