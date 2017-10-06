using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet.Data
{
    [Cmdlet(VerbsCommon.Get, "SvxExtension")]
    public class GetSvxExtension : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public string[] Extension { get; set; }

        [Parameter(Mandatory = false)]
        public string[] AccountID { get; set; }

        [Parameter(Mandatory = false)]
        public string MinExtension { get; set; }

        [Parameter(Mandatory = false)]
        public string MaxExtension { get; set; }

        [Parameter(Mandatory = false)]
        public ExtensionType[] Type { get; set; }

        [Parameter(Mandatory = false)]
        public DateTime? MinCreationDate { get; set; }

        [Parameter(Mandatory = false)]
        public DateTime? MaxCreationDate { get; set; }

        protected override void ProcessRecord()
        {
            var extensions = client.Extensions.Search(MinExtension, MaxExtension, MinCreationDate, MaxCreationDate, 100000, 1, Type);

            if (Extension != null && Extension.Length > 0)
                extensions = extensions.Where(e => Extension.Any(ex => ex == e.Extension)).ToList();

            if (AccountID != null && AccountID.Length > 0)
                extensions = extensions.Where(e => AccountID.Any(ex => ex == e.AccountId)).ToList();

            if (Name != null)
            {
                WildcardPattern pattern = new WildcardPattern(Name, WildcardOptions.IgnoreCase);

                extensions = extensions.Where(e => pattern.IsMatch(e.DisplayName) || pattern.IsMatch(e.Extension)).ToList();
            }

            WriteObject(extensions, true);

            //todo -- remove item/count parameters for logs
            //todo -- implement count parameter with streaming implementation
        }
    }
}
