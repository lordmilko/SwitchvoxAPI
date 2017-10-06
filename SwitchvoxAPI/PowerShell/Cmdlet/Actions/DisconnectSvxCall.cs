using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet.Actions
{
    [Cmdlet(VerbsCommunications.Disconnect, "SvxCall")]
    public class DisconnectSvxCall : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public CurrentCall Call { get; set; }

        protected override void ProcessRecord()
        {
            client.CurrentCalls.HangUp(Call.Id);
        }
    }
}
