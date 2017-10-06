using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Specifies types extensions can be.
    /// </summary>
    public enum ExtensionType
    {
        /// <summary>
        /// Regular SIP Extension.
        /// </summary>
        [XmlEnum("sip")]
        SIPExtension,

        /// <summary>
        /// Extension is a Call Queue.
        /// </summary>
        [XmlEnum("call_queue")]
        CallQueue,

        /// <summary>
        /// Extension is an IVR
        /// </summary>
        [XmlEnum("ivr")]
        IVR,

        /// <summary>
        /// Extension is a Virtual Extension
        /// </summary>
        [XmlEnum("virtual")]
        Virtual,

        [XmlEnum("intercom")]
        Intercom,

        [XmlEnum("directory")]
        Directory,

        [XmlEnum("feature_intercom")]
        FeatureIntercom,

        [XmlEnum("feature_directed_pickup")]
        FeatureDirectedPickup,

        [XmlEnum("feature_send_to_vm")]
        FeatureSendToVoicemail,

        [XmlEnum("feature_monitor")]
        FeatureCallMonitor,

        [XmlEnum("feature_fax")]
        FeatureSendFax,

        [XmlEnum("voicemail")]
        Voicemail,

        [XmlEnum("simple_conference")]
        SimpleConference,

        [XmlEnum("conference")]
        MeetMeConference,

        [XmlEnum("agentcallbacklogin")]
        AgentLogIn,

        [XmlEnum("agentcallbacklogoff")]
        AgentLogOff,

        [XmlEnum("group_pickup")]
        GroupPickup,

        [XmlEnum("callpark")]
        CallParking,

        [XmlEnum("zap")]
        AnalogPhone,

        [XmlEnum("parkinglog")]
        SingleCallParkingSplot,

        [XmlEnum("autodial")]
        Dialtone
    }
}