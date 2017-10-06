using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Set, "SvxIVRVariable")]
    public class SetSvxIVRVariable : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Id")]
        public int Id { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Default")]
        public GlobalIVRVariable Variable { get; set; }

        [Parameter(Mandatory = false, Position = 1)]
        public string Value { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == "Default")
            {
                Id = Variable.Id;
            }
            else if (ParameterSetName == "Name")
            {
                var ivr = client.IVR.GlobalVariables.GetList().FirstOrDefault(i => i.Name.ToLower() == Name.ToLower());

                if (ivr == null)
                    throw new Exception($"IVR with name '{Name}' does not exist");

                client.IVR.GlobalVariables.Update(ivr.Id, Value);
            }

            client.IVR.GlobalVariables.Update(Id, Value);
        }
    }
}
