using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Specifies events that occur during the course of a phone call.
    /// </summary>
    public enum CallEventType
    {
        [XmlEnum("OUTGOING")]
        Outgoing,

        [XmlEnum("PROVIDER")]
        Provider,

        [XmlEnum("TALKING")]
        Talking,

        [XmlEnum("RINGALL")]
        RingAll,

        [XmlEnum("HANGUP")]
        Hangup,

        [XmlEnum("INCOMING_PROVIDER")]
        IncomingProvider,

        [XmlEnum("INCOMING")]
        Incoming,

        [XmlEnum("QUEUE_EXIT")]
        QueueExit,

        [XmlEnum("INTERNAL")]
        Internal,

        [XmlEnum("ASSISTED_TRANSFEREE")]
        AssistedTransferee,

        [XmlEnum("STATUS")]
        Status,

        [XmlEnum("BLIND_TRANSFERER")]
        BlindTransferer,

        [XmlEnum("NO_EVENTS")]
        NoEvents,

        [XmlEnum("VOICEMAIL")]
        Voicemail,

        [XmlEnum("PARKER")]
        Parker,

        [XmlEnum("PARKING_TIMEOUT")]
        ParkingTimeout,

        [XmlEnum("UNPARKED")]
        Unparked,

        [XmlEnum("CASCADE")]
        Cascade,

        [XmlEnum("FORWARD")]
        Forward,

        [XmlEnum("DIRECTED_PICKUP")]
        DirectedPickup
    }
}