using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    class CallQueueStatusMember : BaseCallQueueStatusMember
    {
        [XmlAttribute("logged_in_status")]
        public string LoggedInStatus { get; set; }

        private string pausedSince;

        [XmlAttribute("paused_since")]
        public string PausedSince
        {
            get { return pausedSince; }
            set
            {
                pausedSince = value == string.Empty ? null : value;
            }
        }

        [XmlAttribute("paused")]
        public bool Paused { get; set; }
    }
}
