using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class ExtensionGroupOverview
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("date_created")]
        public string _RawDateCreated { get; set; }

        [XmlIgnore]
        public DateTime DateCreated => DateTime.Parse(_RawDateCreated);

        [XmlAttribute("vm_quota")]
        public string _RawVmQuota { get; set; }

        public int? VmQuota => string.IsNullOrEmpty(_RawVmQuota) ? null : (int?)Convert.ToInt32(_RawVmQuota);

        [XmlAttribute("editable")]
        public bool Editable { get; set; }

        [XmlAttribute("user_viewable")]
        public bool UserViewable { get; set; }
    }
}
