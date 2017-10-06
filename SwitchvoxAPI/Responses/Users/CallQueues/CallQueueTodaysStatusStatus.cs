using System;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    [XmlRoot("my_status")]
    public class CallQueueTodaysStatusStatus
    {
        [XmlAttribute("login_status")]
        public string LoginStatus { get; set; }

        [XmlAttribute("current_call_duration")]
        public int CurrentCallDuration { get; set; }

        [XmlAttribute("current_caller_id_name")]
        public string CurrentCallerIdName { get; set; }

        [XmlAttribute("current_caller_id_number")]
        public string CurrentCallerIdNumber { get; set; }

        [XmlAttribute("calls_taken")]
        public int CallsTaken { get; set; }
        
        //todo what data format should this be
        [XmlAttribute("login_time")]
        public string LoginTime { get; set; }

        [XmlAttribute("time_of_last_call")]
        public DateTime? LastCallTime { get; set; }

        [XmlAttribute("total_talk_time")]
        public TimeSpan TotalTalkTime { get; set; }

        [XmlAttribute("avg_talk_time")]
        public TimeSpan AverageTalkTime { get; set; }

        [XmlAttribute("max_talk_time")]
        public TimeSpan MaxTalkTime { get; set; }

        [XmlAttribute("paused_time")]
        public TimeSpan PausedTime { get; set; }
    }
}
