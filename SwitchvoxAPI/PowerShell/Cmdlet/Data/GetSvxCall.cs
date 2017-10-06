using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "SvxCall")]
    public class GetSvxCall : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            var calls = client.CurrentCalls.GetList();

            if (Name != null)
            {
                var pattern = new WildcardPattern(Name, WildcardOptions.IgnoreCase);

                calls = calls.Where(c => pattern.IsMatch(c.FromName) || pattern.IsMatch(c.ToNumber)).ToList();
            }

            WriteObject(calls, true);
        }
    }
}
