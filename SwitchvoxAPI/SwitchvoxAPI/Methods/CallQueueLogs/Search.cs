using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SwitchvoxAPI.Methods
{
    public partial class CallQueueLogs
    {
        /// <summary>
        /// Search for call logs against a single call queue.
        /// </summary>
        /// <param name="startDate">The minimum date to search from.</param>
        /// <param name="endDate">The maximum date to search to.</param>
        /// <param name="queueAccountId">The Call Queue Account ID to retrieve data for.</param>
        /// <param name="callTypes">A combination of flags indicating the type of calls to include in the search results.</param>
        /// <param name="ignoreWeekends">Whether weekends should be excluded from the search results.</param>
        /// <param name="itemsPerPage">The number of results to return in this request. Additional items can be retrieved by making additional requests and incrementing the pageNumber parameter</param>
        /// <param name="pageNumber">The page of results to return in this request. Used in conjunction with the itemsPerPage parameter.</param>
        /// <param name="sortOrder">How the search results will be sorted.</param>
        /// <param name="sortField">The field of the search results to sort on</param>
        public CallLogs<CallQueueLog> Search(DateTime startDate, DateTime endDate, string queueAccountId, CallTypes callTypes, bool ignoreWeekends = false, int itemsPerPage = 50, int pageNumber = 1, SortOrder sortOrder = SortOrder.Asc, CallQueueLogSortField sortField = CallQueueLogSortField.StartTime)
        {
            return Search(startDate, endDate, new[] { queueAccountId }, callTypes, ignoreWeekends, itemsPerPage, pageNumber, sortOrder, sortField);
        }

        /// <summary>
        /// Search for call logs against one or more call queues.
        /// </summary>
        /// <param name="startDate">The minimum date to search from.</param>
        /// <param name="endDate">The maximum date to search to.</param>
        /// <param name="queueAccountIds">A list of Call Queue Account IDs to retrieve data for. At least 1 Account ID must be specified.</param>
        /// <param name="callTypes">A combination of flags indicating the type of calls to include in the search results.</param>
        /// <param name="ignoreWeekends">Whether weekends should be excluded from the search results.</param>
        /// <param name="itemsPerPage">The number of results to return in this request. Additional items can be retrieved by making additional requests and incrementing the pageNumber parameter</param>
        /// <param name="pageNumber">The page of results to return in this request. Used in conjunction with the itemsPerPage parameter.</param>
        /// <param name="sortOrder">How the search results will be sorted.</param>
        /// <param name="sortField">The field of the search results to sort on</param>
        public CallLogs<CallQueueLog> Search(DateTime startDate, DateTime endDate, string[] queueAccountIds, CallTypes callTypes, bool ignoreWeekends = false, int itemsPerPage = 50, int pageNumber = 1, SortOrder sortOrder = SortOrder.Asc, CallQueueLogSortField sortField = CallQueueLogSortField.StartTime)
        {
            if (queueAccountIds.Length == 0)
                throw new ArgumentException();

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

            var response = client.Execute<CallLogs<CallQueueLog>>(new RequestMethod("switchvox.callQueueLogs.search", xml));

            return response;
        }

        private List<XElement> CreateAccountIdElms(string[] accountIds)
        {
            return accountIds.Select(accountId => new XElement("queue_account_id", accountId)).ToList();
        }

        private List<XElement> CreateCallTypeElms(CallTypes callTypes)
        {
            List<XElement> xml = new List<XElement>();

            if ((callTypes & CallTypes.AbandonedCalls) == CallTypes.AbandonedCalls)
            {
                xml.Add(new XElement("call_type", "abandoned_calls"));
            }
            if ((callTypes & CallTypes.CompletedCalls) == CallTypes.CompletedCalls)
            {
                xml.Add(new XElement("call_type", "completed_calls"));
            }
            if ((callTypes & CallTypes.RedirectedCalls) == CallTypes.RedirectedCalls)
            {
                xml.Add(new XElement("call_type", "redirected_calls"));
            }

            return xml;
        }

        private string GetSortField(CallQueueLogSortField sortField)
        {
            string val = string.Empty;

            switch (sortField)
            {
                case CallQueueLogSortField.StartTime:
                    val = "start_time";
                    break;

                case CallQueueLogSortField.Type:
                    val = "type";
                    break;

                case CallQueueLogSortField.QueueAccountId:
                    val = "queue_account_id";
                    break;

                case CallQueueLogSortField.CallerIdNumber:
                    val = "caller_id_number";
                    break;

                case CallQueueLogSortField.WaitTime:
                    val = "wait_time";
                    break;

                case CallQueueLogSortField.TalkTime:
                    val = "talk_time";
                    break;

                case CallQueueLogSortField.MemberAccountId:
                    val = "member_account_id";
                    break;

                case CallQueueLogSortField.EnterPosition:
                    val = "enter_position";
                    break;

                case CallQueueLogSortField.ExitPosition:
                    val = "exit_position";
                    break;

                case CallQueueLogSortField.AbandonPosition:
                    val = "abandon_position";
                    break;

                default:
                    throw new NotImplementedException("No handler for the value " + sortField.ToString() + " has been implemented.");
            }

            return val;
        }
    }
}
