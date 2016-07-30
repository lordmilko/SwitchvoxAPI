using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallQueueStatusStatus
    {
        private string loginStatus;

        [XmlAttribute("login_status")]
        public string LoginStatus
        {
            get { return loginStatus; }
            set
            {
                loginStatus = value == string.Empty ? null : value;
            }
        }

        public int CurrentCallDuration => ConvertExtensions.ToInt32(_RawCurrentCallDuration);

        private string currentCallerIdName;

        [XmlAttribute("current_caller_id_name")]
        public string CurrentCallerIdName
        {
            get { return currentCallerIdName; }
            set
            {
                currentCallerIdName = value == string.Empty ? null : value;
            }
        }

        private string currentCallerIdNumber;

        [XmlAttribute("current_caller_id_number")]
        public string CurrentCallerIdNumber
        {
            get { return currentCallerIdNumber; }
            set
            {
                currentCallerIdNumber = value == string.Empty ? null : value;
            }
        }

        public int CallsTaken => ConvertExtensions.ToInt32(_RawCallsTaken);
        
        private string loginTime;

        //todo what data format should this be
        [XmlAttribute("login_time")]
        public string LoginTime
        {
            get { return loginTime; }
            set
            {
                loginTime = value == string.Empty ? null : value;
            }
        }

        public DateTime? LastCallTime => _RawLastCallTime == string.Empty ? (DateTime?)null : DateTime.Parse(_RawLastCallTime);

        public TimeSpan TotalTalkTime => ConvertExtensions.ToTimeSpan(_RawTotalTalkTime);

        public TimeSpan AverageTalkTime => ConvertExtensions.ToTimeSpan(_RawAverageTalkTime);

        public TimeSpan MaxTalkTime => ConvertExtensions.ToTimeSpan(_RawMaxTalkTime);

        public TimeSpan PausedTime => ConvertExtensions.ToTimeSpan(_RawPausedTime);

        [XmlAttribute("current_call_duration")]
        public string _RawCurrentCallDuration { get; set; }

        [XmlAttribute("calls_taken")]
        public string _RawCallsTaken { get; set; }

        [XmlAttribute("time_of_last_call")]
        public string _RawLastCallTime { get; set; }

        [XmlAttribute("total_talk_time")]
        public string _RawTotalTalkTime { get; set; }

        [XmlAttribute("avg_talk_time")]
        public string _RawAverageTalkTime { get; set; }

        [XmlAttribute("max_talk_time")]
        public string _RawMaxTalkTime { get; set; }

        [XmlAttribute("paused_time")]
        public string _RawPausedTime { get; set; }
    }
}
