using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace Switchvox.CallQueues
{
    /// <summary>
    /// Get a list of all members of a queue, their current status as well as the status of all callers currently waiting in the queue
    /// </summary>
    public class GetCurrentStatus : RequestMethod
    {
        /// <summary>
        /// Initializes a new instance of the Switchvox.CallQueues.GetCurrentStatus class.
        /// </summary>
        /// <param name="accountId">The Account ID of a valid call queue</param>
        public GetCurrentStatus(string accountId) : base("switchvox.callQueues.getCurrentStatus")
        {
            SetXml(new XElement("account_id", accountId));
        }
    }
}
