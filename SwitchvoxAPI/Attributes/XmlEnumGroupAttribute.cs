using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class XmlEnumGroupAttribute : Attribute
    {
        public string Name { get; set; }

        internal XmlEnumGroupAttribute(string name)
        {
            Name = name;
        }
    }
}
