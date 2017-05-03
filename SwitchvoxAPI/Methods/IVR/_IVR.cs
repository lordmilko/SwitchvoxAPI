namespace SwitchvoxAPI.Methods
{
    /// <summary>
    /// Methods contained in the Switchvox.IVR namespace.
    /// </summary>
    public partial class IVR
    {
        private readonly SwitchvoxClient client;

        /// <summary>
        /// Methods contained in the Switchvox.IVR.GlobalVariables namespace.
        /// </summary>
        public GlobalVariables GlobalVariables;

        /// <summary>
        /// Initializes a new instance of the <see cref="IVR"/> class.
        /// </summary>
        /// <param name="client"><see cref="SwitchvoxClient"/> methods will use to communicate with the phone server.</param>
        internal IVR(SwitchvoxClient client)
        {
            this.client = client;

            GlobalVariables = new GlobalVariables(client);
        }
    }
}
