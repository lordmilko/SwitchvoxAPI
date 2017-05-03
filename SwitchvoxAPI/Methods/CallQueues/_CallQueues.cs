namespace SwitchvoxAPI.Methods
{
    /// <summary>
    /// Methods contained in the Switchvox.CallQueues namespace.
    /// </summary>
    public partial class CallQueues
    {
        private readonly SwitchvoxClient client;

        internal CallQueues(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
