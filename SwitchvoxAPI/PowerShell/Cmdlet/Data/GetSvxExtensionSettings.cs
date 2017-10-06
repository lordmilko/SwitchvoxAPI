using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "SvxExtensionSettings")]
    public class GetSvxExtensionSettings : SwitchvoxCmdlet
    {
        protected override void ProcessRecord()
        {
            var settings = client.Extensions.Settings.GetInfo();

            WriteObject(settings);
        }
    }
}
