using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public enum CallQueueStrategy
    {
        [XmlEnum("ring_all")]
        RingAll
    }
}