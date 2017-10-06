using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public enum QueueLoginType
    {
        [XmlEnum("permanent")]
        Permanent,

        [XmlEnum("login")]
        Login
    }
}