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
        public DateTime DateCreated { get; set; }

        [XmlAttribute("vm_quota")]
        public int? VmQuota { get; set; }

        [XmlAttribute("editable")]
        public bool Editable { get; set; }

        [XmlAttribute("user_viewable")]
        public bool UserViewable { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
