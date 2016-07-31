using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class GlobalIVRVariable
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        private string value;

        [XmlAttribute("value")]
        public string Value
        {
            get { return value; }
            set { this.value = value == string.Empty ? null : value; }
        }
    }
}
