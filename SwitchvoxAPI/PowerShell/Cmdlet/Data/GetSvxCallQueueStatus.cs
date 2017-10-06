using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "SvxCallQueueStatus", DefaultParameterSetName = "Manual")]
    public class GetSvxCallQueueStatus : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Default")]
        public ExtensionInfo Queue { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Manual")]
        public string QueueAccountId { get; set; }

        protected override void ProcessRecord()
        {
            if (Queue != null && Queue.Type != ExtensionType.CallQueue)
                throw new PSInvalidOperationException($"Extension '{Queue.DisplayName}' was of type '{Queue.Type}', however only extensions of type {ExtensionType.CallQueue} are allowed");

            if (ParameterSetName == "Default")
                QueueAccountId = Queue.AccountId;

            var status = client.CallQueues.GetCurrentStatus(QueueAccountId);

            WriteObject(status);
        }
    }
}
