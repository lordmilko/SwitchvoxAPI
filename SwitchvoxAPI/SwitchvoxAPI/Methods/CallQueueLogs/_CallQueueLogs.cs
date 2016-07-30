using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    public partial class CallQueueLogs
    {
        private readonly SwitchvoxClient client;

        internal CallQueueLogs(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
