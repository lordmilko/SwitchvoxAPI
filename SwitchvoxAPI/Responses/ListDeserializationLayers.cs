using System.Collections.Generic;
using System.Xml.Serialization;

namespace SwitchvoxAPI.ListDeserializationLayers
{
    [XmlRoot("extensions")]
    public class Extensions
    {
        [XmlElement("extension")]
        public List<ExtensionInfo> Items { get; set; }
    }

    [XmlRoot("extension_groups")]
    public class ExtensionGroupOverviews
    {
        [XmlElement("extension_group")]
        public List<ExtensionGroupOverview> Items { get; set; }
    }

    [XmlRoot("members")]
    public class ExtensionGroupMembers
    {
        [XmlElement("member")]
        public List<ExtensionGroupMember> Items { get; set; }
    }

    [XmlRoot("dependents")]
    public class ExtensionGroupDependents
    {
        [XmlElement("dependent")]
        public List<ExtensionGroupDependent> Items { get; set; }
    }

    [XmlRoot("queue_members")]
    public class CallQueueStatusMembers<T>
    {
        [XmlElement("queue_member")]
        public List<T> Items { get; set; }
    }

    [XmlRoot("waiting_callers")]
    public class CallQueueTodaysStatusWaitingCallers
    {
        [XmlElement("waiting_caller")]
        public List<CallQueueTodaysStatusWaitingCall> Items { get; set; }
    }

    [XmlRoot("events")]
    public class CallLogEvents
    {
        [XmlElement("event")]
        public List<CallLogEvent> Items { get; set; }
    }

    [XmlRoot("current_calls")]
    public class CurrentCalls
    {
        [XmlElement("current_call")]
        public List<CurrentCall> Items { get; set; }
    }

    [XmlRoot("global_ivr_variables")]
    public class GlobalIVRVariables
    {
        [XmlElement("global_ivr_variable")]
        public List<GlobalIVRVariable> Items { get; set; }
    }
}

