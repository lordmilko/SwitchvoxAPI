using System;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallQueueLog
    {
        public DateTime StartTime => DateTime.Parse(_RawStartTime);

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
        public CallOrigination Origination { get; set; }

        public TimeSpan WaitTime => TimeSpan.FromSeconds(_RawWaitTime);

        [XmlAttribute("enter_position")]
        public int StartPosition { get; set; }

        public TimeSpan TalkTime => TimeSpan.FromSeconds(_RawTalkTime);

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

        [XmlAttribute("start_time")]
        public string _RawStartTime { get; set; }

        [XmlAttribute("wait_time")]
        public int _RawWaitTime { get; set; }

        [XmlAttribute("talk_time")]
        public int _RawTalkTime { get; set; }
    }
}
