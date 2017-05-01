using System;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    //todo: i dont think this has been done correctly - i think the position attribute should have a position xml attribute?
    public class CallQueueTodaysStatusWaitingCall
    {
        [XmlAttribute("waiting_caller")]
        public int Position { get; set; }

        [XmlAttribute("entered_position")]
        public int StartingPosiiton { get; set; }

        public TimeSpan WaitingDuration => TimeSpan.FromSeconds(_RawWaitingDuration);

        [XmlAttribute("talking_to_name")]
        public string TalkingToName { get; set; }

        [XmlAttribute("talking_to_number")]
        public string TalkingToNumber { get; set; }

        [XmlAttribute("waiting_duration")]
        public int _RawWaitingDuration { get; set; }
    }
}
