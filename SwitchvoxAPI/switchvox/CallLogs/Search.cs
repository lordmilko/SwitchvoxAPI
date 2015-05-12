using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SwitchvoxAPI;

namespace Switchvox.CallLogs
{
    /// <summary>
    /// Search the call logs on the phone system.
    /// </summary>
    public class Search : RequestMethod
    {
        /// <summary>
        /// Specifies a search data type where one or more values must be given.
        /// </summary>
        public enum MultiItemSearchData
        {
            /// <summary>
            /// Perform a search using one or more Account IDs
            /// </summary>
            AccountIds,

            /// <summary>
            /// Perform a search using one or more IAX Provider IDs
            /// </summary>
            IAXProviderIds,

            /// <summary>
            /// Perform a search using one or more SIP Provider IDs
            /// </summary>
            SIPProviderIds,

            /// <summary>
            /// Perform a search using one or more Channel Group IDs
            /// </summary>
            ChannelGroupIds
        }

        /// <summary>
        /// Specifies a search data type where a single value must be given.
        /// </summary>
        public enum SingleItemSearchData
        {
            /// <summary>
            /// Perform a search using a single Caller ID Name
            /// </summary>
            CallerIdName,

            /// <summary>
            /// Perform a search using a single Caller ID Number
            /// </summary>
            CallerIdNumber,

            /// <summary>
            /// Perform a search using a single Incoming DID
            /// </summary>
            IncomingDID
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.CallLogs.Search"/> class for searches where multiple search criteria can be specified.
        /// </summary>
        /// <param name="startDate">The minimum date to search from.</param>
        /// <param name="endDate">The maximum date to search to.</param>
        /// <param name="searchData">A <see cref="Switchvox.CallLogs.Search.MultiItemSearchData"/> value representing the type of data this request will search for.</param>
        /// <param name="data">An array of one or more values to search for that correspond with the type of data specified in <paramref name="searchData"/></param>
        /// <param name="sortOrder">How the response will be sorted</param>
        /// <param name="itemsPerPage">The maximum number of records to be returned by the response. An additional <paramref name="itemsPerPage"/> number of records can be retrieved by making additional requests and modifying the <paramref name="pageNumber"/></param>
        /// <param name="pageNumber">The page number of call record results to return.</param>
        public Search(DateTime startDate, DateTime endDate, MultiItemSearchData searchData, string[] data, SortOrder sortOrder = SortOrder.Desc, int itemsPerPage = 50, int pageNumber = 1) : base("switchvox.callLogs.search")
        {
            if (data.Length == 0)
                throw new NotImplementedException();

            var searchDataElms = GetMultiItemSearchDataElms(searchData, data);
            
            ConstructXml(startDate, endDate, searchDataElms, sortOrder, itemsPerPage, pageNumber);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.CallLogs.Search"/> class for searches where a single search criterion must be specified.
        /// </summary>
        /// <param name="startDate">The minimum date to search from.</param>
        /// <param name="endDate">The maximum date to search to.</param>
        /// <param name="searchData">A <see cref="Switchvox.CallLogs.Search.SingleItemSearchData"/> value representing the type of data this request will search for.</param>
        /// <param name="data">A single value corresponding with the type of data specified in <paramref name="searchData"/></param>
        /// <param name="sortOrder">How the response will be sorted</param>
        /// <param name="itemsPerPage">The maximum number of records to be returned by the response. An additional <paramref name="itemsPerPage"/> number of records can be retrieved by making additional requests and modifying the <paramref name="pageNumber"/></param>
        /// <param name="pageNumber">The page number of call record results to return.</param>
        public Search(DateTime startDate, DateTime endDate, SingleItemSearchData searchData, string data, SortOrder sortOrder = SortOrder.Desc, int itemsPerPage = 50, int pageNumber = 1) : base("switchvox.callLogs.search")
        {
            var searchDataElms = GetSingleItemSearchDataElms(searchData, data);

            ConstructXml(startDate, endDate, searchDataElms, sortOrder, itemsPerPage, pageNumber);
        }

        private void ConstructXml(DateTime startDate, DateTime endDate, XElement searchDataElms, SortOrder sortOrder, int itemsPerPage, int pageNumber)
        {
            var xml = new List<XElement>
            {
                new XElement("start_date", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new XElement("end_date",  endDate.ToString("yyyy-MM-dd HH:mm:ss")),
                searchDataElms,
                new XElement("sort_order", sortOrder.ToString()),
                new XElement("items_per_page", itemsPerPage),
                new XElement("page_number", pageNumber)
            };

            SetXml(xml);
        }

        private XElement GetMultiItemSearchDataElms(MultiItemSearchData searchData, string[] data)
        {
            XElement xml;

            switch(searchData)
            {
                case MultiItemSearchData.AccountIds:
                    xml = GetElmsForGroup("account_ids", "account_id", data);
                    break;

                case MultiItemSearchData.ChannelGroupIds:
                    xml = GetElmsForGroup("channel_group_ids", "channel_group_id", data);
                    break;

                case MultiItemSearchData.IAXProviderIds:
                    xml = GetElmsForGroup("iax_provider_ids", "iax_provider_id", data);
                    break;

                case MultiItemSearchData.SIPProviderIds:
                    xml = GetElmsForGroup("sip_provider_ids", "sip_provider_id", data);
                    break;

                default:
                    throw new NotImplementedException();
            }

            return xml;
        }

        private XElement GetSingleItemSearchDataElms(SingleItemSearchData searchData, string data)
        {
            string tagName;

            switch(searchData)
            {
                case SingleItemSearchData.CallerIdName:
                    tagName = "caller_id_name";
                    break;

                case SingleItemSearchData.CallerIdNumber:
                    tagName = "caller_id_number";
                    break;

                case SingleItemSearchData.IncomingDID:
                    tagName = "incoming_did";
                    break;

                default:
                    throw new NotImplementedException();
            }

            var xml = new XElement(tagName, data);

            return xml;
        }

        private XElement GetElmsForGroup(string groupTag, string instanceTag, string[] data)
        {
            var children = data.Select(value => new XElement(instanceTag, value)).ToList();

            var xml = new XElement(groupTag, children);

            return xml;
        }
    }
}
