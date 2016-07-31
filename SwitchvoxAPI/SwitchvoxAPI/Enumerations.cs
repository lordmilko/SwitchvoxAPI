using System;
using System.Xml.Serialization;

//Global enumerations that can apply to a wide variety of Switchvox Request Methods

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
        Virtual
    }

    /// <summary>
    /// Specifies the state an active call is in.
    /// </summary>
    public enum CurrentCallState
    {
        /// <summary>
        /// <see cref="SwitchvoxAPI.ExtensionType.SIPExtension"/>  is logging into a call queue, or <see cref="SwitchvoxAPI.ExtensionType.Virtual"/> is logging into a phone.
        /// </summary>
        [XmlEnum("agent_login")]
        AgentLogin,

        /// <summary>
        /// <see cref="SwitchvoxAPI.ExtensionType.SIPExtension"/>  is logging out of a call queue, or <see cref="SwitchvoxAPI.ExtensionType.Virtual"/> is logging out of a phone.
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

    /// <summary>
    /// Specifies where a call originated from.
    /// </summary>
    public enum CallOrigination
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
        Voicemail
    }

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
        Redirected
    }

    /// <summary>
    /// Specifies a search data type where one or more values must be given.
    /// </summary>
    public enum CallLogMultiItemSearchData
    {
        /// <summary>
        /// Perform a search using one or more Account IDs
        /// </summary>
        AccountIds,

        /// <summary>
        /// Perform a search using one or more Channel Group IDs
        /// </summary>
        ChannelGroupIds,

        /// <summary>
        /// Perform a search using one or more IAX Provider IDs
        /// </summary>
        IAXProviderIds,

        /// <summary>
        /// Perform a search using one or more SIP Provider IDs
        /// </summary>
        SIPProviderIds
    }

    /// <summary>
    /// Specifies a search data type where a single value must be given.
    /// </summary>
    public enum CallLogSingleItemSearchData
    {
        /// <summary>
        /// Perform a search using a single Account ID
        /// </summary>
        AccountId,

        /// <summary>
        /// Perform a search using a single Channel Group ID
        /// </summary>
        ChannelGroupId,

        /// <summary>
        /// Perform a search using a single IAX Provider ID
        /// </summary>
        IAXProviderId,

        /// <summary>
        /// Perform a search using a single SIP Provider ID
        /// </summary>
        SIPProviderId,

        /// <summary>
        /// Perform a search using a single Caller ID Name
        /// </summary>
        CallerIdName,

        /// <summary>
        /// Perform a search using a single Caller ID Number
        /// </summary>
        CallerIdNumber,

        /// <summary>
        /// Perform a search using a single Incoming DID
        /// </summary>
        IncomingDID
    }

    /// <summary>
    /// Specifies how the search response should be sorted.
    /// </summary>
    public enum CallQueueLogSortField
    {
        /// <summary>
        /// Sort each record returned by its "Start Time" attribute
        /// </summary>
        StartTime,

        /// <summary>
        /// Sort each record returned by its <see cref="SwitchvoxAPI.CallTypes">"type"</see> attribute.
        /// </summary>
        Type,

        /// <summary>
        /// Sort each record returned by its "Queue Account ID" attribute.
        /// </summary>
        QueueAccountId,

        /// <summary>
        /// Sort each record returned by its "Caller ID Number" attribute.
        /// </summary>
        CallerIdNumber,

        /// <summary>
        /// Sort each record returned by its "Wait Time" attribute.
        /// </summary>
        WaitTime,

        /// <summary>
        /// Sort each record returned by its "Talk Time" attribute.
        /// </summary>
        TalkTime,

        /// <summary>
        /// Sort each record returned by its "Member Account ID" attribute.
        /// </summary>
        MemberAccountId,

        /// <summary>
        /// Sort each record returned by its "Enter Position" attribute.
        /// </summary>
        EnterPosition,

        /// <summary>
        /// Sort each record returned by its "Exit Position" attribute.
        /// </summary>
        ExitPosition,

        /// <summary>
        /// Sort each record returned by its "Abandon Position" attribute.
        /// </summary>
        AbandonPosition
    }


    /// <summary>
    /// Specifies the type of identifier being used to represent an Extension.
    /// </summary>
    public enum ExtensionIdentifier
    {
        /// <summary>
        /// An Extension Number.
        /// </summary>
        Extension,

        /// <summary>
        /// The Account ID associated with an Extension Number.
        /// </summary>
        AccountID
    }

    /// <summary>
    /// Specifies the order in which the returned elements should be sorted.
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// Sort in ascending order.
        /// </summary>
        Asc,

        /// <summary>
        /// Sort in descending order.
        /// </summary>
        Desc
    }

    /// <summary>
    /// Specifies call types to be filtered for in a method request.
    /// </summary>
    [Flags]
    public enum CallTypes
    {
        /// <summary>
        /// Calls that were successfully answered by a member of the call queue.
        /// </summary>
        CompletedCalls = 1,

        /// <summary>
        /// Calls that were abandoned before they could be completed (i.e. the caller hung up).
        /// </summary>
        AbandonedCalls = 2,

        /// <summary>
        /// Calls that were somehow redirected out of the call queue, potentially to another call queue.
        /// </summary>
        RedirectedCalls = 4,

        /// <summary>
        /// Include all call types in the method request.
        /// </summary>
        AllCalls = CompletedCalls | AbandonedCalls | RedirectedCalls
    }
}
