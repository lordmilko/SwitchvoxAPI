using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SwitchvoxAPI.Methods
{
    public partial class Users
    {
        private string tagGroup;
        private string tagInstance;

        /// <summary>
        /// Fetch basic information about the extensions on a phone system.
        /// </summary>
        /// <param name="identifier">A <see cref="ExtensionIdentifier"/> value indicating whether Extension numbers or Account IDs will be used to get info for the extensions on your system</param>
        /// <param name="values">A list of Extension Account IDs or Extension Numbers to get information for.</param>
        /// <returns></returns>
        public ExtensionInfo GetMyInfo()
        {
            var extension = client.Execute<ExtensionInfo>(new RequestMethod("switchvox.users.getMyInfo", null));

            return extension;
        }
    }
}
