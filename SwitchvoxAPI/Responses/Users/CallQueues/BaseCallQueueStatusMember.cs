using System;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public abstract class BaseCallQueueStatusMember
    {
        [XmlAttribute("account_id")]
        public string AccountId { get; set; }

        [XmlAttribute("extension")]
        public string Extension { get; set; }

        [XmlAttribute("fullname")]
        public string FullName { get; set; }

        [XmlAttribute("order")]
        public int Order { get; set; }

        //[XmlAttribute("loggedin_status")]
        //public string LoggedInStatus { get; set; }

        [XmlAttribute("login_type")]
        public QueueLoginType LoginType { get; set; }

        private string talkingToName;

        [XmlAttribute("talking_to_name")]
        public string TalkingToName
        {
            get { return talkingToName; }
            set
            {
                talkingToName = value == string.Empty ? null : value;
            }
        }

        [XmlAttribute("talking_to_number")]
        public string TalkingToNumber { get; set; }

        [XmlAttribute("time_of_last_call")]
        public DateTime? LastCallTime { get; set; }

        [XmlAttribute("completed_calls")]
        public int CompletedCalls { get; set; }

        [XmlAttribute("missed_calls")]
        public int MissedCalls { get; set; }

        [XmlAttribute("avg_talk_time")]
        public TimeSpan AverageTalkTime { get; set; }

        [XmlAttribute("max_talk_time")]
        public TimeSpan MaxTalkTime { get; set; }

        //todo: what data format should this be
        private string loginTime;

        [XmlAttribute("login_time")]
        public string LoginTime
        {
            get { return loginTime; }
            set
            {
                loginTime = value == string.Empty ? null : value;
            }
        }

        private string pausedTime;

        //todo: shouldnt this be a duration?
        [XmlAttribute("paused_time")]
        public string PausedTime
        {
            get { return pausedTime; }
            set
            {
                pausedTime = value == string.Empty ? null : value;
            }
        }

        private string callDuration;

        /*//todo: should this be a timespan, and should we check all call durations in the api
        [XmlAttribute("call_duration")]
        public string CallDuration
        {
            get { return callDuration; }
            set
            {
                callDuration = value == string.Empty ? null : value;
            }
        }*/
    }
}
