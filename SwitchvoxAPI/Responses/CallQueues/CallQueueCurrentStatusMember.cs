using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallQueueCurrentStatusMember : BaseCallQueueStatusMember
    {
        [XmlAttribute("logged_in_status")]
        public string LoggedInStatus { get; set; }
    }
}
