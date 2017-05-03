namespace SwitchvoxAPI
{
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
        /// Sort each record returned by its <see cref="CallTypes">"type"</see> attribute.
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
}