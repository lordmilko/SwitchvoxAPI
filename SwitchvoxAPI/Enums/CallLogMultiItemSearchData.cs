namespace SwitchvoxAPI
{
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
}