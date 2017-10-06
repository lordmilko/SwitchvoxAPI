using System;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    [XmlRoot("overview")]
    public class CallQueueTodaysStatusOverview
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
        [XmlAttribute("avg_talk_time")]
        public TimeSpan AverageTalkTime { get; set; }

        /// <summary>
        /// Longest talk time of calls received in the Queue.
        /// </summary>
        [XmlAttribute("max_talk_time")]
        public TimeSpan MaxTalkTime { get; set; }

        [XmlAttribute("avg_wait_time")]
        public TimeSpan AverageWaitTime { get; set; }

        [XmlAttribute("max_wait_time")]
        public TimeSpan MaxWaitTime { get; set; }

        [XmlAttribute("avg_entry_position")]
        public double AverageEntryPosition { get; set; }

        [XmlAttribute("longest_queue_length")]
        public int LongestQueueLength { get; set; }

        [XmlAttribute("max_wait_time_abandoned_calls")]
        public TimeSpan MaxWaitTimeAbandonedCalls { get; set; }

        [XmlAttribute("avg_wait_time_abandoned_calls")]
        public TimeSpan AverageWaitTimeAbandonedCalls { get; set; }

        [XmlAttribute("max_wait_time_completed_calls")]
        public TimeSpan MaxWaitTimeCompletedCalls { get; set; }

        [XmlAttribute("avg_wait_time_completed_calls")]
        public TimeSpan AverageWaitTimeCompletedCalls { get; set; }

        [XmlAttribute("logged_in_count")]
        public int LoggedInCount { get; set; }
    }
}
