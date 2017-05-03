using System.Xml.Linq;

namespace SwitchvoxAPI.Methods
{
    public partial class CallQueues
    {
        /// <summary>
        /// Get a list of all members of a queue, their current status as well as the status of all callers currently waiting in the queue
        /// </summary>
        /// <param name="accountId">The Account ID of a valid call queue</param>
        public CallQueueCurrentStatus GetCurrentStatus(string accountId)
        {
            var response = client.Execute<CallQueueCurrentStatus>(new RequestMethod("switchvox.callQueues.getCurrentStatus", new XElement("account_id", accountId)));

            return response;

            //todo: i dont think the waiting callers code is implemented
        }
    }
}
