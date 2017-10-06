using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "SvxUserCallQueueStatus", DefaultParameterSetName = "Manual")]
    public class GetSvxUserCallQueueStatus : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Default")]
        public ExtensionInfo Queue { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Manual")]
        public string QueueAccountId { get; set; }

        /// <summary>
        /// Account ID of the extension to use to make the request. Extension does not need to be a member of the queue, however should have Queue Visibility Permissions (under Queue -> Permissions -> Queue Visibility Permissions)
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Manual")]
        public string UserAccountId { get; set; }

        protected override void ProcessRecord()
        {
            if (Queue != null && Queue.Type != ExtensionType.CallQueue)
                throw new ParameterBindingException($"Extension {Queue.DisplayName} was of type {Queue.Type}, however only call queues are allowed");

            if (ParameterSetName == "Default")
                QueueAccountId = Queue.AccountId;

            var status = client.Users.CallQueues.GetTodaysStatus(QueueAccountId, UserAccountId);

            WriteObject(status);
        }
    }
}
