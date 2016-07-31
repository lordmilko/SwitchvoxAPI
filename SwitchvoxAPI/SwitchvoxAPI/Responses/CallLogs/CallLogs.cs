using System.Collections.Generic;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    [XmlRoot("calls")]
    public class CallLogs<T>
    {
        [XmlAttribute("page_number")]
        public int PageNumber { get; set; }

        [XmlAttribute("TotalPages")]
        public int TotalPages { get; set; }

        [XmlAttribute("items_per_page")]
        public int ItemsPerPage { get; set; }

        [XmlAttribute("total_items")]
        public int TotalItems { get; set; }

        [XmlElement("call")]
        public List<T> Items { get; set; }
    }
}
