using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI.PowerShell.Base;

namespace SwitchvoxAPI.PowerShell.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "SvxCallLog")]
    public class GetSvxCallLog : SwitchvoxCmdlet
    {
        [Parameter(Mandatory = false)]
        public DateTime StartDate { get; set; } = DateTime.Now.Date;

        [Parameter(Mandatory = false)]
        public DateTime EndDate { get; set; } = DateTime.Now.Date.AddDays(1);

        [Parameter(Mandatory = false)]
        public CallDirection? Direction { get; set; }

        [Parameter(Mandatory = false, Position = 0)]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public string From { get; set; }

        [Parameter(Mandatory = false)]
        public string To { get; set; }

        public int? Count { get; set; }

        protected override void ProcessRecord()
        {
            int result;

            CallLogSearchCriteria? criteria = null;

            if (Name != null)
            {
                if (int.TryParse(Name.Trim('*'), out result))
                {
                    criteria = CallLogSearchCriteria.CallerIdNumber;
                }
                else
                {
                    criteria = CallLogSearchCriteria.CallerIdName;
                }
            }

            var records = client.CallLogs.StreamSearch(StartDate, EndDate, 25, searchCriteria: criteria, searchValues: Name?.Trim('*'));

            if (Name != null)
            {
                var pattern = new WildcardPattern(Name, WildcardOptions.IgnoreCase);

                if (criteria == CallLogSearchCriteria.CallerIdNumber)
                    records = records.Where(r => pattern.IsMatch(r.FromNumber) || pattern.IsMatch(r.ToNumber));
                else if (criteria == CallLogSearchCriteria.CallerIdName)
                    records = records.Where(r => pattern.IsMatch(r.FromName) || pattern.IsMatch(r.ToName));
            }

            if (Direction != null)
            {
                records = records.Where(r => r.Direction == Direction);
            }
            
            foreach (var obj in records)
                WriteObject(obj);
        }
    }
}
