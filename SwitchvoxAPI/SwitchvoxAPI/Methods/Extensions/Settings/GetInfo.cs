using Switchvox;

namespace SwitchvoxAPI
{
    public partial class Settings
    {
        /// <summary>
        /// Retrieve settings that apply to extensions in the system.
        /// </summary>
        public ExtensionSettings GetInfo()
        {
            var response = request.Execute(new RequestMethod("switchvox.extensions.settings.getInfo", null));

            return response.Deserialize<ExtensionSettings>();
        }
    }
}