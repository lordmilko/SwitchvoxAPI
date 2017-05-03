using System.Collections.Generic;

namespace SwitchvoxAPI.Methods
{
    public partial class CurrentCalls
    {
        /// <summary>
        /// Get a list of all current calls in the system.
        /// </summary>
        public List<CurrentCall> GetList()
        {
            var response = client.Execute<ListDeserializationLayers.CurrentCalls>(new RequestMethod("switchvox.currentCalls.getList", null)).Items;

            return response;
        }
    }
}
