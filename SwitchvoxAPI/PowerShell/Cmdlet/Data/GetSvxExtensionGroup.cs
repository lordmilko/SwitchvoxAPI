using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "SvxExtensionGroup")]
    public class GetSvxExtensionGroup : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            var list = client.ExtensionGroups.GetList();

            if (Name != null)
            {
                var wildcard = new WildcardPattern(Name, WildcardOptions.IgnoreCase);

                list = list.Where(e => wildcard.IsMatch(e.Name)).ToList();
            }

            var records = list.Select(e => client.ExtensionGroups.GetInfo(e.Id));

            WriteObject(records, true);
        }
    }
}
