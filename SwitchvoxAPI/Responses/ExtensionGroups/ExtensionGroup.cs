using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SwitchvoxAPI.ListDeserializationLayers;

namespace SwitchvoxAPI
{
    [XmlRoot("extension_group")]
    public class ExtensionGroup : ExtensionGroupOverview
    {
        [XmlElement("members")]
        protected ExtensionGroupMembers members { get; set; }

        [XmlIgnore]
        public List<ExtensionGroupMember> Members => members.Items;

        [XmlElement("dependents")]
        protected ExtensionGroupDependents dependents { get; set; }

        [XmlIgnore]
        public List<ExtensionGroupDependent> Dependents => dependents.Items;
    }
}
