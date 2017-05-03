using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Methods contained in the Switchvox.ExtensionGroups namespace.
    /// </summary>
    public partial class ExtensionGroups
    {
        private readonly SwitchvoxClient client;

        internal ExtensionGroups(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
