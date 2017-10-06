using System;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallQueueLog
    {
        [XmlAttribute("start_time")]
        public DateTime StartTime { get; set; }

        [XmlAttribute("type")]
        public QueueCallType Type { get; set; }

        [XmlAttribute("queue_account_id")]
        public string QueueAccountId { get; set; }

        [XmlAttribute("queue_name")]
        public string QueueName { get; set; }

        [XmlAttribute("queue_extension")]
        public string QueueExtension { get; set; }

        [XmlAttribute("caller_id_number")]
        public string CallerIdNumber { get; set; }

        [XmlAttribute("caller_id_name")]
        public string CallerIdName { get; set; }

        [XmlAttribute("origination")]
        public CallDirection Direction { get; set; }

        [XmlAttribute("wait_time")]
        public TimeSpan WaitTime { get; set; }

        [XmlAttribute("enter_position")]
        public int StartPosition { get; set; }

        [XmlAttribute("talk_time")]
        public TimeSpan TalkTime { get; set; }

        [XmlAttribute("member_account_id")]
        public string MemberAccountId { get; set; }

        [XmlAttribute("member_name")]
        public string MemberName { get; set; }

        [XmlAttribute("member_extension")]
        public string MemberExtension { get; set; }

        [XmlAttribute("member_misses")]
        public int MemberMisses { get; set; }

        [XmlAttribute("uniqueid")]
        public string UniqueId { get; set; }

        [XmlAttribute("result_type")]
        public string ResultType { get; set; }
    }
}
