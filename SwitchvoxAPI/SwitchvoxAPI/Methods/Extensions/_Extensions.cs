namespace SwitchvoxAPI.Methods
{
    /// <summary>
    /// Methods contained in the Switchvox.Extensions namespace.
    /// </summary>
    public partial class Extensions
    {
        private readonly SwitchvoxClient client;

        /// <summary>
        /// Methods contained in the Switchvox.Extensions.Settings namespace.
        /// </summary>
        public Settings Settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.Extensions"/> class.
        /// </summary>
        /// <param name="client"><see cref="T:SwitchvoxAPI.SwitchvoxClient"/> methods will use to communicate with the phone server.</param>
        internal Extensions(SwitchvoxClient client)
        {
            this.client = client;

            Settings = new Settings(client);
        }
    }
}
