namespace SwitchvoxAPI.Methods
{
    /// <summary>
    /// Methods contained in the Switchvox.CallQueueLogs namespace.
    /// </summary>
    public partial class CallQueueLogs
    {
        private readonly SwitchvoxClient client;

        internal CallQueueLogs(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
