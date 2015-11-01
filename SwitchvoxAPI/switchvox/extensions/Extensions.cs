using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI;

namespace SwitchvoxAPI
{
    public partial class Extensions
    {
        private readonly SwitchvoxRequest request;

        public Extensions(SwitchvoxRequest request)
        {
            this.request = request;
        }
    }
}
