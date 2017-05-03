namespace SwitchvoxAPI.Methods
{
    /// <summary>
    /// Methods contained in the Switchvox.Extensions.Settings namespace.
    /// </summary>
    public partial class Settings
    {
        private readonly SwitchvoxClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="client">The <see cref="SwitchvoxClient"/> methods will use to communicate with the phone server.</param>
        internal Settings(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
