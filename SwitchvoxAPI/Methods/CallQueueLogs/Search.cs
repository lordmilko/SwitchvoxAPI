using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SwitchvoxAPI.Methods
{
    public partial class CallQueueLogs
    {
        //http://developers.digium.com/switchvox/wiki/index.php/Switchvox.callQueueLogs.search

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
        public CallLogs<CallQueueLog> Search(DateTime startDate, DateTime endDate, string queueAccountId, CallTypes callTypes = CallTypes.AllCalls, bool ignoreWeekends = false, int itemsPerPage = 50, int pageNumber = 1)
        {
            return Search(startDate, endDate, new[] { queueAccountId }, callTypes, ignoreWeekends, itemsPerPage, pageNumber);
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
        public CallLogs<CallQueueLog> Search(DateTime startDate, DateTime endDate, string[] queueAccountIds, CallTypes callTypes = CallTypes.AllCalls, bool ignoreWeekends = false, int itemsPerPage = 50, int pageNumber = 1)
        {
            if (queueAccountIds.Length == 0)
                throw new ArgumentException("At least one queue account ID must be specified", nameof(queueAccountIds));

            var method = GetRequestMethod(startDate, endDate, ignoreWeekends, queueAccountIds, callTypes, itemsPerPage, pageNumber);

            var response = client.Execute<CallLogs<CallQueueLog>>(method);

            return response;
        }

        /// <summary>
        /// Search for call logs against one or more call queues, automatically requesting additional pages as the results are enumerated.
        /// </summary>
        /// <param name="startDate">The minimum date to search from.</param>
        /// <param name="endDate">The maximum date to search to.</param>
        /// <param name="queueAccountIds">A list of Call Queue Account IDs to retrieve data for. At least 1 Account ID must be specified.</param>
        /// <param name="callTypes">A combination of flags indicating the type of calls to include in the search results.</param>
        /// <param name="ignoreWeekends">Whether weekends should be excluded from the search results.</param>
        /// <param name="itemsPerPage">The number of results to return in each response. SwitchvoxAPI will automatically request additional items as required as the results are enumerated.</param>
        public IEnumerable<CallQueueLog> StreamSearch(DateTime startDate, DateTime endDate, string[] queueAccountIds, CallTypes callTypes = CallTypes.AllCalls, bool ignoreWeekends = false, int itemsPerPage = 50)
        {
            var method = GetRequestMethod(startDate, endDate, ignoreWeekends, queueAccountIds, callTypes, itemsPerPage, 1);

            var response = client.Stream<CallLogs<CallQueueLog>, CallQueueLog>(method, GetTotalItems, SetNextPage, itemsPerPage, r => r.Items);

            return response;
        }

        private RequestMethod GetRequestMethod(DateTime startDate, DateTime endDate, bool ignoreWeekends, string[] queueAccountIds, CallTypes callTypes, int itemsPerPage, int pageNumber)
        {
            List<XElement> xml = new List<XElement>
            {
                new XElement("start_date", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XElement("end_date",  endDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XElement("ignore_weekends", Convert.ToInt32(ignoreWeekends)),
                new XElement("queue_account_ids", CreateAccountIdElms(queueAccountIds)),
                new XElement("call_types", CreateCallTypeElms(callTypes)),
                new XElement("items_per_page", itemsPerPage),
                new XElement("page_number", pageNumber)
            };

            return new RequestMethod("switchvox.callQueueLogs.search", xml);
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

        private void SetNextPage(RequestMethod method, int pageNumber)
        {
            method.Xml.Descendants("page_number").First().Value = pageNumber.ToString();
        }

        private string GetTotalItems(XDocument xDoc)
        {
            return xDoc.Descendants("calls").First().Attribute("total_items").Value;
        }
    }
}
