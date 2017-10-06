using System;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CurrentCall
    {
        [XmlAttribute("state")]
        public CurrentCallState State { get; set; }

        [XmlAttribute("start_time")]
        public DateTime? StartTime { get; set; }

        [XmlAttribute("duration")]
        public TimeSpan Duration { get; set; }

        [XmlAttribute("to_caller_id_name")]
        public string ToName { get; set; }

        [XmlAttribute("to_caller_id_number")]
        public string ToNumber { get; set; }

        [XmlAttribute("from_caller_id_name")]
        public string FromName { get; set; }

        [XmlAttribute("from_caller_id_number")]
        public string FromNumber { get; set; }

        [XmlAttribute("format")]
        public string Format { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("lotnum")]
        public int? LotNum { get; set; }
    }
}
