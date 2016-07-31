using System.Xml.Linq;

namespace SwitchvoxAPI
{
    public partial class CallQueues
    {
        /// <summary>
        /// Get a list of all members of a queue, their current status as well as the status of all callers currently waiting in the queue
        /// </summary>
        /// <param name="accountId">The Account ID of a valid call queue</param>
        public object GetCurrentStatus(string accountId)
        {
            var response = client.Execute(new RequestMethod("switchvox.callQueues.getCurrentStatus", new XElement("account_id", accountId)));

            return null;
        }
    }
}
