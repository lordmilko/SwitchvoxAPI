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

        [XmlEnum("INCOMING")]
        Incoming,

        [XmlEnum("PROVIDER")]
        Provider,

        [XmlEnum("INTERNAL")]
        Internal,

        [XmlEnum("CASCADE")]
        Cascade,

        [XmlEnum("TALKING")]
        Talking,

        [XmlEnum("RINGALL")]
        RingAll,

        [XmlEnum("HANGUP")]
        Hangup,

        [XmlEnum("INCOMING_PROVIDER")]
        IncomingProvider,

        [XmlEnum("DIRECTED_PICKUP")]
        DirectedPickup,

        [XmlEnum("ONHOLD")]
        OnHold,

        [XmlEnum("OFFHOLD")]
        OffHold,

        [XmlEnum("PARKER")]
        Parked,

        [XmlEnum("UNPARKED")]
        Unparked,

        [XmlEnum("FORWARD")]
        Forward,

        [XmlEnum("PARKING_TIMEOUT")]
        ParkingTimeout,

        [XmlEnum("ASSISTED_TRANSFEREE")]
        AssistedTransferee,

        [XmlEnum("BLIND_TRANSFER")]
        BlindTransfer,

        [XmlEnum("BLIND_TRANSFERER")]
        BlindTransferer,

        [XmlEnum("BLIND_RECIPIENT")]
        BlindRecipient,

        [XmlEnum("FAX_RECEIVE_IN")]
        FaxReceiveIn,

        [XmlEnum("FAX_RECEIVE_OUT")]
        FaxReceiveOut,

        [XmlEnum("FAX_SENT")]
        FaxSent,

        [XmlEnum("FAX_RECEIVE_ERROR")]
        FaxReceiveError,

        [XmlEnum("MEETME")]
        MeetMeConference,

        [XmlEnum("QUEUE_EXIT")]
        QueueExit,

        [XmlEnum("STATUS")]
        Status,

        [XmlEnum("VOICEMAIL")]
        Voicemail,

        [XmlEnum("VOICEMAIL_PICKUP")]
        VoicemailPickup,

        [XmlEnum("PEER_VOICEMAIL")]
        VoicemailPeer,

        [XmlEnum("NO_EVENTS")]
        NoEvents,

        //TC_ASSISTED_TRANSFER
        //TC_BLIND_TRANSFER
    }
}