using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "SvxIVRVariable")]
    public class GetSvxIVRVariable : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            var records = client.IVR.GlobalVariables.GetList();

            if (Name != null)
            {
                var wildcard = new WildcardPattern(Name, WildcardOptions.IgnoreCase);

                records = records.Where(r => wildcard.IsMatch(r.Name)).ToList();
            }

            WriteObject(records, true);
        }
    }
}
