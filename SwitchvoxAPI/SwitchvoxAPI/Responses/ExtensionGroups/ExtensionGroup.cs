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
        [XmlElement("member")]
        public ExtensionGroupMembers _RawMembers;

        [XmlIgnore]
        public List<ExtensionGroupMember> Members => _RawMembers.Items;

        [XmlElement("dependent")]
        public ExtensionGroupDependents _RawDependents { get; set; }

        [XmlIgnore]
        public List<ExtensionGroupDependent> Dependents => _RawDependents.Items;
    }
}
