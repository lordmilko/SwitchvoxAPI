using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Methods contained in the Switchvox.Extensions.Settings namespace.
    /// </summary>
    public partial class Settings
    {
        private readonly SwitchvoxClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.Settings"/> class.
        /// </summary>
        /// <param name="client">The <see cref="T:SwitchvoxAPI.SwitchvoxClient"/> methods will use to communicate with the phone server.</param>
        internal Settings(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
