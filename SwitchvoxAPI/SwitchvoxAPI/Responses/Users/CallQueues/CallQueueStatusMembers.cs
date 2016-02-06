using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallQueueStatusMember
    {
        [XmlAttribute("account_id")]
        public string AccountId { get; set; }

        [XmlAttribute("extension")]
        public string Extension { get; set; }

        [XmlAttribute("fullname")]
        public string FullName { get; set; }

        [XmlAttribute("order")]
        public int Order { get; set; }

        [XmlAttribute("loggedin_status")]
        public string LoggedInStatus { get; set; }

        [XmlAttribute("login_type")]
        public string LoginType { get; set; }

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

        private string talkingToNumber;

        [XmlAttribute("talking_to_number")]
        public string TalkingToNumber
        {
            get { return talkingToNumber; }
            set
            {
                talkingToNumber = value == string.Empty ? null : value;
            }
        }

        public DateTime? LastCallTime => _RawLastCallTime == string.Empty ? (DateTime?)null : DateTime.Parse(_RawLastCallTime); //todo what happens if this is an empty string? do we still need this to be nullable anyway?

        public int CompletedCalls => ConvertExtensions.ToInt32(_RawCompletedCalls);

        public int MissedCalls => ConvertExtensions.ToInt32(_RawMissedCalls);

        public TimeSpan AverageTalkTime => TimeSpan.FromSeconds(_RawAverageTalkTime);

        public TimeSpan MaxTalkTime => TimeSpan.FromSeconds(_RawMaxTalkTime);

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

        [XmlAttribute("call_duration")]
        public string CallDuration
        {
            get { return pausedTime; }
            set
            {
                pausedTime = value == string.Empty ? null : value;
            }
        }

        [XmlAttribute("time_of_last_call")]
        public string _RawLastCallTime { get; set; }

        [XmlAttribute("completed_calls")]
        public string _RawCompletedCalls { get; set; }

        [XmlAttribute("missed_calls")]
        public string _RawMissedCalls { get; set; }

        [XmlAttribute("avg_talk_time")]
        public int _RawAverageTalkTime { get; set; }

        [XmlAttribute("max_talk_time")]
        public int _RawMaxTalkTime { get; set; }
    }
}
