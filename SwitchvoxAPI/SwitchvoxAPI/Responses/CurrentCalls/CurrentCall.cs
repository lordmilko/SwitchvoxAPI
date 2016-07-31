using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CurrentCall
    {
        [XmlAttribute("state")]
        public CurrentCallState State { get; set; }

        public DateTime? StartTime => string.IsNullOrEmpty(_RawStartTime) ? null : (DateTime?)DateTime.Parse(_RawStartTime);

        public TimeSpan Duration => TimeSpan.FromSeconds(_RawDuration);

        [XmlAttribute("to_caller_id_name")]
        public string ToCallerIdName { get; set; }

        [XmlAttribute("to_caller_id_number")]
        public string ToCallerIdNumber { get; set; }

        [XmlAttribute("from_caller_id_number")]
        public string FromCallerIdNumber { get; set; }

        [XmlAttribute("from_caller_id_name")]
        public string FromCallerIdName { get; set; }

        [XmlAttribute("format")]
        public string Format { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        public int? LotNum => _RawLotNum == null ? (int?)null : Convert.ToInt32(_RawLotNum);

        [XmlAttribute("start_time")]
        public string _RawStartTime { get; set; }

        [XmlAttribute("duration")]
        public int _RawDuration { get; set; }

        [XmlAttribute("lotnum")]
        public string _RawLotNum { get; set; }
    }
}
