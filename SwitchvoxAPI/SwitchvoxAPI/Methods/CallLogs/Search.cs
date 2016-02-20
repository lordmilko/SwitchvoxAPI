using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SwitchvoxAPI;

namespace SwitchvoxAPI
{
    public partial class CallLogs
    {
        /// <summary>
        /// Search the call logs on the phone system where multiple search criteria can be specified.
        /// </summary>
        /// <param name="startDate">The minimum date to search from.</param>
        /// <param name="endDate">The maximum date to search to.</param>
        /// <param name="searchData">A <see cref="CallLogMultiItemSearchData"/> value representing the type of data this request will search for.</param>
        /// <param name="dataValues">An array of one or more values to search for that correspond with the type of data specified in <paramref name="searchData"/></param>
        /// <param name="sortOrder">How the response will be sorted</param>
        /// <param name="itemsPerPage">The maximum number of records to be returned by the response. An additional <paramref name="itemsPerPage"/> number of records can be retrieved by making additional requests and modifying the <paramref name="pageNumber"/></param>
        /// <param name="pageNumber">The page number of call record results to return.</param>
        public CallLogs<CallLog> Search(DateTime startDate, DateTime endDate, CallLogMultiItemSearchData searchData, string[] dataValues, SortOrder sortOrder = SortOrder.Desc, int itemsPerPage = 50, int pageNumber = 1)
        {
            if (dataValues.Length == 0)
                throw new ArgumentException("At least one value must be specified");

            var searchDataElms = GetMultiItemSearchDataElms(searchData, dataValues);
            
            return ConstructXml(startDate, endDate, searchDataElms, sortOrder, itemsPerPage, pageNumber);
        }

        /// <summary>
        /// Search the call logs on the phone system where a single search criterion must be specified.
        /// </summary>
        /// <param name="startDate">The minimum date to search from.</param>
        /// <param name="endDate">The maximum date to search to.</param>
        /// <param name="searchData">A <see cref="CallLogSingleItemSearchData"/> value representing the type of data this request will search for.</param>
        /// <param name="data">A single value corresponding with the type of data specified in <paramref name="searchData"/></param>
        /// <param name="sortOrder">How the response will be sorted</param>
        /// <param name="itemsPerPage">The maximum number of records to be returned by the response. An additional <paramref name="itemsPerPage"/> number of records can be retrieved by making additional requests and modifying the <paramref name="pageNumber"/></param>
        /// <param name="pageNumber">The page number of call record results to return.</param>
        public CallLogs<CallLog> Search(DateTime startDate, DateTime endDate, CallLogSingleItemSearchData searchData, string data, SortOrder sortOrder = SortOrder.Desc, int itemsPerPage = 50, int pageNumber = 1)
        {
            var searchDataElms = GetSingleItemSearchDataElms(searchData, data);

            return ConstructXml(startDate, endDate, searchDataElms, sortOrder, itemsPerPage, pageNumber);
        }

        private CallLogs<CallLog> ConstructXml(DateTime startDate, DateTime endDate, XElement searchDataElms, SortOrder sortOrder, int itemsPerPage, int pageNumber)
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

            var response = request.Execute(new Switchvox.RequestMethod("switchvox.callLogs.search", xml));

            return response.Deserialize<CallLogs<CallLog>>();
        }

        private XElement GetMultiItemSearchDataElms(CallLogMultiItemSearchData searchData, string[] data)
        {
            XElement xml;

            switch(searchData)
            {
                case CallLogMultiItemSearchData.AccountIds:
                    xml = GetElmsForGroup("account_ids", "account_id", data);
                    break;

                case CallLogMultiItemSearchData.ChannelGroupIds:
                    xml = GetElmsForGroup("channel_group_ids", "channel_group_id", data);
                    break;

                case CallLogMultiItemSearchData.IAXProviderIds:
                    xml = GetElmsForGroup("iax_provider_ids", "iax_provider_id", data);
                    break;

                case CallLogMultiItemSearchData.SIPProviderIds:
                    xml = GetElmsForGroup("sip_provider_ids", "sip_provider_id", data);
                    break;

                default:
                    throw new NotImplementedException("No handler for the value " + searchData.ToString() + " has been implemented.");
            }

            return xml;
        }

        private XElement GetSingleItemSearchDataElms(CallLogSingleItemSearchData searchData, string data)
        {
            string tagName;
            string groupTagName = null;

            switch(searchData)
            {
                case CallLogSingleItemSearchData.AccountIds:
                    groupTagName = "account_ids";
                    tagName = "account_id";
                    break;

                case CallLogSingleItemSearchData.ChannelGroupIds:
                    groupTagName = "channel_group_ids";
                    tagName = "channel_group_id";
                    break;

                case CallLogSingleItemSearchData.IAXProviderIds:
                    groupTagName = "iax_provider_ids";
                    tagName = "iax_provider_id";
                    break;

                case CallLogSingleItemSearchData.SIPProviderIds:
                    groupTagName = "sip_provider_ids";
                    tagName = "sip_provider_id";
                    break;

                case CallLogSingleItemSearchData.CallerIdName:
                    tagName = "caller_id_name";
                    break;

                case CallLogSingleItemSearchData.CallerIdNumber:
                    tagName = "caller_id_number";
                    break;

                case CallLogSingleItemSearchData.IncomingDID:
                    tagName = "incoming_did";
                    break;

                default:
                    throw new NotImplementedException("No handler for the value " + searchData.ToString() + " has been implemented.");
            }

            XElement xml;

            if (groupTagName == null)
                xml = new XElement(tagName, data);
            else
                xml = new XElement(groupTagName, new XElement(tagName, data));

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
