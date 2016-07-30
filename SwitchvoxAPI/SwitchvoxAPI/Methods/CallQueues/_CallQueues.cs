using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    public partial class CallQueues
    {
        private readonly SwitchvoxClient client;

        internal CallQueues(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
