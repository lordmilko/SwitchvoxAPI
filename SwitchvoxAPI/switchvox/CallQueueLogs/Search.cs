using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using SwitchvoxAPI;

namespace Switchvox.CallQueueLogs
{
    /// <summary>
    /// Search the call queue logs on the phone system of one or more queues.
    /// </summary>
    public class Search : RequestMethod
    {
        /// <summary>
        /// Specifies how the search response should be sorted.
        /// </summary>
        public enum SortField
        {
            /// <summary>
            /// Sort each record returned by its "Start Time" attribute
            /// </summary>
            StartTime,

            /// <summary>
            /// Sort each record returned by its <see cref="Switchvox.CallQueueLogs.Search.CallTypes">"type"</see> attribute.
            /// </summary>
            Type,

            /// <summary>
            /// Sort each record returned by its "Queue Account ID" attribute.
            /// </summary>
            QueueAccountId,

            /// <summary>
            /// Sort each record returned by its "Caller ID Number" attribute.
            /// </summary>
            CallerIdNumber,

            /// <summary>
            /// Sort each record returned by its "Wait Time" attribute.
            /// </summary>
            WaitTime,

            /// <summary>
            /// Sort each record returned by its "Talk Time" attribute.
            /// </summary>
            TalkTime,

            /// <summary>
            /// Sort each record returned by its "Member Account ID" attribute.
            /// </summary>
            MemberAccountId,

            /// <summary>
            /// Sort each record returned by its "Enter Position" attribute.
            /// </summary>
            EnterPosition,

            /// <summary>
            /// Sort each record returned by its "Exit Position" attribute.
            /// </summary>
            ExitPosition,

            /// <summary>
            /// Sort each record returned by its "Abandon Position" attribute.
            /// </summary>
            AbandonPosition
        }

        /// <summary>
        /// Specifies the type of calls that can searched for.
        /// </summary>
        public enum CallTypes
        {
            /// <summary>
            /// Calls that were successfully answered by a member of the call queue.
            /// </summary>
            CompletedCalls,
            AbandonedCalls,
            RedirectedCalls
        }

        public Search(DateTime startDate, DateTime endDate, string[] queueAccountIds, CallTypes[] callTypes, bool ignoreWeekends = false, int itemsPerPage = 50, int pageNumber = 1, SortOrder sortOrder = SortOrder.Asc, SortField sortField = SortField.StartTime) : base("switchvox.callQueueLogs.search")
        {
            if (queueAccountIds.Length == 0)
                throw new NotImplementedException();

            if (callTypes.Length == 0)
                throw new NotImplementedException();

            List<XElement> xml = new List<XElement>
            {
                new XElement("start_date", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XElement("end_date",  endDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XElement("ignore_weekends", Convert.ToInt32(ignoreWeekends)),
                new XElement("queue_account_ids", CreateAccountIdElms(queueAccountIds)),
                new XElement("call_types", CreateCallTypeElms(callTypes)),
                new XElement("sort_field", GetSortField(sortField)),
                new XElement("sort_order", sortOrder.ToString()),
                new XElement("items_per_page", itemsPerPage),
                new XElement("page_number", pageNumber)
            };

            SetXml(xml);
        }

        private List<XElement> CreateAccountIdElms(string[] accountIds)
        {
            return accountIds.Select(accountId => new XElement("queue_account_id", accountId)).ToList();
        }

        private List<XElement> CreateCallTypeElms(CallTypes[] callTypes)
        {
            List<XElement> xml = new List<XElement>();

            foreach (CallTypes callType in callTypes)
            {
                string xmlVal = string.Empty;

                if (callType == CallTypes.AbandonedCalls)
                {
                    xmlVal = "abandoned_calls";
                }
                else if (callType == CallTypes.CompletedCalls)
                {
                    xmlVal = "completed_calls";
                }
                else if (callType == CallTypes.RedirectedCalls)
                {
                    xmlVal = "redirected_calls";
                }
                else
                    throw new NotImplementedException();

                xml.Add(new XElement("call_type", xmlVal));
            }

            return xml;
        }

        private string GetSortField(SortField sortField)
        {
            string val = string.Empty;

            switch (sortField)
            {
                case SortField.StartTime:
                    val = "start_time";
                    break;

                case SortField.Type:
                    val = "type";
                    break;

                case SortField.QueueAccountId:
                    val = "queue_account_id";
                    break;

                case SortField.CallerIdNumber:
                    val = "caller_id_number";
                    break;

                case SortField.WaitTime:
                    val = "wait_time";
                    break;

                case SortField.TalkTime:
                    val = "talk_time";
                    break;

                case SortField.MemberAccountId:
                    val = "member_account_id";
                    break;

                case SortField.EnterPosition:
                    val = "enter_position";
                    break;

                case SortField.ExitPosition:
                    val = "exit_position";
                    break;

                case SortField.AbandonPosition:
                    val = "abandon_position";
                    break;

                default:
                    throw new NotImplementedException();
            }

            return val;
        }
    }
}
