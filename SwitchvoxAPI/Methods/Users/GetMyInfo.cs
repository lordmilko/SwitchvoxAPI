namespace SwitchvoxAPI.Methods
{
    public partial class Users
    {
        /// <summary>
        /// Fetch basic extension information for the currently authenticated user.
        /// </summary>
        /// <returns>The extension information for the currently authenticated user.</returns>
        public ExtensionInfo GetMyInfo()
        {
            var extension = client.Execute<ExtensionInfo>(new RequestMethod("switchvox.users.getMyInfo", null));

            return extension;
        }
    }
}
