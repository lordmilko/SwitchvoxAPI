using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace Switchvox.Users.CallQueues
{
    /// <summary>
    /// Get today's status for a call queue. The results returned by this method will differ depending on the Switchboard Panel Permissions for the specified User Account ID.
    /// </summary>
    public class GetTodaysStatus : RequestMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.Users.CallQueues.GetTodaysStatus"/> class
        /// </summary>
        /// <param name="callQueueAccountId">The ID of the Call Queue to get today's status for</param>
        /// <param name="userAccountId">The Account ID of the user making the request. While the user does not need to be a member of the call queue, they should have Queue Visibility Permissions (under Queue -> Permissions -> Queue Visibility Permissions)</param>
        public GetTodaysStatus(string callQueueAccountId, string userAccountId) : base("switchvox.users.callQueues.getTodaysStatus")
        {
            var xml = new List<XElement>
            {
                new XElement("account_id", userAccountId),
                new XElement("call_queue_account_id", callQueueAccountId)
            };

            SetXml(xml);
        }
    }
}
