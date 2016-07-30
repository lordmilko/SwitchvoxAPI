using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallQueueStatusOverview
    {
        /// <summary>
        /// Number of calls currently waiting to be answered.
        /// </summary>
        [XmlAttribute("callers_waiting")]
        public int CallersWaiting { get; set; }

        /// <summary>
        /// Number of Queue Members currently in a call.
        /// </summary>
        [XmlAttribute("members_on_call")]
        public int MembersOnCall { get; set; }

        /// <summary>
        /// Total number of Queue Members in the Call Queue.
        /// </summary>
        [XmlAttribute("total_members")]
        public int TotalMembers { get; set; }

        /// <summary>
        /// Total number of calls that have been received by the Call Queue.
        /// </summary>
        [XmlAttribute("total_calls")]
        public int TotalCalls { get; set; }

        /// <summary>
        /// Number of calls that were hung up before being answered.
        /// </summary>
        [XmlAttribute("abandoned_calls")]
        public int AbandonedCalls { get; set; }

        /// <summary>
        /// Number of calls that were redirected/forwarded before being answered.
        /// </summary>
        [XmlAttribute("redirected_calls")]
        public int RedirectedCalls { get; set; }

        /// <summary>
        /// Number of calls that have been answered today.
        /// </summary>
        [XmlAttribute("completed_calls_today")]
        public int CallsCompletedToday { get; set; }

        /// <summary>
        /// Average talk time of calls received in the Queue.
        /// </summary>
        public TimeSpan AverageTalkTime => ConvertExtensions.ToTimeSpan(_RawAverageTalkTime);

        /// <summary>
        /// Longest talk time of calls received in the Queue.
        /// </summary>
        public TimeSpan MaxTalkTime => ConvertExtensions.ToTimeSpan(_RawMaxTalkTime);

        public TimeSpan AverageWaitTime => ConvertExtensions.ToTimeSpan(_RawAverageWaitTime);
        public TimeSpan MaxWaitTime => ConvertExtensions.ToTimeSpan(_RawMaxWaitTime);

        public double AverageEntryPosition => ConvertExtensions.ToDouble(_RawAverageEntryPosition);

        [XmlAttribute("longest_queue_length")]
        public int LongestQueueLength { get; set; }
        
        public TimeSpan MaxWaitTimeAbandonedCalls => ConvertExtensions.ToTimeSpan(_RawMaxWaitTimeAbandonedCalls);        
        public TimeSpan AverageWaitTimeAbandonedCalls => ConvertExtensions.ToTimeSpan(_RawAverageWaitTimeAbandonedCalls);

        public TimeSpan MaxWaitTimeCompletedCalls => ConvertExtensions.ToTimeSpan(_RawMaxWaitTimeCompletedCalls);
        public TimeSpan AverageWaitTimeCompletedCalls => ConvertExtensions.ToTimeSpan(_RawAverageWaitTimeCompletedCalls);

        [XmlAttribute("logged_in_count")]
        public int LoggedInCount { get; set; }

        [XmlAttribute("avg_talk_time")]
        public string _RawAverageTalkTime { get; set; }

        [XmlAttribute("max_talk_time")]
        public string _RawMaxTalkTime { get; set; }

        [XmlAttribute("avg_wait_time")]
        public string _RawAverageWaitTime { get; set; }

        [XmlAttribute("max_wait_time")]
        public string _RawMaxWaitTime { get; set; }

        [XmlAttribute("avg_entry_position")]
        public string _RawAverageEntryPosition { get; set; }

        [XmlAttribute("max_wait_time_abandoned_calls")]
        public string _RawMaxWaitTimeAbandonedCalls { get; set; }

        [XmlAttribute("avg_wait_time_abandoned_calls")]
        public string _RawAverageWaitTimeAbandonedCalls { get; set; }

        [XmlAttribute("max_wait_time_completed_calls")]
        public string _RawMaxWaitTimeCompletedCalls { get; set; }

        [XmlAttribute("avg_wait_time_completed_calls")]
        public string _RawAverageWaitTimeCompletedCalls { get; set; }
    }
}
