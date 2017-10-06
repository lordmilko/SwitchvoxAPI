using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI.PowerShell.Base
{
    public abstract class SwitchvoxCmdlet : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            if(SwitchvoxSessionState.Client == null)
                throw new Exception("You are not connected to a Switchvox Server. Please connect first using Connect-SvxServer.");
        }

        protected SwitchvoxClient client => SwitchvoxSessionState.Client;
    }
}
