namespace SwitchvoxAPI
{
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
}