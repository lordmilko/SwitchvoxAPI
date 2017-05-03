using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommunications.Disconnect, "SvoxServer")]
    public class DisconnectSvoxServer : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            SwitchvoxSessionState.Client = null;
        }
    }
}
