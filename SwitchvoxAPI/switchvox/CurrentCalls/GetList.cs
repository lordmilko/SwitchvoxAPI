namespace Switchvox.CurrentCalls
{
    /// <summary>
    /// Get a list of all current calls in the system
    /// </summary>
    public class GetList : RequestMethod
    {
        /// <summary>
        /// Initializes a new instance of the Switchvox.CurrentCalls.GetList class.
        /// </summary>
        public GetList() : base("switchvox.currentCalls.getList")
        {
            SetXml(null);
        }
    }
}
