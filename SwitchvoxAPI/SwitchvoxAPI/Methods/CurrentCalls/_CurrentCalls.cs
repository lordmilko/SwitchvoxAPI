using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Methods contained in the Switchvox.CurrentCalls namespace.
    /// </summary>
    public partial class CurrentCalls
    {
        private readonly SwitchvoxRequest request;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.CurrentCalls"/> class.
        /// </summary>
        /// <param name="request">The <see cref="T:SwitchvoxAPI.SwitchvoxRequest"/> methods will use to communicate with the phone server.</param>
        internal CurrentCalls(SwitchvoxRequest request)
        {
            this.request = request;
        }
    }
}
