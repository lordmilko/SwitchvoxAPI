namespace SwitchvoxAPI.Methods
{
    /// <summary>
    /// Methods contained in the Switchvox.CurrentCalls namespace.
    /// </summary>
    public partial class CurrentCalls
    {
        private readonly SwitchvoxClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentCalls"/> class.
        /// </summary>
        /// <param name="client">The <see cref="SwitchvoxClient"/> methods will use to communicate with the phone server.</param>
        internal CurrentCalls(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
