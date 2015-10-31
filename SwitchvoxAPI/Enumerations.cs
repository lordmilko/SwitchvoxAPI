using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Global enumerations that can apply to a wide variety of Switchvox Request Methods

namespace SwitchvoxAPI
{
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
