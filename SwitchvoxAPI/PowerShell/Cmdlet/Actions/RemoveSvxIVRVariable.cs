using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Remove, "SvxIVRVariable")]
    public class RemoveSvxIVRVariable : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Name")]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Id")]
        public int Id { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Default")]
        public GlobalIVRVariable Variable { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == "Default")
                Id = Variable.Id;

            if (ParameterSetName == "Name")
            {
                var wildcard = new WildcardPattern(Name, WildcardOptions.IgnoreCase);

                var toRemove = client.IVR.GlobalVariables.GetList().Where(v => wildcard.IsMatch(v.Name)).ToList();

                foreach (var ivr in toRemove)
                {
                    client.IVR.GlobalVariables.Remove(ivr.Id);
                }
            }
            else
                client.IVR.GlobalVariables.Remove(Id);
        }
    }
}
