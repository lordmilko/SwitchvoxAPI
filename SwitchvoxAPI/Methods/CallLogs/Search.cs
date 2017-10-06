using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using SwitchvoxAPI.Attributes;

namespace SwitchvoxAPI.Methods
{
    //http://developers.digium.com/switchvox/wiki/index.php/Switchvox.callLogs.search

    public partial class CallLogs
    {
        /// <summary>
        /// Search the call logs of a Switchvox Phone System.
        /// </summary>
        /// <param name="startDate">The start date/time to search from.</param>
        /// <param name="endDate">The end date/time to search to.</param>
        /// <param name="itemsPerPage">The number of items to return in the response. Additional records can be retrieved by increasing the <paramref name="pageNumber"/> and re-calling this method.</param>
        /// <param name="pageNumber">The page of the response to return. Increasing this value allows the caller to retrieve additional records from the system that exceeded the <paramref name="itemsPerPage"/> of previous requests.</param>
        /// <param name="searchCriteria">An optional search criteria type used to filter the results</param>
        /// <param name="searchValues">The search criterion used to filter the results. If the <paramref name="searchCriteria"/> specified does not support multiple values this value must contain a single value.</param>
        /// <returns></returns>
        public CallLogs<CallLog> Search(DateTime startDate, DateTime endDate, int itemsPerPage = 50, int pageNumber = 1, CallLogSearchCriteria? searchCriteria = null, params string[] searchValues)
        {
            var method = GetRequestMethod(startDate, endDate, itemsPerPage, pageNumber, searchCriteria, searchValues);

            var response = client.Execute<CallLogs<CallLog>>(method);

            return response;
        }

        /// <summary>
        /// Search the call logs of a Switchvox Phone System, automatically requesting additional pages as the results are enumerated.
        /// </summary>
        /// <param name="startDate">The start date/time to search from.</param>
        /// <param name="endDate">The end date/time to search to.</param>
        /// <param name="itemsPerPage">The number of items to return in each response. SwitchvoxAPI will automatically request additional items as required as the results are enumerated.
        /// <param name="searchCriteria">An optional search criteria type used to filter the results</param>
        /// <param name="searchValues">The search criterion used to filter the results. If the <paramref name="searchCriteria"/> specified does not support multiple values this value must contain a single value.</param>
        /// <returns></returns>
        public IEnumerable<CallLog> StreamSearch(DateTime startDate, DateTime endDate, int itemsPerPage = 50, CallLogSearchCriteria? searchCriteria = null, params string[] searchValues)
        {
            var method = GetRequestMethod(startDate, endDate, itemsPerPage, 1, searchCriteria, searchValues);

            var response = client.Stream<CallLogs<CallLog>, CallLog>(method, GetTotalItems, SetNextPage, itemsPerPage, r => r.Items);

            return response;
        }

        private RequestMethod GetRequestMethod(DateTime startDate, DateTime endDate, int itemsPerPage, int pageNumber, CallLogSearchCriteria? searchCriteria, params string[] searchValues)
        {
            var data = GetData(searchCriteria, searchValues);

            var xml = new List<XElement>
            {
                new XElement("start_date", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XElement("end_date",  endDate.ToString("yyyy-MM-dd HH:mm:ss")),
                data,
                new XElement("items_per_page", itemsPerPage),
                new XElement("page_number", pageNumber)
            };

            return new RequestMethod("switchvox.callLogs.search", xml);
        }

        private void SetNextPage(RequestMethod method, int pageNumber)
        {
            method.Xml.Descendants("page_number").First().Value = pageNumber.ToString();
        }

        private string GetTotalItems(XDocument xDoc)
        {
            return xDoc.Descendants("calls").First().Attribute("total_items").Value;
        }

        private XElement GetData(CallLogSearchCriteria? searchCriteria, params string[] searchValues)
        {
            if (searchCriteria != null)
            {
                if (!(searchValues?.Length > 0))
                {
                    throw new ArgumentException("At least one value for searchValues must be specified", nameof(searchValues));
                }

                var groupAttribute = searchCriteria.GetEnumAttribute<XmlEnumGroupAttribute>()?.Name;

                if (groupAttribute == null && searchValues.Length > 1)
                    throw new ArgumentException($"Search criteria '{searchCriteria}' only accepts a single value, however '{searchValues.Length}' criterion were specified");

                var elementAttribute = searchCriteria.GetEnumAttribute<XmlEnumAttribute>(true).Name;

                if (groupAttribute == null)
                    return new XElement(elementAttribute, searchValues.Single());

                return new XElement(groupAttribute, searchValues.Select(v => new XElement(elementAttribute, v)));
            }

            return null;
        }
    }
}
