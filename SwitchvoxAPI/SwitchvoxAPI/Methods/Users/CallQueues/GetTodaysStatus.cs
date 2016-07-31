using System.Collections.Generic;
using System.Xml.Linq;

namespace SwitchvoxAPI
{
    public partial class CallQueuesUsers
    {
        /// <summary>
        /// Get today's status for a call queue. The results returned by this method will differ depending on the Switchboard Panel Permissions for the specified User Account ID.
        /// </summary>
        /// <param name="callQueueAccountId">The ID of the Call Queue to get today's status for</param>
        /// <param name="userAccountId">The Account ID of the user making the request. While the user does not need to be a member of the call queue, they should have Queue Visibility Permissions (under Queue -> Permissions -> Queue Visibility Permissions)</param>
        public CallQueueStatus GetTodaysStatus(string callQueueAccountId, string userAccountId)
        {
            var xml = new List<XElement>
            {
                new XElement("account_id", userAccountId),
                new XElement("call_queue_account_id", callQueueAccountId)
            };

            var response = client.Execute(new RequestMethod("switchvox.users.callQueues.getTodaysStatus", xml));

            return response.Deserialize<CallQueueStatus>();
        }
    }
}
