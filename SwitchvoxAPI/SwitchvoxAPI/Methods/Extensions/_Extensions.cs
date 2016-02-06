using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitchvoxAPI;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Methods contained under Switchvox.Extensions
    /// </summary>
    public partial class Extensions
    {
        private readonly SwitchvoxRequest request;

        public Settings Settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.Extensions"/> class.
        /// </summary>
        /// <param name="request"><see cref="T:SwitchvoxAPI.SwitchvoxRequest"/> methods will use to communicate with the phone server.</param>
        internal Extensions(SwitchvoxRequest request)
        {
            this.request = request;

            Settings = new Settings(request);
        }
    }
}
