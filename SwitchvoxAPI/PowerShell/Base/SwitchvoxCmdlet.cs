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
        protected SwitchvoxClient client => SwitchvoxSessionState.Client;
    }
}
