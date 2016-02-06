using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    public partial class Settings
    {
        private readonly SwitchvoxRequest request;

        internal Settings(SwitchvoxRequest request)
        {
            this.request = request;
        }
    }
}
