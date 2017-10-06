using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "SvxCallQueueLog", DefaultParameterSetName = "Manual")]
    public class GetSvxCallQueueLog : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = false)]
        public DateTime StartDate { get; set; } = DateTime.Now.Date;

        [Parameter(Mandatory = false)]
        public DateTime EndDate { get; set; } = DateTime.Now.Date.AddDays(1);

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Default")]
        public ExtensionInfo Queue { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Manual")]
        public string QueueAccountId { get; set; }

        [Parameter(Mandatory = false)]
        public CallTypes CallTypes { get; set; } = CallTypes.AllCalls;

        [Parameter(Mandatory = false)]
        public bool IgnoreWeekends { get; set; } = false;

        protected override void ProcessRecord()
        {
            if (Queue != null && Queue.Type != ExtensionType.CallQueue)
                throw new PSInvalidOperationException($"Extension '{Queue.DisplayName}' was of type '{Queue.Type}', however only extensions of type {ExtensionType.CallQueue} are allowed");

            if (ParameterSetName == "Default")
                QueueAccountId = Queue.AccountId;

            var results = client.CallQueueLogs.StreamSearch(StartDate, EndDate, new[] { QueueAccountId }, CallTypes, IgnoreWeekends);

            foreach (var result in results)
                WriteObject(result);
        }
    }
}
