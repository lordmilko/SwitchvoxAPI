using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommunications.Connect, "SvoxServer")]
    public class ConnectSvoxServer : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The address of the Switchvox Server to connect to. If the server does not use HTTPS, http:// must be specified.")]
        public string Server { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1)]
        public PSCredential Credential { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            if (SwitchvoxSessionState.Client == null || Force.IsPresent)
            {
                SwitchvoxSessionState.Client = new SwitchvoxClient(Server, Credential.GetNetworkCredential().UserName, Credential.GetNetworkCredential().Password);
            }
            else
            {
                throw new Exception($"Already connected to server {SwitchvoxSessionState.Client.Server}. To override please specify -Force");
            }
        }
    }
}
