using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    //todo: need to make notes as to whether fields will have values based on whether this is another group or is a real member

    public class ExtensionGroupMember
    {
        [XmlAttribute("account_id")]
        public string AccountId { get; set; }

        [XmlAttribute("extension")]
        public string Extension { get; set; }

        [XmlAttribute("first_name")]
        public string FirstName { get; set; }

        [XmlAttribute("last_name")]
        public string LastName { get; set; }

        [XmlAttribute("display_name")]
        public string DisplayName { get; set; }

        [XmlAttribute("position")]
        public int Position { get; set; }

        [XmlAttribute("id")]
        public int? Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlAttribute("date_created")]
        public DateTime DateCreated { get; set; }

        [XmlAttribute("vm_quota")]
        public int? VmQuota { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
