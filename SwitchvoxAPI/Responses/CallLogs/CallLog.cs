using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallLog
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("origination")]
        public CallDirection Direction { get; set; }

        [XmlAttribute("start_time")]
        public DateTime StartTime { get; set; }

        [XmlAttribute("from")]
        public string From { get; set; }

        [XmlAttribute("from_account_id")]
        public string FromAccountId { get; set; }

        [XmlAttribute("from_name")]
        public string FromName { get; set; }

        [XmlAttribute("from_number")]
        public string FromNumber { get; set; }

        [XmlAttribute("to")]
        public string To { get; set; }

        [XmlAttribute("to_account_id")]
        public string ToAccountId { get; set; }

        [XmlAttribute("to_name")]
        public string ToName { get; set; }

        [XmlAttribute("to_number")]
        public string ToNumber { get; set; }

        [XmlAttribute("total_duration")]
        public TimeSpan TotalDuration { get; set; }

        [XmlAttribute("talk_duration")]
        public TimeSpan TalkDuration { get; set; }

        [XmlAttribute("cdr_call_id")]
        public string CDRId { get; set; }

        [XmlIgnore]
        public List<CallLogEvent> Events => events.Items;

        [XmlElement("events")]
        protected ListDeserializationLayers.CallLogEvents events { get; set; }

        public override string ToString()
        {
            return $"{FromName} => {ToName ?? ToNumber}";
        }
    }
}
