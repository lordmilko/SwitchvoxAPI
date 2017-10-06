using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "SvxClient")]
    public class GetSvxClient : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(SwitchvoxSessionState.Client);
        }
    }
}
