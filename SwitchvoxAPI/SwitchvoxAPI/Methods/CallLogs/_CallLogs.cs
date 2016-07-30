using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    public partial class CallLogs
    {
        private readonly SwitchvoxClient client;

        internal CallLogs(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
