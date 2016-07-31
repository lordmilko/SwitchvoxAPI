using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class CallQueueUserStatusMember : BaseCallQueueStatusMember
    {
        private string callDuration;

        //todo: why is this not a timespan
        [XmlAttribute("call_duration")]
        public string CallDuration
        {
            get { return callDuration; }
            set
            {
                callDuration = value == string.Empty ? null : value;
            }
        }

        [XmlAttribute("loggedin_status")]
        public string LoggedInStatus { get; set; }
    }
}
