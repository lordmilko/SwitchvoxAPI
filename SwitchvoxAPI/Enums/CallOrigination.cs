using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Specifies where a call originated from.
    /// </summary>
    public enum CallDirection
    {
        /// <summary>
        /// Call originated from an extension inside the phone system.
        /// </summary>
        [XmlEnum("incoming")]
        Incoming,

        /// <summary>
        /// Call originated outside the phone system.
        /// </summary>
        [XmlEnum("outgoing")]
        Outgoing
    }
}