using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell
{
    [Cmdlet(VerbsLifecycle.Invoke, "SvxCall")]
    public class InvokeSvxCall : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName ="Extension")]
        public string Extension { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Extension")]
        public string AccountId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Info", ValueFromPipeline = true)]
        public ExtensionInfo ExtensionInfo { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Extension", Position = 2)]
        [Parameter(Mandatory = true, ParameterSetName = "Info", Position = 0)]
        public string Number { get; set; }

        [Parameter(Mandatory = false)]
        public int Timeout { get; set; } = 10;

        [Parameter(Mandatory = false)]
        public string CallerId { get; set; }

        public SwitchParameter IgnoreUserAPI { get; set; }

        protected override void ProcessRecord()
        {
            var ignoreUserAPISettings = IgnoreUserAPI.IsPresent;

            if (ParameterSetName == "Info")
            {
                Extension = ExtensionInfo.Extension;
                AccountId = ExtensionInfo.AccountId;
            }

            client.Call(Extension, Number, AccountId, ignoreUserAPISettings, CallerId ?? "PBX", timeout: Timeout);
        }
    }
}
