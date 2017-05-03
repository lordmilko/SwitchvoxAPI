namespace SwitchvoxAPI.Methods
{
    /// <summary>
    /// Methods contained in the Switchvox.IVR.GlobalVariables namespace.
    /// </summary>
    public partial class GlobalVariables
    {
        private readonly SwitchvoxClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalVariables"/> class.
        /// </summary>
        /// <param name="client"><see cref="SwitchvoxClient"/> methods will use to communicate with the phone server.</param>
        internal GlobalVariables(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
