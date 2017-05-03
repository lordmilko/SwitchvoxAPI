using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Specifies the type of call in a Call Queue Log.
    /// </summary>
    public enum QueueCallType
    {
        /// <summary>
        /// The call was successfully completed by a user hanging up.
        /// </summary>
        [XmlEnum("completed")]
        Completed,

        /// <summary>
        /// The call was forwarded, potentially outside of the phone system.
        /// </summary>
        [XmlEnum("redirected")]
        Redirected,

        [XmlEnum("abandoned")]
        Abandoned
    }
}