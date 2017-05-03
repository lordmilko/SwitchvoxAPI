using System;

namespace SwitchvoxAPI
{
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
