using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Add, "SvxIVRVariable")]
    public class AddSvxIVRVariable : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Name { get; set; }

        [Parameter(Mandatory = false, Position = 1)]
        public string Value { get; set; }

        protected override void ProcessRecord()
        {
            client.IVR.GlobalVariables.Add(Name, Value);
        }
    }
}
