using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    public partial class CallQueues
    {
        private readonly SwitchvoxRequest request;

        internal CallQueues(SwitchvoxRequest request)
        {
            this.request = request;
        }
    }
}
