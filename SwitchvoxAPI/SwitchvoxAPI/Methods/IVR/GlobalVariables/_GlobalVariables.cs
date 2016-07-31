namespace SwitchvoxAPI
{
    /// <summary>
    /// Methods contained in the Switchvox.IVR.GlobalVariables namespace.
    /// </summary>
    public partial class GlobalVariables
    {
        private readonly SwitchvoxClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.GlobalVariables"/> class.
        /// </summary>
        /// <param name="client"><see cref="T:SwitchvoxAPI.SwitchvoxClient"/> methods will use to communicate with the phone server.</param>
        internal GlobalVariables(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
