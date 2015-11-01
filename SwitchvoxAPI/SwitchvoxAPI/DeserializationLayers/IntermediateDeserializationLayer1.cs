using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SwitchvoxAPI;

namespace SwitchvoxAPI.DeserializationLayers
{
    public class IntermediateDezerializationLayer1
    {
        [XmlElement("extension")]
        public List<Extension> Extensions { get; set; }
    }
}
