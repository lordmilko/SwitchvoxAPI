using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    public partial class CallQueueLogs
    {
        private readonly SwitchvoxRequest request;

        internal CallQueueLogs(SwitchvoxRequest request)
        {
            this.request = request;
        }
    }
}
