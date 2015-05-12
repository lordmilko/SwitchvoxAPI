namespace Switchvox.Extensions.Settings
{
    /// <summary>
    /// Retrieve settings that apply to extensions in the system
    /// </summary>
    public class GetInfo : RequestMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.Extensions.Settings.GetInfo"/> class.
        /// </summary>
        public GetInfo() : base("switchvox.extensions.settings.getInfo")
        {
            SetXml(null);
        }
    }
}