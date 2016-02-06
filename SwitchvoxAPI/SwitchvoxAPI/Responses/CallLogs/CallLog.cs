using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallLog
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("origination")]
        public CallOrigination Origination { get; set; }

        public DateTime StartTime => DateTime.Parse(_RawStartTime);

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

        public TimeSpan TotalDuration => TimeSpan.FromSeconds(_RawTotalDuration);

        public TimeSpan TalkDuration => TimeSpan.FromSeconds(_RawTalkDuration);

        [XmlAttribute("start_time")]
        public string _RawStartTime { get; set; }

        [XmlAttribute("cdr_call_id")]
        public string CDRId { get; set; }

        [XmlAttribute("total_duration")]
        public int _RawTotalDuration { get; set; }

        [XmlAttribute("talk_duration")]
        public int _RawTalkDuration { get; set; }

        [XmlIgnore]
        public List<CallLogEvent> Events => _RawEvents.CallLogEvents;

        [XmlElement("events")]
        public ListDeserializationLayer _RawEvents { get; set; }
    }
}
