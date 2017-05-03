using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Specifies the state an active call is in.
    /// </summary>
    public enum CurrentCallState
    {
        /// <summary>
        /// <see cref="ExtensionType.SIPExtension"/>  is logging into a call queue, or <see cref="ExtensionType.Virtual"/> is logging into a phone.
        /// </summary>
        [XmlEnum("agent_login")]
        AgentLogin,

        /// <summary>
        /// <see cref="ExtensionType.SIPExtension"/>  is logging out of a call queue, or <see cref="ExtensionType.Virtual"/> is logging out of a phone.
        /// </summary>
        [XmlEnum("agent_logout")]
        AgentLogout,

        [XmlEnum("checking_voicemail")]
        CheckingVoicemail,

        [XmlEnum("conference")]
        Conference,

        [XmlEnum("directory")]
        Directory,

        [XmlEnum("faxing")]
        Faxing,

        [XmlEnum("intercom")]
        Intercom,

        [XmlEnum("ivr")]
        IVR,

        [XmlEnum("leaving_voicemail")]
        LeavingVoicemail,

        [XmlEnum("monitoring")]
        Monitoring,

        /// <summary>
        /// Call is currently parked (on hold), waiting to be retrieved from another phone.
        /// </summary>
        [XmlEnum("parked")]
        Parked,

        /// <summary>
        /// Call is waiting to be answered by a call queue.
        /// </summary>
        [XmlEnum("queued")]
        Queued,

        [XmlEnum("receiving_fax")]
        ReceivingFax,

        /// <summary>
        /// Call is in the process of ringing.
        /// </summary>
        [XmlEnum("ringing")]
        Ringing,

        [XmlEnum("talking")]
        Talking,

        [XmlEnum("unknown")]
        Unknown
    }
}